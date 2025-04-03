using Domain.Interfaces;
using Domain.Models;
using Moq;
namespace Domain.Tests.ProjectTests;

public class ContainsDates
{
    [Fact]
    public void WhenPassingValidData_ThenContainsDatesReturnTrue()
    {
        //arrange
        Mock<IPeriodDate> periodDateMock = new Mock<IPeriodDate>();
        Project project = new Project("Titulo 1", "T1", periodDateMock.Object);

        Mock<IPeriodDate> searchPeriodDateMock = new Mock<IPeriodDate>();
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
        Mock<IPeriodDate> periodDateMock = new Mock<IPeriodDate>();
        Project project = new Project("Titulo 1", "T1", periodDateMock.Object);

        Mock<IPeriodDate> searchPeriodDateMock = new Mock<IPeriodDate>();
        periodDateMock.Setup(pd => pd.Contains(searchPeriodDateMock.Object)).Returns(false);

        //act
        bool result = project.ContainsDates(searchPeriodDateMock.Object);

        //assert
        Assert.False(result);
    }
}
