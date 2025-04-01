namespace Domain.Tests.AssociationProjectCollaboratorTests;

using Moq;
using Domain.Interfaces;
using Domain.Models;

public class HasProject
{
    [Fact]
    public void WhenHasProjectReceivesSameProject_ReturnTrue()
    {
        //arrange
        DateOnly initDate = DateOnly.FromDateTime(DateTime.Now);
        DateOnly finalDate = DateOnly.FromDateTime(DateTime.Now.AddYears(1));

        Mock<IProject> ProjectMock = new Mock<IProject>();
        ProjectMock.Setup(p => p.ContainsDates(It.IsAny<DateOnly>(), It.IsAny<DateOnly>())).Returns(true);

        ProjectMock.Setup(c => c.IsFinished()).Returns(false);

        Mock<ICollaborator> CollaboradorMock = new Mock<ICollaborator>();
        CollaboradorMock.Setup(c => c.ContractContainsDates(It.IsAny<DateTime>(), It.IsAny<DateTime>())).Returns(true);

        var assocProjCollab = new AssociationProjectCollaborator(initDate, finalDate, CollaboradorMock.Object, ProjectMock.Object);

        //act
        bool result = assocProjCollab.HasProject(ProjectMock.Object);
        //assert
        Assert.True(result);
    }

    [Fact]
    public void WhenHasProjectReceivesDifferentProject_ReturnFalse()
    {
        //arrange
        DateOnly initDate = DateOnly.FromDateTime(DateTime.Now);
        DateOnly finalDate = DateOnly.FromDateTime(DateTime.Now.AddYears(1));

        Mock<IProject> ProjectMock = new Mock<IProject>();
        ProjectMock.Setup(p => p.ContainsDates(It.IsAny<DateOnly>(), It.IsAny<DateOnly>())).Returns(true);

        ProjectMock.Setup(c => c.IsFinished()).Returns(false);

        Mock<ICollaborator> CollaboradorMock = new Mock<ICollaborator>();
        CollaboradorMock.Setup(c => c.ContractContainsDates(It.IsAny<DateTime>(), It.IsAny<DateTime>())).Returns(true);

        var assocProjCollab = new AssociationProjectCollaborator(initDate, finalDate, CollaboradorMock.Object, ProjectMock.Object);

        Mock<IProject> ProjectMock2 = new Mock<IProject>();
        //act
        bool result = assocProjCollab.HasProject(ProjectMock2.Object);
        //assert
        Assert.False(result);
    }
}
