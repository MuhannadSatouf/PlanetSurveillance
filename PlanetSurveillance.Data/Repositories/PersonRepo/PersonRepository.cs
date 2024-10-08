using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PlanetSurveillance.Data.Models;

namespace PlanetSurveillance.Data.Repositories.PersonRepo
{
    public class PersonRepository : IPersonRepository
    {
        private readonly PlanetSurveillanceDbContext _dbContext;
        private readonly ILogger<PersonRepository> _logger;

        public PersonRepository(PlanetSurveillanceDbContext context, ILogger<PersonRepository> logger)
        {
            _dbContext = context;
            _logger = logger;
        }

        public async Task<IEnumerable<Person>> GetAllAsync()
        {
            try
            {
                return await _dbContext.Persons.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving persons.");
                throw new ApplicationException("An error occurred while retrieving persons", ex);
            }
        }

        public async Task<Person> GetBySWAPIIdAsync(string swapiId)
        {
            try
            {
                var person = await _dbContext.Persons.FirstOrDefaultAsync(p => p.SWAPIId == swapiId);
                if (person == null)
                {
                    _logger.LogWarning("No person found with the specified SWAPI ID: {SWAPIId}", swapiId);
                    return null;
                }
                return person;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while searching for a person with SWAPI ID: {SWAPIId}", swapiId);
                throw new ApplicationException("An error occurred while searching for a person", ex);
            }
        }

        public async Task AddAsync(Person person)
        {
            try
            {
                _dbContext.Persons.Add(person);
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Could not add the person to the database due to update exception.");
                throw new InvalidOperationException("Could not add the person to the database", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while adding a person.");
                throw new ApplicationException("An unexpected error occurred while adding a person", ex);
            }
        }
    }
}
