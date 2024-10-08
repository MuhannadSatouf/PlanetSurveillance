using PlanetSurveillance.Data.Models;

namespace PlanetSurveillance.Data.Repositories.VisitRepo
{
    public interface IVisitRepository
    {
        Task<IEnumerable<Visit>> GetAllAsync();
        Task<Visit> GetByIdAsync(int id);
        Task AddAsync(Visit visit);
        Task<IEnumerable<Visit>> GetVisitsByPersonSWAPIIdAsync(string swapiId);
        Task<IEnumerable<Visit>> GetVisitsByPlanetSWAPIIdAsync(string swapiId);
        Task<Visit> CheckVisitAsync(int personId, int planetId);
    }
}
