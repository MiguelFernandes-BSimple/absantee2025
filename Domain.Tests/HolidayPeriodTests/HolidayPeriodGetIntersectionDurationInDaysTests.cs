using Domain.Models;
using Moq;
using Domain.Interfaces;

namespace Domain.Tests.HolidayPeriodTests;

public class HolidayPeriodGetIntersectionDurationInDaysTests
{
    /**
    * Test method to get duration in days of an intersection - between two period dates
    * Happy path - they overlap
    */
    [Fact]
    public void WhenPassingIntersectingPeriod_ReturnIntersectionDuration()
    {
        // Arrange
        var startReference = new DateOnly(2024, 4, 1);
        var endReference = new DateOnly(2024, 4, 20); // Reference period (20 days)
        var startInputed = new DateOnly(2024, 4, 10);
        var endInputed = new DateOnly(2024, 4, 25); // Inputed period (15 days)

        var periodReference = new PeriodDate(startReference, endReference);
        var periodInputed = new PeriodDate(startInputed, endInputed);

        // Calculate intersection
        var intersectionStart = startInputed > startReference ? startInputed : startReference;
        var intersectionEnd = endInputed < endReference ? endInputed : endReference;

        // Duration of the intersection should be 10 days
        int expectedIntersectionDuration = intersectionEnd.DayNumber - intersectionStart.DayNumber + 1;

        // Instatiate HolidayPeriod
        HolidayPeriod holidayPeriod = new HolidayPeriod(periodReference);

        // Act
        int result = holidayPeriod.GetInterceptionDurationInDays(periodInputed);

        // Assert
        Assert.Equal(expectedIntersectionDuration, result);
    }

    /**
    * Test method to get duration in days of an intersection - between two period dates
    * They don't overlap
    */
    [Fact]
    public void WhenPassingNotIntersectingPeriod_ReturnZero()
    {
        // Arrange
        var startReference = new DateOnly(2024, 4, 1);
        var endReference = new DateOnly(2024, 4, 10); // Reference period (10 days)
        var startInputed = new DateOnly(2024, 4, 15);
        var endInputed = new DateOnly(2024, 4, 20); // Inputed period (5 days)

        var periodReference = new PeriodDate(startReference, endReference);
        var periodInputed = new PeriodDate(startInputed, endInputed);

        // Instatiate HolidayPeriod
        HolidayPeriod holidayPeriod = new HolidayPeriod(periodReference);

        // Act
        int result = holidayPeriod.GetInterceptionDurationInDays(periodInputed);

        // Assert
        Assert.Equal(0, result); // No intersection
    }
}


