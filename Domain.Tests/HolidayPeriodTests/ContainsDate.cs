namespace Domain.Tests.HolidayPeriodTests;

using Domain.Models;
using Xunit;
using System;
using Moq;
using Domain.Interfaces;

public class ContainsDate
{
    /**
    * Test to verify if date is contained in the holiday period
    * Its contained - true
    */
    [Fact]
    public void WhenPeriodIsFullyContained_ThenReturnsTrue()
    {
        // Arrange
        Mock<IPeriodDate> doublePeriodDate = new Mock<IPeriodDate>();

        // Random date that should be contained in the Period
        // for the context of the test -> value not important
        DateOnly dateToVerify = DateOnly.FromDateTime(DateTime.Now);

        // Establish that the date must be contained in it
        // Reference period CONTAINS date to Verify
        doublePeriodDate.Setup(pd => pd.ContainsDate(dateToVerify)).Returns(true);

        // Instatiate Holiday Period
        HolidayPeriod hPeriod = new HolidayPeriod(doublePeriodDate.Object);

        // Act
        bool result = hPeriod.ContainsDate(dateToVerify);

        // Assert
        Assert.True(result);
    }

    /**
    * Test to verify if date is contained in the holiday period
    * It's not contained - False
    */
    [Fact]
    public void WhenPeriodIsNotContained_ThenReturnsFalse()
    {
        // Arrange
        Mock<IPeriodDate> doublePeriodDate = new Mock<IPeriodDate>();

        // Random date that should not be contained in the Period
        // for the context of the test -> value not important
        DateOnly dateToVerify = DateOnly.FromDateTime(DateTime.Now);

        // Establish that the date must be contained in it
        // Reference period DOESN'T contain date to Verify
        doublePeriodDate.Setup(pd => pd.ContainsDate(dateToVerify)).Returns(false);

        // Instatiate Holiday Period
        HolidayPeriod hPeriod = new HolidayPeriod(doublePeriodDate.Object);

        // Act
        bool result = hPeriod.ContainsDate(dateToVerify);

        // Assert
        Assert.False(result);
    }
}