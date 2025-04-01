namespace Domain.Tests.HolidayPeriodTests;

using Domain;
using Xunit;
using System;
using System.Collections.Generic;

public class Contains
{
    public static IEnumerable<object[]> ContainedPeriods()
    {
        yield return new object[] { new HolidayPeriod(new DateOnly(2024, 6, 1), new DateOnly(2024, 6, 10)) };
        yield return new object[] { new HolidayPeriod(new DateOnly(2024, 6, 3), new DateOnly(2024, 6, 6)) };
    }

    [Theory]
    [MemberData(nameof(ContainedPeriods))]
    public void WhenPeriodIsFullyContained_ThenReturnsTrue(IHolidayPeriod containedPeriod)
    {
        var referencePeriod = new HolidayPeriod(new DateOnly(2024, 6, 1), new DateOnly(2024, 6, 10));
        var result = referencePeriod.Contains(containedPeriod);
        Assert.True(result);
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