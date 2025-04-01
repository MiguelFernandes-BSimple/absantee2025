namespace Domain.Tests.HolidayPeriodTests;

using Domain.Models;
using Xunit;
using System;

public class GetDurationInDays
{
    [Fact]
    public void GetDurationInDays_FullOverlap_ReturnsFullDuration()
    {
        var holidayPeriod = new HolidayPeriod(new DateOnly(2024, 6, 1), new DateOnly(2024, 6, 10));
        int duration = holidayPeriod.GetDurationInDays(new DateOnly(2024, 6, 1), new DateOnly(2024, 6, 10));
        Assert.Equal(10, duration);
    }


    [Fact]
    public void GetDurationInDays_PartialOverlap_StartInside_ReturnsCorrectDays()
    {
        //Arrange
        var holidayPeriod = new HolidayPeriod(new DateOnly(2024, 6, 5), new DateOnly(2024, 6, 15));
        //Act
        int duration = holidayPeriod.GetDurationInDays(new DateOnly(2024, 6, 1), new DateOnly(2024, 6, 10));
        //Assert
        Assert.Equal(6, duration); // 5 a 10
    }
}