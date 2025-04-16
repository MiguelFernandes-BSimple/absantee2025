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
        var mockPeriod = new Mock<IPeriodDate>();
        var searchingPeriod = new Mock<IPeriodDate>();

        mockPeriod.Setup(p => p.GetNumberOfCommonUtilDaysBetweenPeriods(searchingPeriod.Object)).Returns(5);

        IHolidayPeriod holidayPeriod = new HolidayPeriod(mockPeriod.Object);

        // Act
        int result = holidayPeriod.GetNumberOfCommonUtilDaysBetweenPeriods(searchingPeriod.Object);

        // Assert
        Assert.Equal(5, result);
    }
}