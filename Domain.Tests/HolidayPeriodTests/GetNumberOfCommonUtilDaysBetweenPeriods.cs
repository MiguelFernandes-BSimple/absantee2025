namespace Domain.Tests.HolidayPeriodTests;

using Domain.Models;
using Xunit;
using System;
using System.Collections.Generic;

public class GetNumberOfCommonUtilDaysBetweenPeriods
{
    public static IEnumerable<object[]> GetCommonUtilDaysData()
    {
        yield return new object[] { new DateOnly(2020, 6, 1), new DateOnly(2020, 7, 1), 11 };
    }

    [Theory]
    [MemberData(nameof(GetCommonUtilDaysData))]
    public void WhenCalculatingTheNumberOfCommonUtilDaysBetweenPeriods_ThenCorrectNumberIsReturned(DateOnly initDate, DateOnly endDate, int expectedDays)
    {
        var hp = new HolidayPeriod(new DateOnly(2020, 6, 1), new DateOnly(2020, 6, 15));
        int numberOfDays = hp.GetNumberOfCommonUtilDaysBetweenPeriods(initDate, endDate);
        Assert.Equal(expectedDays, numberOfDays);
    }
}