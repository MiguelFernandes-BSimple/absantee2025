namespace Domain.Tests.HolidayPeriodTests;

using Domain;
using Xunit;
using System;
using System.Collections.Generic;


public class IsLongerThan{

    [Theory]
    [InlineData(0)]
    [InlineData(10)]
    public void WhenPeriodDurationIsGreaterThanLimit_ThenShouldReturnTrue(int days)
    {
        //arrange
        DateOnly initDate = new DateOnly(2020, 6, 1);
        DateOnly finalDate = new DateOnly(2020, 6, 15);
        IHolidayPeriod holidayPeriod = new HolidayPeriod(initDate, finalDate);

        //act
        bool result = holidayPeriod.IsLongerThan(days);

        //assert
        Assert.True(result);
    }

    [Theory]
    [InlineData(15)]
    [InlineData(20)]
    public void WhenPeriodDurationIsLessOrEqualThanLimit_ThenShouldReturnFalse(int days)
    {
        //arrange
        DateOnly initDate = new DateOnly(2020, 6, 1);
        DateOnly finalDate = new DateOnly(2020, 6, 15);
        IHolidayPeriod holidayPeriod = new HolidayPeriod(initDate, finalDate);

        //act
        bool result = holidayPeriod.IsLongerThan(days);

        //assert
        Assert.False(result);
    }
}