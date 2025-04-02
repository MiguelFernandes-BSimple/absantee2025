namespace Domain.Tests.HolidayPeriodTests;

using Domain.Interfaces;
using Domain.Models;
using Xunit;
using Moq;

public class Contains
{
    /**
    * Test method to verify if Holiday Period is contained in another Holiday Period
    * Happy Path
    */
    [Fact]
    public void WhenPeriodIsFullyContained_ThenReturnsTrue()
    {
        // Arrange
        Mock<IPeriodDate> doublePeriodDateReference = new Mock<IPeriodDate>();
        Mock<IPeriodDate> doublePeriodDateToVerify = new Mock<IPeriodDate>();

        // Establish that the other IPeriodDate must be contained in it
        // Reference period CONTAINS period to Verify
        doublePeriodDateReference.Setup(pd => pd.Contains(doublePeriodDateToVerify.Object)).Returns(true);

        // Instatiate both Holiday Periods
        HolidayPeriod referenceHPeriod = new HolidayPeriod(doublePeriodDateReference.Object);
        HolidayPeriod toVerifyHPeriod = new HolidayPeriod(doublePeriodDateToVerify.Object);

        // Act
        bool result = referenceHPeriod.Contains(toVerifyHPeriod);

        // Assert
        Assert.True(result);
    }

    /**
    * Test method to verify if Holiday Period is contained in another Holiday Period
    * It's not contained
    */
    [Fact]
    public void WhenPeriodIsNotFullyContained_ThenReturnsFalse()
    {
        // Arrange
        Mock<IPeriodDate> doublePeriodDateReference = new Mock<IPeriodDate>();
        Mock<IPeriodDate> doublePeriodDateToVerify = new Mock<IPeriodDate>();

        // Establish that the other IPeriodDate ISN'T contained in it
        // Reference period DOESN'T contain period to Verify
        doublePeriodDateReference.Setup(pd => pd.Contains(doublePeriodDateToVerify.Object)).Returns(false);

        // Instatiate both Holiday Periods
        HolidayPeriod referenceHPeriod = new HolidayPeriod(doublePeriodDateReference.Object);
        HolidayPeriod toVerifyHPeriod = new HolidayPeriod(doublePeriodDateToVerify.Object);

        // Act
        bool result = referenceHPeriod.Contains(toVerifyHPeriod);

        // Assert
        Assert.False(result);
    }
}