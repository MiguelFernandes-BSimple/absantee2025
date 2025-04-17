using Domain.Interfaces;
using Domain.Models;
using Moq;

namespace Domain.Tests.HolidayPeriodTests;

public class HolidayPeriodContainsTests
{
    [Fact]
    public void WhenHolidayPeriodIsFullyContained_ThenReturnsTrue()
    {
        // Arrange
        var referenceStart = DateOnly.FromDateTime(DateTime.Now);
        var referenceEnd = referenceStart.AddDays(10);
        var innerStart = referenceStart.AddDays(2);
        var innerEnd = referenceStart.AddDays(5);

        var referencePeriod = new PeriodDate(referenceStart, referenceEnd);
        var innerPeriod = new PeriodDate(innerStart, innerEnd);

        var referenceHPeriod = new HolidayPeriod(referencePeriod);
        var toVerifyHPeriod = new HolidayPeriod(innerPeriod);

        // Act
        bool result = referenceHPeriod.Contains(toVerifyHPeriod);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void WhenHolidayPeriodIsNotFullyContained_ThenReturnsFalse()
    {
        // Arrange
        var referenceStart = DateOnly.FromDateTime(DateTime.Now);
        var referenceEnd = referenceStart.AddDays(5);
        var outerStart = referenceEnd.AddDays(1);
        var outerEnd = referenceEnd.AddDays(5);

        var referencePeriod = new PeriodDate(referenceStart, referenceEnd);
        var outerPeriod = new PeriodDate(outerStart, outerEnd);

        var referenceHPeriod = new HolidayPeriod(referencePeriod);
        var toVerifyHPeriod = new HolidayPeriod(outerPeriod);

        // Act
        bool result = referenceHPeriod.Contains(toVerifyHPeriod);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void WhenPeriodIsFullyContained_ThenReturnsTrue()
    {
        // Arrange
        var start = DateOnly.FromDateTime(DateTime.Now);
        var end = start.AddDays(5);
        var innerDate = start.AddDays(2);

        var period = new PeriodDate(start, end);
        var holidayPeriod = new HolidayPeriod(period);

        // Act
        bool result = holidayPeriod.ContainsDate(innerDate);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void WhenPeriodIsNotFullyContained_ThenReturnsFalse()
    {
        // Arrange
        var start = DateOnly.FromDateTime(DateTime.Now);
        var end = start.AddDays(5);
        var outsideDate = end.AddDays(1);

        var period = new PeriodDate(start, end);
        var holidayPeriod = new HolidayPeriod(period);

        // Act
        bool result = holidayPeriod.ContainsDate(outsideDate);

        // Assert
        Assert.False(result);
    }
}
