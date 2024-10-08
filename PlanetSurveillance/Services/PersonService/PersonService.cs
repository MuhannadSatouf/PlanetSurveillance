using Microsoft.Extensions.Logging;
using PlanetSurveillance.Data.Models;
using PlanetSurveillance.Data.Repositories.PersonRepo;
using PlanetSurveillance.Services.PersonService;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class PersonService : IPersonService
{
    private readonly IPersonRepository _personRepository;
    private readonly ILogger<PersonService> _logger;

    public PersonService(IPersonRepository personRepository, ILogger<PersonService> logger)
    {
        _personRepository = personRepository;
        _logger = logger;
    }

    public async Task<IEnumerable<Person>> GetAllPersonsAsync()
    {
        try
        {
            return await _personRepository.GetAllAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to retrieve all persons.");
            throw new ApplicationException("An error occurred while retrieving all persons", ex);
        }
    }

    public async Task<Person> GetPersonBySWAPIIdAsync(string swapiId)
    {
        try
        {
            var person = await _personRepository.GetBySWAPIIdAsync(swapiId);
            if (person == null)
            {
                _logger.LogWarning("No person found with SWAPI ID: {SWAPIId}", swapiId);
            }
            return person;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to retrieve person with SWAPI ID: {SWAPIId}", swapiId);
            throw new ApplicationException("An error occurred while retrieving the person by SWAPI ID", ex);
        }
    }

    public async Task CreatePersonAsync(Person person)
    {
        try
        {
            var existingPerson = await _personRepository.GetBySWAPIIdAsync(person.SWAPIId);
            if (existingPerson == null)
            {
                await _personRepository.AddAsync(person);
                _logger.LogInformation("Created new person with SWAPI ID: {SWAPIId}", person.SWAPIId);
            }
            else
            {
                _logger.LogInformation("Attempted to create a duplicate person with SWAPI ID: {SWAPIId}", person.SWAPIId);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to create person with SWAPI ID: {SWAPIId}", person.SWAPIId);
            throw new ApplicationException("An error occurred while creating the person", ex);
        }
    }
}
