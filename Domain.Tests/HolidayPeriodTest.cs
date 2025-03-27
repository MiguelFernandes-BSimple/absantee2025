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
}