using Domain;
using Moq;

namespace Domain.Tests;
public class HolidayPlanRepositoryTest
{
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
        var colaborator = new Mock<IColaborator>();

        var holidayPeriod = new Mock<IHolidayPeriod>();
        holidayPeriod.Setup(p => p.GetInitDate()).Returns(initDateForPeriod);
        holidayPeriod.Setup(p => p.GetFinalDate()).Returns(endDateForPeriod);

        var holidayPlan = new Mock<IHolidayPlan>();
        holidayPlan.Setup(hp => hp.HasColaborator(colaborator.Object)).Returns(true);
        holidayPlan
            .Setup(hp => hp.GetHolidayPeriods())
            .Returns(new List<IHolidayPeriod> { holidayPeriod.Object });

        // Adicionar o plano de férias ao repositório
        var hpRepo = new HolidayPlanRepository(new List<IHolidayPlan> { holidayPlan.Object });

        // Act
        var result = hpRepo.FindAllHolidayPeriodsForCollaboratorBetweenDates(
            colaborator.Object,
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
        var colaborator = new Mock<IColaborator>();

        var holidayPeriod1 = new Mock<IHolidayPeriod>();
        holidayPeriod1.Setup(p => p.GetInitDate()).Returns(initDateForPeriod_1);
        holidayPeriod1.Setup(p => p.GetFinalDate()).Returns(endDateForPeriod_1);

        var holidayPeriod2 = new Mock<IHolidayPeriod>();
        holidayPeriod2.Setup(p => p.GetInitDate()).Returns(initDateForPeriod_2);
        holidayPeriod2.Setup(p => p.GetFinalDate()).Returns(endDateForPeriod_2);

        var holidayPlan = new Mock<IHolidayPlan>();
        holidayPlan.Setup(hp => hp.HasColaborator(colaborator.Object)).Returns(true);
        holidayPlan
            .Setup(hp => hp.GetHolidayPeriods())
            .Returns(new List<IHolidayPeriod> { holidayPeriod1.Object, holidayPeriod2.Object });

        // Adicionar o plano de férias ao repositório
        var hpRepo = new HolidayPlanRepository(new List<IHolidayPlan> { holidayPlan.Object });

        // Act
        var result = hpRepo.FindAllHolidayPeriodsForCollaboratorBetweenDates(
            colaborator.Object,
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
        var colaborator = new Mock<IColaborator>();
        var initDateForSearching = new DateOnly(2025, 7, 15);
        var endDateForSearching = new DateOnly(2025, 8, 1);
        var initDateForPeriod = new DateOnly(2025, 9, 20);
        var endDateForPeriod = new DateOnly(2025, 10, 25);

        var holidayPeriod = new Mock<IHolidayPeriod>();
        holidayPeriod.Setup(p => p.GetInitDate()).Returns(initDateForPeriod);
        holidayPeriod.Setup(p => p.GetFinalDate()).Returns(endDateForPeriod);

        var holidayPlan = new Mock<IHolidayPlan>();
        holidayPlan.Setup(hp => hp.HasColaborator(colaborator.Object)).Returns(true);
        holidayPlan
            .Setup(hp => hp.GetHolidayPeriods())
            .Returns(new List<IHolidayPeriod> { holidayPeriod.Object });

        // Adicionar o plano de férias ao repositório
        var hpRepo = new HolidayPlanRepository(new List<IHolidayPlan> { holidayPlan.Object });

        // Act
        var result = hpRepo.FindAllHolidayPeriodsForCollaboratorBetweenDates(
            colaborator.Object,
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
        var colaborator = new Mock<IColaborator>();
        var initDate = new DateOnly(2025, 8, 15);
        var endDate = new DateOnly(2025, 7, 1);

        // Act
        var result = hpRepo.FindAllHolidayPeriodsForCollaboratorBetweenDates(
            colaborator.Object,
            initDate,
            endDate
        );

        // asset
        Assert.Empty(result);
    }

    // US14
    public static IEnumerable<object[]> ValidDatesColaborator()
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
    [MemberData(nameof(ValidDatesColaborator))]
    public void WhenCollaboratorHasHolidayPeriodWithinDateRange_ThenReturnsCollaborator(
        DateOnly initDate,
        DateOnly endDate,
        DateOnly initDatePeriod,
        DateOnly endDatePeriod
    )
    {
        // Arrange
        var colaborator = new Mock<IColaborator>();

        var holidayPeriod = new Mock<IHolidayPeriod>();
        holidayPeriod.Setup(p => p.GetInitDate()).Returns(initDatePeriod);
        holidayPeriod.Setup(p => p.GetFinalDate()).Returns(endDatePeriod);

        var holidayPlan = new Mock<IHolidayPlan>();
        holidayPlan.Setup(hp => hp.HasColaborator(colaborator.Object)).Returns(true);
        holidayPlan
            .Setup(hp => hp.GetHolidayPeriods())
            .Returns(new List<IHolidayPeriod> { holidayPeriod.Object });
        holidayPlan.Setup(hp => hp.GetColaborator()).Returns(colaborator.Object);

        var hpRepo = new HolidayPlanRepository(new List<IHolidayPlan> { holidayPlan.Object });
        // Act
        var result = hpRepo.FindAllCollaboratorsWithHolidayPeriodsBetweenDates(initDate, endDate);

        // Assert
        Assert.Single(result);
        Assert.Contains(colaborator.Object, result);
    }

    public static IEnumerable<object[]> ValidDatesMultipleColaborators()
    {
        // para dois colaboradores, com periodos que têm uma data dentro do periodo procurado
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
    [MemberData(nameof(ValidDatesMultipleColaborators))]
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
        var colaborator1 = new Mock<IColaborator>();
        var colaborator2 = new Mock<IColaborator>();

        var holidayPeriod1 = new Mock<IHolidayPeriod>();
        holidayPeriod1.Setup(p => p.GetInitDate()).Returns(initDatePeriod1);
        holidayPeriod1.Setup(p => p.GetFinalDate()).Returns(endDatePeriod1);

        var holidayPeriod2 = new Mock<IHolidayPeriod>();
        holidayPeriod2.Setup(p => p.GetInitDate()).Returns(initDatePeriod2);
        holidayPeriod2.Setup(p => p.GetFinalDate()).Returns(endDatePeriod2);

        var holidayPlan1 = new Mock<IHolidayPlan>();
        holidayPlan1.Setup(hp => hp.HasColaborator(colaborator1.Object)).Returns(true);
        holidayPlan1
            .Setup(hp => hp.GetHolidayPeriods())
            .Returns(new List<IHolidayPeriod> { holidayPeriod1.Object });
        holidayPlan1.Setup(hp => hp.GetColaborator()).Returns(colaborator1.Object);

        var holidayPlan2 = new Mock<IHolidayPlan>();
        holidayPlan2.Setup(hp => hp.HasColaborator(colaborator2.Object)).Returns(true);
        holidayPlan2
            .Setup(hp => hp.GetHolidayPeriods())
            .Returns(new List<IHolidayPeriod> { holidayPeriod2.Object });
        holidayPlan2.Setup(hp => hp.GetColaborator()).Returns(colaborator2.Object);

        var hpRepo = new HolidayPlanRepository(
            new List<IHolidayPlan> { holidayPlan1.Object, holidayPlan2.Object }
        );

        // Act
        var result = hpRepo.FindAllCollaboratorsWithHolidayPeriodsBetweenDates(initDate, endDate);

        // Assert
        Assert.Equal(2, result.Count());
        Assert.Contains(colaborator1.Object, result);
        Assert.Contains(colaborator2.Object, result);
    }

    public static IEnumerable<object[]> ValidDates_ForOnlyOneColab()
    {
        // para um unico colaborador com dois periodos de ferias num unico plano
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
    [MemberData(nameof(ValidDates_ForOnlyOneColab))]
    public void WhenColaboratorHasPeriodsBothInsideAndOutsideRange_ThenReturnsCollaborator(
        DateOnly initDate,
        DateOnly endDate,
        DateOnly initDatePeriod1,
        DateOnly endDatePeriod1,
        DateOnly initDatePeriod2,
        DateOnly endDatePeriod2
    )
    {
        // Arrange
        var colaborator = new Mock<IColaborator>();

        var holidayPeriod1 = new Mock<IHolidayPeriod>();
        holidayPeriod1.Setup(p => p.GetInitDate()).Returns(initDatePeriod1);
        holidayPeriod1.Setup(p => p.GetFinalDate()).Returns(endDatePeriod1);

        var holidayPeriod2 = new Mock<IHolidayPeriod>();
        holidayPeriod2.Setup(p => p.GetInitDate()).Returns(initDatePeriod2);
        holidayPeriod2.Setup(p => p.GetFinalDate()).Returns(endDatePeriod2);

        var holidayPlan = new Mock<IHolidayPlan>();
        holidayPlan.Setup(hp => hp.HasColaborator(colaborator.Object)).Returns(true);
        holidayPlan
            .Setup(hp => hp.GetHolidayPeriods())
            .Returns(new List<IHolidayPeriod> { holidayPeriod1.Object, holidayPeriod2.Object });
        holidayPlan.Setup(hp => hp.GetColaborator()).Returns(colaborator.Object);

        var hpRepo = new HolidayPlanRepository(new List<IHolidayPlan> { holidayPlan.Object });
        // Act
        var result = hpRepo.FindAllCollaboratorsWithHolidayPeriodsBetweenDates(initDate, endDate);

        // Assert
        Assert.Single(result);
        Assert.Contains(colaborator.Object, result);
    }

    [Fact]
    public void WhenNoCollaboratorsHaveHolidayPeriodsInDateRange_ThenReturnsEmptyList()
    {
        // Arrange
        var colaborator = new Mock<IColaborator>();
        var initDate = new DateOnly(2025, 7, 15);
        var endDate = new DateOnly(2025, 8, 1);

        var holidayPeriod = new Mock<IHolidayPeriod>();
        holidayPeriod.Setup(p => p.GetInitDate()).Returns(new DateOnly(2025, 6, 1));
        holidayPeriod.Setup(p => p.GetFinalDate()).Returns(new DateOnly(2025, 6, 10));

        var holidayPlan = new Mock<IHolidayPlan>();
        holidayPlan.Setup(hp => hp.HasColaborator(colaborator.Object)).Returns(true);
        holidayPlan
            .Setup(hp => hp.GetHolidayPeriods())
            .Returns(new List<IHolidayPeriod> { holidayPeriod.Object });
        holidayPlan.Setup(hp => hp.GetColaborator()).Returns(colaborator.Object);

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
        Mock<IAssociationProjectColaborator> associationDouble = new Mock<IAssociationProjectColaborator>();
        Mock<IHolidayPlan> holidayPlanDouble = new Mock<IHolidayPlan>();
        Mock<IColaborator> collaboratorDouble = new Mock<IColaborator>();

        DateOnly initDate = new DateOnly(2025, 6, 1);
        DateOnly finalDate = new DateOnly(2025, 6, 10);

        associationDouble.Setup(a => a.GetColaborator()).Returns(collaboratorDouble.Object);
        associationDouble.Setup(a => a.GetInitDate()).Returns(initDate);
        associationDouble.Setup(a => a.GetFinalDate()).Returns(finalDate);

        holidayPlanDouble.Setup(hp => hp.GetColaborator()).Returns(collaboratorDouble.Object);
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
        Mock<IAssociationProjectColaborator> associationDouble = new Mock<IAssociationProjectColaborator>();
        Mock<IColaborator> collaboratorDouble = new Mock<IColaborator>();

        associationDouble.Setup(a => a.GetColaborator()).Returns(collaboratorDouble.Object);

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

        Mock<IColaborator> colaboratorDouble1 = new Mock<IColaborator>();
        Mock<IColaborator> colaboratorDouble2 = new Mock<IColaborator>();

        Mock<IHolidayPlan> holidayPlanDouble1 = new Mock<IHolidayPlan>();
        holidayPlanDouble1.Setup(p => p.HasPeriodLongerThan(days)).Returns(true);
        holidayPlanDouble1.Setup(p => p.GetColaborator()).Returns(colaboratorDouble1.Object);

        Mock<IHolidayPlan> holidayPlanDouble2 = new Mock<IHolidayPlan>();
        holidayPlanDouble2.Setup(p => p.HasPeriodLongerThan(days)).Returns(false);
        holidayPlanDouble2.Setup(p => p.GetColaborator()).Returns(colaboratorDouble2.Object);

        IHolidayPlanRepository repository = new HolidayPlanRepository(new List<IHolidayPlan> { holidayPlanDouble1.Object, holidayPlanDouble2.Object });

        //act
        var result = repository.FindAllCollaboratorsWithHolidayPeriodsLongerThan(days).ToList();

        //assert
        Assert.Contains(colaboratorDouble1.Object, result);
        Assert.DoesNotContain(colaboratorDouble2.Object, result);
    }

    [Fact]
    public void WhenNoCollaboratorHaveHolidayPeriodLongerThan_ThenShouldReturnEmptyList()
    {
        //arrange
        int days = 5;

        Mock<IColaborator> colaboratorDouble1 = new Mock<IColaborator>();
        Mock<IColaborator> colaboratorDouble2 = new Mock<IColaborator>();

        Mock<IHolidayPlan> holidayPlanDouble1 = new Mock<IHolidayPlan>();
        holidayPlanDouble1.Setup(p => p.HasPeriodLongerThan(days)).Returns(false);
        holidayPlanDouble1.Setup(p => p.GetColaborator()).Returns(colaboratorDouble1.Object);

        Mock<IHolidayPlan> holidayPlanDouble2 = new Mock<IHolidayPlan>();
        holidayPlanDouble2.Setup(p => p.HasPeriodLongerThan(days)).Returns(false);
        holidayPlanDouble2.Setup(p => p.GetColaborator()).Returns(colaboratorDouble2.Object);

        IHolidayPlanRepository repository = new HolidayPlanRepository(new List<IHolidayPlan> { holidayPlanDouble1.Object, holidayPlanDouble2.Object });

        //act
        var result = repository.FindAllCollaboratorsWithHolidayPeriodsLongerThan(days).ToList();

        //assert
        Assert.Empty(result);
    }
    [Fact]
    public void WhenGivenCollaboratorAndGoodDate_ThenReturnPeriod() {
        //arrange
        DateOnly date = new DateOnly(2020, 1, 1);
        var colab = new Mock<IColaborator>();
        
        var holidayPeriod = new Mock<IHolidayPeriod>();

        var holidayPlan = new Mock<IHolidayPlan>();
        holidayPlan.Setup(a => a.HasCollaborator(colab.Object)).Returns(true);
        holidayPlan.Setup(a => a.GetHolidayPeriodContainingDate(date)).Returns(holidayPeriod.Object);

        var hpRepo = new HolidayPlanRepository(holidayPlan.Object);

        //act
        var result = hpRepo.GetHolidayPeriodContainingDate(colab.Object, date);

        //assert
        Assert.NotNull(result);
    }

    [Fact]
    public void WhenGivenCollaboratorAndBadDate_ThenReturnEmpty() {
        //arrange
        DateOnly date = new DateOnly(2020, 1, 1);
        var colab = new Mock<IColaborator>();

        var holidayPlan = new Mock<IHolidayPlan>();
        holidayPlan.Setup(a => a.HasCollaborator(colab.Object)).Returns(true);
        holidayPlan.Setup(a => a.GetHolidayPeriodContainingDate(date)).Returns((IHolidayPeriod?)null);

        var hpRepo = new HolidayPlanRepository(holidayPlan.Object);

        //act
        var result = hpRepo.GetHolidayPeriodContainingDate(colab.Object, date);

        //assert
        Assert.Null(result);
    }

    [Fact]
    public void WhenGivenGoodCollaboratorAndDatesAndLength_ThenReturnPeriods() {
        //arrange
        DateOnly ini = new DateOnly(2020, 1, 1);
        DateOnly end = new DateOnly(2020, 3, 1);
        var colab = new Mock<IColaborator>();

        var holidayPeriod = new Mock<IHolidayPeriod>();

        var holidayPlan = new Mock<IHolidayPlan>();
        holidayPlan.Setup(a => a.HasCollaborator(colab.Object)).Returns(true);

        IEnumerable<IHolidayPeriod> ret = new List<IHolidayPeriod>() {holidayPeriod.Object};
        holidayPlan.Setup(a => a.FindAllHolidayPeriodsBetweenDatesLongerThan(ini, end, 4)).Returns(ret);

        var hpRepo = new HolidayPlanRepository(holidayPlan.Object);

        //act
        var result = hpRepo.FindAllHolidayPeriodsLongerThanForCollaboratorBetweenDates(colab.Object, ini, end, 4);

        //assert
        Assert.Equal(ret, result);
    }

    [Fact]
    public void WhenGivenBadCollaboratorAndDatesAndLength_ThenReturnEmptyLists() {
        //arrange
        DateOnly ini = new DateOnly(2020, 1, 1);
        DateOnly end = new DateOnly(2020, 3, 1);
        var colab = new Mock<IColaborator>();

        var holidayPlan = new Mock<IHolidayPlan>();
        holidayPlan.Setup(a => a.HasCollaborator(colab.Object)).Returns(true);

        IEnumerable<IHolidayPeriod> ret = new List<IHolidayPeriod>();
        holidayPlan.Setup(a => a.FindAllHolidayPeriodsBetweenDatesLongerThan(ini, end, 4)).Returns(ret);

        var hpRepo = new HolidayPlanRepository(holidayPlan.Object);

        //act
        var result = hpRepo.FindAllHolidayPeriodsLongerThanForCollaboratorBetweenDates(colab.Object, ini, end, 4);

        //assert
        Assert.Empty(result);
    }
}

