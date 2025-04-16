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
        Mock<IProjectVisitor> projectVisitorDouble = new Mock<IProjectVisitor>();
        projectVisitorDouble.Setup(v => v.Id).Returns(1);
        projectVisitorDouble.Setup(v => v.Title).Returns("Projeto A");
        projectVisitorDouble.Setup(v => v.Acronym).Returns("PA2024");
        projectVisitorDouble.Setup(v => v.PeriodDate).Returns(It.IsAny<PeriodDate>());

        Mock<IProjectRepository> ProjectRepositoryDouble = new Mock<IProjectRepository>();
        var projectFactory = new ProjectFactory(ProjectRepositoryDouble.Object);

        //act
        var result = projectFactory.Create(projectVisitorDouble.Object);

        // assert
        Assert.NotNull(result);
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
        Mock<IPeriodDate> periodDateDouble = new Mock<IPeriodDate>();
        periodDateDouble.Setup(p => p.GetInitDate()).Returns(It.IsAny<DateOnly>());
        periodDateDouble.Setup(p => p.GetFinalDate()).Returns(It.IsAny<DateOnly>());

        Mock<IProjectRepository> ProjectRepositoryDouble = new Mock<IProjectRepository>();
        var projectFactory = new ProjectFactory(ProjectRepositoryDouble.Object);

        //assert
        ArgumentException exception = await Assert.ThrowsAsync<ArgumentException>(() =>
             //act
             projectFactory.Create(1, Title, Acronym, periodDateDouble.Object));

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
        Mock<IPeriodDate> periodDateDouble = new Mock<IPeriodDate>();
        periodDateDouble.Setup(p => p.GetInitDate()).Returns(It.IsAny<DateOnly>());
        periodDateDouble.Setup(p => p.GetFinalDate()).Returns(It.IsAny<DateOnly>()); ;
        Mock<IProjectRepository> ProjectRepositoryDouble = new Mock<IProjectRepository>();
        var projectFactory = new ProjectFactory(ProjectRepositoryDouble.Object);


        //assert
        ArgumentException exception = await Assert.ThrowsAsync<ArgumentException>(() =>
             //act
             projectFactory.Create(1, Title, Acronym, periodDateDouble.Object));

        Assert.Equal("Invalid Arguments", exception.Message);
    }

    [Fact]
    public async Task WhenPassinSameAcronym_ThenThrowsArgumentException()
    {
        // arrange
        var projectRepoDouble = new Mock<IProjectRepository>();
        projectRepoDouble.Setup(prd => prd.CheckIfAcronymIsUnique(It.IsAny<string>()))
                         .ReturnsAsync(false);

        var projectFactory = new ProjectFactory(projectRepoDouble.Object);

        // act & assert
        ArgumentException exception = await Assert.ThrowsAsync<ArgumentException>(() =>
            projectFactory.Create(1, "Projeto Teste", "ABC123", It.IsAny<IPeriodDate>()));

        Assert.Equal("Invalid Arguments", exception.Message);
    }
}
