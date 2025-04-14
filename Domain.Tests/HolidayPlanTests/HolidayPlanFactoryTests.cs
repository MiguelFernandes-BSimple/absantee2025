using Domain.Factory;
using Domain.Interfaces;
using Domain.IRepository;
using Domain.Models;
using Domain.Visitor;
using Moq;

public class HolidayPlanFactoryTests
{
    [Fact]
    public void WhenPassingValidSiglePeriod_ThenFactoryCreatesNewHolidayPlan()
    {

        //Arrange

        var holidayPeriodDouble = new Mock<IHolidayPeriod>();

        var collabDouble = new Mock<ICollaborator>();
        collabDouble.Setup(c => c.GetId()).Returns(1);

        var periodsList = new List<IHolidayPeriod> { holidayPeriodDouble.Object };

        var collabRepoDouble = new Mock<ICollaboratorRepository>();
        collabRepoDouble.Setup(cr => cr.GetById(1)).Returns(collabDouble.Object);

        var holidayPlanFactory = new HolidayPlanFactory(collabRepoDouble.Object);

        //Act
        var result = holidayPlanFactory.Create(1, periodsList);

        //Assert
        Assert.NotNull(result);
    }

    [Fact]
    public void WhenPassingValidMultiplePeriods_ThenFactoryCreatesNewHolidayPlan()
    {
        //Arrange
        var holidayPeriodDouble1 = new Mock<IHolidayPeriod>();
        var holidayPeriodDouble2 = new Mock<IHolidayPeriod>();

        var collabDouble = new Mock<ICollaborator>();
        collabDouble.Setup(c => c.GetId()).Returns(1);

        var periodsList = new List<IHolidayPeriod> { holidayPeriodDouble1.Object, holidayPeriodDouble2.Object };

        var collabRepoDouble = new Mock<ICollaboratorRepository>();
        collabRepoDouble.Setup(cr => cr.GetById(1)).Returns(collabDouble.Object);

        var holidayPlanFactory = new HolidayPlanFactory(collabRepoDouble.Object);

        //Act
        var result = holidayPlanFactory.Create(1, periodsList);

        //Arrange
        Assert.NotNull(result);
    }

    [Fact]
    public void WhenPassingCollaboratorThatDoesNotExist_ThenShouldThrowArgumentException()
    {
        //Arrange

        var holidayPeriodDouble1 = new Mock<IHolidayPeriod>();
        var periodList = new List<IHolidayPeriod> { holidayPeriodDouble1.Object };

        var collabRepoDouble = new Mock<ICollaboratorRepository>();
        collabRepoDouble.Setup(cr => cr.GetById(1)).Returns((ICollaborator?)null);

        var holidayPlanFactory = new HolidayPlanFactory(collabRepoDouble.Object);

        //Assert
        var exception = Assert.Throws<ArgumentException>(() =>
            //Act
            holidayPlanFactory.Create(1, periodList));

        Assert.Equal("Collaborator doesn't exist.", exception.Message);

    }

    [Fact]
    public void WhenPassingDataModel_ThenFactoryCreatesNewHolidayPlan()
    {
        //Arrange
        var holidayPlanVisitorDouble = new Mock<IHolidayPlanVisitor>();

        var collabRepoDouble = new Mock<ICollaboratorRepository>();

        var holidayPlanFactory = new HolidayPlanFactory(collabRepoDouble.Object);

        //Act
        var result = holidayPlanFactory.Create(holidayPlanVisitorDouble.Object);

        //Assert
        Assert.NotNull(result);
    }
}