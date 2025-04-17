using Domain.Interfaces;
using Domain.Models;
using Moq;

namespace Domain.Tests.ProjectManagerTests;

public class ProjectManagerGetPeriodDateTimeTests
{
    [Fact]
    public void WhenGettingPeriodDateTime_ThenReturnsPeriodDateTime()
    {
        //arrange
        var periodDateTime = new Mock<PeriodDateTime>();
        var ProjectManager = new ProjectManager(It.IsAny<long>(), It.IsAny<long>(), periodDateTime.Object);
        //act
        var ProjectManagerPeriodDateTime = ProjectManager.GetPeriodDateTime();

        //assert
        Assert.Equal(periodDateTime.Object, ProjectManagerPeriodDateTime);
    }
}
