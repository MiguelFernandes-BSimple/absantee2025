namespace Domain.Tests.HolidayPeriodTests;

using Domain.Interfaces;
using Domain.Models;
using Xunit;
using System;
using System.Collections.Generic;


public class GetDuration
{
    public static IEnumerable<object[]> GetHolidayPeriod_OfLength()
    {
        yield return new object[] { DateOnly.FromDateTime(new DateTime(2020, 1, 1)), DateOnly.FromDateTime(new DateTime(2020, 1, 1)), 1 };
        yield return new object[] { DateOnly.FromDateTime(new DateTime(2020, 1, 1)), DateOnly.FromDateTime(new DateTime(2020, 1, 3)), 3 };
        yield return new object[] { DateOnly.FromDateTime(new DateTime(2020, 1, 1)), DateOnly.FromDateTime(new DateTime(2020, 1, 5)), 5 };
    }

    [Theory]
    [MemberData(nameof(GetHolidayPeriod_OfLength))]
    public void WhenGivenGoodPeriod_ThenReturnLength(DateOnly ini, DateOnly end, int len)
    {
        //arrange
        var holidayPeriod = new HolidayPeriod(ini, end);

        //act
        var result = holidayPeriod.GetDuration();

        //assert
        Assert.Equal(len, result);
    }

    public static IEnumerable<object[]> GetPeriodDuration()
    {
        yield return new object[] { new DateOnly(2025, 6, 1), new DateOnly(2025, 6, 1), 1 };
        yield return new object[] { new DateOnly(2025, 6, 1), new DateOnly(2025, 6, 5), 5 };
        yield return new object[] { new DateOnly(2025, 6, 1), new DateOnly(2025, 7, 20), 50 };
    }

    [Theory]
    [MemberData(nameof(GetPeriodDuration))]
    public void WhenCalculatingDurationOfPeriod_ThenResultShouldBeCorrect(DateOnly initDate, DateOnly finalDate, int expectedResult)
    {
        //arrange
        IHolidayPeriod holidayPeriod = new HolidayPeriod(initDate, finalDate);

        //act
        int duration = holidayPeriod.GetDuration();

        //assert
        Assert.Equal(expectedResult, duration);
    }

}