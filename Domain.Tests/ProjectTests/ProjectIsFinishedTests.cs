using Domain.Interfaces;
using Domain.Models;
using Moq;
namespace Domain.Tests.ProjectTests;

public class ProjectIsFinishedTests
{
    [Fact]
    public void WhenProjectIsFinished_ThenReturnTrue()
    {
        //arrange
        var projectInitDate = DateOnly.FromDateTime(DateTime.Now).AddYears(-1);
        var projectFinalDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-1));
        var periodDate = new PeriodDate(projectInitDate, projectFinalDate);

        var project = new Project(Guid.NewGuid(), "Titulo 1", "T1", periodDate);

        //act
        bool result = project.IsFinished();

        //assert
        Assert.True(result);
    }

    [Fact]
    public void WhenProjectIsNotFinished_ThenReturnFalse()
    {
        //arrange
        var projectInitDate = DateOnly.FromDateTime(DateTime.Now).AddYears(-1);
        var projectFinalDate = DateOnly.FromDateTime(DateTime.Now.AddDays(10));
        var periodDate = new PeriodDate(projectInitDate, projectFinalDate);

        var project = new Project(Guid.NewGuid(), "Titulo 1", "T1", periodDate);

        //act
        bool result = project.IsFinished();

        //assert
        Assert.False(result);
    }
}
