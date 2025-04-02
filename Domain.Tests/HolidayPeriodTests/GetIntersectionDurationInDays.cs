namespace Domain.Tests.HolidayPeriodTests;

using Domain.Models;
using Xunit;
using System;
using Moq;
using Domain.Interfaces;

public class GetIntersectionDurationInDays
{
    /**
    * Test method to get duration in days of an intersection - between two period dates
    * Happy path - they overlap
    */
    [Fact]
    public void WhenPassingIntersectingPeriod_ReturnIntersectionDuration()
    {
        // Arrange
        // doubles for PeriodDates - stubs
        Mock<IPeriodDate> doublePeriodReference = new Mock<IPeriodDate>();
        Mock<IPeriodDate> doublePeriodInputed = new Mock<IPeriodDate>();
        Mock<IPeriodDate> doublePeriodIntersectionResult = new Mock<IPeriodDate>();

        // Establish that they intersect and return a new PeriodDate with the intersection
        doublePeriodReference.Setup(pd => pd.GetIntersection(doublePeriodInputed.Object))
                             .Returns(doublePeriodIntersectionResult.Object);

        Random rnd = new Random();
        int expected = rnd.Next(10, 100);

        // Establish the expected duration of period
        doublePeriodIntersectionResult.Setup(pd => pd.Duration()).Returns(expected);

        // Instatiate HolidayPeriod
        HolidayPeriod holidayPeriod = new HolidayPeriod(doublePeriodReference.Object);

        // Act
        int result = holidayPeriod.GetInterceptionDurationInDays(doublePeriodInputed.Object);

        // Assert
        Assert.Equal(expected, result);
    }

    /**
    * Test method to get duration in days of an intersection - between two period dates
    * They don't overlap
    */
    [Fact]
    public void WhenPassingNotIntersectingPeriod_ReturnZero()
    {
        // Arrange
        // doubles for PeriodDates - stubs
        Mock<IPeriodDate> doublePeriodReference = new Mock<IPeriodDate>();
        Mock<IPeriodDate> doublePeriodInputed = new Mock<IPeriodDate>();
        Mock<IPeriodDate> doublePeriodIntersectionResult = new Mock<IPeriodDate>();

        // Establish that they DON'T intersect and return null
        doublePeriodReference.Setup(pd => pd.GetIntersection(doublePeriodInputed.Object))
                             .Returns((IPeriodDate?)null);

        // The expected duration is going to be 0
        int expected = 0;

        // Instatiate HolidayPeriod
        HolidayPeriod holidayPeriod = new HolidayPeriod(doublePeriodReference.Object);

        // Act
        int result = holidayPeriod.GetInterceptionDurationInDays(doublePeriodInputed.Object);

        // Assert
        Assert.Equal(expected, result);
    }

}