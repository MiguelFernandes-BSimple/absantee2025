using Application.Services;
using Domain.Interfaces;
using Domain.Models;
using Moq;

namespace Application.Tests.CollaboratorServiceTests
{
    public class CollaboratorServiceGetBySurnames : CollaboratorServiceTestBase
    {
        [Fact]
        public async Task GetBySurname_WhenPassingValidSurname_ReturnsGuidList()
        {
            // arrange
            var period = new PeriodDateTime(DateTime.UtcNow.AddDays(-5), DateTime.UtcNow.AddDays(150));
            var user1Id = Guid.NewGuid();
            var user2Id = Guid.NewGuid();

            var surname = "Afonso";

            var user1 = new Mock<IUser>();
            user1.Setup(u => u.Id).Returns(user1Id);
            var user2 = new Mock<IUser>();
            user2.Setup(u => u.Id).Returns(user2Id);

            UserRepositoryDouble.Setup(repo => repo.GetBySurnamesAsync(surname)).ReturnsAsync(new List<IUser> { user1.Object, user2.Object });

            var collabId1 = Guid.NewGuid();
            var collabId2 = Guid.NewGuid();

            var collab1 = new Mock<ICollaborator>();
            collab1.Setup(c => c.Id).Returns(collabId1);
            var collab2 = new Mock<ICollaborator>();
            collab2.Setup(c => c.Id).Returns(collabId2);

            CollaboratorRepositoryDouble.Setup(repo => repo.GetByUsersIdsAsync(new List<Guid> { user1Id, user2Id })).ReturnsAsync(new List<ICollaborator> { collab1.Object, collab2.Object });

            // act
            var result = await CollaboratorService.GetBySurnames(surname);

            // assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.Contains(collabId1, result);
            Assert.Contains(collabId2, result);
        }

        [Fact]
        public async Task GetBySurname_WhenUsersAreFoundButNoCollaboratorsExist_ReturnsEmptyList()
        {
            // arrange
            var period = new PeriodDateTime(DateTime.UtcNow.AddDays(-5), DateTime.UtcNow.AddDays(150));
            var user1Id = Guid.NewGuid();
            var user2Id = Guid.NewGuid();

            var surname = "Afonso";

            var user1 = new Mock<IUser>();
            user1.Setup(u => u.Id).Returns(user1Id);
            var user2 = new Mock<IUser>();
            user2.Setup(u => u.Id).Returns(user2Id);

            UserRepositoryDouble.Setup(repo => repo.GetBySurnamesAsync(surname)).ReturnsAsync(new List<IUser> { user1.Object, user2.Object });

            CollaboratorRepositoryDouble.Setup(repo => repo.GetByUsersIdsAsync(new List<Guid> { user1Id, user2Id })).ReturnsAsync(new List<ICollaborator>());

            // act
            var result = await CollaboratorService.GetBySurnames(surname);

            // assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetBySurname_WhenPassingInvalidSurname_ReturnsEmptyList()
        {
            // arrange
            var surname = "Afonso";

            UserRepositoryDouble.Setup(repo => repo.GetBySurnamesAsync(surname)).ReturnsAsync(new List<IUser>());

            CollaboratorRepositoryDouble.Setup(repo => repo.GetByUsersIdsAsync(new List<Guid>())).ReturnsAsync(new List<ICollaborator>());

            // act
            var result = await CollaboratorService.GetBySurnames(surname);

            // assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }
    }
}