using Moq;
using Infrastructure.Repositories;
using Domain.Interfaces;
namespace Infrastructure.Tests.HolidayPlanRepositoryTests;

public class FindHolidayPeriodsByCollaborator
{
    [Fact]
    public void WhenFindingHolidayPeriodsByCollaborator_ThenReturnsCorrectPeriods()
    {
        // Arrange
        var collaboratorMock = new Mock<ICollaborator>();

        var holidayPeriods = new List<IHolidayPeriod>() 
        {
            new Mock<IHolidayPeriod>().Object,
            new Mock<IHolidayPeriod>().Object
        };


        var holidayPlanMock = new Mock<IHolidayPlan>();
        holidayPlanMock.Setup(hp => hp.HasCollaborator(collaboratorMock.Object)).Returns(true);
        holidayPlanMock.Setup(hp => hp.GetHolidayPeriods()).Returns(holidayPeriods);

        var holidayPlanRepo = new HolidayPlanRepository(new List<IHolidayPlan> { holidayPlanMock.Object });

        // Act
        var result = holidayPlanRepo.FindHolidayPeriodsByCollaborator(collaboratorMock.Object);

        // Assert
        Assert.True(result.SequenceEqual(holidayPeriods));
    }

    [Fact]
    public void WhenNotFindingHolidayPeriodsByCollaborator_ThenReturnsEmptyList()
    {
        // Arrange
        var collaboratorMock = new Mock<ICollaborator>();


        var holidayPlanMock = new Mock<IHolidayPlan>();
        holidayPlanMock.Setup(hp => hp.HasCollaborator(collaboratorMock.Object)).Returns(false);

        var holidayPlanRepo = new HolidayPlanRepository(new List<IHolidayPlan> { holidayPlanMock.Object });

        // Act
        var result = holidayPlanRepo.FindHolidayPeriodsByCollaborator(collaboratorMock.Object);

        // Assert
        Assert.Empty(result);
    }
}