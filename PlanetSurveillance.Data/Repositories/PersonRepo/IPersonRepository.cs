using PlanetSurveillance.Data.Models;

namespace PlanetSurveillance.Data.Repositories.PersonRepo
{
    public interface IPersonRepository
    {
        Task<IEnumerable<Person>> GetAllAsync();
        Task<Person> GetBySWAPIIdAsync(string swapiId);
        Task AddAsync(Person person);
    }
}
