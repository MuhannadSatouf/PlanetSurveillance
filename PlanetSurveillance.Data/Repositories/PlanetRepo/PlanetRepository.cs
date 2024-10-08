using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PlanetSurveillance.Data.Models;

namespace PlanetSurveillance.Data.Repositories.PlanetRepo
{
    public class PlanetRepository : IPlanetRepository
    {
        private readonly PlanetSurveillanceDbContext _dbContext;
        private readonly ILogger<PlanetRepository> _logger;

        public PlanetRepository(PlanetSurveillanceDbContext context, ILogger<PlanetRepository> logger)
        {
            _dbContext = context;
            _logger = logger;
        }

        public async Task<IEnumerable<Planet>> GetAllAsync()
        {
            try
            {
                return await _dbContext.Planets.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving planets.");
                throw;
            }
        }

        public async Task<Planet> GetBySWAPIIdAsync(string swapiId)
        {
            try
            {
                var planet = await _dbContext.Planets.FirstOrDefaultAsync(p => p.SWAPIId == swapiId);
                if (planet == null)
                {
                    _logger.LogWarning("No planet found with the specified SWAPI ID: {SWAPIId}", swapiId);
                    return null;
                }
                return planet;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while searching for a planet with SWAPI ID: {SWAPIId}", swapiId);
                throw;
            }
        }

        public async Task AddAsync(Planet planet)
        {
            try
            {
                _dbContext.Planets.Add(planet);
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Could not add the planet to the database due to update exception.");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while adding a planet.");
                throw;
            }
        }
    }
}
