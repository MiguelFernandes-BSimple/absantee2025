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
        Mock<PeriodDate> periodDateMock = new Mock<PeriodDate>();
        Project project = new Project(1, "Titulo 1", "T1", periodDateMock.Object);

        Mock<PeriodDate> searchPeriodDateMock = new Mock<PeriodDate>();
        periodDateMock.Setup(pd => pd.Contains(searchPeriodDateMock.Object)).Returns(true);

        //act
        bool result = project.ContainsDates(searchPeriodDateMock.Object);

        //assert
        Assert.True(result);
    }

    [Fact]
    public void WhenPassingInvalidData_ThenContainsDatesReturnFalse()
    {
        //arrange
        Mock<PeriodDate> periodDateMock = new Mock<PeriodDate>();
        Project project = new Project(1, "Titulo 1", "T1", periodDateMock.Object);

        Mock<PeriodDate> searchPeriodDateMock = new Mock<PeriodDate>();
        periodDateMock.Setup(pd => pd.Contains(searchPeriodDateMock.Object)).Returns(false);

        //act
        bool result = project.ContainsDates(searchPeriodDateMock.Object);

        //assert
        Assert.False(result);
    }
}
