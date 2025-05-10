using Domain.Models;
using Moq;

namespace Application.Tests.CollaboratorServiceTests
{
    public class CollaboratorServiceGetAll : CollaboratorServiceTestBase
    {
        [Fact]
        public async Task GetAll_ReturnsGuidList()
        {
            // arrange
            var collabId1 = Guid.NewGuid();
            var collabId2 = Guid.NewGuid();

            var user1Id = Guid.NewGuid();
            var user2Id = Guid.NewGuid();

            var period = new PeriodDateTime(DateTime.UtcNow.AddDays(-5), DateTime.UtcNow.AddDays(150));

            var collaborators = new List<Collaborator>
            {
                new Collaborator(
                    collabId1,
                    user1Id,
                    period
                ),
                new Collaborator(
                    collabId2,
                    user2Id,
                    period
                )
            };

            var expected = new List<Guid> { collabId1, collabId2 };

            CollaboratorRepositoryDouble.Setup(repo => repo.GetAllAsync()).ReturnsAsync(collaborators);

            // act
            var result = await CollaboratorService.GetAll();

            // assert
            Assert.NotNull(result);
            Assert.Equal(expected, result.Value.ToList());
        }


        [Fact]
        public async Task GetAll_ReturnsEmptyList_WhenNoCollaboratorsExist()
        {
            // arrange
            var collaborators = new List<Collaborator>();
            CollaboratorRepositoryDouble.Setup(repo => repo.GetAllAsync()).ReturnsAsync(collaborators);

            // act
            var result = await CollaboratorService.GetAll();

            // assert
            Assert.NotNull(result);
            Assert.Empty(result.Value.ToList());
        }
    }
}