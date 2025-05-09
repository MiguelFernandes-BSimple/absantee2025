using Domain.Models;
using Moq;

namespace Domain.Tests.HRManagerTests;

public class HRManagerConstructorTests
{
    [Fact]
    public void WhenCreatingHRManagerWithValidUserIdAndPeriod_ThenHRManagerIsCreatedCorrectly()
    {
        //Arrange

        //Act
        var result = new HRManager(It.IsAny<Guid>(), It.IsAny<PeriodDateTime>());

        //Assert
        Assert.NotNull(result);
    }

    [Fact]
    public void WhenCreatingHRManagerWithValidIdandUserIdAndPeriod_ThenHRManagerIsCreatedCorrectly()
    {
        //Arrange

        //Act
        var result = new HRManager(It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<PeriodDateTime>());

        //Assert
        Assert.NotNull(result);
    }
}
