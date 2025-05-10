using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Services;
using Domain.Factory;
using Domain.Interfaces;
using Domain.IRepository;
using Domain.Models;
using Moq;

namespace Application.Tests.CollaboratorServiceTests
{
    public class GetActiveCollaboratorsWithNoTrainingModuleFinishedInSubjectTests : CollaboratorServiceTestBase
    {

        [Fact]
        public async Task GetActiveCollaboratorsWithNoTrainingModuleFinishedInSubject_ReturnsExpectedResult()
        {
            // Arrange
            var subjectId = Guid.NewGuid();

            var user1Id = Guid.NewGuid();
            var user2Id = Guid.NewGuid();
            var collabId1 = Guid.NewGuid();
            var collabId2 = Guid.NewGuid();
            var period = new PeriodDateTime(DateTime.UtcNow.AddDays(5), DateTime.UtcNow.AddDays(15));

            var collab1 = new Collaborator(collabId1, user1Id, period);
            var collab2 = new Collaborator(collabId2, user2Id, period);

            var activeCollaborators = new List<Collaborator> { collab1, collab2 };

            CollaboratorRepositoryDouble.Setup(repo => repo.GetActiveCollaborators()).ReturnsAsync(activeCollaborators);

            var trainingModule1Id = new Guid();
            var trainingModule2Id = new Guid();
            var finishedTrainingModules = new List<TrainingModule>
            {
                new TrainingModule(trainingModule1Id, subjectId, new List<PeriodDateTime> { 
                    new PeriodDateTime(It.IsAny<DateTime>(), It.IsAny<DateTime>()) 
                }),
                new TrainingModule(trainingModule2Id, subjectId, new List<PeriodDateTime> {
                    new PeriodDateTime(It.IsAny<DateTime>(), It.IsAny<DateTime>())
                })
            };

            TrainingModuleRepositoryDouble.Setup(repo => repo.GetBySubjectIdAndFinished(subjectId, It.IsAny<DateTime>())).ReturnsAsync(finishedTrainingModules);

            var finishedTrainigModulesIds = new List<Guid>{trainingModule1Id, trainingModule2Id};

            var associationTrainingModuleCollaboratorId1 = new Guid();
            var associationTrainingModuleCollaboratorId2 = new Guid();
            var finishedCollaborators = new List<AssociationTrainingModuleCollaborator>
            {
                new AssociationTrainingModuleCollaborator(associationTrainingModuleCollaboratorId1, trainingModule1Id, It.IsAny<Guid>()),
                new AssociationTrainingModuleCollaborator(associationTrainingModuleCollaboratorId2, trainingModule2Id, It.IsAny<Guid>()),
            };

            AssociationTrainingModuleCollaboratorsRepositoryDouble.Setup(repo => repo.GetByTrainingModuleIds(finishedTrainigModulesIds)).ReturnsAsync(finishedCollaborators);

            // Act
            var result = await CollaboratorService.GetActiveCollaboratorsWithNoTrainingModuleFinishedInSubject(subjectId);

            // Assert;
            Assert.Equal(2, result.Value.Count);
        }
    }
}
