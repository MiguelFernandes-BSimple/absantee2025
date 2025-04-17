using Domain.Models;

namespace Domain.Tests.PeriodDateTimeTests;

public class PeriodDateTimeContainsTests
{
    public static IEnumerable<object[]> GetPeriodDates_ValidFields()
    {
        yield return new object[] { DateTime.Now, DateTime.Now.AddYears(1) };
        yield return new object[] { DateTime.Now.AddMonths(-1), DateTime.Now.AddMonths(1) };
        yield return new object[] { DateTime.Now.AddDays(1), DateTime.Now.AddDays(12) };
    }

    [Theory]
    [MemberData(nameof(GetPeriodDates_ValidFields))]
    public void WhenPassingGoodPeriodDateTime_ThenReturnTrue(DateTime initDate, DateTime finalDate)
    {
        // Arrange
        DateTime referenceInitDate = DateTime.Now.AddYears(-1);
        DateTime referenceFinalDate = DateTime.Now.AddYears(3);

        PeriodDateTime referencePeriodDateTime = new PeriodDateTime(referenceInitDate, referenceFinalDate);

        PeriodDateTime inputPeriodDate = new PeriodDateTime(initDate, finalDate);

        // Act
        bool result = referencePeriodDateTime.Contains(inputPeriodDate);

        // Assert
        Assert.True(result);
    }

    public static IEnumerable<object[]> GetPeriodDates_CommonValidFields()
    {
        yield return new object[] { new DateTime(2025, 4, 3), new DateTime(2025, 4, 7) };
        yield return new object[] { new DateTime(2025, 4, 6), new DateTime(2025, 4, 10) };
        yield return new object[] { new DateTime(2025, 4, 3), new DateTime(2025, 4, 10) };

    }

    [Theory]
    [MemberData(nameof(GetPeriodDates_CommonValidFields))]
    public void WhenPassingGoodPeriodDateTimeWithSameInitOrFinalDates_ThenReturnTrue(DateTime initDate, DateTime finalDate)
    {
        // Arrange
        DateTime referenceInitDate = new DateTime(2025, 4, 3);
        DateTime referenceFinalDate = new DateTime(2025, 4, 10);

        PeriodDateTime referencePeriodDateTime = new PeriodDateTime(referenceInitDate, referenceFinalDate);
        PeriodDateTime inputPeriodDate = new PeriodDateTime(initDate, finalDate);

        // Act
        bool result = referencePeriodDateTime.Contains(inputPeriodDate);

        // Assert
        Assert.True(result);
    }

    public static IEnumerable<object[]> GetPeriodDates_InvalidFields()
    {
        yield return new object[] { DateTime.Now, DateTime.Now.AddYears(1) };
        yield return new object[] { DateTime.Now.AddMonths(-2), DateTime.Now.AddDays(20) };
        yield return new object[] { DateTime.Now.AddDays(21), DateTime.Now.AddDays(30) };
    }

    [Theory]
    [MemberData(nameof(GetPeriodDates_InvalidFields))]
    public void WhenPassingNotContainedPeriodDateTime_ThenReturnFalse(DateTime initDate, DateTime finalDate)
    {
        // Arrange
        // Reference PeriodDateTime to compare 
        // All dates can't be contained in this period's dates
        DateTime referenceInitDate = DateTime.Now.AddMonths(-1);
        DateTime referenceFinalDate = DateTime.Now.AddDays(20);

        // Instatiate Reference PeriodDateTime
        PeriodDateTime referencePeriodDateTime = new PeriodDateTime(referenceInitDate, referenceFinalDate);

        // Instatiate Input PeriodDateTime
        PeriodDateTime inputPeriodDate = new PeriodDateTime(initDate, finalDate);

        // Act
        bool result = referencePeriodDateTime.Contains(inputPeriodDate);

        // Assert
        Assert.False(result);
    }


}