// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using Moq;
// using Domain.Models;
// using Domain.Interfaces;
// using Domain.Visitor;
// using Domain.Factory;
// using Domain.IRepository;

// namespace Domain.Tests.ProjectTests;

// public class ProjectFactoryTests
// {
//     [Fact]
//     public void WhenPassingVisitor_ThenProjectIsCreated()
//     {
//         //arrange
//         Mock<IProjectVisitor> projectVisitorDouble = new Mock<IProjectVisitor>();
//         projectVisitorDouble.Setup(v => v.Id).Returns(1);
//         projectVisitorDouble.Setup(v => v.Title).Returns("Projeto A");
//         projectVisitorDouble.Setup(v => v.Acronym).Returns("PA2024");
//         projectVisitorDouble.Setup(v => v.PeriodDate).Returns(It.IsAny<PeriodDate>());

//         Mock<IProjectRepository> ProjectRepositoryDouble = new Mock<IProjectRepository>();
//         var projectFactory = new ProjectFactory(ProjectRepositoryDouble.Object);

//         //act
//         var result = projectFactory.Create(projectVisitorDouble.Object);

//         // assert
//         Assert.NotNull(result);
//     }

//     [Fact]
//     public async Task WhenPassingValidParameters_ThenCreatesProject()
//     {
//         // arrange
//         var projectRepoDouble = new Mock<IProjectRepository>();
//         projectRepoDouble.Setup(prd => prd.CheckIfAcronymIsUnique(It.IsAny<string>())).ReturnsAsync(true);
//         var projectFactory = new ProjectFactory(projectRepoDouble.Object);

//         // act
//         await projectFactory.Create("something", "SMT", It.IsAny<PeriodDate>());
//     }

//     [Fact]
//     public async Task WhenPassinSameAcronym_ThenThrowsArgumentException()
//     {
//         // arrange
//         var projectRepoDouble = new Mock<IProjectRepository>();
//         projectRepoDouble.Setup(prd => prd.CheckIfAcronymIsUnique(It.IsAny<string>()))
//                          .ReturnsAsync(false);

//         var projectFactory = new ProjectFactory(projectRepoDouble.Object);

//         // act & assert
//         ArgumentException exception = await Assert.ThrowsAsync<ArgumentException>(() =>
//             projectFactory.Create("Projeto Teste", "ABC123", It.IsAny<PeriodDate>()));

//         Assert.Equal("Invalid Arguments", exception.Message);
//     }
// }
