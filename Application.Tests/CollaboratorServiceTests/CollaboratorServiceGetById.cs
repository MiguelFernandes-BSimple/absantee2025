using Application.DTO.Collaborators;
using Domain.Models;
using Moq;

namespace Application.Tests.CollaboratorServiceTests
{
    public class CollaboratorServiceGetById : CollaboratorServiceTestBase
    {
        [Fact]
        public async Task GetById_ReturnsCorrectId()
        {
            // arrange
            var collabId = Guid.NewGuid();
            var userId = Guid.NewGuid();
            var period = new PeriodDateTime(DateTime.UtcNow.AddDays(-5), DateTime.UtcNow.AddDays(150));

            var collab = new Collaborator(collabId, userId, period);

            CollaboratorRepositoryDouble.Setup(repo => repo.GetByIdAsync(collabId)).ReturnsAsync(collab);

            MapperDouble.Setup(m => m.Map<CollaboratorDTO>(collab)).Returns<Collaborator>(c => new CollaboratorDTO(c.Id, c.UserId, c.PeriodDateTime));

            // act
            var result = await CollaboratorService.GetById(collabId);

            // assert
            Assert.NotNull(result);
            Assert.Equal(collabId, result.Value.Id);
        }

        [Fact]
        public async Task GetById_WhenIdDoesNotExist_ReturnsNotFound(){
            // arrange
            var collabId = Guid.NewGuid();

            CollaboratorRepositoryDouble.Setup(repo => repo.GetByIdAsync(collabId)).ReturnsAsync((Collaborator?)null);

            // act
            var result = await CollaboratorService.GetById(collabId);

            // assert
            Assert.NotNull(result);
            Assert.False(result.IsSuccess);
            Assert.Equal("User not found", result.Error.Message);
        }
    }
}