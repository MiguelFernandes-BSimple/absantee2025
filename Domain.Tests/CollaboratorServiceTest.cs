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
        public void WhenFindingALlCollaboratorsWithHolidayPeriodsLongerThan_ThenShouldReturnCorrectCollaborators()
        {
            //arrange
            int days = 5;

            Mock<ICollaborator> collaboratorDouble1 = new Mock<ICollaborator>();
            Mock<ICollaborator> collaboratorDouble2 = new Mock<ICollaborator>();

            Mock<IHolidayPlan> holidayPlanDouble1 = new Mock<IHolidayPlan>();
            holidayPlanDouble1.Setup(p => p.HasPeriodLongerThan(days)).Returns(true);
            holidayPlanDouble1.Setup(p => p.GetCollaborator()).Returns(collaboratorDouble1.Object);

            Mock<IHolidayPlan> holidayPlanDouble2 = new Mock<IHolidayPlan>();
            holidayPlanDouble2.Setup(p => p.HasPeriodLongerThan(days)).Returns(false);
            holidayPlanDouble2.Setup(p => p.GetCollaborator()).Returns(collaboratorDouble2.Object);

            Mock<IHolidayPlanRepository> holidayPlanRepositoryDouble = new Mock<IHolidayPlanRepository>();
            holidayPlanRepositoryDouble.Setup(hpr => hpr.FindAll()).Returns(new List<IHolidayPlan> { holidayPlanDouble1.Object, holidayPlanDouble2.Object });

            CollaboratorService service = new CollaboratorService(holidayPlanRepositoryDouble.Object);

            //act
            var result = service.FindAllWithHolidayPeriodsLongerThan(days).ToList();

            //assert
            Assert.Contains(collaboratorDouble1.Object, result);
            Assert.DoesNotContain(collaboratorDouble2.Object, result);
        }

        [Fact]
        public void WhenNoCollaboratorHaveHolidayPeriodLongerThan_ThenShouldReturnEmptyList()
        {
            //arrange
            int days = 5;

            Mock<ICollaborator> collaboratorDouble1 = new Mock<ICollaborator>();
            Mock<ICollaborator> collaboratorDouble2 = new Mock<ICollaborator>();

            Mock<IHolidayPlan> holidayPlanDouble1 = new Mock<IHolidayPlan>();
            holidayPlanDouble1.Setup(p => p.HasPeriodLongerThan(days)).Returns(false);
            holidayPlanDouble1.Setup(p => p.GetCollaborator()).Returns(collaboratorDouble1.Object);

            Mock<IHolidayPlan> holidayPlanDouble2 = new Mock<IHolidayPlan>();
            holidayPlanDouble2.Setup(p => p.HasPeriodLongerThan(days)).Returns(false);
            holidayPlanDouble2.Setup(p => p.GetCollaborator()).Returns(collaboratorDouble2.Object);

            Mock<IHolidayPlanRepository> holidayPlanRepositoryDouble = new Mock<IHolidayPlanRepository>();
            holidayPlanRepositoryDouble.Setup(hpr => hpr.FindAll()).Returns(new List<IHolidayPlan> { holidayPlanDouble1.Object, holidayPlanDouble2.Object });

            CollaboratorService service = new CollaboratorService(holidayPlanRepositoryDouble.Object);

            //act
            var result = service.FindAllWithHolidayPeriodsLongerThan(days).ToList();

            //assert
            Assert.Empty(result);
        }
    }
}