using Application.Services;
using Moq;
using Domain.Models;
using Application.DTO.Collaborators;

namespace Application.Tests.CollaboratorServiceTests
{
    public class CollaboratorServiceFindAllByProjectAndBetweenPeriod : CollaboratorServiceTestBase
    {
        // [Fact]
        // public async Task FindAllByProjectAndBetweenPeriod_ReturnsCollaboratorDTOList()
        // {
        //     // arrange
        //     var projectId = Guid.NewGuid();
        //     var period = new PeriodDate(new DateOnly(2025, 05, 10), new DateOnly(2025, 05, 20));
        //     var periodDate = new PeriodDateTime(DateTime.UtcNow.AddDays(-5), DateTime.UtcNow.AddDays(150));

        //     var user1Id = Guid.NewGuid();
        //     var user2Id = Guid.NewGuid();

        //     var collabId1 = Guid.NewGuid();
        //     var collabId2 = Guid.NewGuid();

        //     var associations = new List<AssociationProjectCollaborator>
        //     {
        //         new AssociationProjectCollaborator(
        //             collabId1,
        //             projectId,
        //             new PeriodDate(
        //                 DateOnly.FromDateTime(DateTime.UtcNow),
        //                 DateOnly.FromDateTime(DateTime.UtcNow.AddDays(100))
        //             )
        //         ),
        //         new AssociationProjectCollaborator(
        //             collabId2,
        //             projectId,
        //             new PeriodDate(
        //                 DateOnly.FromDateTime(DateTime.UtcNow.AddDays(-10)),
        //                 DateOnly.FromDateTime(DateTime.UtcNow.AddDays(10))
        //             )
        //         )
        //     };

        //     AssociationProjectCollaboratorRepositoryDouble.Setup(repo => repo.FindAllByProjectAndIntersectingPeriodAsync(projectId, period)).ReturnsAsync(associations);

        //     var collaborators = new List<Collaborator>
        //     {
        //         new Collaborator(
        //             collabId1,
        //             user1Id,
        //             periodDate
        //         ),
        //         new Collaborator(
        //             collabId2,
        //             user2Id,
        //             periodDate
        //         )
        //     };

        //     CollaboratorRepositoryDouble.Setup(repo => repo.GetByIdsAsync(It.IsAny<IEnumerable<Guid>>())).ReturnsAsync(collaborators);

        //     MapperDouble
        //         .Setup(m => m.Map<Collaborator, CollaboratorDTO>(It.IsAny<Collaborator>()))
        //         .Returns<Collaborator>(c => new CollaboratorDTO
        //         (
        //             c.Id,
        //             c.UserId,
        //             c.PeriodDateTime
        //         ));

        //     // act
        //     var result = await CollaboratorService.FindAllByProjectAndBetweenPeriod(projectId, period);

        //     // assert
        //     Assert.True(result.IsSuccess);
        //     Assert.NotNull(result.Value);
        //     Assert.Equal(2, result.Value.Count());
        //     Assert.Contains(result.Value, c => c.Id == collabId1);
        //     Assert.Contains(result.Value, c => c.Id == collabId2);
        // }

        [Fact]
        public async Task FindAllByProjectAndBetweenPeriod_ReturnsEmptyList()
        {
            // arrange
            var projectId = Guid.NewGuid();
            var period = new PeriodDate(new DateOnly(2025, 05, 10), new DateOnly(2025, 05, 20));

            var associations = new List<AssociationProjectCollaborator>();

            AssociationProjectCollaboratorRepositoryDouble.Setup(repo => repo.FindAllByProjectAndIntersectingPeriodAsync(projectId, period)).ReturnsAsync(associations);

            var collaborators = new List<Collaborator>();

            CollaboratorRepositoryDouble.Setup(repo => repo.GetByIdsAsync(It.IsAny<IEnumerable<Guid>>())).ReturnsAsync(collaborators);

            MapperDouble
                .Setup(m => m.Map<Collaborator, CollaboratorDTO>(It.IsAny<Collaborator>()))
                .Returns<Collaborator>(c => new CollaboratorDTO
                (
                    c.Id,
                    c.UserId,
                    c.PeriodDateTime
                ));

            // act
            var result = await CollaboratorService.FindAllByProjectAndBetweenPeriod(projectId, period);

            // assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Value);
            Assert.Empty(result.Value);
        }
    }
}
