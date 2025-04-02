using Domain.Models;

namespace Domain.Tests.PeriodDateTimeTests;

public class Contains
{
    /**
    * Test method to verify if a PeriodDateTime is inside another
    * Case where it's true -> fully contained
    */
    public static IEnumerable<object[]> GetPeriodDates_ValidFields()
    {
        yield return new object[] { DateTime.Now, DateTime.Now.AddYears(1) };
        yield return new object[] { DateTime.Now.AddMonths(-1), DateTime.Now.AddMonths(1) };
        yield return new object[] { DateTime.Now.AddDays(1), DateTime.Now.AddDays(12) };
    }

    [Theory]
    [MemberData(nameof(GetPeriodDates_ValidFields))]
    public void WhenPassingContainedPeriodDateTime_ThenReturnTrue(DateTime initDate, DateTime endDate)
    {
        // Arrange
        // Reference PeriodDateTime to compare 
        // All dates must be contained in this periods dates
        DateTime referenceInitDate = DateTime.Now.AddYears(-1);
        DateTime referenceEndDate = DateTime.Now.AddYears(3);

        // Instatiate Reference PeriodDateTime
        PeriodDateTime referencePeriodDateTime = new PeriodDateTime(referenceInitDate, referenceEndDate);

        // Instatiate Input PeriodDateTime
        PeriodDateTime inputPeriodDate = new PeriodDateTime(initDate, endDate);

        // Act
        bool result = referencePeriodDateTime.Contains(inputPeriodDate);

        // Assert
        Assert.True(result);
    }

    /**
    * Test method to verify if a PeriodDateTime is inside another
    * case where it's false -> not fully contained or not eve intersected
    */
    public static IEnumerable<object[]> GetPeriodDates_InvalidFields()
    {
        yield return new object[] { DateTime.Now, DateTime.Now.AddYears(1) };
        yield return new object[] { DateTime.Now.AddMonths(-2), DateTime.Now.AddDays(20) };
        yield return new object[] { DateTime.Now.AddDays(21), DateTime.Now.AddDays(30) };
    }

    [Theory]
    [MemberData(nameof(GetPeriodDates_InvalidFields))]
    public void WhenPassingNotContainedPeriodDateTime_ThenReturnFalse(DateTime initDate, DateTime endDate)
    {
        // Arrange
        // Reference PeriodDateTime to compare 
        // All dates can't be contained in this period's dates
        DateTime referenceInitDate = DateTime.Now.AddMonths(-1);
        DateTime referenceEndDate = DateTime.Now.AddDays(20);

        // Instatiate Reference PeriodDateTime
        PeriodDateTime referencePeriodDateTime = new PeriodDateTime(referenceInitDate, referenceEndDate);

        // Instatiate Input PeriodDateTime
        PeriodDateTime inputPeriodDate = new PeriodDateTime(initDate, endDate);

        // Act
        bool result = referencePeriodDateTime.Contains(inputPeriodDate);

        // Assert
        Assert.False(result);
    }


}