namespace Domain.Tests.HolidayPeriodTests;

using Domain.Interfaces;
using Domain.Models;
using Xunit;
using Moq;

public class IsLongerThan
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
        Mock<IPeriodDate> doublePeriodDate = new Mock<IPeriodDate>();

        // For test -> duration = 20 day
        // Inputed days have to be < 20
        int periodDays = 20;
        doublePeriodDate.Setup(pd => pd.Duration()).Returns(periodDays);

        // Instatiate HolidayPeriod
        HolidayPeriod holidayPeriod = new HolidayPeriod(doublePeriodDate.Object);

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
        Mock<IPeriodDate> doublePeriodDate = new Mock<IPeriodDate>();

        // For test -> duration = 10 day
        // Inputed days have to be > 10
        int periodDays = 10;
        doublePeriodDate.Setup(pd => pd.Duration()).Returns(periodDays);

        // Instatiate HolidayPeriod
        HolidayPeriod holidayPeriod = new HolidayPeriod(doublePeriodDate.Object);

        // Act
        bool result = holidayPeriod.IsLongerThan(days);

        // Assert
        Assert.False(result);
    }
}