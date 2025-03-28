using System;
using System.Linq;
using Domain;
using Moq;
using Xunit;

public class HolidayPlanRepositoryTest
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

        var holidayRepo = new HolidayPlanRepository(holidayPlans);

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

        var associationMock = new Mock<IAssociationProjectColaboratorRepository>();
        associationMock
            .Setup(a =>
                a.FindAllProjectCollaboratorsBetween(
                    It.IsAny<IProject>(),
                    It.IsAny<DateOnly>(),
                    It.IsAny<DateOnly>()
                )
            )
            .Returns(new List<IColaborator> { collaboratorMock.Object });
        var holidayPlanMock = new Mock<IHolidayPlan>();
        holidayPlanMock.Setup(hp => hp.GetColaborator()).Returns(collaboratorMock.Object);
        holidayPlanMock
            .Setup(hp => hp.GetHolidayPeriods())
            .Returns(Enumerable.Empty<IHolidayPeriod>);

        var holidayPeriodMock = new Mock<IHolidayPeriod>();
        holidayPeriodMock.Setup(hp => hp.GetInitDate()).Returns(It.IsAny<DateOnly>());
        holidayPeriodMock.Setup(hp => hp.GetFinalDate()).Returns(It.IsAny<DateOnly>());

        var holidayRepo = new HolidayPlanRepository(holidayPlans);

        // Act
        var result = holidayRepo.FindAllHolidayPeriodsForAllProjectCollaboratorsBetweenDates(
            projectMock.Object,
            new DateOnly(2025, 6, 1),
            new DateOnly(2025, 6, 10)
        );

        // Assert
        Assert.Empty(result);
    }

    public static IEnumerable<object[]> GetHolidayDaysForProjectCollaboratorBetweenDatesData()
    {
        yield return new object[]
        {
            new DateOnly(2025, 7, 15),
            new DateOnly(2025, 8, 1),
            new DateOnly(2025, 7, 20),
            new DateOnly(2025, 7, 25),
            6,
        };

        yield return new object[]
        {
            new DateOnly(2025, 7, 15),
            new DateOnly(2025, 8, 1),
            new DateOnly(2025, 7, 5),
            new DateOnly(2025, 7, 20),
            6,
        };

        yield return new object[]
        {
            new DateOnly(2025, 7, 15),
            new DateOnly(2025, 8, 1),
            new DateOnly(2025, 7, 25),
            new DateOnly(2025, 8, 1),
            7,
        };

        yield return new object[]
        {
            new DateOnly(2025, 7, 15),
            new DateOnly(2025, 8, 1),
            new DateOnly(2025, 9, 1),
            new DateOnly(2025, 9, 10),
            0,
        };

        yield return new object[]
        {
            new DateOnly(2025, 7, 15),
            new DateOnly(2025, 7, 15),
            new DateOnly(2025, 7, 15),
            new DateOnly(2025, 7, 15),
            1,
        };
    }

    [Theory]
    [MemberData(nameof(GetHolidayDaysForProjectCollaboratorBetweenDatesData))]
    public void WhenGettingHolidayDaysForProjectCollaboratorBetweenDates_ThenReturnsCorrectHolidayDays(
        DateOnly initDate,
        DateOnly endDate,
        DateOnly holidayInitDate,
        DateOnly holidayEndDate,
        int expectedHolidayDays
    )
    {
        // Arrange
        var collaboratorMock = new Mock<IColaborator>();
        var projectMock = new Mock<IProject>();

        var associationMock = new Mock<IAssociationProjectColaborator>();
        associationMock.Setup(a => a.GetColaborator()).Returns(collaboratorMock.Object);
        associationMock.Setup(a => a.GetProject()).Returns(projectMock.Object);
        associationMock
            .Setup(a => a.AssociationIntersectDates(It.IsAny<DateOnly>(), It.IsAny<DateOnly>()))
            .Returns(true);

        var holidayPeriodMock = new Mock<IHolidayPeriod>();
        holidayPeriodMock.Setup(hp => hp.GetInitDate()).Returns(holidayInitDate);
        holidayPeriodMock.Setup(hp => hp.GetFinalDate()).Returns(holidayEndDate);
        //nao sei se é assim que meto o metodo da funçao no setup
        holidayPeriodMock
            .Setup(hp =>
                hp.GetNumberOfCommonUtilDaysBetweenPeriods(
                    It.IsAny<DateOnly>(),
                    It.IsAny<DateOnly>()
                )
            )
            .Returns(expectedHolidayDays);

        var holidayPlanMock = new Mock<IHolidayPlan>();
        holidayPlanMock.Setup(hp => hp.GetColaborator()).Returns(collaboratorMock.Object);
        holidayPlanMock
            .Setup(hp => hp.GetHolidayPeriods())
            .Returns(new List<IHolidayPeriod> { holidayPeriodMock.Object });
        var holidayPlans = new List<IHolidayPlan> { holidayPlanMock.Object };
        //o qye passo aqui?
        var holidayPlanRepository = new HolidayPlanRepository(holidayPlans);
        // Act
        var result = holidayPlanRepository.GetHolidayDaysForProjectCollaboratorBetweenDates(
            associationMock.Object,
            initDate,
            endDate
        );

        // Assert
        Assert.Equal(expectedHolidayDays, result);
    }
}
