using AutoMapper;
using AutoMapper.Configuration.Annotations;
using Domain.Factory;
using Domain.Interfaces;
using Domain.IRepository;
using Domain.Models;
using Domain.Visitor;
using Moq;

namespace Domain.Tests.HolidayPlanTests;

public class HolidayPlanFactoryTests
{
    [Fact]
    public void WhenPassingValidSiglePeriod_ThenFactoryCreatesNewHolidayPlan()
    {

        //Arrange
        var periodDate1 = new PeriodDate();

        var collabDouble = new Mock<Collaborator>();
        collabDouble.Setup(c => c.Id).Returns(It.IsAny<Guid>());

        var periodsList = new List<PeriodDate> { periodDate1 };

        var collabRepoDouble = new Mock<ICollaboratorRepository>();
        collabRepoDouble.Setup(cr => cr.GetById(It.IsAny<Guid>())).Returns(collabDouble.Object);

        var holidayPlanRepoDouble = new Mock<IHolidayPlanRepository>();
        holidayPlanRepoDouble.Setup(repo => repo.CanInsertHolidayPlan(It.IsAny<Guid>())).ReturnsAsync(true);

        var mapperDouble = new Mock<IMapper>();

        var holidayPeriod = new Mock<HolidayPeriod>();
        holidayPeriod.Setup(hp => hp.Intersects(It.IsAny<PeriodDate>())).Returns(false);

        var holidayPeriodFactoryDouble = new Mock<HolidayPeriodFactory>();
        holidayPeriodFactoryDouble.Setup(f => f.CreateWithoutHolidayPlan(collabDouble.Object, 
                It.IsAny<DateOnly>(), It.IsAny<DateOnly>())).Returns(holidayPeriod.Object);

        var holidayPlanFactory = new HolidayPlanFactory(collabRepoDouble.Object, 
                    holidayPlanRepoDouble.Object, mapperDouble.Object, holidayPeriodFactoryDouble.Object);

        //Act
        var result = holidayPlanFactory.Create(It.IsAny<Guid>(), periodsList);

        //Assert
        Assert.NotNull(result);
    }

    [Fact]
    public void WhenPassingValidMultiplePeriods_ThenFactoryCreatesNewHolidayPlan()
    {
        //Arrange
        var periodDate1 = new PeriodDate();
        var periodDate2 = new PeriodDate();

        var collabDouble = new Mock<Collaborator>();
        collabDouble.Setup(c => c.Id).Returns(It.IsAny<Guid>());

        var periodsList = new List<PeriodDate> { periodDate1, periodDate2 };

        var collabRepoDouble = new Mock<ICollaboratorRepository>();
        collabRepoDouble.Setup(cr => cr.GetById(It.IsAny<Guid>())).Returns(collabDouble.Object);

        var holidayPlanRepoDouble = new Mock<IHolidayPlanRepository>();

        var mapperDouble = new Mock<IMapper>();

        var holidayPeriodFactoryDouble = new Mock<HolidayPeriodFactory>();

        var holidayPlanFactory = new HolidayPlanFactory(collabRepoDouble.Object, 
                    holidayPlanRepoDouble.Object, mapperDouble.Object, holidayPeriodFactoryDouble.Object);

        //Act
        var result = holidayPlanFactory.Create(It.IsAny<Guid>(), periodsList);

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