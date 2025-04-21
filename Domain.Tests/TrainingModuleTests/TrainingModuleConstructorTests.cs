using Domain.Models;
using Moq;

namespace Domain.Tests.TrainingModuleTests;

public class TrainingModuleConstructorTests
{
    [Fact]
    public void WhenPassingValidTrainingSubjectId_ThenInstatiateObject()
    {
        // Arrange

        // Act
        var result = new TrainingModule(It.IsAny<long>());

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public void WhenPassingValidTrainingSubjectIdAndPeriodListCombo_ThenInstatiateObject()
    {
        // Arrange
        PeriodDateTime period1 = new PeriodDateTime(DateTime.Now.AddDays(10), DateTime.Now.AddMonths(10));
        PeriodDateTime period2 = new PeriodDateTime(DateTime.Now.AddYears(1), DateTime.Now.AddYears(2));

        List<PeriodDateTime> resultList = new List<PeriodDateTime> { period1, period2 };
        // Act
        var result = new TrainingModule(It.IsAny<long>(), resultList);

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public void WhenPassingPeriodNotInFuture_ThenThrowException()
    {
        // Arrange
        PeriodDateTime period1 = new PeriodDateTime(DateTime.Now.AddMonths(-10), DateTime.Now.AddYears(1));
        PeriodDateTime period2 = new PeriodDateTime(DateTime.Now.AddYears(1), DateTime.Now.AddYears(2));

        List<PeriodDateTime> resultList = new List<PeriodDateTime> { period1, period2 };

        // Assert
        ArgumentException exception = Assert.Throws<ArgumentException>(() =>
            // Act
            new TrainingModule(It.IsAny<long>(), resultList));

        Assert.Equal("Invalid input", exception.Message);
    }

    [Fact]
    public void WhenPassingIntersectingPeriod_ThenInstatiateObject()
    {
        // Arrange
        PeriodDateTime period1 = new PeriodDateTime(DateTime.Now.AddDays(1), DateTime.Now.AddMonths(14));
        PeriodDateTime period2 = new PeriodDateTime(DateTime.Now.AddYears(1), DateTime.Now.AddYears(2));

        List<PeriodDateTime> resultList = new List<PeriodDateTime> { period1, period2 };

        // Assert
        ArgumentException exception = Assert.Throws<ArgumentException>(() =>
            // Act
            new TrainingModule(It.IsAny<long>(), resultList));

        Assert.Equal("Invalid inputs", exception.Message);
    }
}