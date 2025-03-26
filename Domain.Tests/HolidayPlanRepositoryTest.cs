using System;
using System.Collections.Generic;
using System.Linq;
using Domain;
using Moq;
using Xunit;

public class HolidayPlanRepositoryTests
{
    public static IEnumerable<object[]> GetHolidayPeriodsForProjectCollaborators()
    {
        yield return new object[]
        {
            new DateOnly(2025, 6, 1),
            new DateOnly(2025, 6, 10),
            new List<IHolidayPeriod>
            {
                new HolidayPeriod(new DateOnly(2025, 6, 1), new DateOnly(2025, 6, 5)),
                new HolidayPeriod(new DateOnly(2025, 6, 6), new DateOnly(2025, 6, 10)),
            },
        };

        yield return new object[]
        {
            new DateOnly(2025, 6, 3),
            new DateOnly(2025, 6, 8),
            new List<IHolidayPeriod>
            {
                new HolidayPeriod(new DateOnly(2025, 6, 1), new DateOnly(2025, 6, 5)),
                new HolidayPeriod(new DateOnly(2025, 6, 6), new DateOnly(2025, 6, 10)),
            },
        };

        yield return new object[]
        {
            new DateOnly(2025, 6, 10),
            new DateOnly(2025, 6, 20),
            new List<IHolidayPeriod>(),
        };
    }

    [Theory]
    [MemberData(nameof(GetHolidayPeriodsForProjectCollaborators))]
    public void WhenFindingHolidayPeriodsForProjectCollaborators_ThenReturnCorrectPeriods(
        DateOnly initDate,
        DateOnly endDate,
        List<IHolidayPeriod> expectedPeriods
    )
    {
        // Arrange
        var projectMock = new Mock<IProject>();

        var collaboratorMock = new Mock<IColaborator>();

        var holidayPlanMock = new Mock<IHolidayPlan>();
        holidayPlanMock.Setup(hp => hp.GetColaborator()).Returns(collaboratorMock.Object);
        holidayPlanMock.Setup(hp => hp.GetHolidayPeriods()).Returns(expectedPeriods);

        var holidayPlans = new List<IHolidayPlan> { holidayPlanMock.Object };

        var associationRepoMock = new Mock<IAssociationProjectColaboratorRepository>();
        associationRepoMock
            .Setup(repo =>
                repo.FindAllProjectCollaboratorsBetween(
                    It.IsAny<IProject>(),
                    It.IsAny<DateOnly>(),
                    It.IsAny<DateOnly>()
                )
            )
            .Returns(new List<IColaborator> { collaboratorMock.Object });

        var holidayRepo = new HolidayPlanRepository(associationRepoMock.Object);
        holidayPlans.ForEach(plan => holidayRepo.AddHolidayPlan(plan));

        // Act
        var result = holidayRepo.FindAllHolidayPeriodsForAllProjectCollaboratorsBetweenDates(
            projectMock.Object,
            initDate,
            endDate
        );

        // Assert
        Assert.Equal(expectedPeriods.Count, result.Count());
    }

    [Fact]
    public void WhenNoHolidayPeriodsForCollaboratorWithinInterval_ThenReturnEmptyList()
    {
        // Arrange
        var projectMock = new Mock<IProject>();
        var collaboratorMock = new Mock<IColaborator>();

        var holidayPlans = new List<IHolidayPlan>();

        var holidayRepo = new HolidayPlanRepository(
            new Mock<IAssociationProjectColaboratorRepository>().Object
        );
        holidayPlans.ForEach(plan => holidayRepo.AddHolidayPlan(plan));

        // Act
        var result = holidayRepo.FindAllHolidayPeriodsForAllProjectCollaboratorsBetweenDates(
            projectMock.Object,
            new DateOnly(2025, 6, 1),
            new DateOnly(2025, 6, 10)
        );

        // Assert
        Assert.Empty(result); // A lista deve estar vazia
    }

    public static IEnumerable<object[]> GetHolidayDaysData()
    {
        yield return new object[] { new DateOnly(2025, 6, 1), new DateOnly(2025, 6, 10), 10 };
        yield return new object[] { new DateOnly(2025, 6, 5), new DateOnly(2025, 6, 10), 6 };
        yield return new object[] { new DateOnly(2025, 6, 1), new DateOnly(2025, 6, 3), 3 };

        // periodo começa antes do intervalo
        yield return new object[] { new DateOnly(2025, 6, 3), new DateOnly(2025, 6, 10), 8 };

        // periodo termina depois do intervalo
        yield return new object[] { new DateOnly(2025, 6, 1), new DateOnly(2025, 6, 7), 7 };

        // periodo esta fora do intervalo
        yield return new object[] { new DateOnly(2025, 7, 1), new DateOnly(2025, 7, 5), 0 };
    }

    [Theory]
    [MemberData(nameof(GetHolidayDaysData))]
    public void WhenGettingHolidayDaysForProjectCollaboratorBetweenDates_ThenReturnCorrectDays(
        DateOnly initDate,
        DateOnly endDate,
        int expectedDays
    )
    {
        // Arrange
        var projectMock = new Mock<IProject>();
        var collaboratorMock = new Mock<IColaborator>();

        var holidayPeriodMock = new Mock<IHolidayPeriod>();
        holidayPeriodMock.Setup(hp => hp.GetInitDate()).Returns(initDate);
        holidayPeriodMock.Setup(hp => hp.GetFinalDate()).Returns(endDate);

        var repositoryMock = new Mock<IHolidayPlanRepository>();
        repositoryMock
            .Setup(r =>
                r.GetHolidayDaysForProjectCollaboratorBetweenDates(
                    It.IsAny<IProject>(),
                    It.IsAny<IColaborator>(),
                    It.IsAny<DateOnly>(),
                    It.IsAny<DateOnly>()
                )
            )
            .Returns(expectedDays);

        // Act
        int result = repositoryMock.Object.GetHolidayDaysForProjectCollaboratorBetweenDates(
            projectMock.Object,
            collaboratorMock.Object,
            initDate,
            endDate
        );

        // Assert
        Assert.Equal(expectedDays, result);
    }
}
