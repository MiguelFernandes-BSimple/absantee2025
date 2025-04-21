using Domain.Models;
using Moq;

namespace Domain.Tests.ProjectManagerTests;

public class TrainingManagerConstructorTests
{
    [Fact]
    public void WhenCreatingTrainingManagerWithValidPeriod_ThenTrainingManagerIsCreatedCorrectly()
    {
        //arrange

        //act
        new TrainingManager(It.IsAny<long>(), It.IsAny<PeriodDateTime>());
        //assert
    }

    [Fact]
    public void WhenCreatingTrainingManagerWithValidInitDate_ThenTrainingManagerIsCreatedCorrectly()
    {
        //arrange

        //act
        var trainingManager = new TrainingManager(It.IsAny<long>(), It.IsAny<long>(), It.IsAny<PeriodDateTime>());
        //assert
        Assert.NotNull(trainingManager);
    }
}
