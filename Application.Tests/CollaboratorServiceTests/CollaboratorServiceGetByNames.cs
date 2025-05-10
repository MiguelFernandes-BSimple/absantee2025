using Domain.Models;
using Moq;

namespace Application.Tests.CollaboratorServiceTests
{
    public class CollaboratorServiceGetByNames : CollaboratorServiceTestBase
    {
        [Fact]
        public async Task GetByNames_WhenPassingValidName_ReturnsGuidList()
        {
            // arrange
            var period = new PeriodDateTime(DateTime.UtcNow.AddDays(-5), DateTime.UtcNow.AddDays(150));
            var user1Id = Guid.NewGuid();
            var user2Id = Guid.NewGuid();

            var name = "Joao";

            var user1 = new User(user1Id, "Joao", "Afonso", "joao@gmail.com", period);
            var user2 = new User(user2Id, "Joao", "Alberto", "joao@gmail.com", period);

            UserRepositoryDouble.Setup(repo => repo.GetByNamesAsync(name)).ReturnsAsync(new List<User> { user1, user2 });

            var collabId1 = Guid.NewGuid();
            var collabId2 = Guid.NewGuid();

            var collab1 = new Collaborator(collabId1, user1Id, period);
            var collab2 = new Collaborator(collabId2, user2Id, period);

            CollaboratorRepositoryDouble.Setup(repo => repo.GetByUsersIdsAsync(new List<Guid> { user1Id, user2Id })).ReturnsAsync(new List<Collaborator> { collab1, collab2 });

            // act
            var result = await CollaboratorService.GetByNames(name);

            // assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.Contains(collabId1, result);
            Assert.Contains(collabId2, result);
        }

        [Fact]
        public async Task GetByNames_WhenUsersAreFoundButNoCollaboratorsExist_ReturnsEmptyList()
        {
            // arrange
            var period = new PeriodDateTime(DateTime.UtcNow.AddDays(-5), DateTime.UtcNow.AddDays(150));
            var user1Id = Guid.NewGuid();
            var user2Id = Guid.NewGuid();

            var name = "Joao";

            var user1 = new User(user1Id, "Joao", "Afonso", "joao@gmail.com", period);
            var user2 = new User(user2Id, "Joao", "Alberto", "joao@gmail.com", period);

            UserRepositoryDouble.Setup(repo => repo.GetByNamesAsync(name)).ReturnsAsync(new List<User> { user1, user2 });

            CollaboratorRepositoryDouble.Setup(repo => repo.GetByUsersIdsAsync(new List<Guid> { user1Id, user2Id })).ReturnsAsync(new List<Collaborator>());

            // act
            var result = await CollaboratorService.GetByNames(name);

            // assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetByNames_WhenPassingInvalidName_ReturnsEmptyList()
        {
            // arrange
            var name = "Joao";

            UserRepositoryDouble.Setup(repo => repo.GetByNamesAsync(name)).ReturnsAsync(new List<User>());

            CollaboratorRepositoryDouble.Setup(repo => repo.GetByUsersIdsAsync(new List<Guid>())).ReturnsAsync(new List<Collaborator>());

            // act
            var result = await CollaboratorService.GetByNames(name);

            // assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }
    }
}
