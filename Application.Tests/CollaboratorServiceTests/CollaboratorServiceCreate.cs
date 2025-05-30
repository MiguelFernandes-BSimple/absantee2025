using Application.DTO;
using Application.DTO.Collaborators;
using Domain.Models;
using Moq;

namespace Application.Tests.CollaboratorServiceTests
{
    public class CollaboratorServiceCreate : CollaboratorServiceTestBase
    {
        [Fact]
        public async Task Create_WhenPassingValidDTO_CreatesCollaborator()
        {
            // arrange
            var period = new PeriodDateTime(DateTime.UtcNow, DateTime.UtcNow.AddDays(30));
            var firstName = "firstname";
            var lastName = "lastname";
            var email = "email@gmail.com";
            var deactivationDate = DateTime.UtcNow.AddDays(30);

            var collaboratorDto = new CreateCollaboratorDto(firstName, lastName, email, deactivationDate, period);

            var userFromFactory = new User(firstName, lastName, email, deactivationDate);

            UserFactoryDouble.Setup(f => f.Create(collaboratorDto.Names, collaboratorDto.Surnames, collaboratorDto.Email, collaboratorDto.deactivationDate)).ReturnsAsync(userFromFactory);

            var userId = Guid.NewGuid();
            var user = new User(userId, firstName, lastName, email, period);
            UserRepositoryDouble.Setup(repo => repo.Add(userFromFactory)).Returns(user);

            var collabFromFactory = new Collaborator(userId, period);
            CollaboratorFactoryDouble.Setup(f => f.Create(user, period)).ReturnsAsync(collabFromFactory);

            var collabId = Guid.NewGuid();
            var collab = new Collaborator(collabId, userId, period);

            CollaboratorRepositoryDouble.Setup(repo => repo.Add(collabFromFactory)).Returns(collab);


            // act
            var result = (await CollaboratorService.Create(collaboratorDto)).Value;

            // assert
            Assert.Equal(collab.Id, result.Id);
            Assert.Equal(collab.UserId, result.UserId);
            Assert.Equal(collab.PeriodDateTime, result.PeriodDateTime);
        }
    }
}