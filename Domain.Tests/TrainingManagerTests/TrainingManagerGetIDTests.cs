using Domain.Interfaces;
using Domain.Models;
using Moq;

namespace Domain.Tests.TrainingManagerTests;

public class TrainingManagerGetIDTests
{
    [Fact]
    public void WhenGettingId_ThenReturnsId()
    {
        //arrange
        var id = 1;

        var TrainingManager = new TrainingManager(id, It.IsAny<long>(), It.IsAny<PeriodDateTime>());
        //act
        var TrainingManagerID = TrainingManager.GetId();

        //assert
        Assert.Equal(id, TrainingManagerID);
    }
}
