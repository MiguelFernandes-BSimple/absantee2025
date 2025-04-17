using Domain.Models;
using Domain.Interfaces;
using Moq;

namespace Domain.Tests.HolidayPlanTests;



public class HolidayPlanGetHolidayPeriodsTests{

    [Fact]
    public void WhenGettingHolidayPeriods_ThenReturnsTheCorrectList()
    {
        // Arrange
        var id = 2;
        var collaboratorId = 4;

        var holidayPeriod1 = new Mock<IHolidayPeriod>();
        var holidayPeriod2 = new Mock<IHolidayPeriod>();

        var expectedList = new List<IHolidayPeriod>
        {
            holidayPeriod1.Object,
            holidayPeriod2.Object
        };

        var holidayPlan = new HolidayPlan(id, collaboratorId, expectedList);

        // Act
        var result = holidayPlan.GetHolidayPeriods();

        // Assert
        Assert.Equal(expectedList, result);
    }

}