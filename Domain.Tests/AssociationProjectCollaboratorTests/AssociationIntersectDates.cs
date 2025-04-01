namespace Domain.Tests.AssociationProjectCollaboratorTests;

using Moq;

public class AssociationIntersectDates {
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
