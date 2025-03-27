using Domain;
using Moq;

public class AssociationProjectCollaboratorTest
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
        CollaboradorMock.Setup(c => c.ContractContainsDates(It.IsAny<DateTime>(), It.IsAny<DateTime>())).Returns(true);

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
        CollaboradorMock.Setup(c => c.ContractContainsDates(It.IsAny<DateTime>(), It.IsAny<DateTime>())).Returns(true);

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
        CollaboradorMock.Setup(c => c.ContractContainsDates(It.IsAny<DateTime>(), It.IsAny<DateTime>())).Returns(true);

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
        CollaboradorMock.Setup(c => c.ContractContainsDates(It.IsAny<DateTime>(), It.IsAny<DateTime>())).Returns(false);


        //assert
        ArgumentException exception = Assert.Throws<ArgumentException>(() =>
            //act
            new AssociationProjectCollaborator(initDate, finalDate, CollaboradorMock.Object, ProjectMock.Object));

        Assert.Equal("Invalid Arguments", exception.Message);
    }

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

    public static IEnumerable<object[]> ValidIntersectDates()
    {
        yield return new object[] { new DateOnly(2020, 1, 1), new DateOnly(2020,12,31) };
        yield return new object[] { new DateOnly(2019, 1, 1), new DateOnly(2020, 1, 1) };
        yield return new object[] { new DateOnly(2020,12,31), new DateOnly(2021,12,31) };
    }

    [Theory]
    [MemberData(nameof(ValidIntersectDates))]
    public void WhenAssociationIntersectDatesReceivesValidaData_ReturnTrue(DateOnly InitDate, DateOnly FinalDate)
    {
        //arrange
        DateOnly initDateAssoc = new DateOnly(2020, 1, 1);
        DateOnly finalDateAssoc = new DateOnly(2020,12,31);

        Mock<IProject> ProjectMock = new Mock<IProject>();
        ProjectMock.Setup(p => p.ContainsDates(It.IsAny<DateOnly>(), It.IsAny<DateOnly>())).Returns(true);

        ProjectMock.Setup(c => c.IsFinished()).Returns(false);

        Mock<ICollaborator> CollaboradorMock = new Mock<ICollaborator>();
        CollaboradorMock.Setup(c => c.ContractContainsDates(It.IsAny<DateTime>(), It.IsAny<DateTime>())).Returns(true);

        var assocProjCollab = new AssociationProjectCollaborator(initDateAssoc, finalDateAssoc, CollaboradorMock.Object, ProjectMock.Object);

        //act
        bool result = assocProjCollab.AssociationIntersectDates(InitDate, FinalDate);
        //assert
        Assert.True(result);
    }

    public static IEnumerable<object[]> InvalidIntersectDates()
    {
        yield return new object[] { new DateOnly(2019, 1, 1), new DateOnly(2019,12,31) };
        yield return new object[] { new DateOnly(2021, 1, 1), new DateOnly(2022, 1, 1) };

    }

    [Theory]
    [MemberData(nameof(InvalidIntersectDates))]
    public void WhenAssociationIntersectDatesReceivesValidaData_ReturnFalse(DateOnly InitDate, DateOnly FinalDate)
    {
        //arrange
        DateOnly initDateAssoc = new DateOnly(2020, 1, 1);
        DateOnly finalDateAssoc = new DateOnly(2020,12,31);

        Mock<IProject> ProjectMock = new Mock<IProject>();
        ProjectMock.Setup(p => p.ContainsDates(It.IsAny<DateOnly>(), It.IsAny<DateOnly>())).Returns(true);

        ProjectMock.Setup(c => c.IsFinished()).Returns(false);

        Mock<ICollaborator> CollaboradorMock = new Mock<ICollaborator>();
        CollaboradorMock.Setup(c => c.ContractContainsDates(It.IsAny<DateTime>(), It.IsAny<DateTime>())).Returns(true);

        var assocProjCollab = new AssociationProjectCollaborator(initDateAssoc, finalDateAssoc, CollaboradorMock.Object, ProjectMock.Object);

        //act
        bool result = assocProjCollab.AssociationIntersectDates(InitDate, FinalDate);
        //assert
        Assert.False(result);
    }

}