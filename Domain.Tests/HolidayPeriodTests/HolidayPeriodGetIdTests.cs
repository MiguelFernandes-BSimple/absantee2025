using Domain.Interfaces;
using Domain.Models;
using Moq;

namespace Domain.Tests.HolidayPeriodTests;

public class HolidayPeriodGetIdTests
{

    [Fact]
    public void WhenGettingId_ThenReturnsTheId()
    {

        var expected = 2;


        // Arrange

        var mockPeriodDate = new Mock<PeriodDate>();


        var holidayPlan = new HolidayPeriod(expected, mockPeriodDate.Object);

        // Act
        var result = holidayPlan.GetId();

        // Assert

        Assert.Equal(expected, result);

    }






}