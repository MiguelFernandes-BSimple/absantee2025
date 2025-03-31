using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;

namespace Domain.Tests
{
    public class HolidayPlanServiceTest
    {
        [Fact]
        public void WhenCalculatingHolidayDaysOfCollaboratorInAProject_ThenReturnCorrectValue()
        {
            //arrange
            Mock<IAssociationProjectCollaborator> associationDouble = new Mock<IAssociationProjectCollaborator>();
            Mock<IHolidayPlan> holidayPlanDouble = new Mock<IHolidayPlan>();
            Mock<ICollaborator> collaboratorDouble = new Mock<ICollaborator>();

            DateOnly initDate = new DateOnly(2025, 6, 1);
            DateOnly finalDate = new DateOnly(2025, 6, 10);

            associationDouble.Setup(a => a.GetCollaborator()).Returns(collaboratorDouble.Object);
            associationDouble.Setup(a => a.GetInitDate()).Returns(initDate);
            associationDouble.Setup(a => a.GetFinalDate()).Returns(finalDate);

            holidayPlanDouble.Setup(hp => hp.GetCollaborator()).Returns(collaboratorDouble.Object);
            holidayPlanDouble.Setup(hp => hp.GetNumberOfHolidayDaysBetween(initDate, finalDate)).Returns(5);

            Mock<IHolidayPlanRepository> holidayPlanRepositoryDouble = new Mock<IHolidayPlanRepository>();
            holidayPlanRepositoryDouble.Setup(hpr => hpr.FindHolidayPlanByAssociationProjectCollaborator(associationDouble.Object)).Returns(holidayPlanDouble.Object);

            HolidayPlanService service = new HolidayPlanService(holidayPlanRepositoryDouble.Object);

            //act
            int result = service.GetHolidayDaysOfCollaboratorInProject(associationDouble.Object);

            //assert
            Assert.Equal(5, result);

        }

        [Fact]
        public void WhenCalculatingHolidayDaysOfCollaboratorInAProjectWithoutHolidayPlan_ThenReturnZero()
        {
            //arrange
            Mock<IAssociationProjectCollaborator> associationDouble = new Mock<IAssociationProjectCollaborator>();
            Mock<ICollaborator> collaboratorDouble = new Mock<ICollaborator>();

            associationDouble.Setup(a => a.GetCollaborator()).Returns(collaboratorDouble.Object);

            Mock<IHolidayPlanRepository> holidayPlanRepositoryDouble = new Mock<IHolidayPlanRepository>();
            holidayPlanRepositoryDouble.Setup(hpr => hpr.FindHolidayPlanByAssociationProjectCollaborator(associationDouble.Object)).Returns((IHolidayPlan?)null);


            HolidayPlanService service = new HolidayPlanService(holidayPlanRepositoryDouble.Object);

            //act
            int result = service.GetHolidayDaysOfCollaboratorInProject(associationDouble.Object);

            //assert
            Assert.Equal(0, result);
        }
    }
}