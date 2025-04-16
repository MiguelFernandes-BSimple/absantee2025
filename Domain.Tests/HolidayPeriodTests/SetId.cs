
namespace Domain.Tests.HolidayPeriodTests;

using Domain.Interfaces;
using Domain.Models;
using Xunit;
using Moq;

public class SetId{

    [Fact]
    public void WhenSettingId_ThenGetIdReturnsNewValue()
    {
        // Arrange
        var initialId = 1L;
        var newId = 2L;
        var mockPeriodDate = new Mock<IPeriodDate>();
        var holidayPlan = new HolidayPeriod(initialId, mockPeriodDate.Object);

        // Act
        holidayPlan.SetId(newId);
        var result = holidayPlan.GetId();

        // Assert
        Assert.Equal(newId, result);
    }
}