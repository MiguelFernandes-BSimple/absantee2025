using Domain.Interfaces;
using Domain.Models;
using Moq;
namespace Domain.Tests.ProjectTests;
public class Constructor
{
    public static IEnumerable<object[]> ProjectData_ValidData()
    {
        yield return new object[] { "Titulo 1", "T1" };
        yield return new object[] { "Projeto_Academia_2024", "PA2024" };
        yield return new object[] { "Projeto-Academia-2024(2)", "PA2024V2" };
        yield return new object[] { "Projeto-Academia-2024(3)", "PA2024V3" };
    }

    [Theory]
    [MemberData(nameof(ProjectData_ValidData))]
    public void WhenPassingValidData_ThenProjectIsCreated(string Title, string Acronym)
    {
        //arrange
        DateOnly initDate = DateOnly.FromDateTime(DateTime.Now);
        DateOnly finalDate = DateOnly.FromDateTime(DateTime.Now.AddDays(1));
        Mock<IPeriodDate> periodDateMock = new Mock<IPeriodDate>();
        periodDateMock.Setup(p => p.GetInitDate()).Returns(initDate);
        periodDateMock.Setup(p => p.GetFinalDate()).Returns(finalDate);


        //act
        new Project(Title, Acronym, periodDateMock.Object);
    }
    public static IEnumerable<object[]> ProjectData_InvalidTitle()
    {
        yield return new object[] { "", "T1" };
        yield return new object[] { new string('a', 51), "T1" };
    }

    [Theory]
    [MemberData(nameof(ProjectData_InvalidTitle))]
    public void WhenPassingInvalidTitle_ThenThrowException(string Title, string Acronym)
    {
        //arrange
        DateOnly initDate = DateOnly.FromDateTime(DateTime.Now);
        DateOnly finalDate = DateOnly.FromDateTime(DateTime.Now.AddDays(1));
        Mock<IPeriodDate> periodDateMock = new Mock<IPeriodDate>();
        periodDateMock.Setup(p => p.GetInitDate()).Returns(initDate);
        periodDateMock.Setup(p => p.GetFinalDate()).Returns(finalDate);

        //assert
        ArgumentException exception = Assert.Throws<ArgumentException>(() =>
            //act
            new Project(Title, Acronym, periodDateMock.Object));

        Assert.Equal("Invalid Arguments", exception.Message);
    }

    public static IEnumerable<object[]> ProjectData_InvalidAcronym()
    {
        yield return new object[] { "Titulo 1", "T_1" };
        yield return new object[] { "Projeto_Academia_2024", "" };
        yield return new object[] { "Projeto_Academia_2024_", new string('A', 11) };
        yield return new object[] { "Projeto_Academia_2024_", "pa2024" };
    }

    [Theory]
    [MemberData(nameof(ProjectData_InvalidAcronym))]
    public void WhenPassingInvalidAcronym_ThenThrowException(string Title, string Acronym)
    {
        //arrange
        DateOnly initDate = DateOnly.FromDateTime(DateTime.Now);
        DateOnly finalDate = DateOnly.FromDateTime(DateTime.Now.AddDays(1));
        Mock<IPeriodDate> periodDateMock = new Mock<IPeriodDate>();
        periodDateMock.Setup(p => p.GetInitDate()).Returns(initDate);
        periodDateMock.Setup(p => p.GetFinalDate()).Returns(finalDate);

        //assert
        ArgumentException exception = Assert.Throws<ArgumentException>(() =>
            //act
            new Project(Title, Acronym, periodDateMock.Object));

        Assert.Equal("Invalid Arguments", exception.Message);
    }

}
