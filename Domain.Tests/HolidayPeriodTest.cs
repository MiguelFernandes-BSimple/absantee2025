namespace Domain.Tests;

using Domain;

public class HolidayPeriodTest {
    public static IEnumerable<object[]> GetHolidayPeriodData_ValidFields()
    {
        yield return new object[] { DateOnly.FromDateTime(DateTime.Now), DateOnly.FromDateTime(DateTime.Now.AddDays(1)) };
        yield return new object[] { DateOnly.FromDateTime(DateTime.Now.AddYears(1)), DateOnly.FromDateTime(DateTime.Now.AddYears(2)) };
        yield return new object[] { DateOnly.FromDateTime(DateTime.Now), DateOnly.FromDateTime(DateTime.Now) };
    }

    [Theory]
    [MemberData(nameof(GetHolidayPeriodData_ValidFields))]
    public void WhenGivenGoodFields_ThenObjectIsInstantized(DateOnly ini, DateOnly end){
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
    public void WhenGivenBadDateFields_ThenExceptionIsThrown(DateOnly ini, DateOnly end){
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
    public void WhenTimePeriodIsGiven_ThenOverlapIsEvaluated(DateOnly ini, DateOnly end, bool expected){
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
}