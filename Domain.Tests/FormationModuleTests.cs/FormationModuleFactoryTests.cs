using Domain.Factory;
using Domain.Interfaces;
using Domain.IRepository;
using Domain.Visitor;
using Moq;

namespace Domain.Tests.FormationModuleTests;

public class FormationModuleFactoryTests
{
    [Fact]
    public void WhenPassingValidSiglePeriod_ThenFactoryCreatesNewFormationModule()
    {

        //Arrange

        var formationPeriodDouble = new Mock<IFormationPeriod>();

        var subjectDouble = new Mock<IFormationSubject>();
        subjectDouble.Setup(c => c.GetId()).Returns(1);

        var periodsList = new List<IFormationPeriod> { formationPeriodDouble.Object };

        var subjectRepoDouble = new Mock<IFormationSubjectRepository>();
        subjectRepoDouble.Setup(cr => cr.GetById(1)).Returns(subjectDouble.Object);

        var formationModuleFactory = new FormationModuleFactory(subjectRepoDouble.Object);

        //Act
        var result = formationModuleFactory.Create(1, periodsList);

        //Assert
        Assert.NotNull(result);
    }

    [Fact]
    public void WhenPassingValidMultiplePeriods_ThenFactoryCreatesNewFormationModule()
    {
        //Arrange
        var formationPeriodDouble1 = new Mock<IFormationPeriod>();
        var formationPeriodDouble2 = new Mock<IFormationPeriod>();

        var subjecDouble = new Mock<IFormationSubject>();
        subjecDouble.Setup(c => c.GetId()).Returns(1);

        var periodsList = new List<IFormationPeriod> { formationPeriodDouble1.Object, formationPeriodDouble2.Object };

        var subjectRepoDouble = new Mock<IFormationSubjectRepository>();
        subjectRepoDouble.Setup(cr => cr.GetById(1)).Returns(subjecDouble.Object);

        var formationModuleFactory = new FormationModuleFactory(subjectRepoDouble.Object);

        //Act
        var result = formationModuleFactory.Create(1, periodsList);

        //Arrange
        Assert.NotNull(result);
    }

    [Fact]
    public void WhenPassingSubjectThatDoesNotExist_ThenShouldThrowArgumentException()
    {
        //Arrange

        var formationPeriodDouble = new Mock<IFormationPeriod>();
        var periodList = new List<IFormationPeriod> { formationPeriodDouble.Object };

        var subjectRepoDouble = new Mock<IFormationSubjectRepository>();
        subjectRepoDouble.Setup(cr => cr.GetById(1)).Returns((IFormationSubject?)null);

        var formationModuleFactory = new FormationModuleFactory(subjectRepoDouble.Object);

        //Assert
        var exception = Assert.Throws<ArgumentException>(() =>
            //Act
            formationModuleFactory.Create(1, periodList));

        Assert.Equal("Formation subject does not exist.", exception.Message);

    }

    [Fact]
    public void WhenPassingDataModel_ThenFactoryCreatesNewFormationDouble()
    {
        //Arrange
        var formationModuleVisitorDouble = new Mock<IFormationModuleVisitor>();

        var subjectRepoDouble = new Mock<IFormationSubjectRepository>();

        var formationModuleFactory = new FormationModuleFactory(subjectRepoDouble.Object);

        //Act
        var result = formationModuleFactory.Create(formationModuleVisitorDouble.Object);

        //Assert
        Assert.NotNull(result);
    }
}