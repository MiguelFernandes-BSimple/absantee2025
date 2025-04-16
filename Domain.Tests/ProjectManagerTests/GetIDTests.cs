using Domain.Interfaces;
using Domain.Models;
using Moq;

namespace Domain.Tests.ProjectManagerTests;

public class GetIDTests
{
    [Fact]
    public void WhenGettingId_ThenReturnsId()
    {
        //arrange
        var id = 1;

        var ProjectManager = new ProjectManager(id, It.IsAny<long>(), It.IsAny<IPeriodDateTime>());
        //act
        var ProjectManagerID = ProjectManager.GetId();

        //assert
        Assert.Equal(id, ProjectManagerID);
    }
}
