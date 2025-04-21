using Domain.Interfaces;
using Domain.IRepository;
using Application.Services;
using Moq;
using System.Linq.Expressions;
using Domain.Factory;
using System.Threading.Tasks;

namespace Application.Tests.CollaboratorServiceTests
{
    public class CollaboratorServiceFindAllWithHolidayPeriodsLongerThan
    {
        [Fact]
        public async Task WhenFindingAllCollaboratorsWithHolidayPeriodsLongerThan_ThenShouldReturnCorrectCollaborators()
        {
            //arrange
            int days = 5;

            Mock<ICollaborator> collaboratorDouble1 = new Mock<ICollaborator>();
            Mock<ICollaborator> collaboratorDouble2 = new Mock<ICollaborator>();
            var collabsIds = new List<long>()
            {
                1, 2
            };

            var expected = new List<ICollaborator> { collaboratorDouble1.Object, collaboratorDouble2.Object };

            Mock<IHolidayPlan> holidayPlanDouble1 = new Mock<IHolidayPlan>();
            Mock<IHolidayPlan> holidayPlanDouble2 = new Mock<IHolidayPlan>();
            var holidayPlans = new List<IHolidayPlan>()
            {
                holidayPlanDouble1.Object,
                holidayPlanDouble2.Object
            };
            holidayPlanDouble1.Setup(p => p.GetCollaboratorId()).Returns(collabsIds[0]);
            holidayPlanDouble2.Setup(p => p.GetCollaboratorId()).Returns(collabsIds[1]);

            Mock<IAssociationProjectCollaboratorRepository> assocRepoMock = new Mock<IAssociationProjectCollaboratorRepository>();
            Mock<IHolidayPlanRepository> holidayPlanRepositoryDouble = new Mock<IHolidayPlanRepository>();
            holidayPlanRepositoryDouble.Setup(hpr => hpr.FindAllWithHolidayPeriodsLongerThanAsync(days)).ReturnsAsync(holidayPlans);

            Mock<ICollaboratorRepository> collabRepository = new Mock<ICollaboratorRepository>();
            collabRepository.Setup(c => c.GetByIdsAsync(collabsIds)).ReturnsAsync(expected);

            var userRepo = new Mock<IUserRepository>();
            var tsRepo = new Mock<ITrainingSubjectRepository>();
            var tmRepo = new Mock<ITrainingModuleRepository>();
            var assocRepo = new Mock<IAssociationTrainingModuleCollaboratorRepository>();
            var collabFactory = new Mock<ICollaboratorFactory>();
            var service = new CollaboratorService(assocRepoMock.Object, holidayPlanRepositoryDouble.Object, collabRepository.Object, userRepo.Object, tsRepo.Object, tmRepo.Object, assocRepo.Object, collabFactory.Object);

            //act
            var result = await service.FindAllWithHolidayPeriodsLongerThan(days);

            //assert
            Assert.True(expected.SequenceEqual(result));
        }

        [Fact]
        public async Task WhenNoCollaboratorHaveHolidayPeriodLongerThan_ThenShouldReturnEmptyList()
        {
            //arrange
            int days = 5;

            Mock<IAssociationProjectCollaboratorRepository> assocRepoMock = new Mock<IAssociationProjectCollaboratorRepository>();
            Mock<IHolidayPlanRepository> holidayPlanRepositoryDouble = new Mock<IHolidayPlanRepository>();
            holidayPlanRepositoryDouble.Setup(hpr => hpr.FindAllWithHolidayPeriodsLongerThanAsync(days)).ReturnsAsync(new List<IHolidayPlan> { });

            Mock<ICollaboratorRepository> collabRepository = new Mock<ICollaboratorRepository>();

            var userRepo = new Mock<IUserRepository>();
            var tsRepo = new Mock<ITrainingSubjectRepository>();
            var tmRepo = new Mock<ITrainingModuleRepository>();
            var assocRepo = new Mock<IAssociationTrainingModuleCollaboratorRepository>();
            var collabFactory = new Mock<ICollaboratorFactory>();
            var service = new CollaboratorService(assocRepoMock.Object, holidayPlanRepositoryDouble.Object, collabRepository.Object, userRepo.Object, tsRepo.Object, tmRepo.Object, assocRepo.Object, collabFactory.Object);

            //act
            var result = await service.FindAllWithHolidayPeriodsLongerThan(days);

            //assert
            Assert.Empty(result);
        }
    }
}
