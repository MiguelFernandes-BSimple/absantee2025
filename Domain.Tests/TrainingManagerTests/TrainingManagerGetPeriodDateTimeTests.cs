using Domain.Interfaces;
using Domain.Models;
using Moq;

namespace Domain.Tests.TrainingManagerTests;

public class TrainingManagerGetPeriodDateTimeTests
{
    [Fact]
    public void WhenGettingPeriodDateTime_ThenReturnsPeriodDateTime()
    {
        //arrange
        PeriodDateTime periodDateTime = new PeriodDateTime(It.IsAny<DateTime>(), It.IsAny<DateTime>());

        var TrainingManager = new TrainingManager(It.IsAny<long>(), It.IsAny<long>(), periodDateTime);
        //act
        var TrainingManagerPeriodDateTime = TrainingManager.GetPeriodDateTime();

        //assert
        Assert.Equal(periodDateTime, TrainingManagerPeriodDateTime);
    }
}
