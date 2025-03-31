using System;
using System.Linq;
using Domain;
using Moq;
using Xunit;

public class HolidayPlanRepositoryTest
{
    [Fact]
    public void WhenConstructingHolidayPlanRepositoryWithSingleHolidayPlan_ThenHolidayPlansIsInitialized()
    {
        // Arrange
        var holidayPlanMock = new Mock<IHolidayPlan>();

        // Act
        new HolidayPlanRepository(holidayPlanMock.Object);

        // Assert
    }

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

    //[Theory]
    //[MemberData(nameof(GetHolidayPeriodsForProjectCollaborators))]
    //public void WhenFindingHolidayPeriodsForProjectCollaborators_ThenReturnCorrectPeriods(
    //    DateOnly initDate,
    //    DateOnly endDate,
    //    List<IHolidayPeriod> expectedPeriods
    //)
    //{
    //    // Arrange
    //    var collaboratorMock = new Mock<ICollaborator>();
    //
    //    var associationMock = new Mock<IAssociationProjectCollaboratorRepository>();
    //    associationMock
    //        .Setup(am =>
    //            am.FindAllByProjectAndPeriod(
    //                It.IsAny<IProject>(),
    //                It.IsAny<DateOnly>(),
    //                It.IsAny<DateOnly>()
    //            )
    //        )
    //        .Returns(new List<ICollaborator> { collaboratorMock.Object });
    //
    //    var holidayPeriodMock = new Mock<IHolidayPeriod>();
    //    holidayPeriodMock.Setup(hp => hp.GetInitDate()).Returns(It.IsAny<DateOnly>());
    //    holidayPeriodMock.Setup(hp => hp.GetFinalDate()).Returns(It.IsAny<DateOnly>());
    //
    //    var holidayPlanMock = new Mock<IHolidayPlan>();
    //    holidayPlanMock.Setup(hp => hp.GetCollaborator()).Returns(collaboratorMock.Object);
    //    holidayPlanMock.Setup(hp => hp.GetHolidayPeriods()).Returns(expectedPeriods);
    //
    //    var projectMock = new Mock<IProject>();
    //    var holidayRepo = new HolidayPlanRepository(associationMock.Object, holidayPlanMock.Object);
    //    // Act
    //    var result = holidayRepo.FindAllHolidayPeriodsForAllProjectCollaboratorsBetweenDates(
    //        projectMock.Object,
    //        initDate,
    //        endDate
    //    );
    //
    //    Assert.Equal(expectedPeriods.Count, result.Count());
    //}

    //[Fact]
    //public void WhenNoHolidayPeriodsForCollaboratorWithinInterval_ThenReturnEmptyList()
    //{
    //    // Arrange
    //    var projectMock = new Mock<IProject>();
    //    var collaboratorMock = new Mock<ICollaborator>();
    //    var associationMock = new Mock<IAssociationProjectCollaboratorRepository>();
    //    associationMock
    //        .Setup(a =>
    //            a.FindAllByProjectAndPeriod(
    //                It.IsAny<IProject>(),
    //                It.IsAny<DateOnly>(),
    //                It.IsAny<DateOnly>()
    //            )
    //        )
    //        .Returns(new List<ICollaborator> { collaboratorMock.Object });
    //
    //    var holidayPlanMock = new Mock<IHolidayPlan>();
    //    holidayPlanMock.Setup(hp => hp.GetCollaborator()).Returns(collaboratorMock.Object);
    //    holidayPlanMock
    //        .Setup(hp => hp.GetHolidayPeriods())
    //        .Returns(new List<IHolidayPeriod>());
    //
    //    var holidayRepo = new HolidayPlanRepository(associationMock.Object, holidayPlanMock.Object);
    //
    //    // Act
    //    var result = holidayRepo.FindAllHolidayPeriodsForAllProjectCollaboratorsBetweenDates(
    //        projectMock.Object,
    //        new DateOnly(2025, 6, 1),
    //        new DateOnly(2025, 6, 10)
    //    );
    //
    //    // Assert
    //    Assert.Empty(result);
    //}

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

        var associationMock = new Mock<IAssociationProjectCollaborator>();
        associationMock.Setup(a => a.GetCollaborator()).Returns(collaboratorMock.Object);
        associationMock.Setup(a => a.GetProject()).Returns(projectMock.Object);
        associationMock
            .Setup(a => a.AssociationIntersectDates(It.IsAny<DateOnly>(), It.IsAny<DateOnly>()))
            .Returns(true);

        var holidayPeriodMock = new Mock<IHolidayPeriod>();
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

        var holidayPlanMock = new Mock<IHolidayPlan>();
        holidayPlanMock.Setup(hp => hp.GetCollaborator()).Returns(collaboratorMock.Object);
        holidayPlanMock
            .Setup(hp => hp.GetHolidayPeriods())
            .Returns(new List<IHolidayPeriod> { holidayPeriodMock.Object });
        var holidayPlans = new List<IHolidayPlan> { holidayPlanMock.Object };
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

    [Fact]
    public void GivenProjectWithCollaborators_WhenGetHolidayDaysForAllCollaborators_ThenReturnsCorrectDays()
    {
        // Arrange
        var mockColaborator = new Mock<ICollaborator>();
        var mockHolidayPlan = new Mock<IHolidayPlan>();

        // Configurando o colaborador
        mockColaborator.Setup(c => c.ContractContainsDates(It.IsAny<DateTime>(), It.IsAny<DateTime>())).Returns(true);

        // Período de férias
        var mockHolidayPeriod = new Mock<IHolidayPeriod>();
        mockHolidayPeriod.Setup(h => h.GetInitDate()).Returns(new DateOnly(2024, 6, 1));
        mockHolidayPeriod.Setup(h => h.GetFinalDate()).Returns(new DateOnly(2024, 6, 10));
        mockHolidayPeriod.Setup(h => h.GetDurationInDays(It.IsAny<DateOnly>(), It.IsAny<DateOnly>())).Returns(10);

        // Criando o plano de férias para o colaborador
        var holidayPlan = new HolidayPlan(mockHolidayPeriod.Object, mockColaborator.Object);

        // Criar lista de colaboradores
        var collaboratorsList = new List<ICollaborator>() { mockColaborator.Object };

        // Criando a instância do repositório de planos de férias
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



    // US13

    public static IEnumerable<object[]> ValidDates()
    {
        yield return new object[]
        {
            // data dentro do periodo procurado
            new DateOnly(2025, 7, 15),
            new DateOnly(2025, 8, 1),
            new DateOnly(2025, 7, 20),
            new DateOnly(2025, 7, 25),
        };
        yield return new object[]
        {
            // começa antes, mas termina dentro do periodo procurado
            new DateOnly(2025, 7, 15),
            new DateOnly(2025, 8, 1),
            new DateOnly(2025, 7, 5),
            new DateOnly(2025, 7, 20),
        };
        yield return new object[]
        {
            // começa antes, mas termina exatamente no inicio do periodo procurado
            new DateOnly(2025, 7, 15),
            new DateOnly(2025, 8, 1),
            new DateOnly(2025, 7, 5),
            new DateOnly(2025, 7, 15),
        };
        yield return new object[]
        {
            // começa dentro, mas termina depois do periodo procurado
            new DateOnly(2025, 7, 15),
            new DateOnly(2025, 8, 1),
            new DateOnly(2025, 7, 25),
            new DateOnly(2025, 8, 15),
        };
        yield return new object[]
        {
            // começa dentro e termina exatamente no ultimo dia do periodo procurado
            new DateOnly(2025, 7, 15),
            new DateOnly(2025, 8, 1),
            new DateOnly(2025, 7, 25),
            new DateOnly(2025, 8, 1),
        };
        yield return new object[]
        {
            // tudo no mesmo dia
            new DateOnly(2025, 7, 15),
            new DateOnly(2025, 7, 15),
            new DateOnly(2025, 7, 15),
            new DateOnly(2025, 7, 15),
        };
    }

    [Theory]
    [MemberData(nameof(ValidDates))]
    public void WhenPassinValidDates_ThenReturnsCorrectPeriod(
        DateOnly initDateForSearching,
        DateOnly endDateForSearching,
        DateOnly initDateForPeriod,
        DateOnly endDateForPeriod
    )
    {
        // Arrange
        var collaborator = new Mock<ICollaborator>();

        var holidayPeriod = new Mock<IHolidayPeriod>();
        holidayPeriod.Setup(p => p.GetInitDate()).Returns(initDateForPeriod);
        holidayPeriod.Setup(p => p.GetFinalDate()).Returns(endDateForPeriod);

        var holidayPlan = new Mock<IHolidayPlan>();
        holidayPlan.Setup(hp => hp.HasCollaborator(collaborator.Object)).Returns(true);
        holidayPlan
            .Setup(hp => hp.GetHolidayPeriods())
            .Returns(new List<IHolidayPeriod> { holidayPeriod.Object });

        // Adicionar o plano de férias ao repositório
        var hpRepo = new HolidayPlanRepository(new List<IHolidayPlan> { holidayPlan.Object });

        // Act
        var result = hpRepo.FindAllHolidayPeriodsForCollaboratorBetweenDates(
            collaborator.Object,
            initDateForSearching,
            endDateForSearching
        );

        // Assert
        Assert.Single(result);
    }

    public static IEnumerable<object[]> MultipleValidDates()
    {
        yield return new object[]
        {
            // quando o primeiro acaba no ultimo dia procurado, e o segundo começa no primeiro dia procurado
            new DateOnly(2025, 7, 15),
            new DateOnly(2025, 8, 1),
            new DateOnly(2025, 7, 5),
            new DateOnly(2025, 7, 15),
            new DateOnly(2025, 8, 1),
            new DateOnly(2025, 8, 10),
        };
        yield return new object[]
        {
            // quando o primeiro começa no primeiro dia procurado, e o segundo termina no ultimo dia procurado
            new DateOnly(2025, 7, 15),
            new DateOnly(2025, 8, 1),
            new DateOnly(2025, 7, 15),
            new DateOnly(2025, 7, 20),
            new DateOnly(2025, 7, 25),
            new DateOnly(2025, 8, 1),
        };
    }

    [Theory]
    [MemberData(nameof(MultipleValidDates))]
    public void WhenTwoPeriodsStartAndEndAtSearchingEdgeCase_ThenReturnsCorrectPeriods(
        DateOnly initDateForSearching,
        DateOnly endDateForSearching,
        DateOnly initDateForPeriod_1,
        DateOnly endDateForPeriod_1,
        DateOnly initDateForPeriod_2,
        DateOnly endDateForPeriod_2
    )
    {
        var collaborator = new Mock<ICollaborator>();

        var holidayPeriod1 = new Mock<IHolidayPeriod>();
        holidayPeriod1.Setup(p => p.GetInitDate()).Returns(initDateForPeriod_1);
        holidayPeriod1.Setup(p => p.GetFinalDate()).Returns(endDateForPeriod_1);

        var holidayPeriod2 = new Mock<IHolidayPeriod>();
        holidayPeriod2.Setup(p => p.GetInitDate()).Returns(initDateForPeriod_2);
        holidayPeriod2.Setup(p => p.GetFinalDate()).Returns(endDateForPeriod_2);

        var holidayPlan = new Mock<IHolidayPlan>();
        holidayPlan.Setup(hp => hp.HasCollaborator(collaborator.Object)).Returns(true);
        holidayPlan
            .Setup(hp => hp.GetHolidayPeriods())
            .Returns(new List<IHolidayPeriod> { holidayPeriod1.Object, holidayPeriod2.Object });

        // Adicionar o plano de férias ao repositório
        var hpRepo = new HolidayPlanRepository(new List<IHolidayPlan> { holidayPlan.Object });

        // Act
        var result = hpRepo.FindAllHolidayPeriodsForCollaboratorBetweenDates(
            collaborator.Object,
            initDateForSearching,
            endDateForSearching
        );

        // assert
        Assert.Equal(2, result.Count());
        Assert.Contains(holidayPeriod1.Object, result);
        Assert.Contains(holidayPeriod2.Object, result);
    }

    [Fact]
    public void WhenHolidayPeriodIsOutOfRangeThanTheOneBeingSearchedFor_ThenReturnsEmptyList()
    {
        // Arrange
        var collaborator = new Mock<ICollaborator>();
        var initDateForSearching = new DateOnly(2025, 7, 15);
        var endDateForSearching = new DateOnly(2025, 8, 1);
        var initDateForPeriod = new DateOnly(2025, 9, 20);
        var endDateForPeriod = new DateOnly(2025, 10, 25);

        var holidayPeriod = new Mock<IHolidayPeriod>();
        holidayPeriod.Setup(p => p.GetInitDate()).Returns(initDateForPeriod);
        holidayPeriod.Setup(p => p.GetFinalDate()).Returns(endDateForPeriod);

        var holidayPlan = new Mock<IHolidayPlan>();
        holidayPlan.Setup(hp => hp.HasCollaborator(collaborator.Object)).Returns(true);
        holidayPlan
            .Setup(hp => hp.GetHolidayPeriods())
            .Returns(new List<IHolidayPeriod> { holidayPeriod.Object });

        // Adicionar o plano de férias ao repositório
        var hpRepo = new HolidayPlanRepository(new List<IHolidayPlan> { holidayPlan.Object });

        // Act
        var result = hpRepo.FindAllHolidayPeriodsForCollaboratorBetweenDates(
            collaborator.Object,
            initDateForSearching,
            endDateForSearching
        );

        // Assert
        Assert.Empty(result);
    }

    // ----

    [Fact]
    public void WhenInitDateBiggerThanFinalDate_ThenReturnsEmptyList()
    {
        // Arrange
        var holidayPlans = new List<IHolidayPlan>();
        var hpRepo = new HolidayPlanRepository(holidayPlans);
        var collaborator = new Mock<ICollaborator>();
        var initDate = new DateOnly(2025, 8, 15);
        var endDate = new DateOnly(2025, 7, 1);

        // Act
        var result = hpRepo.FindAllHolidayPeriodsForCollaboratorBetweenDates(
            collaborator.Object,
            initDate,
            endDate
        );

        // asset
        Assert.Empty(result);
    }

    [Fact]
    public void WhenCalculatingHolidayDaysOfCollaboratorInAProject_ThenReturnCorrectValue()
    {
        //arrange
        Mock<IAssociationProjectCollaborator> associationDouble = new Mock<IAssociationProjectCollaborator>();
        Mock<IHolidayPlan> holidayPlanDouble = new Mock<IHolidayPlan>();
        Mock<ICollaborator> collaboratorDouble = new Mock<ICollaborator>();

        DateOnly initDate = new DateOnly(2025, 6, 1);
        DateOnly finalDate = new DateOnly(2025, 6, 10);

        associationDouble.Setup(a => a.GetCollaborator()).Returns(collaboratorDouble.Object);
        associationDouble.Setup(a => a.GetInitDate()).Returns(initDate);
        associationDouble.Setup(a => a.GetFinalDate()).Returns(finalDate);

        holidayPlanDouble.Setup(hp => hp.GetCollaborator()).Returns(collaboratorDouble.Object);
        holidayPlanDouble.Setup(hp => hp.GetNumberOfHolidayDaysBetween(initDate, finalDate)).Returns(5);

        IHolidayPlanRepository repository = new HolidayPlanRepository(new List<IHolidayPlan> { holidayPlanDouble.Object });

        //act
        int result = repository.GetHolidayDaysOfCollaboratorInProject(associationDouble.Object);

        //assert
        Assert.Equal(5, result);

    }


    [Fact]
    public void WhenNoHolidayPlanIsFound_ThenReturnsZero()
    {
        //arrange
        Mock<IAssociationProjectCollaborator> associationDouble = new Mock<IAssociationProjectCollaborator>();
        Mock<ICollaborator> collaboratorDouble = new Mock<ICollaborator>();

        associationDouble.Setup(a => a.GetCollaborator()).Returns(collaboratorDouble.Object);

        List<IHolidayPlan> emptyList = new List<IHolidayPlan>();
        IHolidayPlanRepository repository = new HolidayPlanRepository(emptyList);

        //act
        int result = repository.GetHolidayDaysOfCollaboratorInProject(associationDouble.Object);

        //assert
        Assert.Equal(0, result);
    }

    [Fact]
    public void WhenPassingCorrectValues_ThenObjectIsCreated()
    {
        //arrange
        Mock<IHolidayPlan> holidayPlanDouble = new Mock<IHolidayPlan>();
        List<IHolidayPlan> holidayPlans = new List<IHolidayPlan> { holidayPlanDouble.Object };

        //act
        new HolidayPlanRepository(holidayPlans);

        //assert
    }

    [Fact]
    public void WhenFindingALlCollaboratorsWithHolidayPeriodsLongerThan_ThenShouldReturnCorrectCollaborators()
    {
        //arrange
        int days = 5;

        Mock<ICollaborator> collaboratorDouble1 = new Mock<ICollaborator>();
        Mock<ICollaborator> collaboratorDouble2 = new Mock<ICollaborator>();

        Mock<IHolidayPlan> holidayPlanDouble1 = new Mock<IHolidayPlan>();
        holidayPlanDouble1.Setup(p => p.HasPeriodLongerThan(days)).Returns(true);
        holidayPlanDouble1.Setup(p => p.GetCollaborator()).Returns(collaboratorDouble1.Object);

        Mock<IHolidayPlan> holidayPlanDouble2 = new Mock<IHolidayPlan>();
        holidayPlanDouble2.Setup(p => p.HasPeriodLongerThan(days)).Returns(false);
        holidayPlanDouble2.Setup(p => p.GetCollaborator()).Returns(collaboratorDouble2.Object);

        IHolidayPlanRepository repository = new HolidayPlanRepository(new List<IHolidayPlan> { holidayPlanDouble1.Object, holidayPlanDouble2.Object });

        //act
        var result = repository.FindAllCollaboratorsWithHolidayPeriodsLongerThan(days).ToList();

        //assert
        Assert.Contains(collaboratorDouble1.Object, result);
        Assert.DoesNotContain(collaboratorDouble2.Object, result);
    }

    [Fact]
    public void WhenNoCollaboratorHaveHolidayPeriodLongerThan_ThenShouldReturnEmptyList()
    {
        //arrange
        int days = 5;

        Mock<ICollaborator> collaboratorDouble1 = new Mock<ICollaborator>();
        Mock<ICollaborator> collaboratorDouble2 = new Mock<ICollaborator>();

        Mock<IHolidayPlan> holidayPlanDouble1 = new Mock<IHolidayPlan>();
        holidayPlanDouble1.Setup(p => p.HasPeriodLongerThan(days)).Returns(false);
        holidayPlanDouble1.Setup(p => p.GetCollaborator()).Returns(collaboratorDouble1.Object);

        Mock<IHolidayPlan> holidayPlanDouble2 = new Mock<IHolidayPlan>();
        holidayPlanDouble2.Setup(p => p.HasPeriodLongerThan(days)).Returns(false);
        holidayPlanDouble2.Setup(p => p.GetCollaborator()).Returns(collaboratorDouble2.Object);

        IHolidayPlanRepository repository = new HolidayPlanRepository(new List<IHolidayPlan> { holidayPlanDouble1.Object, holidayPlanDouble2.Object });

        //act
        var result = repository.FindAllCollaboratorsWithHolidayPeriodsLongerThan(days).ToList();

        //assert
        Assert.Empty(result);
    }

    [Fact]
    public void WhenGivenCollaboratorAndGoodDate_ThenReturnPeriod()
    {
        //arrange
        DateOnly date = new DateOnly(2020, 1, 1);
        var collab = new Mock<ICollaborator>();

        var holidayPeriod = new Mock<IHolidayPeriod>();

        var holidayPlan = new Mock<IHolidayPlan>();
        holidayPlan.Setup(a => a.HasCollaborator(collab.Object)).Returns(true);
        holidayPlan.Setup(a => a.GetHolidayPeriodContainingDate(date)).Returns(holidayPeriod.Object);

        var hpRepo = new HolidayPlanRepository(holidayPlan.Object);

        //act
        var result = hpRepo.GetHolidayPeriodContainingDate(collab.Object, date);

        //assert
        Assert.NotNull(result);
    }

    [Fact]
    public void WhenGivenCollaboratorAndBadDate_ThenReturnEmpty()
    {
        //arrange
        DateOnly date = new DateOnly(2020, 1, 1);
        var collab = new Mock<ICollaborator>();

        var holidayPlan = new Mock<IHolidayPlan>();
        holidayPlan.Setup(a => a.HasCollaborator(collab.Object)).Returns(true);
        holidayPlan.Setup(a => a.GetHolidayPeriodContainingDate(date)).Returns((IHolidayPeriod?)null);

        var hpRepo = new HolidayPlanRepository(holidayPlan.Object);

        //act
        var result = hpRepo.GetHolidayPeriodContainingDate(collab.Object, date);

        //assert
        Assert.Null(result);
    }

    [Fact]
    public void WhenGivenGoodCollaboratorAndDatesAndLength_ThenReturnPeriods()
    {
        //arrange
        DateOnly ini = new DateOnly(2020, 1, 1);
        DateOnly end = new DateOnly(2020, 3, 1);
        var collab = new Mock<ICollaborator>();

        var holidayPeriod = new Mock<IHolidayPeriod>();

        var holidayPlan = new Mock<IHolidayPlan>();
        holidayPlan.Setup(a => a.HasCollaborator(collab.Object)).Returns(true);

        IEnumerable<IHolidayPeriod> ret = new List<IHolidayPeriod>() { holidayPeriod.Object };
        holidayPlan.Setup(a => a.FindAllHolidayPeriodsBetweenDatesLongerThan(ini, end, 4)).Returns(ret);

        var hpRepo = new HolidayPlanRepository(holidayPlan.Object);

        //act
        var result = hpRepo.FindAllHolidayPeriodsLongerThanForCollaboratorBetweenDates(collab.Object, ini, end, 4);

        //assert
        Assert.Equal(ret, result);
    }

    [Fact]
    public void WhenGivenBadCollaboratorAndDatesAndLength_ThenReturnEmptyLists()
    {
        //arrange
        DateOnly ini = new DateOnly(2020, 1, 1);
        DateOnly end = new DateOnly(2020, 3, 1);
        var collab = new Mock<ICollaborator>();

        var holidayPlan = new Mock<IHolidayPlan>();
        holidayPlan.Setup(a => a.HasCollaborator(collab.Object)).Returns(true);

        IEnumerable<IHolidayPeriod> ret = new List<IHolidayPeriod>();
        holidayPlan.Setup(a => a.FindAllHolidayPeriodsBetweenDatesLongerThan(ini, end, 4)).Returns(ret);

        var hpRepo = new HolidayPlanRepository(holidayPlan.Object);

        //act
        var result = hpRepo.FindAllHolidayPeriodsLongerThanForCollaboratorBetweenDates(collab.Object, ini, end, 4);

        //assert
        Assert.Empty(result);
    }
}
