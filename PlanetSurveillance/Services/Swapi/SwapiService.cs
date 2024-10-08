using Newtonsoft.Json;
using PlanetSurveillance.Data.Models;
using PlanetSurveillance.Models;

namespace PlanetSurveillance.Services.Swapi
{
    public class SwapiService : ISwapiService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<SwapiService> _logger;

        public SwapiService(HttpClient httpClient, ILogger<SwapiService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<Person> GetPersonBySWAPIIdAsync(string swapiId)
        {
            if (!int.TryParse(swapiId, out int personId))
            {
                _logger.LogWarning($"Invalid SWAPI person ID: {swapiId}");
                return null;
            }

            try
            {
                var response = await _httpClient.GetAsync($"https://swapi.dev/api/people/{personId}/");

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogWarning($"Received HTTP {response.StatusCode} when trying to fetch person {personId} from SWAPI.");
                    return null;
                }

                var content = await response.Content.ReadAsStringAsync();
                var swapiPerson = JsonConvert.DeserializeObject<SwapiPerson>(content);

                if (swapiPerson == null || swapiPerson.Name == null)
                {
                    _logger.LogWarning($"Person with SWAPI ID {personId} not found or missing required fields.");
                    return null;
                }

                var person = new Person
                {
                    SWAPIId = personId.ToString(),
                    Name = swapiPerson.Name ?? "Unknown",
                    Gender = swapiPerson.Gender ?? "Unknown",
                    Height = swapiPerson.Height ?? "Unknown",
                    Mass = swapiPerson.Mass ?? "Unknown",
                    HairColor = swapiPerson.HairColor ?? "Unknown",
                    SkinColor = swapiPerson.SkinColor ?? "Unknown",
                    EyeColor = swapiPerson.EyeColor ?? "Unknown",
                    BirthYear = swapiPerson.BirthYear ?? "Unknown",
                    Homeworld = swapiPerson.Homeworld ?? "Unknown",
                    Films = swapiPerson.Films ?? new List<string>(),
                    Species = swapiPerson.Species ?? new List<string>(),
                    Vehicles = swapiPerson.Vehicles ?? new List<string>(),
                    Starships = swapiPerson.Starships ?? new List<string>(),
                    Created = swapiPerson.Created ?? "Unknown",
                    Edited = swapiPerson.Edited ?? "Unknown",
                    Url = swapiPerson.Url ?? "Unknown"
                };

                return person;
            }
            catch (HttpRequestException httpEx)
            {
                _logger.LogError(httpEx, $"Network error occurred while fetching person {personId} from SWAPI.");
                return null;
            }
            catch (JsonSerializationException jsonEx)
            {
                _logger.LogError(jsonEx, $"Error deserializing JSON for person {personId}.");
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An unexpected error occurred while fetching person {personId}.");
                return null;
            }
        }

        public async Task<Planet> GetPlanetBySWAPIIdAsync(string swapiId)
        {
            if (!int.TryParse(swapiId, out int planetId))
            {
                _logger.LogWarning($"Invalid SWAPI planet ID: {swapiId}");
                return null;
            }

            try
            {
                var response = await _httpClient.GetAsync($"https://swapi.dev/api/planets/{planetId}/");

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogWarning($"Failed to fetch planet with SWAPI ID {planetId}. HTTP Status: {response.StatusCode}");
                    return null;
                }

                var content = await response.Content.ReadAsStringAsync();
                var swapiPlanet = JsonConvert.DeserializeObject<SwapiPlanet>(content);

                if (swapiPlanet == null || swapiPlanet.Name == null)
                {
                    _logger.LogWarning($"Planet with SWAPI ID {planetId} not found or missing required fields.");
                    return null;
                }

                var planet = new Planet
                {
                    SWAPIId = planetId.ToString(),
                    Name = swapiPlanet.Name ?? "Unknown",
                    RotationPeriod = swapiPlanet.RotationPeriod ?? "Unknown",
                    OrbitalPeriod = swapiPlanet.OrbitalPeriod ?? "Unknown",
                    Diameter = swapiPlanet.Diameter ?? "Unknown",
                    Climate = swapiPlanet.Climate ?? "Unknown",
                    Gravity = swapiPlanet.Gravity ?? "Unknown",
                    Terrain = swapiPlanet.Terrain ?? "Unknown",
                    SurfaceWater = swapiPlanet.SurfaceWater ?? "Unknown",
                    Population = swapiPlanet.Population ?? "Unknown",
                    Residents = swapiPlanet.Residents ?? new List<string>(),
                    Films = swapiPlanet.Films ?? new List<string>(),
                    Created = swapiPlanet.Created ?? "Unknown",
                    Edited = swapiPlanet.Edited ?? "Unknown",
                    Url = swapiPlanet.Url ?? "Unknown"
                };

                return planet;
            }
            catch (HttpRequestException httpEx)
            {
                _logger.LogError(httpEx, $"Network error while fetching planet with SWAPI ID {planetId}.");
                return null;
            }
            catch (JsonSerializationException jsonEx)
            {
                _logger.LogError(jsonEx, $"JSON deserialization error while processing planet with SWAPI ID {planetId}.");
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Unexpected error occurred while fetching planet with SWAPI ID {planetId}.");
                return null;
            }
        }
    }
}
