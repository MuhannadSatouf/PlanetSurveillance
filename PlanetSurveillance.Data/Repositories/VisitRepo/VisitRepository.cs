using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PlanetSurveillance.Data.Models;

namespace PlanetSurveillance.Data.Repositories.VisitRepo
{
    public class VisitRepository : IVisitRepository
    {
        private readonly PlanetSurveillanceDbContext _dbContext;
        private readonly ILogger<VisitRepository> _logger;

        public VisitRepository(PlanetSurveillanceDbContext context, ILogger<VisitRepository> logger)
        {
            _dbContext = context;
            _logger = logger;
        }

        public async Task<IEnumerable<Visit>> GetAllAsync()
        {
            try
            {
                return await _dbContext.Visits
                    .Include(v => v.Person)
                    .Include(v => v.Planet)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to retrieve all visits.");
                throw new ApplicationException("An error occurred while retrieving all visits", ex);
            }
        }

        public async Task<Visit> GetByIdAsync(int id)
        {
            try
            {
                var visit = await _dbContext.Visits
                    .Include(v => v.Person)
                    .Include(v => v.Planet)
                    .FirstOrDefaultAsync(v => v.VisitId == id);

                if (visit == null)
                {
                    _logger.LogWarning("Visit not found for ID: {VisitId}", id);
                    return null;
                }
                return visit;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving a visit by ID: {VisitId}", id);
                throw new ApplicationException("An error occurred while retrieving a visit by ID", ex);
            }
        }

        public async Task AddAsync(Visit visit)
        {
            try
            {
                _dbContext.Visits.Add(visit);
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Could not add the visit due to a database update issue.");
                throw new InvalidOperationException("Could not add the visit", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while adding a visit.");
                throw new ApplicationException("An unexpected error occurred while adding a visit", ex);
            }
        }

        public async Task<IEnumerable<Visit>> GetVisitsByPersonSWAPIIdAsync(string swapiId)
        {
            try
            {
                return await _dbContext.Visits
                    .Include(v => v.Planet)
                    .Where(v => v.Person.SWAPIId == swapiId)
                    .ToListAsync();
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
                return await _dbContext.Visits
                    .Include(v => v.Person)
                    .Where(v => v.Planet.SWAPIId == swapiId)
                    .ToListAsync();
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
                var today = DateTime.UtcNow.Date;

                var visit = await _dbContext.Visits
                    .FirstOrDefaultAsync(v => v.PersonId == personId && v.PlanetId == planetId && v.DateOfVisit.Date == today);

                if (visit == null)
                {
                    _logger.LogInformation("No visit exists on the current date for the specified person and planet.");
                    return null;
                }
                return visit;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while checking for today's visit.");
                throw new ApplicationException("An error occurred while checking for today's visit", ex);
            }
        }
    }
}
