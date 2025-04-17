using Domain.Models;
using Moq;
using Domain.Interfaces;

namespace Domain.Tests.HolidayPeriodTests;

public class HolidayPeriodGetNumberOfCommonUtilDaysBetweenPeriodsTests
{

    [Fact]
    public void WhenPassingIntersectingPeriod_ThenNumberOfWeekdaysIsReturned()
    {
        // Arrange
        var mockPeriod = new Mock<PeriodDate>();
        var searchingPeriod = new Mock<PeriodDate>();

        mockPeriod.Setup(p => p.GetNumberOfCommonUtilDaysBetweenPeriods(searchingPeriod.Object)).Returns(5);

        IHolidayPeriod holidayPeriod = new HolidayPeriod(mockPeriod.Object);

        // Act
        int result = holidayPeriod.GetNumberOfCommonUtilDaysBetweenPeriods(searchingPeriod.Object);

        // Assert
        Assert.Equal(5, result);
    }
}