using Infrastructure.Repositories;
using Domain.Interfaces;
using Moq;
using Domain.Models;
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
        holidayPlan
            .Setup(hp => hp.GetHolidayPeriodsBetweenPeriod(It.IsAny<IPeriodDate>()))
            .Returns(new List<IHolidayPeriod> { holidayPeriod.Object });

        holidayPeriod.Setup(hperiod => hperiod.Intersects(It.IsAny<IPeriodDate>())).Returns(true);


        // Adicionar o plano de férias ao repositório
        var hpRepo = new HolidayPlanRepository(new List<IHolidayPlan> { holidayPlan.Object });

        var expected = new List<IHolidayPeriod>() { holidayPeriod.Object };

        // Act
        var result = hpRepo.FindAllHolidayPeriodsForCollaboratorBetweenDates(
            collaborator.Object,
            It.IsAny<IPeriodDate>()
        );

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
        holidayPlan
            .Setup(hp => hp.GetHolidayPeriods())
            .Returns(new List<IHolidayPeriod> { holidayPeriod.Object });

        holidayPeriod.Setup(hperiod => hperiod.Intersects(It.IsAny<IPeriodDate>())).Returns(false);


        // Adicionar o plano de férias ao repositório
        var hpRepo = new HolidayPlanRepository(new List<IHolidayPlan> { holidayPlan.Object });

        // Act
        var result = hpRepo.FindAllHolidayPeriodsForCollaboratorBetweenDates(
            collaborator.Object,
            It.IsAny<IPeriodDate>()
        );

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

        holidayPeriod.Setup(hperiod => hperiod.Intersects(It.IsAny<IPeriodDate>())).Returns(true);


        // Adicionar o plano de férias ao repositório
        var hpRepo = new HolidayPlanRepository(new List<IHolidayPlan> { holidayPlan.Object });

        // Act
        var result = hpRepo.FindAllHolidayPeriodsForCollaboratorBetweenDates(
            collaborator.Object,
            It.IsAny<IPeriodDate>()
        );

        // Assert
        Assert.Empty(result);
    }
}