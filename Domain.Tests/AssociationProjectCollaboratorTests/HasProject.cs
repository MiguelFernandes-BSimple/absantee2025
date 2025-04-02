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
        Mock<IPeriodDate> periodDateMock = new Mock<IPeriodDate>();

        ProjectMock.Setup(p => p.ContainsDates(periodDateMock.Object)).Returns(true);

        ProjectMock.Setup(c => c.IsFinished()).Returns(false);

        Mock<ICollaborator> CollaboradorMock = new Mock<ICollaborator>();
        Mock<IPeriodDateTime> mockPeriodDateTime = new Mock<IPeriodDateTime>();

        CollaboradorMock.Setup(c => c.ContractContainsDates(mockPeriodDateTime.Object)).Returns(true);

        Mock<IPeriodDate> secondPeriodDateMock = new Mock<IPeriodDate>();
        secondPeriodDateMock.Setup(p => p.GetInitDate()).Returns(initDate);
        secondPeriodDateMock.Setup(p => p.GetInitDate()).Returns(finalDate);

        var assocProjCollab = new AssociationProjectCollaborator(secondPeriodDateMock.Object, CollaboradorMock.Object, ProjectMock.Object);

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
        Mock<IPeriodDate> periodDateMock = new Mock<IPeriodDate>();

        ProjectMock.Setup(p => p.ContainsDates(periodDateMock.Object)).Returns(true);

        ProjectMock.Setup(c => c.IsFinished()).Returns(false);

        Mock<ICollaborator> CollaboradorMock = new Mock<ICollaborator>();
        Mock<IPeriodDateTime> mockPeriodDateTime = new Mock<IPeriodDateTime>();

        CollaboradorMock.Setup(c => c.ContractContainsDates(mockPeriodDateTime.Object)).Returns(true);

        Mock<IPeriodDate> secondPeriodDateMock = new Mock<IPeriodDate>();
        secondPeriodDateMock.Setup(p => p.GetInitDate()).Returns(initDate);
        secondPeriodDateMock.Setup(p => p.GetInitDate()).Returns(finalDate);

        var assocProjCollab = new AssociationProjectCollaborator(secondPeriodDateMock.Object, CollaboradorMock.Object, ProjectMock.Object);

        Mock<IProject> ProjectMock2 = new Mock<IProject>();
        //act
        bool result = assocProjCollab.HasProject(ProjectMock2.Object);
        //assert
        Assert.False(result);
    }
}
