using Domain.Models;

namespace Domain.Tests.PeriodDateTimeTests;

public class IsFinalDateUndefined
{
    /**
    * Test method to verify if the final date for a periodDateTime is defined
    * Case where its true
    */
    [Fact]
    public void WhenFinalDateIsUndefined_ThenReturnTrue()
    {
        // Arrange
        // Init date is irrelevant for date context
        DateTime initDate = DateTime.Now;
        // End date must be MaxValue -> is undefined
        DateTime endDate = DateTime.MaxValue;

        // Instatiate periodDateTime object
        PeriodDateTime periodDateTime = new PeriodDateTime(initDate, endDate);

        // Act
        bool result = periodDateTime.IsFinalDateUndefined();

        // Assert
        Assert.True(result);
    }

    /**
    * Test method to verify if the final date for a periodDateTime is defined
    * Case where its true
    */
    [Fact]
    public void WhenFinalDateIsDefined_ThenReturnFalse()
    {
        // Arrange
        // Init date is irrelevant for date context
        DateTime initDate = DateTime.Now;
        // End has to be anything but the MaxValue - 1year after init date for testing purposes
        DateTime endDate = initDate.AddYears(1);

        // Instatiate periodDateTime object
        PeriodDateTime periodDateTime = new PeriodDateTime(initDate, endDate);

        // Act
        bool result = periodDateTime.IsFinalDateUndefined();

        // Assert
        Assert.False(result);
    }
}