using Infrastructure.Repositories;
using Domain.Interfaces;
using Moq;

namespace Infrastructure.Tests.HolidayPlanRepositoryTests;

public class FindAllHolidayPeriodsForCollaboratorBetweenDatesAsync
{
    [Fact]
    public async Task WhenPassinValidDatesAsync_ThenReturnsCorrectPeriod()
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
        var result = await hpRepo.FindAllHolidayPeriodsForCollaboratorBetweenDatesAsync(collaborator.Object, It.IsAny<IPeriodDate>());

        // Assert
        Assert.True(result.SequenceEqual(expected));
    }

    [Fact]
    public async Task WhenHolidayPeriodIsOutOfRangeThanTheOneBeingSearchedForAsync_ThenReturnsEmptyList()
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
        var result = await hpRepo.FindAllHolidayPeriodsForCollaboratorBetweenDatesAsync(collaborator.Object, It.IsAny<IPeriodDate>());

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public async Task WhenCollaboratorHasNoHolidayPlansAsync_ThenReturnsEmptyList()
    {
        // Arrange
        var collaborator = new Mock<ICollaborator>();

        var holidayPeriod = new Mock<IHolidayPeriod>();

        var holidayPlan = new Mock<IHolidayPlan>();
        holidayPlan.Setup(hp => hp.HasCollaborator(collaborator.Object)).Returns(false);


        // Adicionar o plano de férias ao repositório
        var hpRepo = new HolidayPlanRepository(new List<IHolidayPlan> { holidayPlan.Object });

        // Act
        var result = await hpRepo.FindAllHolidayPeriodsForCollaboratorBetweenDatesAsync(collaborator.Object, It.IsAny<IPeriodDate>());

        // Assert
        Assert.Empty(result);
    }
}