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
        Mock<IPeriodDate> doublePeriodDate = new Mock<IPeriodDate>();

        // Random int value
        // for test context value is not important
        Random rnd = new Random();
        int expected = rnd.Next(10, 100);

        // PeriodDate must get the expected duration
        doublePeriodDate.Setup(pd => pd.Duration()).Returns(expected);

        // Instatiate Holiday Period
        HolidayPeriod holidayPeriod = new HolidayPeriod(doublePeriodDate.Object);

        // Act
        int result = holidayPeriod.GetDuration();

        // Assert
        Assert.Equal(expected, result);
    }
}