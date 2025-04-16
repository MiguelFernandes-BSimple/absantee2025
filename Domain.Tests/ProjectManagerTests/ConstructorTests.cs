using Domain.Interfaces;
using Domain.Models;
using Moq;

namespace Domain.Tests.ProjectManagerTests;

public class ConstructorTests
{
    [Fact]
    public void WhenCreatingProjectManagerWithValidPeriod_ThenProjectManagerIsCreatedCorrectly()
    {
        //arrange

        //act
        new ProjectManager(It.IsAny<long>(), It.IsAny<IPeriodDateTime>());
        //assert
    }

    [Fact]
    public void WhenCreatingProjectManagerWithValidInitDate_ThenProjectManagerIsCreatedCorrectly()
    {
        //arrange

        //act
        var ProjectManager = new ProjectManager(It.IsAny<long>(), It.IsAny<long>(), It.IsAny<IPeriodDateTime>());
        //assert
        Assert.NotNull(ProjectManager);
    }
}
