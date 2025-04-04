namespace Domain.Tests.HolidayPeriodTests;

using Domain.Models;
using Xunit;
using Moq;
using Domain.Interfaces;

public class GetNumberOfCommonUtilDaysBetweenPeriods
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