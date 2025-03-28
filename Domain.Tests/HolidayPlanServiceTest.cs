using Domain;

using Moq;

public class HolidayPlanServiceTests
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
        var collaboratorMock = new Mock<ICollaborator>();
        var projectMock = new Mock<IProject>();

        var associationRepoMock = new Mock<IAssociationProjectCollaboratorRepository>();
        var associationMock = new Mock<IAssociationProjectCollaborator>();
        associationRepoMock
            .Setup(a => a.FindByProjectAndCollaborator(projectMock.Object, collaboratorMock.Object))
            .Returns(associationMock.Object);

        var holidayPeriodMock = new Mock<IHolidayPeriod>();
        var holidarPeriodList = new List<IHolidayPeriod>(){
                holidayPeriodMock.Object
            };

        var holidayRepoMock = new Mock<IHolidayPlanRepository>();
        holidayRepoMock.Setup(hr => hr.FindHolidayPeriodsByCollaborator(collaboratorMock.Object))
                        .Returns(holidarPeriodList);


        holidayPeriodMock.Setup(hp => hp.GetInitDate()).Returns(holidayInitDate);
        holidayPeriodMock.Setup(hp => hp.GetFinalDate()).Returns(holidayEndDate);
        holidayPeriodMock
            .Setup(hp =>
                hp.GetNumberOfCommonUtilDaysBetweenPeriods(
                    It.IsAny<DateOnly>(),
                    It.IsAny<DateOnly>()
                )
            )
            .Returns(expectedHolidayDays);


        var holidayPlanService = new HolidayPlanService(associationRepoMock.Object, holidayRepoMock.Object);

        // Act
        var result = holidayPlanService.GetHolidayDaysForProjectCollaboratorBetweenDates(
            projectMock.Object,
            collaboratorMock.Object,
            initDate,
            endDate
        );

        // Assert
        Assert.Equal(expectedHolidayDays, result);
    }


    [Fact]
    public void GetHolidayDaysForProjectCollaboratorBetweenDates_ReturnsCorrectDays()
    {
        // Arrange
        var mockProject = new Mock<IProject>();
        var mockAssociationRepo = new Mock<IAssociationProjectCollaboratorRepository>();
        var mockHolidayPlanRepo = new Mock<IHolidayPlanRepository>();

        var mockAssociation = new Mock<IAssociationProjectCollaborator>();
        var mockHolidayPlan = new Mock<IHolidayPlan>();
        var mockHolidayPeriod = new Mock<IHolidayPeriod>();

        var initDate = new DateOnly(2024, 6, 1);
        var endDate = new DateOnly(2024, 6, 10);

        mockHolidayPeriod.Setup(p => p.GetInitDate()).Returns(new DateOnly(2024, 6, 3));
        mockHolidayPeriod.Setup(p => p.GetFinalDate()).Returns(new DateOnly(2024, 6, 8));
        mockHolidayPeriod.Setup(p => p.GetDurationInDays(initDate, endDate)).Returns(6);

        mockHolidayPlan.Setup(hp => hp.GetHolidayPeriods()).Returns(new List<IHolidayPeriod> { mockHolidayPeriod.Object });

        mockHolidayPlanRepo
            .Setup(repo => repo.GetHolidayPlansByAssociations(mockAssociation.Object))
            .Returns(new List<IHolidayPlan> { mockHolidayPlan.Object });

        mockAssociationRepo
            .Setup(repo => repo.FindAllByProject(mockProject.Object))
            .Returns(new List<IAssociationProjectCollaborator> { mockAssociation.Object });

        var service = new HolidayPlanService(mockAssociationRepo.Object, mockHolidayPlanRepo.Object);

        // Act
        var totalHolidayDays = service.GetHolidayDaysForProjectCollaboratorBetweenDates(mockProject.Object, initDate, endDate);

        // Assert
        Assert.Equal(6, totalHolidayDays);
    }

    public static IEnumerable<object[]> ValidHolidayDatesBetweenWeekends()
    {
        yield return new object[] { new DateOnly(2025, 04, 01), new DateOnly(2025, 04, 08) };
        yield return new object[] { new DateOnly(2025, 04, 01), new DateOnly(2025, 04, 07) };
        yield return new object[] { new DateOnly(2025, 04, 02), new DateOnly(2025, 04, 08) };
        yield return new object[] { new DateOnly(2025, 04, 02), new DateOnly(2025, 04, 07) };
        yield return new object[] { new DateOnly(2025, 04, 03), new DateOnly(2025, 04, 06) };
        yield return new object[] { new DateOnly(2025, 04, 03), new DateOnly(2025, 04, 07) };
        yield return new object[] { new DateOnly(2025, 04, 03), new DateOnly(2025, 04, 08) };
        yield return new object[] { new DateOnly(2025, 04, 05), new DateOnly(2025, 04, 05) };
        yield return new object[] { new DateOnly(2025, 04, 06), new DateOnly(2025, 04, 06) };
    }

    [Theory]
    [MemberData(nameof(ValidHolidayDatesBetweenWeekends))]
    public void WhenRetrievingAllHolidayPeriodsForCollaboratorBetweenDatesThatIncludeWeekends_ThenReturnSucessfully(DateOnly searchInitDate, DateOnly searchEndDate)
    {
        //arrange
        Mock<ICollaborator> collab = new Mock<ICollaborator>();

        Mock<IHolidayPeriod> holidayPeriod = new Mock<IHolidayPeriod>();
        DateOnly holidayPeriodStartDate = new DateOnly(2025, 04, 02);
        DateOnly holidayPeriodFinalDate = new DateOnly(2025, 04, 07);
        holidayPeriod.Setup(hp => hp.GetInitDate()).Returns(holidayPeriodStartDate);
        holidayPeriod.Setup(hp => hp.GetFinalDate()).Returns(holidayPeriodFinalDate);
        var holidayPeriodsList = new List<IHolidayPeriod> { holidayPeriod.Object };

        Mock<IHolidayPlanRepository> holidayPlanRepository = new Mock<IHolidayPlanRepository>();
        Mock<IAssociationProjectCollaboratorRepository> associationRepository = new Mock<IAssociationProjectCollaboratorRepository>();
        holidayPlanRepository.Setup(r => r.FindAllHolidayPeriodsForCollaboratorBetweenDates(collab.Object, searchInitDate, searchEndDate)).Returns(holidayPeriodsList);

        HolidayPlanService service = new HolidayPlanService(associationRepository.Object, holidayPlanRepository.Object);

        //act
        var result = service.FindAllHolidayPeriodsForCollaboratorBetweenDatesThatIncludeWeekends(collab.Object, searchInitDate, searchEndDate);

        //assert
        Assert.Equal(holidayPeriodStartDate, result.First().GetInitDate());
        Assert.Equal(holidayPeriodFinalDate, result.First().GetFinalDate());
    }

    public static IEnumerable<object[]> ValidHolidayDatesWithoutWeekends()
    {
        yield return new object[] { new DateOnly(2025, 04, 01), new DateOnly(2025, 04, 04) };
        yield return new object[] { new DateOnly(2025, 04, 10), new DateOnly(2025, 04, 11) };
        yield return new object[] { new DateOnly(2025, 04, 07), new DateOnly(2025, 04, 14) };
        yield return new object[] { new DateOnly(2025, 03, 20), new DateOnly(2025, 04, 04) };
    }

    [Fact]
    public void WhenCalculatingHolidayDaysOfCollaboratorInAProject_ThenReturnCorrectValue()
    {
        //arrange
        Mock<IHolidayPlan> holidayPlanDouble = new Mock<IHolidayPlan>();
        Mock<ICollaborator> collaboratorDouble = new Mock<ICollaborator>();
        Mock<IProject> projectDouble = new Mock<IProject>();
        Mock<IAssociationProjectCollaborator> associationDouble = new Mock<IAssociationProjectCollaborator>();

        DateOnly initDate = new DateOnly(2025, 6, 1);
        DateOnly finalDate = new DateOnly(2025, 6, 10);

        associationDouble.Setup(a => a.GetCollaborator()).Returns(collaboratorDouble.Object);
        associationDouble.Setup(a => a.GetProject()).Returns(projectDouble.Object);
        associationDouble.Setup(a => a.GetInitDate()).Returns(initDate);
        associationDouble.Setup(a => a.GetFinalDate()).Returns(finalDate);

        holidayPlanDouble.Setup(hp => hp.GetCollaborator()).Returns(collaboratorDouble.Object);
        holidayPlanDouble.Setup(hp => hp.GetNumberOfHolidayDaysBetween(initDate, finalDate)).Returns(5);

        Mock<IHolidayPlanRepository> holidayPlanRepositoryDouble = new Mock<IHolidayPlanRepository>();
        holidayPlanRepositoryDouble.Setup(hpr => hpr.FindHolidayPlanByCollaborator(collaboratorDouble.Object)).Returns(holidayPlanDouble.Object);

        Mock<IAssociationProjectCollaboratorRepository> associationProjectCollaboratorRepository = new Mock<IAssociationProjectCollaboratorRepository>();
        associationProjectCollaboratorRepository.Setup(a => a.FindByProjectAndCollaborator(projectDouble.Object, collaboratorDouble.Object)).Returns(associationDouble.Object);

        HolidayPlanService service = new HolidayPlanService(associationProjectCollaboratorRepository.Object, holidayPlanRepositoryDouble.Object);

        //act
        int result = service.GetHolidayDaysOfCollaboratorInProject(projectDouble.Object, collaboratorDouble.Object);

        //assert
        Assert.Equal(5, result);

    }

    //UC20 Data
    public static IEnumerable<object[]> ValidPeriodToSearchOverlapping()
    {
        // intersection
        yield return new object[] {
            new DateOnly(2025, 04, 02), new DateOnly(2025, 04, 08),
            new DateOnly(2025, 04, 05), new DateOnly(2025, 04, 12),
            new DateOnly(2025, 04, 01), new DateOnly(2025, 04, 15),
        };
        // same holiday period
        yield return new object[] {
            new DateOnly(2025, 04, 05), new DateOnly(2025, 04, 12),
            new DateOnly(2025, 04, 05), new DateOnly(2025, 04, 12),
            new DateOnly(2025, 04, 01), new DateOnly(2025, 04, 15),
        };
            // same start holiday period date, end date after
            yield return new object[] {
                new DateOnly(2025, 04, 05), new DateOnly(2025, 04, 12),
                new DateOnly(2025, 04, 05), new DateOnly(2025, 04, 14),
                new DateOnly(2025, 04, 01), new DateOnly(2025, 04, 15),
        };
            // same end holiday period date, start date before
            yield return new object[] {
                new DateOnly(2025, 04, 05), new DateOnly(2025, 04, 11),
                new DateOnly(2025, 04, 04), new DateOnly(2025, 04, 11),
                new DateOnly(2025, 04, 01), new DateOnly(2025, 04, 15),
        };
            // one holiday period inside the other
            yield return new object[] {
                new DateOnly(2025, 04, 02), new DateOnly(2025, 04, 12),
                new DateOnly(2025, 04, 04), new DateOnly(2025, 04, 11),
                new DateOnly(2025, 04, 01), new DateOnly(2025, 04, 15),
        };
            // search for a specific day that contains both holiday periods
            yield return new object[] {
                new DateOnly(2025, 04, 02), new DateOnly(2025, 04, 12),
                new DateOnly(2025, 04, 04), new DateOnly(2025, 04, 11),
                new DateOnly(2025, 04, 10), new DateOnly(2025, 04, 10),
        };
            // holiday period 1 ends when holiday period 2 start
            yield return new object[] {
                new DateOnly(2025, 04, 04), new DateOnly(2025, 04, 10),
                new DateOnly(2025, 04, 10), new DateOnly(2025, 04, 15),
                new DateOnly(2025, 04, 01), new DateOnly(2025, 04, 15),
        };
            // holiday period 1 starts when holiday period 2 ends
            yield return new object[] {
                new DateOnly(2025, 04, 10), new DateOnly(2025, 04, 15),
                new DateOnly(2025, 04, 04), new DateOnly(2025, 04, 10),
                new DateOnly(2025, 04, 01), new DateOnly(2025, 04, 15),
        };
    }

    // UC20 
    [Theory]
    [MemberData(nameof(ValidPeriodToSearchOverlapping))]
    public void WhenGivenCorrectValues_ThenReturnOverlappingHolidayPeriodBetweenTwoCollabsInPeriod(
        DateOnly holidayPeriodStartDate1, DateOnly holidayPeriodFinalDate1,
        DateOnly holidayPeriodStartDate2, DateOnly holidayPeriodFinalDate2,
        DateOnly searchInitDate, DateOnly searchEndDate)
    {
        //arrange
        //collab1
        Mock<ICollaborator> collab1 = new Mock<ICollaborator>();

        Mock<IHolidayPeriod> holidayPeriod1 = new Mock<IHolidayPeriod>();
        holidayPeriod1.Setup(hp => hp.GetInitDate()).Returns(holidayPeriodStartDate1);
        holidayPeriod1.Setup(hp => hp.GetFinalDate()).Returns(holidayPeriodFinalDate1);
        var holidayPeriodsList1 = new List<IHolidayPeriod> { holidayPeriod1.Object };

        //colab2
        Mock<ICollaborator> collab2 = new Mock<ICollaborator>();

        Mock<IHolidayPeriod> holidayPeriod2 = new Mock<IHolidayPeriod>();
        holidayPeriod2.Setup(hp => hp.GetInitDate()).Returns(holidayPeriodStartDate2);
        holidayPeriod2.Setup(hp => hp.GetFinalDate()).Returns(holidayPeriodFinalDate2);
        var holidayPeriodsList2 = new List<IHolidayPeriod> { holidayPeriod2.Object };

        Mock<IHolidayPlanRepository> holidayPlanRepository = new Mock<IHolidayPlanRepository>();
        Mock<IAssociationProjectCollaboratorRepository> associationRepository = new Mock<IAssociationProjectCollaboratorRepository>();
        holidayPlanRepository.Setup(r => r.FindAllHolidayPeriodsForCollaboratorBetweenDates(collab1.Object, searchInitDate, searchEndDate)).Returns(holidayPeriodsList1);
        holidayPlanRepository.Setup(r => r.FindAllHolidayPeriodsForCollaboratorBetweenDates(collab2.Object, searchInitDate, searchEndDate)).Returns(holidayPeriodsList2);

        HolidayPlanService service = new HolidayPlanService(associationRepository.Object, holidayPlanRepository.Object);

        //act
        var result = service.FindAllOverlappingHolidayPeriodsBetweenTwoCollaboratorsBetweenDates(collab1.Object, collab2.Object, searchInitDate, searchEndDate);

        //assert
        Assert.Equal(holidayPeriodStartDate1, result.First().GetInitDate());
        Assert.Equal(holidayPeriodFinalDate1, result.First().GetFinalDate());
        Assert.Equal(holidayPeriodStartDate2, result.ToList()[1].GetInitDate());
        Assert.Equal(holidayPeriodFinalDate2, result.ToList()[1].GetFinalDate());
    }

    public static IEnumerable<object[]> SearchOverlappingPeriodsOutsideHolidayPeriod()
    {
        /* // dates intersect, but search date is outside
        yield return new object[] {
                    new DateOnly(2025, 04, 02), new DateOnly(2025, 04, 08),
                    new DateOnly(2025, 04, 05), new DateOnly(2025, 04, 12),
                    new DateOnly(2025, 04, 13), new DateOnly(2025, 04, 20),
            };
        // dates intersect, but search only contains 1 holiday period (1st date higher range)
        yield return new object[] {
                    new DateOnly(2025, 04, 02), new DateOnly(2025, 04, 12),
                    new DateOnly(2025, 04, 04), new DateOnly(2025, 04, 11),
                    new DateOnly(2025, 04, 12), new DateOnly(2025, 04, 12),
            };
        // dates intersect, but search only contains 1 holiday period (1st date lower range)
        yield return new object[] {
                    new DateOnly(2025, 04, 02), new DateOnly(2025, 04, 12),
                    new DateOnly(2025, 04, 04), new DateOnly(2025, 04, 11),
                    new DateOnly(2025, 04, 02), new DateOnly(2025, 04, 02),
            };
        // dates intersect, but search only contains 1 holiday period (2nd date higher range)
        yield return new object[] {
                    new DateOnly(2025, 04, 04), new DateOnly(2025, 04, 11),
                    new DateOnly(2025, 04, 02), new DateOnly(2025, 04, 12),
                    new DateOnly(2025, 04, 12), new DateOnly(2025, 04, 12),
            };
        // dates intersect, but search only contains 1 holiday period (2nd date lower range)
        yield return new object[] {
                    new DateOnly(2025, 04, 04), new DateOnly(2025, 04, 11),
                    new DateOnly(2025, 04, 02), new DateOnly(2025, 04, 12),
                    new DateOnly(2025, 04, 02), new DateOnly(2025, 04, 02),
            }; */
        // holiday periods don't intercept (2nd date after)
        yield return new object[] {
            new DateOnly(2025, 04, 02), new DateOnly(2025, 04, 10),
            new DateOnly(2025, 04, 11), new DateOnly(2025, 04, 15),
            new DateOnly(2025, 04, 01), new DateOnly(2025, 04, 15),
    };
        // holiday periods don't intercept  (1st date after)
        yield return new object[] {
            new DateOnly(2025, 04, 11), new DateOnly(2025, 04, 15),
            new DateOnly(2025, 04, 02), new DateOnly(2025, 04, 10),
            new DateOnly(2025, 04, 01), new DateOnly(2025, 04, 15),
        };
    }

    [Theory]
    [MemberData(nameof(SearchOverlappingPeriodsOutsideHolidayPeriod))]
    public void WhenGivenSearchPeriodOutsideOverlappingHoliadyPeriod_ThenReturnEmpty(
        DateOnly holidayPeriodStartDate1, DateOnly holidayPeriodFinalDate1,
        DateOnly holidayPeriodStartDate2, DateOnly holidayPeriodFinalDate2,
        DateOnly searchInitDate, DateOnly searchEndDate)
    {
        //arrange
        //collab1
        Mock<ICollaborator> collab1 = new Mock<ICollaborator>();

        Mock<IHolidayPeriod> holidayPeriod1 = new Mock<IHolidayPeriod>();
        holidayPeriod1.Setup(hp => hp.GetInitDate()).Returns(holidayPeriodStartDate1);
        holidayPeriod1.Setup(hp => hp.GetFinalDate()).Returns(holidayPeriodFinalDate1);
        var holidayPeriodsList1 = new List<IHolidayPeriod> { holidayPeriod1.Object };

        //colab2
        Mock<ICollaborator> collab2 = new Mock<ICollaborator>();

        Mock<IHolidayPeriod> holidayPeriod2 = new Mock<IHolidayPeriod>();
        holidayPeriod2.Setup(hp => hp.GetInitDate()).Returns(holidayPeriodStartDate2);
        holidayPeriod2.Setup(hp => hp.GetFinalDate()).Returns(holidayPeriodFinalDate2);
        var holidayPeriodsList2 = new List<IHolidayPeriod> { holidayPeriod2.Object };

        Mock<IHolidayPlanRepository> holidayPlanRepository = new Mock<IHolidayPlanRepository>();
        Mock<IAssociationProjectCollaboratorRepository> associationRepository = new Mock<IAssociationProjectCollaboratorRepository>();
        holidayPlanRepository.Setup(r => r.FindAllHolidayPeriodsForCollaboratorBetweenDates(collab1.Object, searchInitDate, searchEndDate)).Returns(holidayPeriodsList1);
        holidayPlanRepository.Setup(r => r.FindAllHolidayPeriodsForCollaboratorBetweenDates(collab2.Object, searchInitDate, searchEndDate)).Returns(holidayPeriodsList2);

        HolidayPlanService service = new HolidayPlanService(associationRepository.Object, holidayPlanRepository.Object);

        //act
        var result = service.FindAllOverlappingHolidayPeriodsBetweenTwoCollaboratorsBetweenDates(collab1.Object, collab2.Object, searchInitDate, searchEndDate);

        //assert
        Assert.Empty(result);
    }
}

