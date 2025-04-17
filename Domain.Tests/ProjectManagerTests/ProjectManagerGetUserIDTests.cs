using Domain.Interfaces;
using Domain.Models;
using Moq;

namespace Domain.Tests.ProjectManagerTests;

public class ProjectManagerGetUserIDTests
{
    [Fact]
    public void WhenGettingUserId_ThenReturnsUserId()
    {
        //arrange
        var userId = 1;
        var ProjectManager = new ProjectManager(It.IsAny<long>(), userId, It.IsAny<IPeriodDateTime>());
        //act
        var ProjectManagerUserId = ProjectManager.GetUserId();

        //assert
        Assert.Equal(userId, ProjectManagerUserId);
    }
}
