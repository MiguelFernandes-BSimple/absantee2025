using Domain.Interfaces;
using Domain.IRepository;
using Application.Services;
using Moq;
using System.Linq.Expressions;

namespace Application.Tests.CollaboratorServiceTests
{
    public class FindAllWithHolidayPeriodsLongerThan
    {
        [Fact]
        public void WhenFindingAllCollaboratorsWithHolidayPeriodsLongerThan_ThenShouldReturnCorrectCollaborators()
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
            holidayPlanRepositoryDouble.Setup(hpr => hpr.FindAllWithHolidayPeriodsLongerThan(days)).Returns(new List<IHolidayPlan> { holidayPlanDouble1.Object });

            Mock<ICollaboratorRepository> collabRepository = new Mock<ICollaboratorRepository>();
            collabRepository.Setup(c => c.Find(It.IsAny<Expression<Func<ICollaborator, bool>>>())).Returns(expected);

            CollaboratorService collaboratorService = new CollaboratorService(assocRepoMock.Object, holidayPlanRepositoryDouble.Object, collabRepository.Object);

            //act
            var result = collaboratorService.FindAllWithHolidayPeriodsLongerThan(days).ToList();

            //assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void WhenNoCollaboratorHaveHolidayPeriodLongerThan_ThenShouldReturnEmptyList()
        {
            //arrange
            int days = 5;

            Mock<IAssociationProjectCollaboratorRepository> assocRepoMock = new Mock<IAssociationProjectCollaboratorRepository>();
            Mock<IHolidayPlanRepository> holidayPlanRepositoryDouble = new Mock<IHolidayPlanRepository>();
            holidayPlanRepositoryDouble.Setup(hpr => hpr.FindAllWithHolidayPeriodsLongerThan(days)).Returns(new List<IHolidayPlan> { });

            Mock<ICollaboratorRepository> collabRepository = new Mock<ICollaboratorRepository>();

            CollaboratorService service = new CollaboratorService(assocRepoMock.Object, holidayPlanRepositoryDouble.Object, collabRepository.Object);

            //act
            var result = service.FindAllWithHolidayPeriodsLongerThan(days);

            //assert
            Assert.Empty(result);
        }
    }
}
