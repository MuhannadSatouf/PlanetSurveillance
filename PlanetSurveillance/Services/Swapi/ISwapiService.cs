using PlanetSurveillance.Data.Models;

namespace PlanetSurveillance.Services.Swapi
{
    public interface ISwapiService
    {
        Task<Person> GetPersonBySWAPIIdAsync(string swapiId);
        Task<Planet> GetPlanetBySWAPIIdAsync(string swapiId);
    }
}
