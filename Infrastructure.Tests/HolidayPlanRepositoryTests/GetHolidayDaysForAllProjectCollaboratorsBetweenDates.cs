using Infrastructure.Repositories;
using Domain.Interfaces;
using Domain.Models;
using Moq;
namespace Infrastructure.Tests.HolidayPlanRepositoryTests;

public class GetHolidayDaysForAllProjectCollaboratorsBetweenDates
{
    [Fact]
    public void GivenProjectWithCollaborators_WhenGetHolidayDaysForAllCollaborators_ThenReturnsCorrectDays()
    {
        // Arrange
        var collaborator1 = new Mock<ICollaborator>();
        var collaborator2 = new Mock<ICollaborator>();
        var collaboratorList = new List<ICollaborator>() { 
            collaborator1.Object,
            collaborator2.Object
        };

        var holidayPeriod1 = new Mock<IHolidayPeriod>();

        var holidayPlan1 = new Mock<IHolidayPlan>();
        holidayPlan1.Setup(hp => hp.HasCollaborator(collaborator1.Object)).Returns(true);
        holidayPlan1
            .Setup(hp => hp.GetHolidayPeriods())
            .Returns(new List<IHolidayPeriod> { holidayPeriod1.Object });

        holidayPeriod1.Setup(hperiod => hperiod.Intersects(It.IsAny<IPeriodDate>())).Returns(true);
        holidayPeriod1.Setup(hperiod => hperiod.GetDuration()).Returns(10);

        var holidayPeriod2 = new Mock<IHolidayPeriod>();

        var holidayPlan2 = new Mock<IHolidayPlan>();
        holidayPlan2.Setup(hp => hp.HasCollaborator(collaborator1.Object)).Returns(true);
        holidayPlan2
            .Setup(hp => hp.GetHolidayPeriods())
            .Returns(new List<IHolidayPeriod> { holidayPeriod2.Object });

        holidayPeriod2.Setup(hperiod => hperiod.Intersects(It.IsAny<IPeriodDate>())).Returns(true);
        holidayPeriod2.Setup(hperiod => hperiod.GetDuration()).Returns(5);


        // Adicionar o plano de férias ao repositório
        var hpRepo = new HolidayPlanRepository(new List<IHolidayPlan> { holidayPlan1.Object });

        // Act
        var result = hpRepo.GetHolidayDaysForAllProjectCollaboratorsBetweenDates(
            collaboratorList,
            It.IsAny<IPeriodDate>()
        );

        // Assert
        Assert.Equal(15, result);
    }
}