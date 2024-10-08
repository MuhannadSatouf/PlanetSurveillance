using PlanetSurveillance.Data.Models;

namespace PlanetSurveillance.Data.Repositories.PlanetRepo
{
    public interface IPlanetRepository
    {
        Task<IEnumerable<Planet>> GetAllAsync();
        Task<Planet> GetBySWAPIIdAsync(string swapiId);
        Task AddAsync(Planet planet);
    }
}
