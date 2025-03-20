using Domain;

public class TrainingPeriodTest
{
    public static IEnumerable<object[]> GetTrainingPeriodData_ValidDates()
    {
        yield return new object[]
        {
            DateOnly.FromDateTime(DateTime.Now),
            DateOnly.FromDateTime(DateTime.Now.AddDays(1)),
        };
        yield return new object[]
        {
            DateOnly.FromDateTime(DateTime.Now.AddYears(1)),
            DateOnly.FromDateTime(DateTime.Now.AddYears(3)),
        };
        yield return new object[]
        {
            DateOnly.FromDateTime(DateTime.Now.AddYears(1)),
            DateOnly.FromDateTime(DateTime.Now.AddYears(1)),
        };
    }

    [Theory]
    [MemberData(nameof(GetTrainingPeriodData_ValidDates))]
    public void WhenPassingValidDates_CreateTrainingPeriod(DateOnly initDate, DateOnly finalDate)
    {
        //arrange

        //act
        new TrainingPeriod(initDate, finalDate);

        //assert
    }

    public static IEnumerable<object[]> GetTrainingPeriodData_InvalidDates()
    {
        yield return new object[]
        {
            DateOnly.FromDateTime(DateTime.Now).AddDays(5),
            DateOnly.FromDateTime(DateTime.Now.AddDays(1)),
        };
        yield return new object[]
        {
            DateOnly.FromDateTime(DateTime.Now).AddYears(4),
            DateOnly.FromDateTime(DateTime.Now.AddYears(2)),
        };
    }

    [Theory]
    [MemberData(nameof(GetTrainingPeriodData_InvalidDates))]
    public void WhenPassingStartDateAfterFinalDate_ThrowsArgumentException(
        DateOnly initDate,
        DateOnly finalDate
    )
    {
        //arrange

        //assert
        ArgumentException exception = Assert.Throws<ArgumentException>(
            () =>
                //act
                new TrainingPeriod(initDate, finalDate)
        );

        Assert.Equal("Invalid Arguments", exception.Message);
    }

    public static IEnumerable<object[]> GetTrainingPeriodData_InvalidInitialDate()
    {
        yield return new object[]
        {
            DateOnly.FromDateTime(DateTime.Now).AddDays(-5),
            DateOnly.FromDateTime(DateTime.Now.AddDays(1)),
        };
        yield return new object[]
        {
            DateOnly.FromDateTime(DateTime.Now).AddYears(-3),
            DateOnly.FromDateTime(DateTime.Now.AddYears(-1)),
        };
    }

    [Theory]
    [MemberData(nameof(GetTrainingPeriodData_InvalidInitialDate))]
    public void WhenPassingDatesInThePast_ThrowsArgumentException(
        DateOnly initDate,
        DateOnly finalDate
    )
    {
        //arrange

        //assert
        ArgumentException exception = Assert.Throws<ArgumentException>(
            () =>
                //act
                new TrainingPeriod(initDate, finalDate)
        );

        Assert.Equal("Invalid Arguments", exception.Message);
    }
}
