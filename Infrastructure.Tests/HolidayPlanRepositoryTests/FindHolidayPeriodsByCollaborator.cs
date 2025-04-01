using Moq;
using Infrastructure.Repositories;
using Domain.Interfaces;
namespace Infrastructure.Tests.HolidayPlanRepositoryTests;

public class FindHolidayPeriodsByCollaborator
{
    public static IEnumerable<object[]> GetHolidayPeriodsByCollaboratorData()
    {
        yield return new object[]
    {
        new DateOnly(2025, 7, 10),
        new DateOnly(2025, 7, 20),
        1
    };

        yield return new object[]
        {
        new DateOnly(2025, 6, 5),
        new DateOnly(2025, 6, 15),
        1
        };

        yield return new object[]
        {
        DateOnly.MinValue,
        DateOnly.MinValue,
        0
        };

        yield return new object[]
        {
        new DateOnly(2025, 5, 1),
        new DateOnly(2025, 5, 10),
        2
        };

    }

    [Theory]
    [MemberData(nameof(GetHolidayPeriodsByCollaboratorData))]
    public void WhenFindingHolidayPeriodsByCollaborator_ThenReturnsCorrectPeriods(
     DateOnly holidayInitDate,
     DateOnly holidayEndDate,
     int expectedPeriods
 )
    {
        // Arrange
        var collaboratorMock = new Mock<ICollaborator>();

        var holidayPeriods = new List<IHolidayPeriod>();

        if (holidayInitDate != DateOnly.MinValue && holidayEndDate != DateOnly.MinValue)
        {
            var holidayPeriodMock = new Mock<IHolidayPeriod>();
            holidayPeriodMock.Setup(hp => hp.GetInitDate()).Returns(holidayInitDate);
            holidayPeriodMock.Setup(hp => hp.GetFinalDate()).Returns(holidayEndDate);
            holidayPeriods.Add(holidayPeriodMock.Object);
            if (expectedPeriods > 1)
            {
                var holidayPeriodMock2 = new Mock<IHolidayPeriod>();
                holidayPeriodMock2.Setup(hp => hp.GetInitDate()).Returns(holidayInitDate.AddDays(5));
                holidayPeriodMock2.Setup(hp => hp.GetFinalDate()).Returns(holidayEndDate.AddDays(5));
                holidayPeriods.Add(holidayPeriodMock2.Object);
            }
        }

        var holidayPlanMock = new Mock<IHolidayPlan>();
        holidayPlanMock.Setup(hp => hp.GetCollaborator()).Returns(collaboratorMock.Object);
        holidayPlanMock.Setup(hp => hp.GetHolidayPeriods()).Returns(holidayPeriods);

        var holidayPlanRepo = new HolidayPlanRepository(new List<IHolidayPlan> { holidayPlanMock.Object });

        // Act
        var result = holidayPlanRepo.FindHolidayPeriodsByCollaborator(collaboratorMock.Object);

        // Assert
        Assert.Equal(expectedPeriods, result.Count);
    }
}