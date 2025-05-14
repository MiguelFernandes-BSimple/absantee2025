using Domain.Factory.TrainingPeriodFactory;
using Domain.Models;
using Domain.Visitor;
using Moq;
namespace Domain.Tests.TrainingPeriodTests;

public class TrainingPeriodFactoryTests
{
    [Fact]
    public void WhenPassingValidDates_ThenCreateTrainingPeriod()
    {
        // Arrange
        DateOnly InitDate = DateOnly.FromDateTime(DateTime.Now.AddMonths(1));
        DateOnly FinalDate = DateOnly.FromDateTime(DateTime.Now.AddMonths(2));

        TrainingPeriodFactory factory = new TrainingPeriodFactory();

        // Act
        var result = factory.Create(InitDate, FinalDate);

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public void WhenPassingDatesInThePast_ThenThrowsArgumentException()
    {
        // Arrange
        DateOnly InitDate = DateOnly.FromDateTime(DateTime.Now.AddMonths(-1));
        DateOnly FinalDate = DateOnly.FromDateTime(DateTime.Now.AddMonths(2));

        TrainingPeriodFactory factory = new TrainingPeriodFactory();

        // Assert
        ArgumentException exception = Assert.Throws<ArgumentException>(
            () =>
                // Act
                factory.Create(InitDate, FinalDate)

        );

        Assert.Equal("Period date cannot start in the past.", exception.Message);
    }

    [Fact]
    public void WhenPassingVisitor_ThenCreateTrainingPeriod()
    {
        // Arrange
        var visitor = new Mock<ITrainingPeriodVisitor>();
        visitor.Setup(v => v.PeriodDate).Returns(It.IsAny<PeriodDate>());

        var factory = new TrainingPeriodFactory();

        // Act
        var result = factory.Create(visitor.Object);

        // Assert
        Assert.NotNull(result);
    }
}