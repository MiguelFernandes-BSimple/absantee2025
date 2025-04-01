namespace Domain.Tests.HolidayPeriodTests;

using Domain;
using Xunit;
using System;
using System.Collections.Generic;


public class Constructor
{
    public static IEnumerable<object[]> GetHolidayPeriodData_ValidFields()
    {
        yield return new object[] { DateOnly.FromDateTime(DateTime.Now), DateOnly.FromDateTime(DateTime.Now.AddDays(1)) };
        yield return new object[] { DateOnly.FromDateTime(DateTime.Now.AddYears(1)), DateOnly.FromDateTime(DateTime.Now.AddYears(2)) };
        yield return new object[] { DateOnly.FromDateTime(DateTime.Now), DateOnly.FromDateTime(DateTime.Now) };
    }

    [Theory]
    [MemberData(nameof(GetHolidayPeriodData_ValidFields))]
    public void WhenGivenGoodFields_ThenObjectIsInstantized(DateOnly ini, DateOnly end)
    {
        new HolidayPeriod(ini, end);
    }

    public static IEnumerable<object[]> GetHolidayPeriodData_InvalidFields()
    {
        yield return new object[] { DateOnly.FromDateTime(DateTime.Now).AddDays(5), DateOnly.FromDateTime(DateTime.Now.AddDays(1)) };
        yield return new object[] { DateOnly.FromDateTime(DateTime.Now).AddYears(-1), DateOnly.FromDateTime(DateTime.Now.AddYears(-3)) };
    }

    [Theory]
    [MemberData(nameof(GetHolidayPeriodData_InvalidFields))]
    public void WhenGivenBadDateFields_ThenExceptionIsThrown(DateOnly ini, DateOnly end)
    {
        Assert.Throws<ArgumentException>(() => new HolidayPeriod(ini, end));
    }
}