using Domain.Interfaces;
using Domain.Models;
using Moq;

namespace Domain.Tests.HolidayPeriodTests;

public class HolidayPeriodSetIdTests
{

    [Fact]
    public void WhenSettingId_ThenGetIdReturnsNewValue()
    {
        // Arrange
        var initialId = 1L;
        var newId = 2L;
        var start = new DateOnly(2024, 4, 10);
        var end = new DateOnly(2024, 4, 15);
        var periodDate = new PeriodDate(start, end);
        var holidayPlan = new HolidayPeriod(initialId, periodDate);

        // Act
        holidayPlan.SetId(newId);
        var result = holidayPlan.GetId();

        // Assert
        Assert.Equal(newId, result);
    }
}