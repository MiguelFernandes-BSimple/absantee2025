namespace Domain.Tests.HolidayPeriodTests;

using Domain.Models;
using Xunit;
using Moq;
using Domain.Interfaces;

public class GetNumberOfCommonUtilDays
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