using Domain.Interfaces;
using Domain.Models;
using Moq;

namespace Domain.Tests.HolidayPeriodTests;

public class HolidayPeriodConstructorTests
{

    [Fact]
    public void WhenConstructorIsCalled_ThenObjectIsInstantiated()
    {
         // Arrange 
        var start = DateOnly.FromDateTime(DateTime.Now);
        var end = start.AddDays(5);
        var periodDate = new PeriodDate(start, end);

        // Act
        var result = new HolidayPeriod(periodDate);

        // Assert
        Assert.NotNull(result);
    }

}