using Domain.Interfaces;
using Domain.Models;
using Moq;

namespace Domain.Tests.HolidayPeriodTests;

public class HolidayPeriodIntersectsTests
{
    [Fact]
    public void WhenPassingValidPeriodDate_ThenReturnsTrue()
    {
        // Arrange
        var period1 = new PeriodDate(new DateOnly(2024, 4, 1), new DateOnly(2024, 4, 5));
        var holidayPeriod = new HolidayPeriod(period1);

        var period2 = new PeriodDate(new DateOnly(2024, 4, 3), new DateOnly(2024, 4, 7));
        var holidayPeriod2 = new HolidayPeriod(period2);

        // Act
        var result = holidayPeriod.Intersects(holidayPeriod2);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void WhenPassingValidIncorrectPeriodDate_ThenReturnsFalse()
    {
        // Arrange
        var period1 = new PeriodDate(new DateOnly(2024, 4, 1), new DateOnly(2024, 4, 5));
        var holidayPeriod = new HolidayPeriod(period1);

        var period2 = new PeriodDate(new DateOnly(2024, 4, 6), new DateOnly(2024, 4, 10));
        var holidayPeriod2 = new HolidayPeriod(period2);

        // Act
        var result = holidayPeriod.Intersects(holidayPeriod2);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void WhenPassingValidHolidayPeriod_ThenReturnsTrue()
    {
        // Arrange
        var period1 = new PeriodDate(new DateOnly(2024, 4, 1), new DateOnly(2024, 4, 5));
        var holidayPeriod1 = new HolidayPeriod(period1);

        var period2 = new PeriodDate(new DateOnly(2024, 4, 3), new DateOnly(2024, 4, 7));
        var holidayPeriod2 = new HolidayPeriod(period2);

        // Act
        var result = holidayPeriod1.Intersects(holidayPeriod2);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void WhenPassingValidIncorrectHolidayPeriod_ThenReturnsFalse()
    {
        // Arrange
        var period1 = new PeriodDate(new DateOnly(2024, 4, 1), new DateOnly(2024, 4, 5));
        var holidayPeriod1 = new HolidayPeriod(period1);

        var period2 = new PeriodDate(new DateOnly(2024, 4, 6), new DateOnly(2024, 4, 10));
        var holidayPeriod2 = new HolidayPeriod(period2);

        // Act
        var result = holidayPeriod1.Intersects(holidayPeriod2);

        // Assert
        Assert.False(result);
    }
}
