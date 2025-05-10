using Application.Services;
using Domain.Models;
using Moq;

namespace Application.Tests.CollaboratorServiceTests
{
    public class GetAllByFinishedTrainingModuleInSubjectAfterPeriodTests : CollaboratorServiceTestBase
    {
        [Fact]
        public async Task GetCompletedTrainingsAsync_ReturnsEmptyResult_WhenNoActiveCollaborators()
        {
            // Arrange
            var subjectId = Guid.NewGuid();
            var fromDate = DateTime.UtcNow;

            CollaboratorRepositoryDouble.Setup(repo => repo.GetActiveCollaborators()).ReturnsAsync(new List<Collaborator>());

            // Act
            var result = await CollaboratorService.GetCompletedTrainingsAsync(subjectId, fromDate);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.IsSuccess);
            Assert.Empty(result.Value);
        }

        [Fact]
        public async Task GetCompletedTrainingsAsync_ReturnsEmptyResult_WhenNoFinishedTrainingModules()
        {
            // Arrange
            var subjectId = Guid.NewGuid();
            var fromDate = DateTime.UtcNow;

            var activeCollaborators = new List<Collaborator>
            {
                new Collaborator(Guid.NewGuid(), Guid.NewGuid(), new PeriodDateTime(fromDate.AddDays(1), fromDate.AddDays(10)))
            };

            CollaboratorRepositoryDouble.Setup(repo => repo.GetActiveCollaborators()).ReturnsAsync(activeCollaborators);

            TrainingModuleRepositoryDouble.Setup(repo => repo.GetBySubjectAndAfterDateFinished(subjectId, fromDate)).ReturnsAsync(new List<TrainingModule>());

            // Act
            var result = await CollaboratorService.GetCompletedTrainingsAsync(subjectId, fromDate);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.IsSuccess);
            Assert.Empty(result.Value);
        }

        [Fact]
        public async Task GetCompletedTrainingsAsync_ReturnsEmptyResult_WhenNoMatchingCollaborators()
        {
            // Arrange
            var subjectId = Guid.NewGuid();
            var fromDate = DateTime.UtcNow;

            var activeCollaborators = new List<Collaborator>
            {
                new Collaborator(Guid.NewGuid(), Guid.NewGuid(), new PeriodDateTime(fromDate.AddDays(1), fromDate.AddDays(10)))
            };

            var finishedTrainingModules = new List<TrainingModule>
            {
                new TrainingModule(Guid.NewGuid(), subjectId, new List<PeriodDateTime> { new PeriodDateTime(fromDate.AddDays(1), fromDate.AddDays(10)) })
            };

            CollaboratorRepositoryDouble.Setup(repo => repo.GetActiveCollaborators()).ReturnsAsync(activeCollaborators);

            TrainingModuleRepositoryDouble.Setup(repo => repo.GetBySubjectAndAfterDateFinished(subjectId, fromDate)).ReturnsAsync(finishedTrainingModules);
            
            AssociationTrainingModuleCollaboratorsRepositoryDouble.Setup(repo => repo.GetByTrainingModuleIds(It.IsAny<List<Guid>>())).ReturnsAsync(new List<AssociationTrainingModuleCollaborator>());

            // Act
            var result = await CollaboratorService.GetCompletedTrainingsAsync(subjectId, fromDate);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.IsSuccess);
            Assert.Empty(result.Value);
        }

        [Fact]
        public async Task GetCompletedTrainingsAsync_ReturnsExpectedResult_WhenCollaboratorsMatch()
        {
            // Arrange
            var subjectId = Guid.NewGuid();
            var fromDate = DateTime.UtcNow;

            var collabId1 = Guid.NewGuid();
            var collabId2 = Guid.NewGuid();

            var activeCollaborators = new List<Collaborator>
            {
                new Collaborator(collabId1, Guid.NewGuid(), new PeriodDateTime(fromDate.AddDays(1), fromDate.AddDays(10))),
                new Collaborator(collabId2, Guid.NewGuid(), new PeriodDateTime(fromDate.AddDays(1), fromDate.AddDays(10)))
            };

            var trainingModule1Id = Guid.NewGuid();
            var trainingModule2Id = Guid.NewGuid();

            var finishedTrainingModules = new List<TrainingModule>
            {
                new TrainingModule(trainingModule1Id, subjectId, new List<PeriodDateTime> { new PeriodDateTime(fromDate.AddDays(-5), fromDate.AddDays(1)) }),
                new TrainingModule(trainingModule2Id, subjectId, new List<PeriodDateTime> { new PeriodDateTime(fromDate.AddDays(-5), fromDate.AddDays(1)) })
            };

            var finishedCollaborators = new List<AssociationTrainingModuleCollaborator>
            {
                new AssociationTrainingModuleCollaborator(Guid.NewGuid(), trainingModule1Id, collabId1),
                new AssociationTrainingModuleCollaborator(Guid.NewGuid(), trainingModule2Id, collabId2)
            };

            CollaboratorRepositoryDouble.Setup(repo => repo.GetActiveCollaborators()).ReturnsAsync(activeCollaborators);
            TrainingModuleRepositoryDouble.Setup(repo => repo.GetBySubjectAndAfterDateFinished(subjectId, fromDate)).ReturnsAsync(finishedTrainingModules);
            AssociationTrainingModuleCollaboratorsRepositoryDouble.Setup(repo => repo.GetByTrainingModuleIds(It.IsAny<List<Guid>>())).ReturnsAsync(finishedCollaborators);

            // Act
            var result = await CollaboratorService.GetCompletedTrainingsAsync(subjectId, fromDate);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.IsSuccess);
            Assert.Equal(2, result.Value.Count);
            Assert.Contains(collabId1, result.Value);
            Assert.Contains(collabId2, result.Value);
        }
    }
}