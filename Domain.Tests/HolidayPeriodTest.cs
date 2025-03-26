namespace Domain.Tests;

using Domain;

public class HolidayPeriodTest
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
        //arrange

        //act
        new HolidayPeriod(ini, end);

        //assert
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
        //arrange

        //assert
        ArgumentException exception = Assert.Throws<ArgumentException>(() =>
            //act
            new HolidayPeriod(ini, end));

        Assert.Equal("Invalid Arguments", exception.Message);
    }

    public static IEnumerable<object[]> GetHolidayPeriodData_Overlap()
    {
        yield return new object[] { DateOnly.FromDateTime(new DateTime(2020, 1, 1)), DateOnly.FromDateTime(new DateTime(2021, 1, 1)), true };
        yield return new object[] { DateOnly.FromDateTime(new DateTime(2020, 1, 2)), DateOnly.FromDateTime(new DateTime(2020, 12, 31)), true };
        yield return new object[] { DateOnly.FromDateTime(new DateTime(2019, 12, 31)), DateOnly.FromDateTime(new DateTime(2021, 1, 1)), false };
        yield return new object[] { DateOnly.FromDateTime(new DateTime(2020, 1, 2)), DateOnly.FromDateTime(new DateTime(2021, 1, 2)), false };
        yield return new object[] { DateOnly.FromDateTime(new DateTime(2019, 12, 31)), DateOnly.FromDateTime(new DateTime(2021, 1, 2)), false };
    }

    [Theory]
    [MemberData(nameof(GetHolidayPeriodData_Overlap))]
    public void WhenTimePeriodIsGiven_ThenOverlapIsEvaluated(DateOnly ini, DateOnly end, bool expected)
    {
        //arrange
        DateOnly _ini = DateOnly.FromDateTime(new DateTime(2020, 1, 1));
        DateOnly _end = DateOnly.FromDateTime(new DateTime(2021, 1, 1));
        HolidayPeriod _pf = new HolidayPeriod(_ini, _end);
        HolidayPeriod pf = new HolidayPeriod(ini, end);

        //act
        bool result = _pf.HolidayPeriodOverlap(pf);

        //assert
        Assert.Equal(expected, result);
    }

    public static IEnumerable<object[]> GetNumberOfCommonDaysBetweenPeriods()
    {
        yield return new object[] { DateOnly.FromDateTime(new DateTime(2020, 6, 1)), DateOnly.FromDateTime(new DateTime(2020, 7, 1)), 15 };
        yield return new object[] { DateOnly.FromDateTime(new DateTime(2020, 6, 3)), DateOnly.FromDateTime(new DateTime(2020, 6, 9)), 7 };
        yield return new object[] { DateOnly.FromDateTime(new DateTime(2020, 4, 1)), DateOnly.FromDateTime(new DateTime(2020, 5, 1)), 0 };
        yield return new object[] { DateOnly.FromDateTime(new DateTime(2020, 6, 1)), DateOnly.FromDateTime(new DateTime(2020, 6, 10)), 10 };
        yield return new object[] { DateOnly.FromDateTime(new DateTime(2020, 6, 5)), DateOnly.FromDateTime(new DateTime(2020, 6, 15)), 11 };
        yield return new object[] { DateOnly.FromDateTime(new DateTime(2020, 6, 10)), DateOnly.FromDateTime(new DateTime(2020, 6, 10)), 1 };
        yield return new object[] { DateOnly.FromDateTime(new DateTime(2020, 6, 16)), DateOnly.FromDateTime(new DateTime(2020, 6, 20)), 0 };

    }

    [Theory]
    [MemberData(nameof(GetNumberOfCommonDaysBetweenPeriods))]
    public void WhenCalculatingTheNumberOfCommonDaysBetweenPeriods_ThenCorrectNumberIsReturned(DateOnly initDate, DateOnly endDate, int expectedDays)
    {

        //arrange
        DateOnly _ini = DateOnly.FromDateTime(new DateTime(2020, 6, 1));
        DateOnly _end = DateOnly.FromDateTime(new DateTime(2020, 6, 15));
        HolidayPeriod hp = new HolidayPeriod(_ini, _end);

        //act
        int numberOfDays = hp.GetNumberOfCommonDaysBetweenPeriods(initDate, endDate);

        //assert
        Assert.Equal(expectedDays, numberOfDays);
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