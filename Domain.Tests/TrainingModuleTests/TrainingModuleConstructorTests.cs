using Domain.Models;
using Moq;

namespace Domain.Tests.TrainingModuleTests;

public class TrainingModuleConstructorTests
{
    [Fact]
    public void WhenPassingArguments_ThenClassIsInstatiated()
    {
        // Arrange
        var periods = new List<PeriodDateTime>
        {
            new PeriodDateTime(It.IsAny<DateTime>(), It.IsAny<DateTime>())
        };
        // Act
        var result = new TrainingModule(It.IsAny<long>(), periods);

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public void WhenPassingArgumentsWithEmptyList_ThenClassIsInstatiated()
    {
        // Arrange

        // Act
        var result = new TrainingModule(It.IsAny<long>(), new List<PeriodDateTime>());

        // Assert
        Assert.NotNull(result);
    }
}