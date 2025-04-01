using Domain;

namespace Domain.Tests.UtilsTest;

public class ContainsWeekend
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
    public void WhenPassingPeriodThatContainsWeekend_ThenReturnTrue(DateOnly iniDate, DateOnly endDate)
    {
        //arrange

        //act
        bool result = Utils.ContainsWeekend(iniDate, endDate);

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
    public void WhenPassingPeriodThatDontContainWeekend_ThenReturnFalse(DateOnly iniDate, DateOnly endDate)
    {
        //arrange

        //act
        bool result = Utils.ContainsWeekend(iniDate, endDate);

        //assert
        Assert.False(result);
    }

    public static IEnumerable<object[]> InvalidDates()
    {
        yield return new object[] { new DateOnly(2025, 04, 02), new DateOnly(2025, 04, 01) };
        yield return new object[] { new DateOnly(2025, 04, 10), new DateOnly(2025, 04, 05) };
    }

    [Theory]
    [MemberData(nameof(InvalidDates))]
    public void WhenPassingInvalidDates_ThenReturnFalse(DateOnly iniDate, DateOnly endDate)
    {
        //arrange

        //act
        bool result = Utils.ContainsWeekend(iniDate, endDate);

        //assert
        Assert.False(result);
    }
}