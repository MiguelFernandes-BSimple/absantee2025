using Moq;
using Application.Services;
using Domain.Models;
using Application.DTO.Collaborators;


namespace Application.Tests.CollaboratorServiceTests
{
    public class CollaboratorServiceFindAllByProject : CollaboratorServiceTestBase
    {
        [Fact]
        public async Task FindAllByProject_ReturnsCollaboratorDTOs()
        {
            // Arrange
            var projectId = Guid.NewGuid();

            var collabId1 = Guid.NewGuid();
            var collabId2 = Guid.NewGuid();

            var user1Id = Guid.NewGuid();
            var user2Id = Guid.NewGuid();

            var period = new PeriodDateTime(DateTime.UtcNow.AddDays(-5), DateTime.UtcNow.AddDays(150));

            var associations = new List<AssociationProjectCollaborator>
            {
                new AssociationProjectCollaborator(
                    collabId1,
                    projectId,
                    new PeriodDate(
                        DateOnly.FromDateTime(DateTime.UtcNow),
                        DateOnly.FromDateTime(DateTime.UtcNow.AddDays(100))
                    )
                ),
                new AssociationProjectCollaborator(
                    collabId2,
                    projectId,
                    new PeriodDate(
                        DateOnly.FromDateTime(DateTime.UtcNow.AddDays(-10)),
                        DateOnly.FromDateTime(DateTime.UtcNow.AddDays(10))
                    )
                )
            };

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


            var collabDtos = collaborators.Select(c => new CollaboratorDTO(c.Id, c.UserId, c.PeriodDateTime)).ToList();

            AssociationProjectCollaboratorRepositoryDouble
                .Setup(repo => repo.FindAllByProjectAsync(projectId))
                .ReturnsAsync(associations);

            CollaboratorRepositoryDouble
                .Setup(repo => repo.GetByIdsAsync(It.IsAny<IEnumerable<Guid>>()))
                .ReturnsAsync(collaborators);

            MapperDouble
                .Setup(m => m.Map<CollaboratorDTO>(It.IsAny<Collaborator>()))
                .Returns<Collaborator>(c => new CollaboratorDTO
                (
                    c.Id,
                    c.UserId,
                    c.PeriodDateTime
                ));

            // Act
            var result = await CollaboratorService.FindAllByProject(projectId);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Value);
            Assert.Equal(2, result.Value.Count());
            Assert.Contains(result.Value, c => c.Id == collabId1);
            Assert.Contains(result.Value, c => c.Id == collabId2);
        }

        [Fact]
        public async Task FindAllByProject_WhenProjectHasNoCollaborators_ThenReturnEmptyList()
        {
            // Arrange
            var associations = new List<AssociationProjectCollaborator>();
            var collaborators = new List<Collaborator>();

            AssociationProjectCollaboratorRepositoryDouble
                .Setup(repo => repo.FindAllByProjectAsync(It.IsAny<Guid>()))
                .ReturnsAsync(associations);

            CollaboratorRepositoryDouble
                .Setup(repo => repo.GetByIdsAsync(It.IsAny<IEnumerable<Guid>>()))
                .ReturnsAsync(collaborators);

            MapperDouble
                .Setup(m => m.Map<Collaborator, CollaboratorDTO>(It.IsAny<Collaborator>()))
                .Returns<Collaborator>(c => new CollaboratorDTO
                (
                    c.Id,
                    c.UserId,
                    c.PeriodDateTime
                ));

            // Act
            var result = await CollaboratorService.FindAllByProject(It.IsAny<Guid>());

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Value);
            Assert.Empty(result.Value);
        }
    }

}
