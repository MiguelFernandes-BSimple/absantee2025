using Domain;
namespace Domain.Tests.ProjectTests;

public class ProjectIsFinished
{
    [Fact]
    public void WhenProjectIsFinished_ThenReturnTrue()
    {
        //arrange
        DateOnly ProjectInitDate = DateOnly.FromDateTime(DateTime.Now).AddYears(-1);
        DateOnly ProjectFinalDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-1));
        Project project = new Project("Titulo 1", "T1", ProjectInitDate, ProjectFinalDate);

        //act
        bool result = project.IsFinished();

        //assert
        Assert.True(result);
    }
}
