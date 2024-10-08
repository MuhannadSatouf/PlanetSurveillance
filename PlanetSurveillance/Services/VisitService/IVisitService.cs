using PlanetSurveillance.Data.Models;

namespace PlanetSurveillance.Services.VisitService
{
    public interface IVisitService
    {
        Task<IEnumerable<Visit>> GetAllVisitsAsync();
        Task<Visit> GetVisitByIdAsync(int id);
        Task RegisterVisitAsync(Visit visit);
        Task<IEnumerable<Visit>> GetVisitsByPersonSWAPIIdAsync(string swapiId);
        Task<IEnumerable<Visit>> GetVisitsByPlanetSWAPIIdAsync(string swapiId);
        Task<Visit> CheckVisitAsync(int personId, int planetId);
    }

}
