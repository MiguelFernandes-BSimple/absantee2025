using Domain.Models;
using Moq;
using Domain.Interfaces;

namespace Domain.Tests.HolidayPeriodTests;

public class HolidayPeriodGetNumberOfCommonUtilDaysTests
{

    [Fact]
    public void WhenPassingPeriod_ThenNumberOfWeekdaysIsReturned()
    {
        // Arrange
        var mockPeriod = new Mock<IPeriodDate>();

        mockPeriod.Setup(p => p.GetNumberOfCommonUtilDays()).Returns(5);

        IHolidayPeriod holidayPeriod = new HolidayPeriod(mockPeriod.Object);

        // Act
        int result = holidayPeriod.GetNumberOfCommonUtilDays();

        // Assert
        Assert.Equal(5, result);
    }
}