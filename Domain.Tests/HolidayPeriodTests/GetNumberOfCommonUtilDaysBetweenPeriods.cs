namespace Domain.Tests.HolidayPeriodTests;

using Domain.Models;
using Xunit;
using Moq;
using Domain.Interfaces;

public class GetNumberOfCommonUtilDaysBetweenPeriods
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
    public void WhenPassingIntersectingPeriod_ThenNumberOfWeekdaysIsReturned(DateOnly intersectionInitDate, DateOnly intersectionEndDate, int expectedWeekdays)
    {
        // Arrange
        // doubles for IPeriodDates - stubs
        Mock<IPeriodDate> doublePeriodDateReference = new Mock<IPeriodDate>();
        Mock<IPeriodDate> doublePeriodDateInput = new Mock<IPeriodDate>();
        Mock<IPeriodDate> doublePeriodDateIntersection = new Mock<IPeriodDate>();

        // Establish the intersetion between the reference and input periodDates
        doublePeriodDateReference.Setup(pd => pd.GetIntersection(doublePeriodDateInput.Object))
                                 .Returns(doublePeriodDateIntersection.Object);

        // Establish init and end dates for intersection
        doublePeriodDateIntersection.Setup(pd => pd.GetInitDate()).Returns(intersectionInitDate);
        doublePeriodDateIntersection.Setup(pd => pd.GetFinalDate()).Returns(intersectionEndDate);

        // Instatiate HolidayPeriod
        HolidayPeriod holidayPeriod = new HolidayPeriod(doublePeriodDateReference.Object);

        // Act
        int result = holidayPeriod.GetNumberOfCommonUtilDaysBetweenPeriods(doublePeriodDateInput.Object);

        // Assert
        Assert.Equal(expectedWeekdays, result);
    }

    /**
    * Test method to get the number of weekdays in the intersecton between two period Dates
    * They don't intersect
    */
    [Fact]
    public void WhenPassingNotIntersectingPeriod_ThenReturnZero()
    {
        // Arrange
        // doubles for IPeriodDates - stubs
        Mock<IPeriodDate> doublePeriodDateReference = new Mock<IPeriodDate>();
        Mock<IPeriodDate> doublePeriodDateInput = new Mock<IPeriodDate>();

        // Establish that the two periodDates dont intersect
        // Retunrs null
        doublePeriodDateReference.Setup(pd => pd.GetIntersection(doublePeriodDateInput.Object))
                                 .Returns((IPeriodDate?)null);

        // Zero days if they don't intersect
        int expected = 0;

        // Instatiate HolidayPeriod
        HolidayPeriod holidayPeriod = new HolidayPeriod(doublePeriodDateReference.Object);

        // Act
        int result = holidayPeriod.GetNumberOfCommonUtilDaysBetweenPeriods(doublePeriodDateInput.Object);

        // Assert
        Assert.Equal(expected, result);
    }
}