using Domain.Interfaces;
using Domain.IRepository;
using Application.Services;
using Moq;
using System.Linq.Expressions;
using Domain.Factory;
using System.Threading.Tasks;

namespace Application.Tests.CollaboratorServiceTests
{
    public class FindAllWithHolidayPeriodsLongerThan
    {
        [Fact]
        public async Task WhenFindingAllCollaboratorsWithHolidayPeriodsLongerThan_ThenShouldReturnCorrectCollaborators()
        {
            //arrange
            int days = 5;

            Mock<ICollaborator> collaboratorDouble1 = new Mock<ICollaborator>();
            //Mock<ICollaborator> collaboratorDouble2 = new Mock<ICollaborator>();

            var expected = new List<ICollaborator> { collaboratorDouble1.Object };

            Mock<IHolidayPlan> holidayPlanDouble1 = new Mock<IHolidayPlan>();
            holidayPlanDouble1.Setup(p => p.GetCollaboratorId()).Returns(collaboratorDouble1.Object.GetId);
            //holidayPlanDouble1.Setup(p => p.HasPeriodLongerThan(days)).Returns(true);

            Mock<IAssociationProjectCollaboratorRepository> assocRepoMock = new Mock<IAssociationProjectCollaboratorRepository>();
            Mock<IHolidayPlanRepository> holidayPlanRepositoryDouble = new Mock<IHolidayPlanRepository>();
            holidayPlanRepositoryDouble.Setup(hpr => hpr.FindAllWithHolidayPeriodsLongerThanAsync(days)).ReturnsAsync(new List<IHolidayPlan> { holidayPlanDouble1.Object });

            Mock<ICollaboratorRepository> collabRepository = new Mock<ICollaboratorRepository>();
            collabRepository.Setup(c => c.Find(It.IsAny<Expression<Func<ICollaborator, bool>>>())).Returns(expected);

            var userRepo = new Mock<IUserRepository>();
            var collabFactory = new Mock<ICollaboratorFactory>();
            CollaboratorService collaboratorService = new CollaboratorService(assocRepoMock.Object, holidayPlanRepositoryDouble.Object, collabRepository.Object, userRepo.Object, collabFactory.Object);

            //act
            var result = await collaboratorService.FindAllWithHolidayPeriodsLongerThan(days);

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
            var collabFactory = new Mock<ICollaboratorFactory>();
            CollaboratorService service = new CollaboratorService(assocRepoMock.Object, holidayPlanRepositoryDouble.Object, collabRepository.Object, userRepo.Object, collabFactory.Object);

            //act
            var result = await service.FindAllWithHolidayPeriodsLongerThan(days);

            //assert
            Assert.Empty(result);
        }
    }
}
