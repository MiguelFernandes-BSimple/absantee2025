namespace Domain.Tests.HolidayPeriodTests;

using Domain;
using Xunit;
using System;
using System.Collections.Generic;

public class ContainedBetween
{
    public static IEnumerable<object[]> GetHolidayPeriodData_ContaininedBetween()
    {
        yield return new object[] { new DateOnly(2020, 1, 2), new DateOnly(2020, 1, 3), new DateOnly(2020, 1, 1), new DateOnly(2020, 1, 5), true };
        yield return new object[] { new DateOnly(2020, 1, 1), new DateOnly(2020, 1, 1), new DateOnly(2020, 1, 1), new DateOnly(2020, 1, 1), true };
        yield return new object[] { new DateOnly(2020, 4, 2), new DateOnly(2020, 4, 3), new DateOnly(2020, 1, 1), new DateOnly(2020, 1, 5), false };
    }

    [Theory]
    [MemberData(nameof(GetHolidayPeriodData_ContaininedBetween))]
    public void WhenGivenDates_ThenEvaluateIfContainedBetween(DateOnly ini, DateOnly end, DateOnly containsIni, DateOnly containsEnd, bool ret)
    {
        var holidayPeriod = new HolidayPeriod(ini, end);
        var result = holidayPeriod.ContainedBetween(containsIni, containsEnd);
        Assert.Equal(ret, result);
    }
}