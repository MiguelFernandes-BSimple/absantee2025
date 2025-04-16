using Domain.Models;
using Domain.Interfaces;
using Moq;

namespace Domain.Tests.HolidayPlanTests;

public class SetId{

    [Fact]
    public void WhenSettingId_ThenGetIdReturnsUpdatedId()
    {
        // Arrange
        var initialId = 1L;
        var updatedId = 2L;
        var collabId = 4L;

        var holidayPeriod1 = new Mock<IHolidayPeriod>();
        var holidayPeriod2 = new Mock<IHolidayPeriod>();

        var periods = new List<IHolidayPeriod>
        {
            holidayPeriod1.Object,
            holidayPeriod2.Object
        };

        var holidayPlan = new HolidayPlan(initialId, collabId, periods);

        // Act
        holidayPlan.SetId(updatedId);
        var result = holidayPlan.GetId();

        // Assert
        Assert.Equal(updatedId, result);
    }
}