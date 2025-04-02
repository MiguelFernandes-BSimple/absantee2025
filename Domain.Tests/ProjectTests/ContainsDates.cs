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
    public void WhenPassingValidData_ThenContainsDatesReturnTrue(DateOnly InitDate, DateOnly FinalDate)
    {
        //arrange
        DateOnly ProjectInitDate = DateOnly.FromDateTime(DateTime.Now);
        DateOnly ProjectFinalDate = DateOnly.FromDateTime(DateTime.Now).AddYears(1);

        Mock<IPeriodDate> periodDateMock = new Mock<IPeriodDate>();
        periodDateMock.Setup(p => p.GetInitDate()).Returns(ProjectInitDate);
        periodDateMock.Setup(p => p.GetFinalDate()).Returns(ProjectFinalDate);
        Project project = new Project("Titulo 1", "T1", periodDateMock.Object);

        Mock<IPeriodDate> searchPeriodDateMock = new Mock<IPeriodDate>();
        searchPeriodDateMock.Setup(p => p.GetInitDate()).Returns(InitDate);
        searchPeriodDateMock.Setup(p => p.GetFinalDate()).Returns(FinalDate);

        //act
        bool result = project.ContainsDates(searchPeriodDateMock.Object);

        //assert
        Assert.True(result);
    }
    public static IEnumerable<object[]> ContainsDates_InvalidCompareData()
    {
        yield return new object[] { DateOnly.FromDateTime(DateTime.Now.AddDays(-1)), DateOnly.FromDateTime(DateTime.Now.AddDays(10)) };
        yield return new object[] { DateOnly.FromDateTime(DateTime.Now.AddDays(1)), DateOnly.FromDateTime(DateTime.Now.AddYears(2)) };
    }

    [Theory]
    [MemberData(nameof(ContainsDates_InvalidCompareData))]
    public void WhenPassingInvalidData_ThenContainsDatesReturnFalse(DateOnly InitDate, DateOnly FinalDate)
    {
        //arrange
        Mock<IPeriodDate> periodDateMock = new Mock<IPeriodDate>();
        periodDateMock.Setup(p => p.GetInitDate()).Returns(InitDate);
        periodDateMock.Setup(p => p.GetFinalDate()).Returns(FinalDate);
        Project project = new Project("Titulo 1", "T1", periodDateMock.Object);

        Mock<IPeriodDate> searchPeriodDateMock = new Mock<IPeriodDate>();
        searchPeriodDateMock.Setup(p => p.GetInitDate()).Returns(InitDate);
        searchPeriodDateMock.Setup(p => p.GetFinalDate()).Returns(FinalDate);
        //act
        bool result = project.ContainsDates(searchPeriodDateMock.Object);

        //assert
        Assert.False(result);
    }
}
