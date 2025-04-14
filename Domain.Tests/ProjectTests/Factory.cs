using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using Domain.Models;
using Domain.Interfaces;
using Domain.Visitor;
using Domain.Factory;
using Domain.IRepository;

namespace Domain.Tests.ProjectTests;

public class Factory
{
    [Fact]
    public void WhenPassingValidData_ThenProjectIsCreated()
    {
        //arrange
        Mock<IProjectVisitor> projectVisitorMock = new Mock<IProjectVisitor>();
        projectVisitorMock.Setup(v => v.Id).Returns(1);
        projectVisitorMock.Setup(v => v.Title).Returns("Projeto A");
        projectVisitorMock.Setup(v => v.Acronym).Returns("PA2024");
        projectVisitorMock.Setup(v => v.PeriodDate).Returns(It.IsAny<PeriodDate>());
        Mock<IProjectRepository> ProjectRepositoryMock = new Mock<IProjectRepository>();
        var projectFactory = new ProjectFactory(ProjectRepositoryMock.Object);
        //act
        projectFactory.Create(projectVisitorMock.Object);
    }
    public static IEnumerable<object[]> ProjectData_InvalidTitle()
    {
        yield return new object[] { "", "T1" };
        yield return new object[] { new string('a', 51), "T1" };
    }

    [Theory]
    [MemberData(nameof(ProjectData_InvalidTitle))]
    public async Task WhenPassingInvalidTitle_ThenThrowException(string Title, string Acronym)
    {
        //arrange
        Mock<IPeriodDate> periodDateMock = new Mock<IPeriodDate>();
        periodDateMock.Setup(p => p.GetInitDate()).Returns(It.IsAny<DateOnly>());
        periodDateMock.Setup(p => p.GetFinalDate()).Returns(It.IsAny<DateOnly>());

        Mock<IProjectRepository> ProjectRepositoryMock = new Mock<IProjectRepository>();
        var projectFactory = new ProjectFactory(ProjectRepositoryMock.Object);

        //assert
        ArgumentException exception = await Assert.ThrowsAsync<ArgumentException>(() =>
             //act
             projectFactory.Create(1, Title, Acronym, periodDateMock.Object));

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
    public async Task WhenPassingInvalidAcronym_ThenThrowException(string Title, string Acronym)
    {
        //arrange
        Mock<IPeriodDate> periodDateMock = new Mock<IPeriodDate>();
        periodDateMock.Setup(p => p.GetInitDate()).Returns(It.IsAny<DateOnly>());
        periodDateMock.Setup(p => p.GetFinalDate()).Returns(It.IsAny<DateOnly>()); ;
        Mock<IProjectRepository> ProjectRepositoryMock = new Mock<IProjectRepository>();
        var projectFactory = new ProjectFactory(ProjectRepositoryMock.Object);


        //assert
        ArgumentException exception = await Assert.ThrowsAsync<ArgumentException>(() =>
             //act
             projectFactory.Create(1, Title, Acronym, periodDateMock.Object));

        Assert.Equal("Invalid Arguments", exception.Message);
    }
}
