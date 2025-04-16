using Domain.Interfaces;
using Domain.Models;
using Moq;

namespace Domain.Tests.HolidayPeriodTests;

public class HolidayPeriodConstructorTests
{
    /**
    * Test method for HolidayPeriod constructor.
    * It can only be instantiated with a IPeriodDate (which is valid)
    */
    [Fact]
    public void WhenConstructorIsCalled_ThenObjectIsInstantiated()
    {
        // Arrange 
        Mock<IPeriodDate> doublePeriodDate = new Mock<IPeriodDate>();

        // Act
        var result = new HolidayPeriod(doublePeriodDate.Object);

        // Assert
        Assert.NotNull(result);
    }

}