using Domain.Models;
using Moq;
using Domain.Interfaces;

namespace Domain.Tests.HolidayPeriodTests;

public class HolidayPeriodGetNumberOfCommonUtilDaysTests
{

    [Fact]
    public void WhenPassingPeriod_ThenNumberOfWeekdaysIsReturned()
    {
        // Arrange
        

        var startReference = new DateOnly(2024, 4, 1);
        var endReference = new DateOnly(2024, 4, 20); // Reference period (20 days)
       

        var period = new PeriodDate(startReference,endReference);


        IHolidayPeriod holidayPeriod = new HolidayPeriod(period);

        // Act
        int result = holidayPeriod.GetNumberOfCommonUtilDays();

        // Assert
        Assert.Equal(15, result);
    }
}