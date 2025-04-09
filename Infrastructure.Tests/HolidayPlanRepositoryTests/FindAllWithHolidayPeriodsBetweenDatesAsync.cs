using Domain.Interfaces;
using Infrastructure.Repositories;
using Moq;

namespace Infrastructure.Tests.HolidayPlanRepositoryTests;

public class FindAllWithHolidayPeriodsBetweenDatesAsync
{
    [Fact]
    public async Task WhenCollaboratorHasHolidayPeriodWithinDateRangeAsync_ThenReturnsHolidayPlan()
    {
        // Arrange
        var collaborator = new Mock<ICollaborator>();

        var holidayPeriod = new Mock<IHolidayPeriod>();

        var holidayPlan = new Mock<IHolidayPlan>();
        holidayPlan.Setup(hp => hp.HasCollaborator(collaborator.Object)).Returns(true);

        holidayPlan.Setup(hp => hp.HasIntersectingHolidayPeriod(It.IsAny<IPeriodDate>())).Returns(true);

        holidayPlan.Setup(hp => hp.GetCollaborator()).Returns(collaborator.Object);

        var hpRepo = new HolidayPlanRepository(new List<IHolidayPlan> { holidayPlan.Object });

        var expected = new List<IHolidayPlan>() { holidayPlan.Object };
        // Act
        var result = await hpRepo.FindHolidayPlansWithinPeriodAsync(It.IsAny<IPeriodDate>());

        // Assert
        Assert.True(result.SequenceEqual(expected));
    }

    [Fact]
    public async Task WhenNoCollaboratorsHaveHolidayPeriodsInDateRangeAsync_ThenReturnsEmptyList()
    {
        // Arrange
        var collaborator = new Mock<ICollaborator>();

        var holidayPlan = new Mock<IHolidayPlan>();
        holidayPlan.Setup(hp => hp.HasCollaborator(collaborator.Object)).Returns(true);
        holidayPlan.Setup(hp => hp.HasIntersectingHolidayPeriod(It.IsAny<IPeriodDate>())).Returns(false);

        holidayPlan.Setup(hp => hp.GetCollaborator()).Returns(collaborator.Object);

        var hpRepo = new HolidayPlanRepository(new List<IHolidayPlan> { holidayPlan.Object });

        // Act
        var result = await hpRepo.FindHolidayPlansWithinPeriodAsync(It.IsAny<IPeriodDate>());

        // Assert
        Assert.Empty(result);
    }
}

