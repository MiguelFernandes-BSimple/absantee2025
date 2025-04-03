namespace Domain.Tests.AssociationProjectCollaboratorTests;

using Moq;
using Domain.Interfaces;
using Domain.Models;

public class HasCollaborator
{
    [Fact]
    public void WhenReceivesSameCollaborator_ReturnTrue()
    {
        //arrange
        Mock<IProject> ProjectMock = new Mock<IProject>();
        Mock<IPeriodDate> periodDateMock = new Mock<IPeriodDate>();

        ProjectMock.Setup(p => p.ContainsDates(periodDateMock.Object)).Returns(true);

        ProjectMock.Setup(c => c.IsFinished()).Returns(false);

        Mock<ICollaborator> CollaboratorMock = new Mock<ICollaborator>();

        CollaboratorMock.Setup(c => c.ContractContainsDates(It.IsAny<IPeriodDateTime>())).Returns(true);

        var assocProjCollab = new AssociationProjectCollaborator(periodDateMock.Object, CollaboratorMock.Object, ProjectMock.Object);

        //act
        bool result = assocProjCollab.HasCollaborator(CollaboratorMock.Object);
        //assert
        Assert.True(result);
    }

    [Fact]
    public void WhenReceivesDifferentCollaborator_ReturnFalse()
    {
        //arrange
        Mock<IProject> ProjectMock = new Mock<IProject>();
        Mock<IProject> ProjectMock2 = new Mock<IProject>();
        Mock<IPeriodDate> periodDateMock = new Mock<IPeriodDate>();

        ProjectMock.Setup(p => p.ContainsDates(periodDateMock.Object)).Returns(true);

        ProjectMock.Setup(c => c.IsFinished()).Returns(false);

        Mock<ICollaborator> CollaboratorMock = new Mock<ICollaborator>();
        Mock<ICollaborator> CollaboratorMock2 = new Mock<ICollaborator>();

        CollaboratorMock.Setup(c => c.ContractContainsDates(It.IsAny<IPeriodDateTime>())).Returns(true);

        var assocProjCollab = new AssociationProjectCollaborator(periodDateMock.Object, CollaboratorMock.Object, ProjectMock.Object);

        //act
        bool result = assocProjCollab.HasCollaborator(CollaboratorMock2.Object);
        //assert
        Assert.False(result);
    }
}
