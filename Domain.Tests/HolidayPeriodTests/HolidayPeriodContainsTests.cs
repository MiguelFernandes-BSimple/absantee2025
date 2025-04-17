using Domain.Interfaces;
using Domain.Models;
using Moq;

namespace Domain.Tests.HolidayPeriodTests;

public class HolidayPeriodContainsTests
{
    /**
    * Test method to verify if Holiday Period is contained in another Holiday Period
    * Happy Path
    */
    [Fact]
    public void WhenHolidayPeriodIsFullyContained_ThenReturnsTrue()
    {
        // Arrange
        Mock<PeriodDate> doublePeriodDateReference = new Mock<PeriodDate>();
        Mock<PeriodDate> doublePeriodDateToVerify = new Mock<PeriodDate>();

        // Establish that the other PeriodDate must be contained in it
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
    public void WhenHolidayPeriodIsNotFullyContained_ThenReturnsFalse()
    {
        // Arrange
        Mock<PeriodDate> doublePeriodDateReference = new Mock<PeriodDate>();
        Mock<PeriodDate> doublePeriodDateToVerify = new Mock<PeriodDate>();

        // Establish that the other PeriodDate ISN'T contained in it
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

    /**
    * Test method to verify if PeriodDate is contained in another Holiday Period's period
    * Happy Path
    */
    [Fact]
    public void WhenPeriodIsFullyContained_ThenReturnsTrue()
    {
        // Arrange
        Mock<PeriodDate> doublePeriodDateReference = new Mock<PeriodDate>();
        Mock<PeriodDate> doublePeriodDateToVerify = new Mock<PeriodDate>();

        // Establish that the other PeriodDate must be contained in it
        // Reference period CONTAINS period to Verify
        doublePeriodDateReference.Setup(pd => pd.Contains(doublePeriodDateToVerify.Object)).Returns(true);

        // Instatiate both Holiday Periods
        HolidayPeriod referenceHPeriod = new HolidayPeriod(doublePeriodDateReference.Object);

        // Act
        bool result = referenceHPeriod.Contains(doublePeriodDateToVerify.Object);

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
        Mock<PeriodDate> doublePeriodDateReference = new Mock<PeriodDate>();
        Mock<PeriodDate> doublePeriodDateToVerify = new Mock<PeriodDate>();

        // Establish that the other PeriodDate ISN'T contained in it
        // Reference period DOESN'T contain period to Verify
        doublePeriodDateReference.Setup(pd => pd.Contains(doublePeriodDateToVerify.Object)).Returns(false);

        // Instatiate both Holiday Periods
        HolidayPeriod referenceHPeriod = new HolidayPeriod(doublePeriodDateReference.Object);

        // Act
        bool result = referenceHPeriod.Contains(doublePeriodDateToVerify.Object);

        // Assert
        Assert.False(result);
    }


}