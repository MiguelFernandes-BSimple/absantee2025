using Domain.Interfaces;
using Domain.Models;
using Moq;
namespace Domain.Tests.ProjectTests;

public class ContainsDates
{
    public static IEnumerable<object[]> ContainsDates_ValidCompareData()
    {
        yield return new object[] { DateOnly.FromDateTime(DateTime.Now), DateOnly.FromDateTime(DateTime.Now).AddYears(1) };
        yield return new object[] { DateOnly.FromDateTime(DateTime.Now).AddDays(1), DateOnly.FromDateTime(DateTime.Now).AddDays(10) };
    }

    [Theory]
    [MemberData(nameof(ContainsDates_ValidCompareData))]
    public void WhenPassingValidData_ThenContainsDatesReturnTrue(DateOnly initDate, DateOnly finalDate)
    {
        //arrange
        DateOnly ProjectInitDate = initDate;
        DateOnly ProjectFinalDate = finalDate;

        Mock<IPeriodDate> searchPeriodDateMock = new Mock<IPeriodDate>();

        Mock<IPeriodDate> periodDateMock = new Mock<IPeriodDate>();
        periodDateMock.Setup(pd => pd.Contains(searchPeriodDateMock.Object)).Returns(true);
        Project project = new Project("Titulo 1", "T1", periodDateMock.Object);



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
