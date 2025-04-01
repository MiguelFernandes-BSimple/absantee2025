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

        Mock<IHolidayPlan> holidayPlan = new Mock<IHolidayPlan>();
        holidayPlan.Setup(hp => hp.GetCollaborator()).Returns(collab.Object);

        Mock<IHolidayPeriod> holidayPeriod = new Mock<IHolidayPeriod>();
        DateOnly holidayPeriodStartDate = new DateOnly(2025, 04, 02);
        DateOnly holidayPeriodFinalDate = new DateOnly(2025, 04, 07);
        holidayPeriod.Setup(hp => hp.GetInitDate()).Returns(holidayPeriodStartDate);
        holidayPeriod.Setup(hp => hp.GetFinalDate()).Returns(holidayPeriodFinalDate);

        var holidayPeriodsList = new List<IHolidayPeriod> { holidayPeriod.Object };
        holidayPlan.Setup(hp => hp.HasCollaborator(collab.Object)).Returns(true);
        holidayPlan.Setup(hp => hp.GetHolidayPeriods()).Returns(holidayPeriodsList);


        HolidayPlanRepository repository = new HolidayPlanRepository(holidayPlan.Object);

        //act
        var result = repository.FindAllHolidayPeriodsForCollaboratorBetweenDatesThatIncludeWeekends(collab.Object, searchInitDate, searchEndDate);

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

    [Theory]
    [MemberData(nameof(ValidHolidayDatesWithoutWeekends))]
    public void WhenRetrievingAllHolidayPeriodsForCollaboratorBetweenWithDatesThatDontIncludeWeekends_ThenReturnEmpty(DateOnly searchInitDate, DateOnly searchEndDate)
    {

        //arrange
        Mock<ICollaborator> collab = new Mock<ICollaborator>();

        Mock<IHolidayPlan> holidayPlan = new Mock<IHolidayPlan>();
        holidayPlan.Setup(hp => hp.GetCollaborator()).Returns(collab.Object);

        Mock<IHolidayPeriod> holidayPeriod = new Mock<IHolidayPeriod>();
        DateOnly holidayPeriodStartDate = new DateOnly(2025, 04, 01);
        DateOnly holidayPeriodFinalDate = new DateOnly(2025, 04, 09);
        holidayPeriod.Setup(hp => hp.GetInitDate()).Returns(holidayPeriodStartDate);
        holidayPeriod.Setup(hp => hp.GetFinalDate()).Returns(holidayPeriodFinalDate);

        var holidayPeriodsList = new List<IHolidayPeriod> { holidayPeriod.Object };
        holidayPlan.Setup(hp => hp.HasCollaborator(collab.Object)).Returns(true);
        holidayPlan.Setup(hp => hp.GetHolidayPeriods()).Returns(holidayPeriodsList);


        HolidayPlanRepository repository = new HolidayPlanRepository(holidayPlan.Object);

        //act
        var result = repository.FindAllHolidayPeriodsForCollaboratorBetweenDatesThatIncludeWeekends(collab.Object, searchInitDate, searchEndDate);

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

        Mock<IHolidayPlan> holidayPlan1 = new Mock<IHolidayPlan>();
        holidayPlan1.Setup(hp => hp.GetCollaborator()).Returns(collab1.Object);

        Mock<IHolidayPeriod> holidayPeriod1 = new Mock<IHolidayPeriod>();
        holidayPeriod1.Setup(hp => hp.GetInitDate()).Returns(holidayPeriodStartDate1);
        holidayPeriod1.Setup(hp => hp.GetFinalDate()).Returns(holidayPeriodFinalDate1);

        var holidayPeriodsList1 = new List<IHolidayPeriod> { holidayPeriod1.Object };
        holidayPlan1.Setup(hp => hp.HasCollaborator(collab1.Object)).Returns(true);
        holidayPlan1.Setup(hp => hp.GetHolidayPeriods()).Returns(holidayPeriodsList1);

        //colab2
        Mock<ICollaborator> collab2 = new Mock<ICollaborator>();

        Mock<IHolidayPlan> holidayPlan2 = new Mock<IHolidayPlan>();
        holidayPlan2.Setup(hp => hp.GetCollaborator()).Returns(collab2.Object);

        Mock<IHolidayPeriod> holidayPeriod2 = new Mock<IHolidayPeriod>();
        holidayPeriod2.Setup(hp => hp.GetInitDate()).Returns(holidayPeriodStartDate2);
        holidayPeriod2.Setup(hp => hp.GetFinalDate()).Returns(holidayPeriodFinalDate2);

        var holidayPeriodsList2 = new List<IHolidayPeriod> { holidayPeriod2.Object };
        holidayPlan2.Setup(hp => hp.HasCollaborator(collab2.Object)).Returns(true);
        holidayPlan2.Setup(hp => hp.GetHolidayPeriods()).Returns(holidayPeriodsList2);


        HolidayPlanRepository repository = new HolidayPlanRepository(new List<IHolidayPlan> { holidayPlan1.Object, holidayPlan2.Object });

        //act
        var result = repository.FindAllOverlappingHolidayPeriodsBetweenTwoCollaboratorsBetweenDates(collab1.Object, collab2.Object, searchInitDate, searchEndDate);

        //assert
        Assert.Equal(holidayPeriodStartDate1, result.First().GetInitDate());
        Assert.Equal(holidayPeriodFinalDate1, result.First().GetFinalDate());
        Assert.Equal(holidayPeriodStartDate2, result.ToList()[1].GetInitDate());
        Assert.Equal(holidayPeriodFinalDate2, result.ToList()[1].GetFinalDate());
    }

    public static IEnumerable<object[]> SearchOverlappingPeriodsOutsideHolidayPeriod()
    {
        // intersection
        yield return new object[] {
            new DateOnly(2025, 04, 02), new DateOnly(2025, 04, 08),
            new DateOnly(2025, 04, 05), new DateOnly(2025, 04, 12),
            new DateOnly(2025, 04, 13), new DateOnly(2025, 04, 20),
        };
        // search for a specific day that contains only 1 holiday period
        yield return new object[] {
            new DateOnly(2025, 04, 02), new DateOnly(2025, 04, 12),
            new DateOnly(2025, 04, 04), new DateOnly(2025, 04, 11),
            new DateOnly(2025, 04, 12), new DateOnly(2025, 04, 12),
        };
        // search for a specific day that contains only 1 holiday period
        yield return new object[] {
            new DateOnly(2025, 04, 02), new DateOnly(2025, 04, 12),
            new DateOnly(2025, 04, 04), new DateOnly(2025, 04, 11),
            new DateOnly(2025, 04, 02), new DateOnly(2025, 04, 02),
        };
        // bad dates, initial date after end date
        yield return new object[] {
            new DateOnly(2025, 04, 12), new DateOnly(2025, 04, 02),
            new DateOnly(2025, 04, 11), new DateOnly(2025, 04, 04),
            new DateOnly(2025, 04, 10), new DateOnly(2025, 04, 02),
        };
        // holiday periods don't intercept (2nd after)
        yield return new object[] {
            new DateOnly(2025, 04, 02), new DateOnly(2025, 04, 10),
            new DateOnly(2025, 04, 11), new DateOnly(2025, 04, 15),
            new DateOnly(2025, 04, 01), new DateOnly(2025, 04, 15),
        };
        // holiday periods don't intercept lower (1st after)
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

        Mock<IHolidayPlan> holidayPlan1 = new Mock<IHolidayPlan>();
        holidayPlan1.Setup(hp => hp.GetCollaborator()).Returns(collab1.Object);

        Mock<IHolidayPeriod> holidayPeriod1 = new Mock<IHolidayPeriod>();
        holidayPeriod1.Setup(hp => hp.GetInitDate()).Returns(holidayPeriodStartDate1);
        holidayPeriod1.Setup(hp => hp.GetFinalDate()).Returns(holidayPeriodFinalDate1);

        var holidayPeriodsList1 = new List<IHolidayPeriod> { holidayPeriod1.Object };
        holidayPlan1.Setup(hp => hp.HasCollaborator(collab1.Object)).Returns(true);
        holidayPlan1.Setup(hp => hp.GetHolidayPeriods()).Returns(holidayPeriodsList1);

        //colab2
        Mock<ICollaborator> collab2 = new Mock<ICollaborator>();

        Mock<IHolidayPlan> holidayPlan2 = new Mock<IHolidayPlan>();
        holidayPlan2.Setup(hp => hp.GetCollaborator()).Returns(collab2.Object);

        Mock<IHolidayPeriod> holidayPeriod2 = new Mock<IHolidayPeriod>();
        holidayPeriod2.Setup(hp => hp.GetInitDate()).Returns(holidayPeriodStartDate2);
        holidayPeriod2.Setup(hp => hp.GetFinalDate()).Returns(holidayPeriodFinalDate2);

        var holidayPeriodsList2 = new List<IHolidayPeriod> { holidayPeriod2.Object };
        holidayPlan2.Setup(hp => hp.HasCollaborator(collab2.Object)).Returns(true);
        holidayPlan2.Setup(hp => hp.GetHolidayPeriods()).Returns(holidayPeriodsList2);


        HolidayPlanRepository repository = new HolidayPlanRepository(new List<IHolidayPlan> { holidayPlan1.Object, holidayPlan2.Object });

        //act
        var result = repository.FindAllOverlappingHolidayPeriodsBetweenTwoCollaboratorsBetweenDates(collab1.Object, collab2.Object, searchInitDate, searchEndDate);

        //assert
        Assert.Empty(result);
    }
    //uc21 
    public static IEnumerable<object[]> GetHolidayPeriodsForAllCollaboratorsBetweenDatesData()
    {
        yield return new object[]
    {
        new DateOnly(2025, 7, 1),
        new DateOnly(2025, 7, 31),
        new DateOnly(2025, 7, 1),
        new DateOnly(2025, 7, 10),
        1
    };

        yield return new object[]
        {
        new DateOnly(2025, 6, 1),
        new DateOnly(2025, 6, 30),
        new DateOnly(2025, 6, 20), // comeca final
        new DateOnly(2025, 7, 5),
        1
        };

        yield return new object[]
        {
        new DateOnly(2025, 8, 1),
        new DateOnly(2025, 8, 31),
        new DateOnly(2025, 7, 1),
        new DateOnly(2025, 7, 31),//fora
        0
        };

        yield return new object[]
        {
        new DateOnly(2025, 7, 1),
        new DateOnly(2025, 7, 31),
        new DateOnly(2025, 6, 20),
        new DateOnly(2025, 7, 10),
        1
        };

        yield return new object[]
        {
        new DateOnly(2025, 6, 1),
        new DateOnly(2025, 6, 30),
        new DateOnly(2025, 6, 10),
        new DateOnly(2025, 6, 30),//final periodo
        1
        };

        yield return new object[]
        {
        new DateOnly(2025, 6, 1),
        new DateOnly(2025, 6, 30),
        new DateOnly(2025, 6, 10),
        new DateOnly(2025, 6, 20),
        1
        };

        // Nenhum colaborador tem ferias
        yield return new object[]
        {
        new DateOnly(2025, 6, 1),
        new DateOnly(2025, 6, 30),
        DateOnly.MinValue,
        DateOnly.MinValue,
        0
        };
        yield return new object[]
        {
        new DateOnly(2025, 7, 1),
        new DateOnly(2025, 7, 31),
        new DateOnly(2025, 6, 15),
        new DateOnly(2025, 7, 15),
        1
        };

        yield return new object[]
        {
        new DateOnly(2025, 7, 1),
        new DateOnly(2025, 7, 31),
        new DateOnly(2025, 7, 1),
        new DateOnly(2025, 7, 20),
        1
        };

        yield return new object[]
        {
        new DateOnly(2025, 6, 1),
        new DateOnly(2025, 6, 30),
        new DateOnly(2025, 6, 5),
        new DateOnly(2025, 7, 10),
        1
        };
        yield return new object[]
        {
        new DateOnly(2025, 7, 1),
        new DateOnly(2025, 7, 31),
        new DateOnly(2025, 6, 15),
        new DateOnly(2025, 8, 10),
        1
        };

        // periodo começa antes e termina dentro
        yield return new object[]
        {
        new DateOnly(2025, 6, 1),
        new DateOnly(2025, 6, 30),
        new DateOnly(2025, 5, 20),
        new DateOnly(2025, 6, 15),
        1
        };
        yield return new object[]
        {
        new DateOnly(2025, 6, 1),
        new DateOnly(2025, 6, 1),
        new DateOnly(2025, 6, 1),
        new DateOnly(2025, 6, 1),
        1
        };

    }

    [Theory]
    [MemberData(nameof(GetHolidayPeriodsForAllCollaboratorsBetweenDatesData))]
    public void WhenFindingAllHolidayPeriodsForAllCollaboratorsBetweenDates_ThenReturnsCorrectHolidayPeriods(
        DateOnly initDate,
        DateOnly endDate,
        DateOnly holidayInitDate,
        DateOnly holidayEndDate,
        int expectedPeriods
    )
    {
        // Arrange
        var collaboratorMock = new Mock<ICollaborator>();

        var holidayPeriodMock = new Mock<IHolidayPeriod>();
        holidayPeriodMock.Setup(hp => hp.GetInitDate()).Returns(holidayInitDate);
        holidayPeriodMock.Setup(hp => hp.GetFinalDate()).Returns(holidayEndDate);

        var holidayPlanMock = new Mock<IHolidayPlan>();
        holidayPlanMock.Setup(hp => hp.GetCollaborator()).Returns(collaboratorMock.Object);
        holidayPlanMock.Setup(hp => hp.GetHolidayPeriods()).Returns(new List<IHolidayPeriod> { holidayPeriodMock.Object });

        var holidayPlanRepo = new HolidayPlanRepository(new List<IHolidayPlan> { holidayPlanMock.Object });

        // Act
        var result = holidayPlanRepo.FindAllHolidayPeriodsForAllCollaboratorsBetweenDates(
            new List<ICollaborator> { collaboratorMock.Object },
            initDate,
            endDate
        );

        // Assert
        Assert.Equal(expectedPeriods, result.Count());
    }

    //uc22
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
