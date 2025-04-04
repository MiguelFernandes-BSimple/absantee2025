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
        var projectDouble = new Mock<IProject>();
        var periodDateDouble = new Mock<IPeriodDate>();

        projectDouble.Setup(p => p.ContainsDates(periodDateDouble.Object)).Returns(true);
        projectDouble.Setup(c => c.IsFinished()).Returns(false);

        var collaboratorDouble = new Mock<ICollaborator>();
        collaboratorDouble.Setup(c => c.ContractContainsDates(It.IsAny<IPeriodDateTime>())).Returns(true);
        // perguntar se isto tem de ser assim feito ou se temos de mudar o equals na classe AssociationProjectCollaborator
        collaboratorDouble.Setup(c => c.Equals(collaboratorDouble.Object)).Returns(true);

        var assocProjCollab = new AssociationProjectCollaborator(periodDateDouble.Object, collaboratorDouble.Object, projectDouble.Object);

        //act
        bool result = assocProjCollab.HasCollaborator(collaboratorDouble.Object);

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
