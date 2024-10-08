using Microsoft.Extensions.Logging;
using PlanetSurveillance.Data.Models;
using PlanetSurveillance.Data.Repositories.PlanetRepo;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlanetSurveillance.Services.PlanetService
{
    public class PlanetService : IPlanetService
    {
        private readonly IPlanetRepository _planetRepository;
        private readonly ILogger<PlanetService> _logger;

        public PlanetService(IPlanetRepository planetRepository, ILogger<PlanetService> logger)
        {
            _planetRepository = planetRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<Planet>> GetAllPlanetsAsync()
        {
            try
            {
                return await _planetRepository.GetAllAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to retrieve all planets.");
                throw new ApplicationException("An error occurred while retrieving all planets", ex);
            }
        }

        public async Task<Planet> GetPlanetBySWAPIIdAsync(string swapiId)
        {
            try
            {
                var planet = await _planetRepository.GetBySWAPIIdAsync(swapiId);
                if (planet == null)
                {
                    _logger.LogWarning("No planet found with SWAPI ID: {SWAPIId}", swapiId);
                }
                return planet;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to retrieve planet with SWAPI ID: {SWAPIId}", swapiId);
                throw new ApplicationException("An error occurred while retrieving the planet by SWAPI ID", ex);
            }
        }

        public async Task CreatePlanetAsync(Planet planet)
        {
            try
            {
                var existingPlanet = await _planetRepository.GetBySWAPIIdAsync(planet.SWAPIId);
                if (existingPlanet == null)
                {
                    await _planetRepository.AddAsync(planet);
                }
                else
                {
                    _logger.LogInformation("Attempted to create a duplicate planet with SWAPI ID: {SWAPIId}", planet.SWAPIId);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to create planet with SWAPI ID: {SWAPIId}", planet.SWAPIId);
                throw new ApplicationException("An error occurred while creating the planet", ex);
            }
        }
    }
}
