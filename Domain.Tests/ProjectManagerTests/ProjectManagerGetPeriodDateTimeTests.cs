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
        PeriodDateTime periodDateTime = new PeriodDateTime(It.IsAny<DateTime>(), It.IsAny<DateTime>());

        var ProjectManager = new ProjectManager(It.IsAny<long>(), It.IsAny<long>(), periodDateTime);
        //act
        var ProjectManagerPeriodDateTime = ProjectManager.GetPeriodDateTime();

        //assert
        Assert.Equal(periodDateTime, ProjectManagerPeriodDateTime);
    }
}
