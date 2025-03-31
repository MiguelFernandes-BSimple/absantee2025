using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;

namespace Domain.Tests
{
    public class CollaboratorServiceTest
    {

        [Fact]
        public void WhenCreatingWithValidParameters_ThenObjectIsCreated()
        {
            //arrange
            Mock<IHolidayPlanRepository> holidayPlanRepositoryDouble = new Mock<IHolidayPlanRepository>();

            //act & assert
            new CollaboratorService(holidayPlanRepositoryDouble.Object);

        }

        [Fact]
        public void WhenFindingAllCollaboratorsWithHolidayPeriodsLongerThan_ThenShouldReturnCorrectCollaborators()
        {
            //arrange
            int days = 5;

            Mock<ICollaborator> collaboratorDouble1 = new Mock<ICollaborator>();
            //Mock<ICollaborator> collaboratorDouble2 = new Mock<ICollaborator>();

            Mock<IHolidayPlan> holidayPlanDouble1 = new Mock<IHolidayPlan>();
            holidayPlanDouble1.Setup(p => p.GetCollaborator()).Returns(collaboratorDouble1.Object);
            //holidayPlanDouble1.Setup(p => p.HasPeriodLongerThan(days)).Returns(true);

            Mock<IHolidayPlanRepository> holidayPlanRepositoryDouble = new Mock<IHolidayPlanRepository>();
            holidayPlanRepositoryDouble.Setup(hpr => hpr.FindAllWithHolidayPeriodsLongerThan(days)).Returns(new List<IHolidayPlan> { holidayPlanDouble1.Object });

            CollaboratorService collaboratorService = new CollaboratorService(holidayPlanRepositoryDouble.Object);

            //act
            var result = collaboratorService.FindAllWithHolidayPeriodsLongerThan(days).ToList();

            //assert
            var expected = new List<ICollaborator> { collaboratorDouble1.Object };
            Assert.Equal(expected, result);
        }

        [Fact]
        public void WhenNoCollaboratorHaveHolidayPeriodLongerThan_ThenShouldReturnEmptyList()
        {
            //arrange
            int days = 5;

            Mock<IHolidayPlanRepository> holidayPlanRepositoryDouble = new Mock<IHolidayPlanRepository>();
            holidayPlanRepositoryDouble.Setup(hpr => hpr.FindAllWithHolidayPeriodsLongerThan(days)).Returns(new List<IHolidayPlan> { });

            CollaboratorService service = new CollaboratorService(holidayPlanRepositoryDouble.Object);

            //act
            var result = service.FindAllWithHolidayPeriodsLongerThan(days).ToList();

            //assert
            Assert.Empty(result);
        }
    }
}