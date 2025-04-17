using Domain.Models;

namespace Domain.Tests.PeriodDateTests;

public class PeriodDateContainsWeekendTests
{
    public static IEnumerable<object[]> DatesThatContainWeekend()
    {
        yield return new object[] { new DateOnly(2025, 04, 04), new DateOnly(2025, 04, 11) };
        yield return new object[] { new DateOnly(2025, 04, 04), new DateOnly(2025, 04, 05) };
        yield return new object[] { new DateOnly(2025, 04, 04), new DateOnly(2025, 04, 06) };
        yield return new object[] { new DateOnly(2025, 04, 05), new DateOnly(2025, 04, 06) };
        yield return new object[] { new DateOnly(2025, 04, 05), new DateOnly(2025, 04, 05) };
    }

    [Theory]
    [MemberData(nameof(DatesThatContainWeekend))]
    public void WhenPassingPeriodThatContainsWeekend_ThenReturnTrue(DateOnly iniDate, DateOnly finalDate)
    {
        //arrange
        PeriodDate hp = new PeriodDate(iniDate, finalDate);
        //act
        bool result = hp.ContainsWeekend();

        //assert
        Assert.True(result);
    }

    public static IEnumerable<object[]> DatesThatDontContainWeekend()
    {
        yield return new object[] { new DateOnly(2025, 04, 01), new DateOnly(2025, 04, 04) };
        yield return new object[] { new DateOnly(2025, 04, 01), new DateOnly(2025, 04, 01) };
    }

    [Theory]
    [MemberData(nameof(DatesThatDontContainWeekend))]
    public void WhenPassingPeriodThatDontContainWeekend_ThenReturnFalse(DateOnly iniDate, DateOnly finalDate)
    {
        //arrange
        PeriodDate hp = new PeriodDate(iniDate, finalDate);

        //act
        bool result = hp.ContainsWeekend();

        //assert
        Assert.False(result);
    }
}