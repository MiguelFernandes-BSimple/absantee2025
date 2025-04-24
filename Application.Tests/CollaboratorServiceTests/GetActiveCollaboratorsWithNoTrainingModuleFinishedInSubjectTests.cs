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
    public class GetActiveCollaboratorsWithNoTrainingModuleFinishedInSubjectTests
    {
        [Fact]
        public async Task WhenSearchingActiveCollaboratorsWithNoTrainingModuleFinishedInSubject_ThenReturnsExpectedResult()
        {
            //arrange
            var collab1 = new Mock<ICollaborator>();
            var collab2 = new Mock<ICollaborator>();
            var collab3 = new Mock<ICollaborator>();
            collab1.Setup(c1 => c1.GetId()).Returns(1);
            collab2.Setup(c2 => c2.GetId()).Returns(2);
            collab3.Setup(c3 => c3.GetId()).Returns(3);

            var activeCollabs = new List<ICollaborator>()
            {
                collab1.Object, collab2.Object, collab3.Object
            };
            var collabRepository = new Mock<ICollaboratorRepository>();
            collabRepository.Setup(c => c.GetActiveCollaborators()).ReturnsAsync(activeCollabs);

            var trainingModule = new Mock<ITrainingModule>();
            trainingModule.Setup(t => t.Id).Returns(1);
            var trainingModules = new List<ITrainingModule>() { trainingModule.Object };

            var trainingModuleRepo = new Mock<ITrainingModuleRepository>();
            trainingModuleRepo.Setup(trepo => trepo.GetBySubjectIdAndFinished(It.IsAny<long>(), It.IsAny<DateTime>())).ReturnsAsync(trainingModules);

            var trainingModuleCollaborator = new Mock<IAssociationTrainingModuleCollaborator>();
            trainingModuleCollaborator.Setup(t => t.CollaboratorId).Returns(1);
            var trainingModuleCollaboratorList = new List<IAssociationTrainingModuleCollaborator>() { trainingModuleCollaborator.Object };

            var trainingModuleCollaboratorRepo = new Mock<IAssociationTrainingModuleCollaboratorsRepository>();
            trainingModuleCollaboratorRepo.Setup(t => t.GetByTrainingModuleIds(new List<long>() { 1 })).ReturnsAsync(trainingModuleCollaboratorList);

            var expected = new List<ICollaborator>()
            {
                collab2.Object,
                collab3.Object
            };

            var assocRepoMock = new Mock<IAssociationProjectCollaboratorRepository>();
            var holidayPlanRepositoryDouble = new Mock<IHolidayPlanRepository>();
            var userRepo = new Mock<IUserRepository>();
            var collabFactory = new Mock<ICollaboratorFactory>();

            var collaboratorService =
                new CollaboratorService(assocRepoMock.Object, holidayPlanRepositoryDouble.Object,
                                        collabRepository.Object, userRepo.Object, collabFactory.Object,
                                        trainingModuleCollaboratorRepo.Object, trainingModuleRepo.Object);

            //act
            var result = await collaboratorService.GetActiveCollaboratorsWithNoTrainingModuleFinishedInSubject(It.IsAny<long>());

            //assert
            Assert.True(expected.SequenceEqual(result));
        }
    }
}
