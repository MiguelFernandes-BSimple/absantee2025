using Application.Services;
using Domain.Factory;
using Domain.Interfaces;
using Domain.IRepository;
using Moq;

namespace Application.Tests.CollaboratorServiceTests
{
    public class GetAllByFinishedTrainingModuleInSubjectAfterPeriodTests
    {
        [Fact]
        public async Task WhenSearchingByFinishedTrainingModuleInSubjectAfterPeriod_ThenReturnsExpectedResult()
        {
            //arrange
            var trainingModule1 = new Mock<ITrainingModule>();
            trainingModule1.Setup(t => t.Id).Returns(1);
            var trainingModule2 = new Mock<ITrainingModule>();
            trainingModule2.Setup(t => t.Id).Returns(2);

            var trainingModules = new List<ITrainingModule>() { trainingModule1.Object, trainingModule2.Object };

            var trainingModuleRepo = new Mock<ITrainingModuleRepository>();
            trainingModuleRepo.Setup(trepo => trepo.GetBySubjectIdAndFinished(It.IsAny<long>(), It.IsAny<DateTime>())).ReturnsAsync(trainingModules);

            var trainingModuleCollaborator1 = new Mock<IAssociationTrainingModuleCollaborator>();
            trainingModuleCollaborator1.Setup(t => t.CollaboratorId).Returns(1);
            var trainingModuleCollaborator2 = new Mock<IAssociationTrainingModuleCollaborator>();
            trainingModuleCollaborator2.Setup(t => t.CollaboratorId).Returns(2);
            var trainingModuleCollaboratorList = new List<IAssociationTrainingModuleCollaborator>() { trainingModuleCollaborator1.Object, trainingModuleCollaborator2.Object };

            var trainingModuleCollaboratorRepo = new Mock<IAssociationTrainingModuleCollaboratorsRepository>();
            trainingModuleCollaboratorRepo.Setup(t => t.GetByTrainingModuleIds(new List<long>() { 1, 2 })).ReturnsAsync(trainingModuleCollaboratorList);

            var collab1 = new Mock<ICollaborator>();
            var collab2 = new Mock<ICollaborator>();
            var expected = new List<ICollaborator>()
            {
                collab1.Object,
                collab2.Object
            };

            var collabRepository = new Mock<ICollaboratorRepository>();
            collabRepository.Setup(c => c.GetByIdsAsync(new List<long>() { 1, 2 })).ReturnsAsync(expected);

            var assocRepoMock = new Mock<IAssociationProjectCollaboratorRepository>();
            var holidayPlanRepositoryDouble = new Mock<IHolidayPlanRepository>();
            var userRepo = new Mock<IUserRepository>();
            var collabFactory = new Mock<ICollaboratorFactory>();

            var collaboratorService =
                new CollaboratorService(assocRepoMock.Object, holidayPlanRepositoryDouble.Object,
                                        collabRepository.Object, userRepo.Object, collabFactory.Object,
                                        trainingModuleCollaboratorRepo.Object, trainingModuleRepo.Object);

            //act
            var result = await collaboratorService.GetAllByFinishedTrainingModuleInSubjectAfterPeriod(It.IsAny<long>(), It.IsAny<DateTime>());

            //assert
            Assert.True(expected.SequenceEqual(result));
        }
    }
}
