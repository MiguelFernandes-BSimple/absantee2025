using Domain.Interfaces;
using Domain.Models;
using Moq;

namespace Domain.Tests.HolidayPeriodTests;

public class HolidayPeriodGetIdTests
{

    [Fact]
    public void WhenGettingId_ThenReturnsTheId()
    {

        var expected = new Guid();

        // Arrange

        var start = new DateOnly(2024, 4, 10);
        var end = new DateOnly(2024, 4, 15);
        var periodDate = new PeriodDate(start, end);


        var holidayPlan = new HolidayPeriod(expected, periodDate);

        // Act
        var result = holidayPlan.Id;

        // Assert

        Assert.Equal(expected, result);

    }






}