using Domain.Interfaces;
using Domain.Models;
using Moq;
namespace Domain.Tests.ProjectTests;

public class IsFinished
{
    [Fact]
    public void WhenProjectIsFinished_ThenReturnTrue()
    {
        //arrange
        DateOnly ProjectInitDate = DateOnly.FromDateTime(DateTime.Now).AddYears(-1);
        DateOnly ProjectFinalDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-1));

        Mock<IPeriodDate> periodDateMock = new Mock<IPeriodDate>();
        periodDateMock.Setup(p => p.GetInitDate()).Returns(ProjectInitDate);
        periodDateMock.Setup(p => p.GetFinalDate()).Returns(ProjectFinalDate);
        Project project = new Project("Titulo 1", "T1", periodDateMock.Object);

        //act
        bool result = project.IsFinished();

        //assert
        Assert.True(result);
    }
    public static IEnumerable<object[]> ProjectEndDate_NotFinished()
    {
        yield return new object[] { DateOnly.FromDateTime(DateTime.Now) };
        yield return new object[] { DateOnly.FromDateTime(DateTime.Now.AddDays(1)) };
    }
    [Theory]
    [MemberData(nameof(ProjectEndDate_NotFinished))]
    public void WhenProjectIsNotFinished_ThenReturnFalse(DateOnly ProjectFinalDate)
    {
        //arrange
        DateOnly ProjectInitDate = DateOnly.FromDateTime(DateTime.Now).AddYears(-1);
        Mock<IPeriodDate> periodDateMock = new Mock<IPeriodDate>();
        periodDateMock.Setup(p => p.GetInitDate()).Returns(ProjectInitDate);
        periodDateMock.Setup(p => p.GetFinalDate()).Returns(ProjectFinalDate);
        Project project = new Project("Titulo 1", "T1", periodDateMock.Object);

        //act
        bool result = project.IsFinished();

        //assert
        Assert.False(result);
    }
}
