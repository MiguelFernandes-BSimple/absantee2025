namespace Domain.Tests.AssociationProjectCollaboratorTests;

using Moq;
using Domain.Interfaces;
using Domain.Models;

public class Constructor
{
    [Fact]
    public void WhenPassingValidData_ThenAssociationProjectCollaboratorIsCreated()
    {
        //arrange
        Mock<IProject> ProjectMock = new Mock<IProject>();

        Mock<IPeriodDate> periodDateMock = new Mock<IPeriodDate>();

        ProjectMock.Setup(p => p.ContainsDates(It.IsAny<IPeriodDate>())).Returns(true);

        ProjectMock.Setup(c => c.IsFinished()).Returns(false);

        Mock<ICollaborator> CollaboradorMock = new Mock<ICollaborator>();
        Mock<IPeriodDateTime> mockPeriodDateTime = new Mock<IPeriodDateTime>();

        CollaboradorMock.Setup(c => c.ContractContainsDates(It.IsAny<IPeriodDateTime>())).Returns(true);

        //act
        new AssociationProjectCollaborator(periodDateMock.Object, CollaboradorMock.Object, ProjectMock.Object);

        //assert
    }


    [Fact]
    public void WhenAssociationDatesAreInvalid_ThenThrowException()
    {
        //arrange
        Mock<IProject> ProjectMock = new Mock<IProject>();

        Mock<ICollaborator> CollaboradorMock = new Mock<ICollaborator>();
        Mock<IPeriodDate> periodDateMock = new Mock<IPeriodDate>();
        //assert
        ArgumentException exception = Assert.Throws<ArgumentException>(() =>
            //act
            new AssociationProjectCollaborator(periodDateMock.Object, CollaboradorMock.Object, ProjectMock.Object));

        Assert.Equal("Invalid Arguments", exception.Message);
    }


    [Fact]
    public void WhenProjectDatesOutsideAssociation_ThenThrowException()
    {
        //arrange
        Mock<IProject> ProjectMock = new Mock<IProject>();

        Mock<IPeriodDate> periodDateMock = new Mock<IPeriodDate>();
        ProjectMock.Setup(p => p.ContainsDates(periodDateMock.Object)).Returns(false);

        ProjectMock.Setup(c => c.IsFinished()).Returns(false);

        Mock<ICollaborator> CollaboradorMock = new Mock<ICollaborator>();
        Mock<IPeriodDateTime> mockPeriodDateTime = new Mock<IPeriodDateTime>();

        CollaboradorMock.Setup(c => c.ContractContainsDates(mockPeriodDateTime.Object)).Returns(true);
        //assert
        ArgumentException exception = Assert.Throws<ArgumentException>(() =>
            //act
            new AssociationProjectCollaborator(periodDateMock.Object, CollaboradorMock.Object, ProjectMock.Object));

        Assert.Equal("Invalid Arguments", exception.Message);
    }

    [Fact]
    public void WhenProjectIsFinished_ThenThrowException()
    {
        //arranges
        Mock<IProject> ProjectMock = new Mock<IProject>();
        Mock<IPeriodDate> periodDateMock = new Mock<IPeriodDate>();
        ProjectMock.Setup(p => p.ContainsDates(periodDateMock.Object)).Returns(true);

        ProjectMock.Setup(c => c.IsFinished()).Returns(true);

        Mock<ICollaborator> CollaboradorMock = new Mock<ICollaborator>();
        Mock<IPeriodDateTime> mockPeriodDateTime = new Mock<IPeriodDateTime>();

        CollaboradorMock.Setup(c => c.ContractContainsDates(mockPeriodDateTime.Object)).Returns(true);

        //assert
        ArgumentException exception = Assert.Throws<ArgumentException>(() =>
            //act
            new AssociationProjectCollaborator(periodDateMock.Object, CollaboradorMock.Object, ProjectMock.Object));

        Assert.Equal("Invalid Arguments", exception.Message);
    }

    [Fact]
    public void WhenCollaboradorDatesOutsideAssociationDates_ThenThrowException()
    {
        //arrange
        Mock<IProject> ProjectMock = new Mock<IProject>();
        Mock<IPeriodDate> periodDateMock = new Mock<IPeriodDate>();

        ProjectMock.Setup(p => p.ContainsDates(periodDateMock.Object)).Returns(true);

        ProjectMock.Setup(c => c.IsFinished()).Returns(false);

        Mock<ICollaborator> CollaboradorMock = new Mock<ICollaborator>();
        Mock<IPeriodDateTime> mockPeriodDateTime = new Mock<IPeriodDateTime>();

        CollaboradorMock.Setup(c => c.ContractContainsDates(mockPeriodDateTime.Object)).Returns(false);

        //assert
        ArgumentException exception = Assert.Throws<ArgumentException>(() =>
            //act
            new AssociationProjectCollaborator(periodDateMock.Object, CollaboradorMock.Object, ProjectMock.Object));

        Assert.Equal("Invalid Arguments", exception.Message);
    }
}
