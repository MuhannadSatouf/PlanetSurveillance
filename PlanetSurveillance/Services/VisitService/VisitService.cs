using PlanetSurveillance.Data.Models;
using PlanetSurveillance.Data.Repositories.VisitRepo;

namespace PlanetSurveillance.Services.VisitService
{
    public class VisitService : IVisitService
    {
        private readonly IVisitRepository _visitRepository;
        private readonly ILogger<VisitService> _logger;

        public VisitService(IVisitRepository visitRepository, ILogger<VisitService> logger)
        {
            _visitRepository = visitRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<Visit>> GetAllVisitsAsync()
        {
            try
            {
                return await _visitRepository.GetAllAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to retrieve all visits.");
                throw new ApplicationException("An error occurred while retrieving all visits", ex);
            }
        }

        public async Task<Visit> GetVisitByIdAsync(int id)
        {
            try
            {
                var visit = await _visitRepository.GetByIdAsync(id);
                if (visit == null)
                {
                    _logger.LogWarning("No visit found with ID: {Id}", id);
                    return null;
                }
                return visit;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving a visit by ID: {Id}", id);
                throw new ApplicationException("An error occurred while retrieving a visit by ID", ex);
            }
        }

        public async Task RegisterVisitAsync(Visit visit)
        {
            try
            {
                await _visitRepository.AddAsync(visit);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to register a new visit.");
                throw new ApplicationException("An error occurred while registering a new visit", ex);
            }
        }

        public async Task<IEnumerable<Visit>> GetVisitsByPersonSWAPIIdAsync(string swapiId)
        {
            try
            {
                return await _visitRepository.GetVisitsByPersonSWAPIIdAsync(swapiId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to retrieve visits for person with SWAPI ID: {SWAPIId}", swapiId);
                throw new ApplicationException("An error occurred while retrieving visits for a person", ex);
            }
        }

        public async Task<IEnumerable<Visit>> GetVisitsByPlanetSWAPIIdAsync(string swapiId)
        {
            try
            {
                return await _visitRepository.GetVisitsByPlanetSWAPIIdAsync(swapiId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to retrieve visits for planet with SWAPI ID: {SWAPIId}", swapiId);
                throw new ApplicationException("An error occurred while retrieving visits for a planet", ex);
            }
        }

        public async Task<Visit> CheckVisitAsync(int personId, int planetId)
        {
            try
            {
                return await _visitRepository.CheckVisitAsync(personId, planetId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while checking today's visit.");
                throw new ApplicationException("An error occurred while checking today's visit", ex);
            }
        }
    }
}
