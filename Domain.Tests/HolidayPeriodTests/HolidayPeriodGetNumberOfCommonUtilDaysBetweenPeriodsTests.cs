using Domain.Models;
using Moq;
using Domain.Interfaces;

namespace Domain.Tests.HolidayPeriodTests;

public class HolidayPeriodGetNumberOfCommonUtilDaysBetweenPeriodsTests
{

    [Fact]
    public void WhenPassingIntersectingPeriod_ThenNumberOfWeekdaysIsReturned()
    {
        // Arrange
        var startReference = new DateOnly(2024, 4, 1);
        var endReference = new DateOnly(2024, 4, 20); // Reference period (20 days)
        var startInputed = new DateOnly(2024, 4, 10);
        var endInputed = new DateOnly(2024, 4, 25); // Inputed period (15 days)

        var period = new PeriodDate(startReference,endReference);
        var searchingPeriod = new PeriodDate(startInputed,endInputed);


        IHolidayPeriod holidayPeriod = new HolidayPeriod(period);

        // Act
        int result = holidayPeriod.GetNumberOfCommonUtilDaysBetweenPeriods(searchingPeriod);

        // Assert
        Assert.Equal(8, result);
    }
}