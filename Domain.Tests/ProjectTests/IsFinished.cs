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
        periodDateMock.Setup(p => p.IsFinalDateSmallerThan(It.IsAny<DateOnly>())).Returns(true);
        Project project = new Project(1, "Titulo 1", "T1", periodDateMock.Object);

        //act
        bool result = project.IsFinished();

        //assert
        Assert.True(result);
    }
    [Fact]
    public void WhenProjectIsNotFinished_ThenReturnFalse()
    {
        //arrange
        DateOnly ProjectInitDate = DateOnly.FromDateTime(DateTime.Now).AddYears(-1);
        Mock<IPeriodDate> periodDateMock = new Mock<IPeriodDate>();
        periodDateMock.Setup(p => p.IsFinalDateSmallerThan(It.IsAny<DateOnly>())).Returns(false);

        Project project = new Project(1, "Titulo 1", "T1", periodDateMock.Object);

        //act
        bool result = project.IsFinished();

        //assert
        Assert.False(result);
    }
}
