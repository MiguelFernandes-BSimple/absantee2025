namespace Domain.Tests.AssociationProjectCollaboratorTests;

using Moq;
using Domain.Interfaces;
using Domain.Models;

public class Constructor
{
    public static IEnumerable<object[]> ValidDates()
    {
        yield return new object[] { DateOnly.FromDateTime(DateTime.Now), DateOnly.FromDateTime(DateTime.Now).AddYears(1) };
        yield return new object[] { DateOnly.FromDateTime(DateTime.Today), DateOnly.FromDateTime(DateTime.Today) };
    }

    [Theory]
    [MemberData(nameof(ValidDates))]
    public void WhenPassingValidData_ThenAssociationProjectCollaboratorIsCreated(DateOnly initDate, DateOnly finalDate)
    {
        //arrange
        Mock<IProject> ProjectMock = new Mock<IProject>();
        ProjectMock.Setup(p => p.ContainsDates(It.IsAny<DateOnly>(), It.IsAny<DateOnly>())).Returns(true);

        ProjectMock.Setup(c => c.IsFinished()).Returns(false);

        Mock<ICollaborator> CollaboradorMock = new Mock<ICollaborator>();
        Mock<IPeriodDateTime> mockPeriodDateTime = new Mock<IPeriodDateTime>();

        CollaboradorMock.Setup(c => c.ContractContainsDates(mockPeriodDateTime.Object)).Returns(true);

        //act
        new AssociationProjectCollaborator(initDate, finalDate, CollaboradorMock.Object, ProjectMock.Object);

        //assert
    }


    [Fact]
    public void WhenAssociationDatesAreInvalid_ThenThrowException()
    {
        //arrange
        DateOnly initDate = DateOnly.FromDateTime(DateTime.Now).AddYears(2);
        DateOnly finalDate = DateOnly.FromDateTime(DateTime.Now).AddYears(1);
        Mock<IProject> ProjectMock = new Mock<IProject>();

        Mock<ICollaborator> CollaboradorMock = new Mock<ICollaborator>();

        //assert
        ArgumentException exception = Assert.Throws<ArgumentException>(() =>
            //act
            new AssociationProjectCollaborator(initDate, finalDate, CollaboradorMock.Object, ProjectMock.Object));

        Assert.Equal("Invalid Arguments", exception.Message);
    }


    [Fact]
    public void WhenProjectDatesOutsideAssociation_ThenThrowException()
    {
        //arrange
        DateOnly initDate = DateOnly.FromDateTime(DateTime.Now);
        DateOnly finalDate = DateOnly.FromDateTime(DateTime.Now).AddYears(1);

        Mock<IProject> ProjectMock = new Mock<IProject>();
        ProjectMock.Setup(p => p.ContainsDates(It.IsAny<DateOnly>(), It.IsAny<DateOnly>())).Returns(false);

        ProjectMock.Setup(c => c.IsFinished()).Returns(false);

        Mock<ICollaborator> CollaboradorMock = new Mock<ICollaborator>();
        Mock<IPeriodDateTime> mockPeriodDateTime = new Mock<IPeriodDateTime>();

        CollaboradorMock.Setup(c => c.ContractContainsDates(mockPeriodDateTime.Object)).Returns(true);

        //assert
        ArgumentException exception = Assert.Throws<ArgumentException>(() =>
            //act
            new AssociationProjectCollaborator(initDate, finalDate, CollaboradorMock.Object, ProjectMock.Object));

        Assert.Equal("Invalid Arguments", exception.Message);
    }

    [Fact]
    public void WhenProjectIsFinished_ThenThrowException()
    {
        //arrange
        DateOnly initDate = DateOnly.FromDateTime(DateTime.Now);
        DateOnly finalDate = DateOnly.FromDateTime(DateTime.Now.AddYears(1));

        Mock<IProject> ProjectMock = new Mock<IProject>();
        ProjectMock.Setup(p => p.ContainsDates(It.IsAny<DateOnly>(), It.IsAny<DateOnly>())).Returns(true);

        ProjectMock.Setup(c => c.IsFinished()).Returns(true);

        Mock<ICollaborator> CollaboradorMock = new Mock<ICollaborator>();
        Mock<IPeriodDateTime> mockPeriodDateTime = new Mock<IPeriodDateTime>();

        CollaboradorMock.Setup(c => c.ContractContainsDates(mockPeriodDateTime.Object)).Returns(true);

        //assert
        ArgumentException exception = Assert.Throws<ArgumentException>(() =>
            //act
            new AssociationProjectCollaborator(initDate, finalDate, CollaboradorMock.Object, ProjectMock.Object));

        Assert.Equal("Invalid Arguments", exception.Message);
    }

    [Fact]
    public void WhenCollaboradorDatesOutsideAssociationDates_ThenThrowException()
    {
        //arrange
        DateOnly initDate = DateOnly.FromDateTime(DateTime.Now);
        DateOnly finalDate = DateOnly.FromDateTime(DateTime.Now.AddYears(1));

        Mock<IProject> ProjectMock = new Mock<IProject>();
        ProjectMock.Setup(p => p.ContainsDates(It.IsAny<DateOnly>(), It.IsAny<DateOnly>())).Returns(true);

        ProjectMock.Setup(c => c.IsFinished()).Returns(false);

        Mock<ICollaborator> CollaboradorMock = new Mock<ICollaborator>();
        Mock<IPeriodDateTime> mockPeriodDateTime = new Mock<IPeriodDateTime>();

        CollaboradorMock.Setup(c => c.ContractContainsDates(mockPeriodDateTime.Object)).Returns(false);


        //assert
        ArgumentException exception = Assert.Throws<ArgumentException>(() =>
            //act
            new AssociationProjectCollaborator(initDate, finalDate, CollaboradorMock.Object, ProjectMock.Object));

        Assert.Equal("Invalid Arguments", exception.Message);
    }
}
