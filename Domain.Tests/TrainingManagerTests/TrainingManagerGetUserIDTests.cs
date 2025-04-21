using Domain.Interfaces;
using Domain.Models;
using Moq;

namespace Domain.Tests.TrainingManagerTests;

public class TrainingManagerGetUserIDTests
{
    [Fact]
    public void WhenGettingUserId_ThenReturnsUserId()
    {
        //arrange
        var userId = 1;
        var TrainingManager = new TrainingManager(It.IsAny<long>(), userId, It.IsAny<PeriodDateTime>());
        //act
        var TrainingManagerUserId = TrainingManager.GetUserId();

        //assert
        Assert.Equal(userId, TrainingManagerUserId);
    }
}
