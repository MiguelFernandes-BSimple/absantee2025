using Domain.Interfaces;
using Domain.Models;
using Moq;

namespace Domain.Tests.HolidayPeriodTests;

public class HolidayPeriodIsLongerThanTests
{
    /**
    * Test method to verify if a period's duration in days is superior to inputed days
    * Case where it's true
    */
    [Theory]
    [InlineData(0)]
    [InlineData(2)]
    [InlineData(10)]
    public void WhenPeriodDurationIsGreaterThanLimit_ThenShouldReturnTrue(int days)
    {
        // Arrange
        var start = new DateOnly(2024, 4, 1);
        var end = new DateOnly(2024, 4, 20); // Period of 20 days
        var periodDate = new PeriodDate(start, end);

        // Instatiate HolidayPeriod
        HolidayPeriod holidayPeriod = new HolidayPeriod(periodDate);

        // Act
        bool result = holidayPeriod.IsLongerThan(days);

        // Assert
        Assert.True(result);
    }

    [Theory]
    [InlineData(15)]
    [InlineData(20)]
    [InlineData(10)]
    public void WhenPeriodDurationIsLessOrEqualThanLimit_ThenShouldReturnFalse(int days)
    {
        // Arrange
        var start = new DateOnly(2024, 4, 1);
        var end = new DateOnly(2024, 4, 10); // Period of 10 days
        var periodDate = new PeriodDate(start, end);

        // Instatiate HolidayPeriod
        HolidayPeriod holidayPeriod = new HolidayPeriod(periodDate);

        // Act
        bool result = holidayPeriod.IsLongerThan(days);

        // Assert
        Assert.False(result);
    }
}
