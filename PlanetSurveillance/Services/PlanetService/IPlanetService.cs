using PlanetSurveillance.Data.Models;

namespace PlanetSurveillance.Services.PlanetService
{
    public interface IPlanetService
    {
        Task<IEnumerable<Planet>> GetAllPlanetsAsync();
        Task<Planet> GetPlanetBySWAPIIdAsync(string swapiId);
        Task CreatePlanetAsync(Planet planet);
    }
}
