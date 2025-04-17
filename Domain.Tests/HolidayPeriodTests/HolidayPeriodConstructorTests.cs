using Domain.Interfaces;
using Domain.Models;
using Moq;

namespace Domain.Tests.HolidayPeriodTests;

public class HolidayPeriodConstructorTests
{
    /**
    * Test method for HolidayPeriod constructor.
    * It can only be instantiated with a PeriodDate (which is valid)
    */
    [Fact]
    public void WhenConstructorIsCalled_ThenObjectIsInstantiated()
    {
        // Arrange 
        Mock<PeriodDate> doublePeriodDate = new Mock<PeriodDate>();

        // Act
        var result = new HolidayPeriod(doublePeriodDate.Object);

        // Assert
        Assert.NotNull(result);
    }

}