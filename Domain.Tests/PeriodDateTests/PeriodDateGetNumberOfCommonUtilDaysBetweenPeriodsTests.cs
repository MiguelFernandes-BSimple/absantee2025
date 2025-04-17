namespace Domain.Tests.PeriodDateTests;

using Domain.Models;
using Xunit;
using Moq;
using Domain.Interfaces;

public class PeriodDateGetNumberOfCommonUtilDaysBetweenPeriodsTests
{
    /**
    * Test method to get the number of weekdays in the intersecton between two period Dates
    * Happy Path - They intersect
    */
    public static IEnumerable<object[]> GetDatesAndWeekdays()
    {
        yield return new object[] { new DateOnly(2025, 3, 31), new DateOnly(2025, 4, 4), 5 };
        yield return new object[] { new DateOnly(2025, 4, 5), new DateOnly(2025, 4, 6), 0 };
    }

    [Theory]
    [MemberData(nameof(GetDatesAndWeekdays))]
    public void WhenPassingIntersectingPeriod_ThenNumberOfWeekdaysIsReturned(DateOnly dateInit, DateOnly finalDate, int expectedWeekdays)
    {
        // Arrange
        PeriodDate periodDate = new PeriodDate(dateInit, finalDate);

        // Act
        int result = periodDate.GetNumberOfCommonUtilDaysBetweenPeriods(periodDate);

        // Assert
        Assert.Equal(expectedWeekdays, result);
    }
}