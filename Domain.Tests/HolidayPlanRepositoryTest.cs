using Domain;
using Moq;

namespace Domain.Tests;
public class HolidayPlanRepositoryTest
{
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

    [Fact]
    public void WhenInitDateBiggerThanFinalDate_ThenThrowsArgumentException()
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

    // US14
    public static IEnumerable<object[]> ValidDatesCollaborator()
    {
        // quando o periodo de ferias está dentro do que esta a ser procurado
        yield return new object[]
        {
            new DateOnly(2025, 7, 15),
            new DateOnly(2025, 8, 1),
            new DateOnly(2025, 7, 20),
            new DateOnly(2025, 7, 25),
        };

        // quando o periodo começa antes, mas acaba dentro do procurado
        yield return new object[]
        {
            new DateOnly(2025, 7, 15),
            new DateOnly(2025, 8, 1),
            new DateOnly(2025, 7, 5),
            new DateOnly(2025, 7, 25),
        };

        // quando o periodo começa dentro do procurado, mas termina depois
        yield return new object[]
        {
            new DateOnly(2025, 7, 15),
            new DateOnly(2025, 8, 1),
            new DateOnly(2025, 7, 25),
            new DateOnly(2025, 8, 5),
        };

        // quando o periodo começa antes e termina no primeiro dia procurado
        yield return new object[]
        {
            new DateOnly(2025, 7, 15),
            new DateOnly(2025, 8, 1),
            new DateOnly(2025, 7, 5),
            new DateOnly(2025, 7, 15),
        };

        // quando o periodo começa no ultimo dia procurado
        yield return new object[]
        {
            new DateOnly(2025, 7, 15),
            new DateOnly(2025, 8, 1),
            new DateOnly(2025, 8, 1),
            new DateOnly(2025, 8, 10),
        };

        // quando é tudo no mesmo dia
        yield return new object[]
        {
            new DateOnly(2025, 7, 15),
            new DateOnly(2025, 7, 15),
            new DateOnly(2025, 7, 15),
            new DateOnly(2025, 7, 15),
        };
    }

    [Theory]
    [MemberData(nameof(ValidDatesCollaborator))]
    public void WhenCollaboratorHasHolidayPeriodWithinDateRange_ThenReturnsCollaborator(
        DateOnly initDate,
        DateOnly endDate,
        DateOnly initDatePeriod,
        DateOnly endDatePeriod
    )
    {
        // Arrange
        var collaborator = new Mock<ICollaborator>();

        var holidayPeriod = new Mock<IHolidayPeriod>();
        holidayPeriod.Setup(p => p.GetInitDate()).Returns(initDatePeriod);
        holidayPeriod.Setup(p => p.GetFinalDate()).Returns(endDatePeriod);

        var holidayPlan = new Mock<IHolidayPlan>();
        holidayPlan.Setup(hp => hp.HasCollaborator(collaborator.Object)).Returns(true);
        holidayPlan
            .Setup(hp => hp.GetHolidayPeriods())
            .Returns(new List<IHolidayPeriod> { holidayPeriod.Object });
        holidayPlan.Setup(hp => hp.GetCollaborator()).Returns(collaborator.Object);

        var hpRepo = new HolidayPlanRepository(new List<IHolidayPlan> { holidayPlan.Object });
        // Act
        var result = hpRepo.FindAllCollaboratorsWithHolidayPeriodsBetweenDates(initDate, endDate);

        // Assert
        Assert.Single(result);
        Assert.Contains(collaborator.Object, result);
    }

    public static IEnumerable<object[]> ValidDatesMultipleCollaborators()
    {
        // para dois collaboradores, com periodos que têm uma data dentro do periodo procurado
        yield return new object[]
        {
            new DateOnly(2025, 7, 15),
            new DateOnly(2025, 8, 1),
            new DateOnly(2025, 7, 10),
            new DateOnly(2025, 7, 20),
            new DateOnly(2025, 7, 18),
            new DateOnly(2025, 7, 25),
        };
        yield return new object[]
        {
            new DateOnly(2025, 7, 15),
            new DateOnly(2025, 8, 1),
            new DateOnly(2025, 7, 10),
            new DateOnly(2025, 7, 20),
            new DateOnly(2025, 8, 1),
            new DateOnly(2025, 8, 15),
        };
    }

    [Theory]
    [MemberData(nameof(ValidDatesMultipleCollaborators))]
    public void WhenMultipleCollaboratorsHaveHolidayPeriodsWithinDateRange_ThenReturnsAllCollaborators(
        DateOnly initDate,
        DateOnly endDate,
        DateOnly initDatePeriod1,
        DateOnly endDatePeriod1,
        DateOnly initDatePeriod2,
        DateOnly endDatePeriod2
    )
    {
        // Arrange
        var collaborator1 = new Mock<ICollaborator>();
        var collaborator2 = new Mock<ICollaborator>();

        var holidayPeriod1 = new Mock<IHolidayPeriod>();
        holidayPeriod1.Setup(p => p.GetInitDate()).Returns(initDatePeriod1);
        holidayPeriod1.Setup(p => p.GetFinalDate()).Returns(endDatePeriod1);

        var holidayPeriod2 = new Mock<IHolidayPeriod>();
        holidayPeriod2.Setup(p => p.GetInitDate()).Returns(initDatePeriod2);
        holidayPeriod2.Setup(p => p.GetFinalDate()).Returns(endDatePeriod2);

        var holidayPlan1 = new Mock<IHolidayPlan>();
        holidayPlan1.Setup(hp => hp.HasCollaborator(collaborator1.Object)).Returns(true);
        holidayPlan1
            .Setup(hp => hp.GetHolidayPeriods())
            .Returns(new List<IHolidayPeriod> { holidayPeriod1.Object });
        holidayPlan1.Setup(hp => hp.GetCollaborator()).Returns(collaborator1.Object);

        var holidayPlan2 = new Mock<IHolidayPlan>();
        holidayPlan2.Setup(hp => hp.HasCollaborator(collaborator2.Object)).Returns(true);
        holidayPlan2
            .Setup(hp => hp.GetHolidayPeriods())
            .Returns(new List<IHolidayPeriod> { holidayPeriod2.Object });
        holidayPlan2.Setup(hp => hp.GetCollaborator()).Returns(collaborator2.Object);

        var hpRepo = new HolidayPlanRepository(
            new List<IHolidayPlan> { holidayPlan1.Object, holidayPlan2.Object }
        );

        // Act
        var result = hpRepo.FindAllCollaboratorsWithHolidayPeriodsBetweenDates(initDate, endDate);

        // Assert
        Assert.Equal(2, result.Count());
        Assert.Contains(collaborator1.Object, result);
        Assert.Contains(collaborator2.Object, result);
    }

    public static IEnumerable<object[]> ValidDates_ForOnlyOneCollab()
    {
        // para um unico collaborador com dois periodos de ferias num unico plano
        // neste teste, apenas um periodo está dentro do procurado, sendo o outro ignorado
        yield return new object[]
        {
            new DateOnly(2025, 7, 15),
            new DateOnly(2025, 8, 1),
            new DateOnly(2025, 7, 15),
            new DateOnly(2025, 7, 20),
            new DateOnly(2025, 8, 25),
            new DateOnly(2025, 8, 27),
        };
    }

    [Theory]
    [MemberData(nameof(ValidDates_ForOnlyOneCollab))]
    public void WhenCollaboratorHasPeriodsBothInsideAndOutsideRange_ThenReturnsCollaborator(
        DateOnly initDate,
        DateOnly endDate,
        DateOnly initDatePeriod1,
        DateOnly endDatePeriod1,
        DateOnly initDatePeriod2,
        DateOnly endDatePeriod2
    )
    {
        // Arrange
        var collaborator = new Mock<ICollaborator>();

        var holidayPeriod1 = new Mock<IHolidayPeriod>();
        holidayPeriod1.Setup(p => p.GetInitDate()).Returns(initDatePeriod1);
        holidayPeriod1.Setup(p => p.GetFinalDate()).Returns(endDatePeriod1);

        var holidayPeriod2 = new Mock<IHolidayPeriod>();
        holidayPeriod2.Setup(p => p.GetInitDate()).Returns(initDatePeriod2);
        holidayPeriod2.Setup(p => p.GetFinalDate()).Returns(endDatePeriod2);

        var holidayPlan = new Mock<IHolidayPlan>();
        holidayPlan.Setup(hp => hp.HasCollaborator(collaborator.Object)).Returns(true);
        holidayPlan
            .Setup(hp => hp.GetHolidayPeriods())
            .Returns(new List<IHolidayPeriod> { holidayPeriod1.Object, holidayPeriod2.Object });
        holidayPlan.Setup(hp => hp.GetCollaborator()).Returns(collaborator.Object);

        var hpRepo = new HolidayPlanRepository(new List<IHolidayPlan> { holidayPlan.Object });
        // Act
        var result = hpRepo.FindAllCollaboratorsWithHolidayPeriodsBetweenDates(initDate, endDate);

        // Assert
        Assert.Single(result);
        Assert.Contains(collaborator.Object, result);
    }

    [Fact]
    public void WhenNoCollaboratorsHaveHolidayPeriodsInDateRange_ThenReturnsEmptyList()
    {
        // Arrange
        var collaborator = new Mock<ICollaborator>();
        var initDate = new DateOnly(2025, 7, 15);
        var endDate = new DateOnly(2025, 8, 1);

        var holidayPeriod = new Mock<IHolidayPeriod>();
        holidayPeriod.Setup(p => p.GetInitDate()).Returns(new DateOnly(2025, 6, 1));
        holidayPeriod.Setup(p => p.GetFinalDate()).Returns(new DateOnly(2025, 6, 10));

        var holidayPlan = new Mock<IHolidayPlan>();
        holidayPlan.Setup(hp => hp.HasCollaborator(collaborator.Object)).Returns(true);
        holidayPlan
            .Setup(hp => hp.GetHolidayPeriods())
            .Returns(new List<IHolidayPeriod> { holidayPeriod.Object });
        holidayPlan.Setup(hp => hp.GetCollaborator()).Returns(collaborator.Object);

        var hpRepo = new HolidayPlanRepository(new List<IHolidayPlan> { holidayPlan.Object });
        // Act
        var result = hpRepo.FindAllCollaboratorsWithHolidayPeriodsBetweenDates(initDate, endDate);

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public void WhenPassingInitDateBiggerThanEndDate_ThenThrowsArgumentException()
    {
        // Arrange
        var holidayPlans = new List<IHolidayPlan>();
        var hpRepo = new HolidayPlanRepository(holidayPlans);
        var initDate = new DateOnly(2025, 8, 15);
        var endDate = new DateOnly(2025, 7, 1);

        // Act & Assert
        var result = hpRepo.FindAllCollaboratorsWithHolidayPeriodsBetweenDates(initDate, endDate);

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
}

