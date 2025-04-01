namespace Domain.Tests.HolidayPeriodTests;

using Domain.Models;
using Xunit;
using System;
using System.Collections.Generic;


public class ContainsDate
{
    public static IEnumerable<object[]> GetHolidayPeriod_ContainingDate()
    {
        yield return new object[] { new DateOnly(2020, 1, 1), new DateOnly(2020, 1, 5), new DateOnly(2020, 1, 3), true };
    }

    [Theory]
    [MemberData(nameof(GetHolidayPeriod_ContainingDate))]
    public void WhenGivenDate_ThenEvaluateIfContains(DateOnly ini, DateOnly end, DateOnly date, bool ret)
    {
        var holidayPeriod = new HolidayPeriod(ini, end);
        var result = holidayPeriod.ContainsDate(date);
        Assert.Equal(ret, result);
    }
}