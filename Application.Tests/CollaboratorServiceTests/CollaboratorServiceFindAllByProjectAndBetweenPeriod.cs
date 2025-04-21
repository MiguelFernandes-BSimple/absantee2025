using Domain.Interfaces;
using Domain.IRepository;
using Application.Services;
using Moq;
using System.Linq.Expressions;
using Domain.Factory;
using System.Threading.Tasks;
using Domain.Models;

namespace Application.Tests.CollaboratorServiceTests
{
    public class CollaboratorServiceFindAllByProjectAndBetweenPeriod
    {
        [Fact]
        public async Task WhenProjectHasCollaboratorsInPeriod_ThenReturnAllCollaborators()
        {
            // Arrange
            Mock<IProject> projectMock = new Mock<IProject>();

            Mock<IHolidayPlanRepository> holidayPlanRepoMock = new Mock<IHolidayPlanRepository>();
            Mock<IAssociationProjectCollaboratorRepository> assocRepoMock = new Mock<IAssociationProjectCollaboratorRepository>();

            Mock<IAssociationProjectCollaborator> assoc1 = new Mock<IAssociationProjectCollaborator>();
            Mock<IAssociationProjectCollaborator> assoc2 = new Mock<IAssociationProjectCollaborator>();

            List<IAssociationProjectCollaborator> associations = new List<IAssociationProjectCollaborator>()
            {
                assoc1.Object,
                assoc2.Object,
            };

            Mock<ICollaborator> collab1 = new Mock<ICollaborator>();
            Mock<ICollaborator> collab2 = new Mock<ICollaborator>();

            var collabsIds = new List<long>()
            {
                1, 2
            };
            assoc1.Setup(a => a.GetCollaboratorId()).Returns(collabsIds[0]);
            assoc2.Setup(a => a.GetCollaboratorId()).Returns(collabsIds[1]);

            List<ICollaborator> expected = new List<ICollaborator>()
            {
                collab1.Object,
                collab2.Object
            };

            assocRepoMock.Setup(a => a.FindAllByProjectAndBetweenPeriodAsync(It.IsAny<long>(), It.IsAny<PeriodDate>())).ReturnsAsync(associations);

            Mock<ICollaboratorRepository> collabRepository = new Mock<ICollaboratorRepository>();
            collabRepository.Setup(c => c.GetByIdsAsync(collabsIds)).ReturnsAsync(expected);

            var userRepo = new Mock<IUserRepository>();
            var collabFactory = new Mock<ICollaboratorFactory>();
            var assocFormationCollaboratorRepoDouble = new Mock<IAssociationFormationModuleCollaboratorRepository>();
            var formationModuleRepoDouble = new Mock<IFormationModuleRepository>();
            var formationSubjectRepoDouble = new Mock<IFormationSubjectRepository>();
            CollaboratorService collaboratorService = new CollaboratorService(assocRepoMock.Object, holidayPlanRepoMock.Object, collabRepository.Object, userRepo.Object, collabFactory.Object, formationModuleRepoDouble.Object, formationSubjectRepoDouble.Object, assocFormationCollaboratorRepoDouble.Object);

            // Act
            var result = await collaboratorService.FindAllByProjectAndBetweenPeriod(It.IsAny<long>(), It.IsAny<PeriodDate>());

            // Assert
            Assert.True(expected.SequenceEqual(result));
        }


        [Fact]
        public async Task WhenProjectHasNoCollaboratorsInPeriod_ThenReturnEmptyList()
        {
            // Arrange
            Mock<IHolidayPlanRepository> holidayPlanRepoMock = new Mock<IHolidayPlanRepository>();
            Mock<IAssociationProjectCollaboratorRepository> assocRepoMock = new Mock<IAssociationProjectCollaboratorRepository>();

            // No associations for the project in the period
            List<IAssociationProjectCollaborator> emptyAssociations = new List<IAssociationProjectCollaborator>();

            assocRepoMock.Setup(a => a.FindAllByProjectAndBetweenPeriodAsync(It.IsAny<long>(), It.IsAny<PeriodDate>())).ReturnsAsync(emptyAssociations);

            Mock<ICollaboratorRepository> collabRepository = new Mock<ICollaboratorRepository>();

            var userRepo = new Mock<IUserRepository>();
            var collabFactory = new Mock<ICollaboratorFactory>();
            var assocFormationCollaboratorRepoDouble = new Mock<IAssociationFormationModuleCollaboratorRepository>();
            var formationModuleRepoDouble = new Mock<IFormationModuleRepository>();
            var formationSubjectRepoDouble = new Mock<IFormationSubjectRepository>();
            CollaboratorService collaboratorService = new CollaboratorService(assocRepoMock.Object, holidayPlanRepoMock.Object, collabRepository.Object, userRepo.Object, collabFactory.Object, formationModuleRepoDouble.Object, formationSubjectRepoDouble.Object, assocFormationCollaboratorRepoDouble.Object);

            // Act
            var result = await collaboratorService.FindAllByProjectAndBetweenPeriod(It.IsAny<long>(), It.IsAny<PeriodDate>());

            // Assert
            Assert.Empty(result);
        }
    }
}
