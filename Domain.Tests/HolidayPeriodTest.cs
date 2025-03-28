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
        bool result = _pf.Contains(pf);

        //assert
        Assert.Equal(expected, result);
    }

    public class HolidayPeriodTests
    {
        [Fact]
        public void GetDurationInDays_FullOverlap_ReturnsFullDuration()
        {
            //Arrange
            var holidayPeriod = new HolidayPeriod(new DateOnly(2024, 6, 1), new DateOnly(2024, 6, 10));

            //Act
            int duration = holidayPeriod.GetDurationInDays(new DateOnly(2024, 6, 1), new DateOnly(2024, 6, 10));

            //Assert
            Assert.Equal(10, duration);
        }

        [Fact]
        public void GetDurationInDays_PartialOverlap_StartInside_ReturnsCorrectDays()
        {
            //Arrange
            var holidayPeriod = new HolidayPeriod(new DateOnly(2024, 6, 5), new DateOnly(2024, 6, 15));
            //Act
            int duration = holidayPeriod.GetDurationInDays(new DateOnly(2024, 6, 1), new DateOnly(2024, 6, 10));
            //Assert
            Assert.Equal(6, duration); // 5 a 10
        }

        public static IEnumerable<object[]> ContainedPeriods()
        {
            yield return new object[] { new HolidayPeriod(new DateOnly(2024, 6, 1), new DateOnly(2024, 6, 10)) };
            yield return new object[] { new HolidayPeriod(new DateOnly(2024, 6, 3), new DateOnly(2024, 6, 6)) };
        }

        [Theory]
        [MemberData(nameof(ContainedPeriods))]
        public void WhenPeriodIsFullyContained_ThenReturnsTrue(IHolidayPeriod containedPeriod)
        {
            // Arrange
            var referencePeriod = new HolidayPeriod(new DateOnly(2024, 6, 1), new DateOnly(2024, 6, 10));

            // Act
            var result = referencePeriod.Contains(containedPeriod);

            // Assert
            Assert.True(result); // Expected: True
        }

        public static IEnumerable<object[]> NotContainedPeriods()
        {
            yield return new object[] { new HolidayPeriod(new DateOnly(2024, 5, 1), new DateOnly(2024, 6, 10)) };
            yield return new object[] { new HolidayPeriod(new DateOnly(2024, 6, 3), new DateOnly(2024, 7, 6)) };
            yield return new object[] { new HolidayPeriod(new DateOnly(2024, 5, 1), new DateOnly(2024, 7, 6)) };
        }

        [Theory]
        [MemberData(nameof(NotContainedPeriods))]
        public void WhenPeriodIsNotFullyContained_ThenReturnsFalse(IHolidayPeriod nonContainedPeriod)
        {
            // Arrange
            var referencePeriod = new HolidayPeriod(new DateOnly(2024, 6, 1), new DateOnly(2024, 6, 5));

            // Act
            var result = referencePeriod.Contains(nonContainedPeriod);

            // Assert
            Assert.False(result);
        }
    }

    public static IEnumerable<object[]> GetHolidayPeriod_ContainingDate()
    {
        yield return new object[] { new DateOnly(2020, 1, 1), new DateOnly(2020, 1, 5), new DateOnly(2020, 1, 3), true };
        yield return new object[] { new DateOnly(2020, 4, 1), new DateOnly(2020, 4, 5), new DateOnly(2020, 1, 3), false };
        yield return new object[] { new DateOnly(2020, 1, 1), new DateOnly(2020, 1, 1), new DateOnly(2020, 1, 1), true };
        yield return new object[] { new DateOnly(2020, 1, 1), new DateOnly(2020, 1, 3), new DateOnly(2020, 1, 3), true };
    }

    [Theory]
    [MemberData(nameof(GetHolidayPeriod_ContainingDate))]
    public void WhenGivenDate_ThenEvaluateIfContains(DateOnly ini, DateOnly end, DateOnly date, bool ret)
    {
        //arrange
        var holidayPeriod = new HolidayPeriod(ini, end);

        //act
        var result = holidayPeriod.ContainsDate(date);

        //assert
        Assert.Equal(ret, result);
    }

    public static IEnumerable<object[]> GetHolidayPeriod_ContaininedBetween()
    {
        yield return new object[] { new DateOnly(2020, 1, 2), new DateOnly(2020, 1, 3), new DateOnly(2020, 1, 1), new DateOnly(2020, 1, 5), true };
        yield return new object[] { new DateOnly(2020, 1, 1), new DateOnly(2020, 1, 1), new DateOnly(2020, 1, 1), new DateOnly(2020, 1, 1), true };
        yield return new object[] { new DateOnly(2020, 4, 2), new DateOnly(2020, 4, 3), new DateOnly(2020, 1, 1), new DateOnly(2020, 1, 5), false };
    }

    [Theory]
    [MemberData(nameof(GetHolidayPeriod_ContaininedBetween))]
    public void WhenGivenDates_ThenEvaluateIfContainedBetween(DateOnly ini, DateOnly end, DateOnly containsIni, DateOnly containsEnd, bool ret)
    {
        //arrange
        var holidayPeriod = new HolidayPeriod(ini, end);

        //act
        var result = holidayPeriod.ContainedBetween(containsIni, containsEnd);

        //assert
        Assert.Equal(ret, result);
    }

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

    public static IEnumerable<object[]> GetNumberOfCommonUtilDaysBetweenPeriods()
    {
        yield return new object[] { DateOnly.FromDateTime(new DateTime(2020, 6, 1)), DateOnly.FromDateTime(new DateTime(2020, 7, 1)), 11 };
        yield return new object[] { DateOnly.FromDateTime(new DateTime(2020, 6, 3)), DateOnly.FromDateTime(new DateTime(2020, 6, 9)), 5 };
        yield return new object[] { DateOnly.FromDateTime(new DateTime(2020, 4, 1)), DateOnly.FromDateTime(new DateTime(2020, 5, 1)), 0 };
        yield return new object[] { DateOnly.FromDateTime(new DateTime(2020, 6, 1)), DateOnly.FromDateTime(new DateTime(2020, 6, 10)), 8 };
        yield return new object[] { DateOnly.FromDateTime(new DateTime(2020, 6, 5)), DateOnly.FromDateTime(new DateTime(2020, 6, 15)), 7 };
        yield return new object[] { DateOnly.FromDateTime(new DateTime(2020, 6, 10)), DateOnly.FromDateTime(new DateTime(2020, 6, 10)), 1 };
        yield return new object[] { DateOnly.FromDateTime(new DateTime(2020, 6, 16)), DateOnly.FromDateTime(new DateTime(2020, 6, 20)), 0 };
        yield return new object[] { DateOnly.FromDateTime(new DateTime(2020, 6, 6)), DateOnly.FromDateTime(new DateTime(2020, 6, 7)), 0 };


    }

    [Theory]
    [MemberData(nameof(GetNumberOfCommonUtilDaysBetweenPeriods))]
    public void WhenCalculatingTheNumberOfCommonUtilDaysBetweenPeriods_ThenCorrectNumberIsReturned(DateOnly initDate, DateOnly endDate, int expectedDays)
    {

        //arrange
        DateOnly _ini = DateOnly.FromDateTime(new DateTime(2020, 6, 1));
        DateOnly _end = DateOnly.FromDateTime(new DateTime(2020, 6, 15));
        HolidayPeriod hp = new HolidayPeriod(_ini, _end);

        //act
        int numberOfDays = hp.GetNumberOfCommonUtilDaysBetweenPeriods(initDate, endDate);

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
