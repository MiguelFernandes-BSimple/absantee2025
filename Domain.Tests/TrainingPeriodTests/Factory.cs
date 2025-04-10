using Domain.Factory.TrainingPeriodFactory;
using Domain.Interfaces;
using Domain.Models;
using Domain.Visitor;
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

        TrainingPeriodFactory factory = new TrainingPeriodFactory();

        //Act
        factory.Create(periodDate.Object);

        //Assert

    }

    [Fact]
    public void WhenPassingDatesInThePast_ThenThrowsArgumentException()
    {
        // Arrange
        Mock<IPeriodDate> periodDate = new Mock<IPeriodDate>();

        // when init date is smaller than DateOnly(DateTime.Now)
        periodDate.Setup(pd => pd.IsInitDateSmallerThan(DateOnly.FromDateTime(DateTime.Now))).Returns(true);

        TrainingPeriodFactory factory = new TrainingPeriodFactory();


        // Assert
        ArgumentException exception = Assert.Throws<ArgumentException>(
            () =>
                //act
                factory.Create(periodDate.Object)

        );

        Assert.Equal("Period date cannot start in the past.", exception.Message);
    }

    [Fact]
    public void WhenPassingVisitor_ThenCreateTrainingPeriod()
    {
        //arrange
        var visitor = new Mock<ITrainingPeriodVisitor>();
        visitor.Setup(v => v.PeriodDate).Returns(It.IsAny<PeriodDate>());

        var factory = new TrainingPeriodFactory();

        //act
        factory.Create(visitor.Object);

        //assert
    }
}