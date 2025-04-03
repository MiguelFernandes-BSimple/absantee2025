using Domain.Interfaces;
using Domain.Models;
using Moq;
namespace Domain.Tests.TrainingPeriodTests;
public class Constructor
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
    public void WhenPassingValidDates_ThenCreateTrainingPeriod(
        DateOnly initDate,
        DateOnly finalDate
    )
    {
        //arrange
        Mock<IPeriodDate> periodDateMock = new Mock<IPeriodDate>();
        periodDateMock.Setup(p => p.GetInitDate()).Returns(initDate);
        periodDateMock.Setup(p => p.GetFinalDate()).Returns(finalDate);
        //act
        new TrainingPeriod(periodDateMock.Object);

        //assert
    }

    public static IEnumerable<object[]> GetTrainingPeriodData_InvalidInitialDate()
    {
        yield return new object[]
        {
            DateOnly.FromDateTime(DateTime.Now).AddDays(-5),
        };
        yield return new object[]
        {
            DateOnly.FromDateTime(DateTime.Now).AddYears(-3),
        };
    }

    [Theory]
    [MemberData(nameof(GetTrainingPeriodData_InvalidInitialDate))]
    public void WhenPassingDatesInThePast_ThenThrowsArgumentException(
        DateOnly initDate
    )
    {
        //arrange
        Mock<IPeriodDate> periodDateMock = new Mock<IPeriodDate>();
        periodDateMock.Setup(p => p.GetInitDate()).Returns(initDate);
        //assert
        ArgumentException exception = Assert.Throws<ArgumentException>(
            () =>
                //act
                new TrainingPeriod(periodDateMock.Object)
        );

        Assert.Equal("Invalid Arguments", exception.Message);
    }
}