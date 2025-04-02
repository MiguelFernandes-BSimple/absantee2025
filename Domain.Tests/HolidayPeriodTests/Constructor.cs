namespace Domain.Tests.HolidayPeriodTests;

using Domain.Interfaces;
using Domain.Models;
using Moq;

public class Constructor
{
    /**
    * Test method for HolidayPeriod constructor.
    * It can only be instantiated with a IPeriodDate (which is valid)
    */
    [Fact]
    public void WhenConstructorIsCalled_ThenObjectIsInstantiated()
    {
        // Arrange 
        // double for IPeriodDate - Stub
        Mock<IPeriodDate> doublePeriodDate = new Mock<IPeriodDate>();

        // Act
        new HolidayPeriod(doublePeriodDate.Object);

        // Assert
    }

}