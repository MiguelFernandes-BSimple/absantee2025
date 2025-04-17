using Domain.Interfaces;
using Domain.Models;
using Moq;

namespace Domain.Tests.HolidayPeriodTests;

public class HolidayPeriodContainsWeekendTests
{
    [Fact]
    public void WhenPassingPeriodThatContainsWeekend_ThenReturnTrue()
    {
        // Arrange
        // Sexta a Segunda → contém sábado e domingo
        var start = new DateOnly(2024, 4, 12); // Sexta
        var end = new DateOnly(2024, 4, 15);   // Segunda

        var periodDate = new PeriodDate(start, end);
        var holidayPlan = new HolidayPeriod(periodDate);

        // Act
        bool result = holidayPlan.ContainsWeekend();

        // Assert
        Assert.True(result);
    }


    [Fact]
    public void WhenPassingPeriodThatDontContainWeekend_ThenReturnFalse()
    {
        // Arrange
        // Segunda a Sexta → não contém sábado nem domingo
        var start = new DateOnly(2024, 4, 8); // Segunda
        var end = new DateOnly(2024, 4, 12);  // Sexta

        var periodDate = new PeriodDate(start, end);
        var holidayPlan = new HolidayPeriod(periodDate);

        // Act
        bool result = holidayPlan.ContainsWeekend();

        // Assert
        Assert.False(result);
    }

}