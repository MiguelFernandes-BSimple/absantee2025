using Domain.Interfaces;
using Domain.Models;
using Domain.Visitor;
using Moq;

namespace Domain.Tests.HRManagerTests;

public class HRManagerConstructorTests
{
    [Fact]
    public void WhenCreatingHRManagerWithValidUserIdAndPeriod_ThenHRManagerIsCreatedCorrectly()
    {
        //arrange

        //act
        new HRManager(It.IsAny<long>(), It.IsAny<IPeriodDateTime>());
        //assert
    }

    [Fact]
    public void WhenCreatingHRManagerWithValidIdandUserIdAndPeriod_ThenHRManagerIsCreatedCorrectly()
    {
        //arrange

        //act
        new HRManager(It.IsAny<long>(), It.IsAny<long>(), It.IsAny<IPeriodDateTime>());
        //assert
    }
}
