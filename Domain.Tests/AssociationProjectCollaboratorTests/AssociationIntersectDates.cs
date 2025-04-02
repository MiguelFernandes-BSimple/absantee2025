namespace Domain.Tests.AssociationProjectCollaboratorTests;

using Moq;
using Domain.Interfaces;
using Domain.Models;
public class AssociationIntersectDates
{
    public static IEnumerable<object[]> ValidIntersectDates()
    {
        yield return new object[] { new DateOnly(2020, 1, 1), new DateOnly(2020, 12, 31) };
        yield return new object[] { new DateOnly(2019, 1, 1), new DateOnly(2020, 1, 1) };
        yield return new object[] { new DateOnly(2020, 12, 31), new DateOnly(2021, 12, 31) };
    }

    [Theory]
    [MemberData(nameof(ValidIntersectDates))]
    public void WhenAssociationIntersectDatesReceivesValidaData_ReturnTrue(DateOnly InitDate, DateOnly FinalDate)
    {
        //arrange
        DateOnly initDateAssoc = new DateOnly(2020, 1, 1);
        DateOnly finalDateAssoc = new DateOnly(2020, 12, 31);

        Mock<IProject> ProjectMock = new Mock<IProject>();
        Mock<IPeriodDate> periodDateMock = new Mock<IPeriodDate>();

        ProjectMock.Setup(p => p.ContainsDates(periodDateMock.Object)).Returns(true);

        ProjectMock.Setup(c => c.IsFinished()).Returns(false);

        Mock<ICollaborator> CollaboradorMock = new Mock<ICollaborator>();
        Mock<IPeriodDateTime> mockPeriodDateTime = new Mock<IPeriodDateTime>();

        CollaboradorMock.Setup(c => c.ContractContainsDates(mockPeriodDateTime.Object)).Returns(true);
        Mock<IPeriodDate> secondPeriodDateMock = new Mock<IPeriodDate>();
        secondPeriodDateMock.Setup(p => p.GetInitDate()).Returns(initDateAssoc);
        secondPeriodDateMock.Setup(p => p.GetInitDate()).Returns(finalDateAssoc);

        Mock<IPeriodDate> thirdPeriodDateMock = new Mock<IPeriodDate>();
        thirdPeriodDateMock.Setup(p => p.GetInitDate()).Returns(InitDate);
        thirdPeriodDateMock.Setup(p => p.GetInitDate()).Returns(FinalDate);

        var assocProjCollab = new AssociationProjectCollaborator(secondPeriodDateMock.Object, CollaboradorMock.Object, ProjectMock.Object);

        //act
        bool result = assocProjCollab.AssociationIntersectPeriod(thirdPeriodDateMock.Object);
        //assert
        Assert.True(result);
    }

    public static IEnumerable<object[]> InvalidIntersectDates()
    {
        yield return new object[] { new DateOnly(2019, 1, 1), new DateOnly(2019, 12, 31) };
        yield return new object[] { new DateOnly(2021, 1, 1), new DateOnly(2022, 1, 1) };

    }

    [Theory]
    [MemberData(nameof(InvalidIntersectDates))]
    public void WhenAssociationIntersectDatesReceivesValidaData_ReturnFalse(DateOnly InitDate, DateOnly FinalDate)
    {
        //arrange
        DateOnly initDateAssoc = new DateOnly(2020, 1, 1);
        DateOnly finalDateAssoc = new DateOnly(2020, 12, 31);

        Mock<IProject> ProjectMock = new Mock<IProject>();
        Mock<IPeriodDate> periodDateMock = new Mock<IPeriodDate>();

        ProjectMock.Setup(p => p.ContainsDates(periodDateMock.Object)).Returns(true);

        ProjectMock.Setup(c => c.IsFinished()).Returns(false);

        Mock<ICollaborator> CollaboradorMock = new Mock<ICollaborator>();
        Mock<IPeriodDateTime> mockPeriodDateTime = new Mock<IPeriodDateTime>();

        CollaboradorMock.Setup(c => c.ContractContainsDates(mockPeriodDateTime.Object)).Returns(true);

        Mock<IPeriodDate> secondPeriodDateMock = new Mock<IPeriodDate>();
        secondPeriodDateMock.Setup(p => p.GetInitDate()).Returns(initDateAssoc);
        secondPeriodDateMock.Setup(p => p.GetInitDate()).Returns(finalDateAssoc);

        Mock<IPeriodDate> thirdPeriodDateMock = new Mock<IPeriodDate>();
        thirdPeriodDateMock.Setup(p => p.GetInitDate()).Returns(InitDate);
        thirdPeriodDateMock.Setup(p => p.GetInitDate()).Returns(FinalDate);

        var assocProjCollab = new AssociationProjectCollaborator(secondPeriodDateMock.Object, CollaboradorMock.Object, ProjectMock.Object);

        //act
        bool result = assocProjCollab.AssociationIntersectPeriod(thirdPeriodDateMock.Object);
        //assert
        Assert.False(result);
    }
}
