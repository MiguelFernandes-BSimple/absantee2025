using Infrastructure.Repositories;
using Domain.Interfaces;
using Domain.Models;
using Moq;
namespace Infrastructure.Tests.HolidayPlanRepositoryTests;

public class GetHolidayDaysForAllProjectCollaboratorsBetweenDates
{
    [Fact]
    public void GivenProjectWithCollaborators_WhenGetHolidayDaysForAllCollaborators_ThenReturnsCorrectDays()
    {
        // Arrange
        var mockColaborator = new Mock<ICollaborator>();
        var mockHolidayPlan = new Mock<IHolidayPlan>();


        mockColaborator.Setup(c => c.ContractContainsDates(It.IsAny<DateTime>(), It.IsAny<DateTime>())).Returns(true);


        var mockHolidayPeriod = new Mock<IHolidayPeriod>();
        mockHolidayPeriod.Setup(h => h.GetInitDate()).Returns(new DateOnly(2024, 6, 1));
        mockHolidayPeriod.Setup(h => h.GetFinalDate()).Returns(new DateOnly(2024, 6, 10));
        mockHolidayPeriod.Setup(h => h.GetDurationInDays(It.IsAny<DateOnly>(), It.IsAny<DateOnly>())).Returns(10);


        var holidayPlan = new HolidayPlan(mockHolidayPeriod.Object, mockColaborator.Object);


        var collaboratorsList = new List<ICollaborator>() { mockColaborator.Object };


        var holidayPlanRepository = new HolidayPlanRepository(new List<IHolidayPlan> { holidayPlan });

        // Act
        int result = holidayPlanRepository.GetHolidayDaysForAllProjectCollaboratorsBetweenDates(
            collaboratorsList,
            new DateOnly(2024, 6, 1),
            new DateOnly(2024, 6, 10)
        );

        // Assert
        Assert.Equal(10, result);
    }
}