using Microsoft.AspNetCore.Mvc;
using Moq;
using PlanetSurveillance.Data.Models;
using PlanetSurveillance.Services.PlanetService;
using PlanetSurveillance.Services.VisitService;
using PlanetSurveillance.Services.PersonService;
using PlanetSurveillance.Services.Swapi;
using Microsoft.Extensions.Logging;

namespace PlanetSurveillance.Tests.Controllers
{
    public class PersonControllerTests
    {
        private readonly Mock<IPersonService> _mockPersonService;
        private readonly Mock<IPlanetService> _mockPlanetService;
        private readonly Mock<IVisitService> _mockVisitService;
        private readonly Mock<ISwapiService> _mockSwapiService;
        private readonly Mock<ILogger<PersonController>> _mockLogger;
        private readonly PersonController _controller;

        public PersonControllerTests()
        {
            _mockPersonService = new Mock<IPersonService>();
            _mockPlanetService = new Mock<IPlanetService>();
            _mockVisitService = new Mock<IVisitService>();
            _mockSwapiService = new Mock<ISwapiService>();
            _mockLogger = new Mock<ILogger<PersonController>>();

            _controller = new PersonController(
                _mockPersonService.Object,
                _mockPlanetService.Object,
                _mockVisitService.Object,
                _mockSwapiService.Object,
                _mockLogger.Object);
        }

        [Fact]
        public async Task RegisterVisit_PersonAndPlanetExist_ReturnsCreated()
        {
            // Arrange
            var swapiPersonId = "1";
            var swapiPlanetId = "5";

            var person = new Person
            {
                PersonId = 1,
                SWAPIId = swapiPersonId,
                Name = "Luke Skywalker"
            };

            var planet = new Planet
            {
                PlanetId = 5,
                SWAPIId = swapiPlanetId,
                Name = "Tatooine"
            };

            var visit = new Visit
            {
                VisitId = 1,
                PersonId = person.PersonId,
                PlanetId = planet.PlanetId,
                DateOfVisit = System.DateTime.UtcNow
            };

            _mockPersonService.Setup(p => p.GetPersonBySWAPIIdAsync(swapiPersonId)).ReturnsAsync(person);
            _mockPlanetService.Setup(p => p.GetPlanetBySWAPIIdAsync(swapiPlanetId)).ReturnsAsync(planet);
            _mockVisitService.Setup(v => v.CheckVisitAsync(person.PersonId, planet.PlanetId)).ReturnsAsync((Visit)null);
            _mockVisitService.Setup(v => v.RegisterVisitAsync(It.IsAny<Visit>())).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.RegisterVisit(swapiPersonId, swapiPlanetId);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            var returnedVisit = createdAtActionResult.Value;
            Assert.NotNull(returnedVisit);
        }

        [Fact]
        public async Task RegisterVisit_PersonAndPlanetDoNotExist_CreatesNewEntities()
        {
            // Arrange
            var swapiPersonId = "2";
            var swapiPlanetId = "6";

            var newPerson = new Person
            {
                SWAPIId = swapiPersonId,
                Name = "Han Solo"
            };

            var newPlanet = new Planet
            {
                SWAPIId = swapiPlanetId,
                Name = "Hoth"
            };

            var visit = new Visit
            {
                PersonId = newPerson.PersonId,
                PlanetId = newPlanet.PlanetId,
                DateOfVisit = System.DateTime.UtcNow
            };

            _mockPersonService.Setup(p => p.GetPersonBySWAPIIdAsync(swapiPersonId)).ReturnsAsync((Person)null);
            _mockSwapiService.Setup(s => s.GetPersonBySWAPIIdAsync(swapiPersonId)).ReturnsAsync(newPerson);
            _mockPlanetService.Setup(p => p.GetPlanetBySWAPIIdAsync(swapiPlanetId)).ReturnsAsync((Planet)null);
            _mockSwapiService.Setup(s => s.GetPlanetBySWAPIIdAsync(swapiPlanetId)).ReturnsAsync(newPlanet);
            _mockVisitService.Setup(v => v.RegisterVisitAsync(It.IsAny<Visit>())).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.RegisterVisit(swapiPersonId, swapiPlanetId);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            var returnedVisit = createdAtActionResult.Value;
            Assert.NotNull(returnedVisit);
        }
    }
}
