using Microsoft.AspNetCore.Mvc;
using PlanetSurveillance.Services.PersonService;
using PlanetSurveillance.Services.Swapi;
using PlanetSurveillance.Data.Models;
using Swashbuckle.AspNetCore.Annotations;
using PlanetSurveillance.Services.PlanetService;
using PlanetSurveillance.Services.VisitService;
using System.ComponentModel.DataAnnotations;

[ApiController]
[Route("api/[controller]")]
public class PersonController : ControllerBase
{
    private readonly IPersonService _personService;
    private readonly IPlanetService _planetService;
    private readonly IVisitService _visitService;
    private readonly ISwapiService _swapiService;
    private readonly ILogger<PersonController> _logger;

    public PersonController(
        IPersonService personService,
        IPlanetService planetService,
        IVisitService visitService,
        ISwapiService swapiService,
        ILogger<PersonController> logger)
    {
        _personService = personService;
        _planetService = planetService;
        _visitService = visitService;
        _swapiService = swapiService;
        _logger = logger;
    }

    /// <summary>
    /// Registers a visit for a person on a planet.
    /// </summary>
    /// <param name="swapiPersonId">The SWAPI ID of the person.</param>
    /// <param name="swapiPlanetId">The SWAPI ID of the planet.</param>
    /// <returns>Returns the registered visit details or an error message.</returns>
    [SwaggerOperation(Summary = "Register a visit", Description = "Registers a visit between a person and a planet.")]
    [ProducesResponseType(typeof(Visit), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpPost("register-visit")]
    public async Task<IActionResult> RegisterVisit(
        [FromQuery][Required] string swapiPersonId,
        [FromQuery][Required] string swapiPlanetId)
    {
        try
        {
            var person = await _personService.GetPersonBySWAPIIdAsync(swapiPersonId) ??
                         await _swapiService.GetPersonBySWAPIIdAsync(swapiPersonId);

            if (person == null)
            {
                return NotFound(new
                {
                    StatusCode = 404,
                    Message = $"Person with SWAPI ID {swapiPersonId} not found."
                });
            }

            if (person.PersonId == 0)
            {
                await _personService.CreatePersonAsync(person);
            }

            var planet = await _planetService.GetPlanetBySWAPIIdAsync(swapiPlanetId) ??
                         await _swapiService.GetPlanetBySWAPIIdAsync(swapiPlanetId);

            if (planet == null)
            {
                return NotFound(new
                {
                    StatusCode = 404,
                    Message = $"Planet with SWAPI ID {swapiPlanetId} not found."
                });
            }

            if (planet.PlanetId == 0)
            {
                await _planetService.CreatePlanetAsync(planet);
            }
            
            var existingVisit = await _visitService.CheckVisitAsync(person.PersonId, planet.PlanetId);
            if (existingVisit != null)
            {
                return Conflict(new
                {
                    StatusCode = 409,
                    Message = "This visit is already registered for today.",
                    ExistingVisit = existingVisit
                });
            }

            var visit = new Visit
            {
                PersonId = person.PersonId,
                PlanetId = planet.PlanetId,
                DateOfVisit = DateTime.UtcNow
            };
            await _visitService.RegisterVisitAsync(visit);

            return CreatedAtAction(nameof(ListVisitsBySwapiId), new { swapiPersonId = person.SWAPIId }, new
            {
                StatusCode = 201,
                Message = "Visit successfully registered.",
                Visit = visit
            });
        }
        catch (HttpRequestException httpEx)
        {
            _logger.LogError(httpEx, "Network error occurred while communicating with SWAPI.");
            return StatusCode(503, new
            {
                StatusCode = 503,
                Message = "SWAPI is currently unavailable. Please try again later.",
                Details = httpEx.Message
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while registering the visit.");
            return StatusCode(500, new
            {
                StatusCode = 500,
                Message = "Internal server error occurred.",
                Details = ex.Message
            });
        }
    }



    /// <summary>
    /// Lists all visits for a person based on their SWAPI ID.
    /// </summary>
    /// <param name="swapiPersonId">The SWAPI ID of the person.</param>
    /// <returns>Returns a list of visits.</returns>
    [SwaggerOperation(Summary = "List visits by SWAPI ID", Description = "Lists all visits for a person based on their SWAPI ID.")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet("visits/swapi/{swapiPersonId}")]
    public async Task<IActionResult> ListVisitsBySwapiId(string swapiPersonId)
    {
        try
        {
            var person = await _personService.GetPersonBySWAPIIdAsync(swapiPersonId);
            if (person == null)
            {
                return NotFound($"No person found with SWAPI ID {swapiPersonId}.");
            }

            var visits = await _visitService.GetVisitsByPersonSWAPIIdAsync(swapiPersonId);

            var result = visits.Select(visit => new
            {
                id = visit.VisitId,
                personId = visit.Person.PersonId,
                personName = visit.Person.Name,
                planetId = visit.Planet.PlanetId,
                planetName = visit.Planet.Name,
                dateOfVisit = visit.DateOfVisit
            });

            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while listing visits.");
            return StatusCode(500, "Internal server error.");
        }
    }

    /// <summary>
    /// Lists all persons who have visited a planet based on its SWAPI ID.
    /// </summary>
    /// <param name="swapiPlanetId">The SWAPI ID of the planet.</param>
    /// <returns>Returns a list of persons who visited the planet.</returns>
    [SwaggerOperation(Summary = "List persons by planet SWAPI ID", Description = "Lists all persons who have visited a planet based on its SWAPI ID.")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet("visitors/swapi/{swapiPlanetId}")]
    public async Task<IActionResult> ListPersonsForPlanetBySwapiId(string swapiPlanetId)
    {
        try
        {
            var planet = await _planetService.GetPlanetBySWAPIIdAsync(swapiPlanetId);
            if (planet == null)
            {
                return NotFound(new
                {
                    status = 404,
                    message = $"No visits found for the planet with SWAPI ID {swapiPlanetId}.",
                    details = "Ensure the SWAPI ID is valid and the planet exists."
                });
            }

            var persons = await _visitService.GetVisitsByPlanetSWAPIIdAsync(swapiPlanetId);
            if (!persons.Any())
            {
                return NoContent();
            }

            var result = persons.Select(visit => new
            {
                personId = visit.Person.PersonId,
                personName = visit.Person.Name,
                swapiId = visit.Person.SWAPIId,
                dateOfVisit = visit.DateOfVisit.ToString("yyyy-MM-ddTHH:mm")
            });

            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while retrieving persons.");
            return StatusCode(500, new
            {
                status = 500,
                message = "Internal server error.",
                details = ex.Message
            });
        }
    }
}
