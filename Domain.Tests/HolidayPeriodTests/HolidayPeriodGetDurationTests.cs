using Domain.Interfaces;
using Domain.Models;
using Moq;

namespace Domain.Tests.HolidayPeriodTests;

public class HolidayPeriodGetDurationTests
{
    /**
    * Method to test the duration process
    */
    [Fact]
    public void WhenQueried_ThenReturnLength()
    {
        // Arrange
        var start = new DateOnly(2024, 4, 10);
        var end = new DateOnly(2024, 4, 15);
        var periodDate = new PeriodDate(start, end);

        int expected = (end.DayNumber - start.DayNumber) + 1;

        HolidayPeriod holidayPeriod = new HolidayPeriod(periodDate);

        // Act
        int result = holidayPeriod.GetDuration();

        // Assert
        Assert.Equal(expected, result);
    }
}