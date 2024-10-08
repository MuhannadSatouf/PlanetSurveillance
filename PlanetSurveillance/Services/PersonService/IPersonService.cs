using PlanetSurveillance.Data.Models;

namespace PlanetSurveillance.Services.PersonService
{
    public interface IPersonService
    {
        Task<IEnumerable<Person>> GetAllPersonsAsync();
        Task<Data.Models.Person> GetPersonBySWAPIIdAsync(string swapiId);
        Task CreatePersonAsync(Person person);
    }
}
