using Moq;
using Domain.Interfaces;
using Domain.IRepository;
using Application.Services;
using Domain.Models;
using System.Linq.Expressions;
using Domain.Factory;
using System.Threading.Tasks;

namespace Application.Tests.CollaboratorServiceTests
{
    public class CollaboratorServiceFindAllByProject
    {
        [Fact]
        public async Task WhenFindingCollaboratorsByProject_ThenReturnAllAssociatedCollaborators()
        {
            //arrange
            Mock<ICollaborator> collab1 = new Mock<ICollaborator>();
            Mock<ICollaborator> collab2 = new Mock<ICollaborator>();

            IEnumerable<ICollaborator> expected = new List<ICollaborator>()
            {
                collab1.Object,
                collab2.Object
            };

            Mock<IAssociationProjectCollaborator> assoc1 = new Mock<IAssociationProjectCollaborator>();
            Mock<IAssociationProjectCollaborator> assoc2 = new Mock<IAssociationProjectCollaborator>();

            var collabsIds = new List<long>()
            {
                1, 2
            };
            assoc1.Setup(a => a.GetCollaboratorId()).Returns(collabsIds[0]);
            assoc2.Setup(a => a.GetCollaboratorId()).Returns(collabsIds[1]);

            List<IAssociationProjectCollaborator> associations = new List<IAssociationProjectCollaborator>()
            {
                assoc1.Object,
                assoc2.Object,
            };

            Mock<IAssociationProjectCollaboratorRepository> assocRepoMock = new Mock<IAssociationProjectCollaboratorRepository>();
            assocRepoMock.Setup(a => a.FindAllByProjectAsync(It.IsAny<long>())).ReturnsAsync(associations);

            Mock<IHolidayPlanRepository> holidayPlanRepoMock = new Mock<IHolidayPlanRepository>();
            Mock<ICollaboratorRepository> collabRepository = new Mock<ICollaboratorRepository>();

            collabRepository.Setup(c => c.GetByIdsAsync(collabsIds)).ReturnsAsync(expected);

            var userRepo = new Mock<IUserRepository>();
            var tsRepo = new Mock<ITrainingSubjectRepository>();
            var tmRepo = new Mock<ITrainingModuleRepository>();
            var assocRepo = new Mock<IAssociationTrainingModuleCollaboratorRepository>();

            var collabFactory = new Mock<ICollaboratorFactory>();
            CollaboratorService collaboratorService = new CollaboratorService(assocRepoMock.Object, holidayPlanRepoMock.Object, collabRepository.Object, userRepo.Object, tsRepo.Object, tmRepo.Object, assocRepo.Object, collabFactory.Object);

            //act
            var result = await collaboratorService.FindAllByProject(It.IsAny<long>());

            //assert
            Assert.True(expected.SequenceEqual(result));
        }

        [Fact]
        public async Task WhenProjectHasNoCollaborators_ThenReturnEmptyList()
        {
            // Arrange
            Mock<IHolidayPlanRepository> holidayPlanRepoMock = new Mock<IHolidayPlanRepository>();
            Mock<ICollaboratorRepository> collabRepository = new Mock<ICollaboratorRepository>();

            // No associations for the project
            List<IAssociationProjectCollaborator> emptyAssociations = new List<IAssociationProjectCollaborator>();

            Mock<IAssociationProjectCollaboratorRepository> assocRepoMock = new Mock<IAssociationProjectCollaboratorRepository>();
            assocRepoMock.Setup(a => a.FindAllByProjectAsync(It.IsAny<long>())).ReturnsAsync(emptyAssociations);

            var userRepo = new Mock<IUserRepository>();
            var tsRepo = new Mock<ITrainingSubjectRepository>();
            var tmRepo = new Mock<ITrainingModuleRepository>();
            var assocRepo = new Mock<IAssociationTrainingModuleCollaboratorRepository>();

            var collabFactory = new Mock<ICollaboratorFactory>();
            CollaboratorService collaboratorService = new CollaboratorService(assocRepoMock.Object, holidayPlanRepoMock.Object, collabRepository.Object, userRepo.Object, tsRepo.Object, tmRepo.Object, assocRepo.Object, collabFactory.Object);

            // Act
            var result = await collaboratorService.FindAllByProject(It.IsAny<long>());

            // Assert
            Assert.Empty(result);
        }
    }
}
