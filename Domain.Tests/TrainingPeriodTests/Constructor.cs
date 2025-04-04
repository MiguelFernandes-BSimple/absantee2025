using Domain.Interfaces;
using Domain.Models;
using Moq;
namespace Domain.Tests.TrainingPeriodTests;

public class Constructor
{
    [Fact]
    public void WhenPassingValidDates_ThenCreateTrainingPeriod()
    {
        //Arrange
        Mock<IPeriodDate> periodDate = new Mock<IPeriodDate>();

        // Period init date has to be bigger than DateOnly(DateTime.Now)
        // It's in the future
        periodDate.Setup(pd => pd.IsInitDateSmallerThan(DateOnly.FromDateTime(DateTime.Now))).Returns(false);

        //Act
        new TrainingPeriod(periodDate.Object);

        //Assert
    }

    [Fact]
    public void WhenPassingDatesInThePast_ThenThrowsArgumentException()
    {
        // Arrange
        Mock<IPeriodDate> periodDate = new Mock<IPeriodDate>();

        // when init date is smaller than DateOnly(DateTime.Now)
        periodDate.Setup(pd => pd.IsInitDateSmallerThan(DateOnly.FromDateTime(DateTime.Now))).Returns(true);

        // Assert
        ArgumentException exception = Assert.Throws<ArgumentException>(
            () =>
                //act
                new TrainingPeriod(periodDate.Object)
        );

        Assert.Equal("Invalid Arguments", exception.Message);
    }
}