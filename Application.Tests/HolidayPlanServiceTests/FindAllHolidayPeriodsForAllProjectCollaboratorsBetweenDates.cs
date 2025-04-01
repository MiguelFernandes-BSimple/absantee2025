using Domain;
using Moq;

public class FindAllHolidayPeriodsForAllProjectCollaboratorsBetweenDates
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
        var collaboratorMock = new Mock<ICollaborator>();
        var collaboratorList = new List<ICollaborator>(){
                collaboratorMock.Object
            };

        var associationRepoMock = new Mock<IAssociationProjectCollaboratorRepository>();
        var associationMock = new Mock<IAssociationProjectCollaborator>();
        var associationsList = new List<IAssociationProjectCollaborator> {
                associationMock.Object
            };
        associationRepoMock
            .Setup(a =>
                a.FindAllByProjectAndBetweenPeriod(
                    It.IsAny<IProject>(),
                    It.IsAny<DateOnly>(),
                    It.IsAny<DateOnly>()
                )
            )
            .Returns(associationsList);

        associationMock.Setup(a => a.GetCollaborator()).Returns(collaboratorMock.Object);

        var holidayPeriodMock = new Mock<IHolidayPeriod>();

        var projectMock = new Mock<IProject>();
        var holidayRepoMock = new Mock<IHolidayPlanRepository>();
        holidayRepoMock.Setup(hr => hr.FindAllHolidayPeriodsForAllCollaboratorsBetweenDates(collaboratorList, initDate, endDate))
                                    .Returns(expectedPeriods);
        var holidayPlanService = new HolidayPlanService(associationRepoMock.Object, holidayRepoMock.Object);
        // Act
        var result = holidayPlanService.FindAllHolidayPeriodsForAllProjectCollaboratorsBetweenDates(
            projectMock.Object,
            initDate,
            endDate
        );

        Assert.Equal(expectedPeriods.Count, result.Count());
    }

    [Fact]
    public void WhenNoHolidayPeriodsForCollaboratorWithinInterval_ThenReturnEmptyList()
    {
        // Arrange
        var projectMock = new Mock<IProject>();
        var collaboratorMock = new Mock<ICollaborator>();
        var associationRepoMock = new Mock<IAssociationProjectCollaboratorRepository>();
        var associationMock = new Mock<IAssociationProjectCollaborator>();
        var associationsList = new List<IAssociationProjectCollaborator> {
                associationMock.Object
            };
        associationRepoMock
            .Setup(a =>
                a.FindAllByProjectAndBetweenPeriod(
                    It.IsAny<IProject>(),
                    It.IsAny<DateOnly>(),
                    It.IsAny<DateOnly>()
                )
            )
            .Returns(associationsList);

        associationMock.Setup(a => a.GetCollaborator()).Returns(collaboratorMock.Object);

        var expected = new List<IHolidayPeriod>();
        var holidayRepoMock = new Mock<IHolidayPlanRepository>();
        holidayRepoMock.Setup(hr => hr.FindAllHolidayPeriodsForAllCollaboratorsBetweenDates(It.IsAny<List<ICollaborator>>(), It.IsAny<DateOnly>(), It.IsAny<DateOnly>()))
                        .Returns(expected);
        var holidayPlanService = new HolidayPlanService(associationRepoMock.Object, holidayRepoMock.Object);


        // Act
        var result = holidayPlanService.FindAllHolidayPeriodsForAllProjectCollaboratorsBetweenDates(
            projectMock.Object,
            new DateOnly(2025, 6, 1),
            new DateOnly(2025, 6, 10)
        );

        // Assert
        Assert.Empty(result);
    }

    [Theory]
    [InlineData("2025-07-01", "2025-06-30")]  // initDate > endDate
    [InlineData("2025-08-01", "2025-07-31")]  // initDate > endDate
    [InlineData("2025-07-01", "2025-07-01")]  // initDate == endDate
    public void WhenInitDateIsGreaterThanOrEqualToEndDate_ThenReturnsEmptyList(string initDateStr, string endDateStr)
    {
        DateOnly initDate = DateOnly.Parse(initDateStr);
        DateOnly endDate = DateOnly.Parse(endDateStr);

        // Arrange
        var collaboratorMock = new Mock<ICollaborator>();
        var collaboratorList = new List<ICollaborator>() { collaboratorMock.Object };

        var associationRepoMock = new Mock<IAssociationProjectCollaboratorRepository>();
        var associationMock = new Mock<IAssociationProjectCollaborator>();
        var associationsList = new List<IAssociationProjectCollaborator> { associationMock.Object };
        associationRepoMock
            .Setup(a => a.FindAllByProjectAndBetweenPeriod(It.IsAny<IProject>(), It.IsAny<DateOnly>(), It.IsAny<DateOnly>()))
            .Returns(associationsList);

        var holidayRepoMock = new Mock<IHolidayPlanRepository>();
        holidayRepoMock.Setup(hr => hr.FindAllHolidayPeriodsForAllCollaboratorsBetweenDates(It.IsAny<List<ICollaborator>>(), It.IsAny<DateOnly>(), It.IsAny<DateOnly>()))
                        .Returns(new List<IHolidayPeriod>());

        var holidayPlanService = new HolidayPlanService(associationRepoMock.Object, holidayRepoMock.Object);

        // Act
        var result = holidayPlanService.FindAllHolidayPeriodsForAllProjectCollaboratorsBetweenDates(
            new Mock<IProject>().Object,
            initDate,
            endDate
        );

        // Assert
        Assert.Empty(result);
    }
}