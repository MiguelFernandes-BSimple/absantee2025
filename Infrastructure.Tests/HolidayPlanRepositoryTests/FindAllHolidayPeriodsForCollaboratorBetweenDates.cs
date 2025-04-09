using Infrastructure.Repositories;
using Domain.Interfaces;
using Moq;

namespace Infrastructure.Tests.HolidayPlanRepositoryTests;

public class FindAllHolidayPeriodsForCollaboratorBetweenDates
{
    [Fact]
    public void WhenPassinValidDates_ThenReturnsCorrectPeriod()
    {
        // Arrange
        var collaborator = new Mock<ICollaborator>();

        var holidayPeriod = new Mock<IHolidayPeriod>();

        var holidayPlan = new Mock<IHolidayPlan>();
        holidayPlan.Setup(hp => hp.HasCollaborator(collaborator.Object)).Returns(true);
        holidayPlan.Setup(hp => hp.GetHolidayPeriodsBetweenPeriod(It.IsAny<IPeriodDate>())).Returns(new List<IHolidayPeriod> { holidayPeriod.Object });

        holidayPeriod.Setup(hperiod => hperiod.Intersects(It.IsAny<IPeriodDate>())).Returns(true);


        // Adicionar o plano de férias ao repositório
        var hpRepo = new HolidayPlanRepository(new List<IHolidayPlan> { holidayPlan.Object });

        var expected = new List<IHolidayPeriod>() { holidayPeriod.Object };

        // Act
        var result = hpRepo.FindAllHolidayPeriodsForCollaboratorBetweenDates(collaborator.Object, It.IsAny<IPeriodDate>());

        // Assert
        Assert.True(result.SequenceEqual(expected));
    }

    [Fact]
    public void WhenHolidayPeriodIsOutOfRangeThanTheOneBeingSearchedFor_ThenReturnsEmptyList()
    {
        // Arrange
        var collaborator = new Mock<ICollaborator>();

        var holidayPeriod = new Mock<IHolidayPeriod>();

        var holidayPlan = new Mock<IHolidayPlan>();
        holidayPlan.Setup(hp => hp.HasCollaborator(collaborator.Object)).Returns(true);
        holidayPlan.Setup(hp => hp.GetHolidayPeriodsBetweenPeriod(It.IsAny<IPeriodDate>())).Returns(new List<IHolidayPeriod>());

        // Adicionar o plano de férias ao repositório
        var hpRepo = new HolidayPlanRepository(new List<IHolidayPlan> { holidayPlan.Object });

        // Act
        var result = hpRepo.FindAllHolidayPeriodsForCollaboratorBetweenDates(collaborator.Object, It.IsAny<IPeriodDate>());

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public void WhenCollaboratorHasNoHolidayPlans_ThenReturnsEmptyList()
    {
        // Arrange
        var collaborator = new Mock<ICollaborator>();

        var holidayPeriod = new Mock<IHolidayPeriod>();

        var holidayPlan = new Mock<IHolidayPlan>();
        holidayPlan.Setup(hp => hp.HasCollaborator(collaborator.Object)).Returns(false);


        // Adicionar o plano de férias ao repositório
        var hpRepo = new HolidayPlanRepository(new List<IHolidayPlan> { holidayPlan.Object });

        // Act
        var result = hpRepo.FindAllHolidayPeriodsForCollaboratorBetweenDates(collaborator.Object, It.IsAny<IPeriodDate>());

        // Assert
        Assert.Empty(result);
    }

    // tests for method with id:

    [Fact]
    public void WhenPassingValidDates_ThenReturnsCorrectPeriod()
    {
        // Arrange
        var collaborator = new Mock<ICollaborator>();
        collaborator.Setup(c => c.GetId()).Returns(1);

        var holidayPeriod = new Mock<IHolidayPeriod>();

        var holidayPlan = new Mock<IHolidayPlan>();
        holidayPlan.Setup(hp => hp.HasCollaboratorId(1)).Returns(true);
        holidayPlan.Setup(hp => hp.GetHolidayPeriodsBetweenPeriod(It.IsAny<IPeriodDate>())).Returns(new List<IHolidayPeriod> { holidayPeriod.Object });

        holidayPeriod.Setup(hperiod => hperiod.Intersects(It.IsAny<IPeriodDate>())).Returns(true);


        // Adicionar o plano de férias ao repositório
        var hpRepo = new HolidayPlanRepository(new List<IHolidayPlan> { holidayPlan.Object });

        var expected = new List<IHolidayPeriod>() { holidayPeriod.Object };

        // Act
        var result = hpRepo.FindAllHolidayPeriodsForCollaboratorBetweenDates(1, It.IsAny<IPeriodDate>());

        // Assert
        Assert.True(result.SequenceEqual(expected));
    }

    [Fact]
    public void WhenHolidayPeriodIsOutOfRangeThanTheOneBeingSearchedFor_ThenReturnEmptyList()
    {
        // Arrange
        var collaborator = new Mock<ICollaborator>();
        collaborator.Setup(c => c.GetId()).Returns(1);

        var holidayPeriod = new Mock<IHolidayPeriod>();

        var holidayPlan = new Mock<IHolidayPlan>();
        holidayPlan.Setup(hp => hp.HasCollaboratorId(1)).Returns(true);
        holidayPlan.Setup(hp => hp.GetHolidayPeriodsBetweenPeriod(It.IsAny<IPeriodDate>())).Returns(new List<IHolidayPeriod>());

        // Adicionar o plano de férias ao repositório
        var hpRepo = new HolidayPlanRepository(new List<IHolidayPlan> { holidayPlan.Object });

        // Act
        var result = hpRepo.FindAllHolidayPeriodsForCollaboratorBetweenDates(1, It.IsAny<IPeriodDate>());

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public void WhenCollaboratorHasNoHolidayPlans_ThenReturnEmptyList()
    {
        // Arrange
        var collaborator = new Mock<ICollaborator>();
        collaborator.Setup(c => c.GetId()).Returns(1);

        var holidayPeriod = new Mock<IHolidayPeriod>();

        var holidayPlan = new Mock<IHolidayPlan>();
        holidayPlan.Setup(hp => hp.HasCollaboratorId(1)).Returns(false);


        // Adicionar o plano de férias ao repositório
        var hpRepo = new HolidayPlanRepository(new List<IHolidayPlan> { holidayPlan.Object });

        // Act
        var result = hpRepo.FindAllHolidayPeriodsForCollaboratorBetweenDates(1, It.IsAny<IPeriodDate>());

        // Assert
        Assert.Empty(result);
    }
}