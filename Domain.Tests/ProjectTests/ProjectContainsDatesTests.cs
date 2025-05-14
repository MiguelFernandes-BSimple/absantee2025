using Domain.Interfaces;
using Domain.Models;
using Moq;
namespace Domain.Tests.ProjectTests;

public class ProjectContainsDatesTests
{
    [Fact]
    public void WhenPassingValidData_ThenContainsDatesReturnTrue()
    {
        //arrange
        var initDate = new DateOnly(2025, 4, 1);
        var endDate = new DateOnly(2025, 4, 15);
        var periodDate = new PeriodDate(initDate, endDate);
        var project = new Project(Guid.NewGuid(), "Titulo 1", "T1", periodDate);

        var initDateExpected = new DateOnly(2025, 4, 5);
        var endDateExpected = new DateOnly(2025, 4, 10);
        var periodDateExpected = new PeriodDate(initDateExpected, endDateExpected);

        //act
        bool result = project.ContainsDates(periodDateExpected);

        //assert
        Assert.True(result);
    }

    [Fact]
    public void WhenPassingInvalidData_ThenContainsDatesReturnFalse()
    {
        //arrange
        var initDate = new DateOnly(2025, 4, 1);
        var endDate = new DateOnly(2025, 4, 15);
        var periodDate = new PeriodDate(initDate, endDate);
        var project = new Project(Guid.NewGuid(), "Titulo 1", "T1", periodDate);

        var initDateExpected = new DateOnly(2025, 5, 5);
        var endDateExpected = new DateOnly(2025, 5, 10);
        var periodDateExpected = new PeriodDate(initDateExpected, endDateExpected);

        //act
        bool result = project.ContainsDates(periodDateExpected);

        //assert
        Assert.False(result);
    }
}
