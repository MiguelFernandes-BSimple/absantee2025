using Domain.Factory.TrainingPeriodFactory;
using Domain.Interfaces;
using Domain.Models;
using Domain.Visitor;
using Moq;
namespace Domain.Tests.TrainingPeriodTests;

public class TrainingPeriodFactoryTests
{
    [Fact]
    public void WhenPassingValidDates_ThenCreateTrainingPeriod()
    {
        //Arrange
        Mock<PeriodDate> periodDate = new Mock<PeriodDate>();

        // Period init date has to be bigger than DateOnly(DateTime.Now)
        // It's in the future
        periodDate.Setup(pd => pd.IsInitDateSmallerThan(DateOnly.FromDateTime(DateTime.Now))).Returns(false);

        TrainingPeriodFactory factory = new TrainingPeriodFactory();

        //Act
        var result = factory.Create(periodDate.Object);

        //Assert
        Assert.NotNull(result);
    }

    [Fact]
    public void WhenPassingDatesInThePast_ThenThrowsArgumentException()
    {
        // Arrange
        Mock<PeriodDate> periodDate = new Mock<PeriodDate>();

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
        var result = factory.Create(visitor.Object);

        //assert
        Assert.NotNull(result);
    }
}