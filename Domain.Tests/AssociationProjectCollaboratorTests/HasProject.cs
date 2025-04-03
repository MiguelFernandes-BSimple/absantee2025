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
        Mock<IProject> ProjectMock = new Mock<IProject>();
        Mock<IPeriodDate> periodDateMock = new Mock<IPeriodDate>();

        ProjectMock.Setup(p => p.ContainsDates(periodDateMock.Object)).Returns(true);

        ProjectMock.Setup(c => c.IsFinished()).Returns(false);

        Mock<ICollaborator> CollaboradorMock = new Mock<ICollaborator>();

        CollaboradorMock.Setup(c => c.ContractContainsDates(It.IsAny<IPeriodDateTime>())).Returns(true);

        var assocProjCollab = new AssociationProjectCollaborator(periodDateMock.Object, CollaboradorMock.Object, ProjectMock.Object);

        //act
        bool result = assocProjCollab.HasProject(ProjectMock.Object);
        //assert
        Assert.True(result);
    }

    [Fact]
    public void WhenHasProjectReceivesDifferentProject_ReturnFalse()
    {
        //arrange
        Mock<IProject> ProjectMock = new Mock<IProject>();
        Mock<IProject> ProjectMock2 = new Mock<IProject>();
        Mock<IPeriodDate> periodDateMock = new Mock<IPeriodDate>();

        ProjectMock.Setup(p => p.ContainsDates(periodDateMock.Object)).Returns(true);

        ProjectMock.Setup(c => c.IsFinished()).Returns(false);

        Mock<ICollaborator> CollaboradorMock = new Mock<ICollaborator>();

        CollaboradorMock.Setup(c => c.ContractContainsDates(It.IsAny<IPeriodDateTime>())).Returns(true);

        var assocProjCollab = new AssociationProjectCollaborator(periodDateMock.Object, CollaboradorMock.Object, ProjectMock.Object);

        //act
        bool result = assocProjCollab.HasProject(ProjectMock2.Object);
        //assert
        Assert.False(result);
    }
}
