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
        PeriodDate periodDate =
            new PeriodDate(DateOnly.FromDateTime(DateTime.Now.AddMonths(1)), DateOnly.FromDateTime(DateTime.Now.AddMonths(2)));

        TrainingPeriodFactory factory = new TrainingPeriodFactory();

        // Act
        var result = factory.Create(periodDate);

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public void WhenPassingDatesInThePast_ThenThrowsArgumentException()
    {
        // Arrange
        PeriodDate periodDate =
            new PeriodDate(DateOnly.FromDateTime(DateTime.Now.AddMonths(-1)), DateOnly.FromDateTime(DateTime.Now.AddMonths(2)));

        TrainingPeriodFactory factory = new TrainingPeriodFactory();

        // Assert
        ArgumentException exception = Assert.Throws<ArgumentException>(
            () =>
                // Act
                factory.Create(periodDate)

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