using Domain;
using Moq;

public class HolidayPlanRepositoryTest
{
    /**
    * Test for empty constructor
    */
    [Fact]
    public void WhenNotPassingAnyArguments_ThenObjectIsCreated()
    {
        //Arrange

        //Act
        new HolidayPlanRepository();

        //Assert
    }

    /**
    * Test for constructor that receives a list of HolidaPlans
    */
    [Fact]
    public void WhenPassingCorrectHolidayPlansList_ThenObjectIsCreated()
    {
        // Arrange 
        // HolidayPlan doubles - stubs
        Mock<IHolidayPlan> doubleHolidayPlan1 = new Mock<IHolidayPlan>();
        Mock<IHolidayPlan> doubleHolidayPlan2 = new Mock<IHolidayPlan>();
        Mock<IHolidayPlan> doubleHolidayPlan3 = new Mock<IHolidayPlan>();

        //Collaborator doubles for constructor verification
        Mock<ICollaborator> doubleColab1 = new Mock<ICollaborator>();
        Mock<ICollaborator> doubleColab2 = new Mock<ICollaborator>();
        Mock<ICollaborator> doubleColab3 = new Mock<ICollaborator>();

        // Create HolidayPlan List
        List<IHolidayPlan> holidayPlansList =
            new List<IHolidayPlan> { doubleHolidayPlan1.Object, doubleHolidayPlan2.Object, doubleHolidayPlan3.Object };

        // To be insertable, all holidayPlans must have distinct collaborators
        // no holidayPlan iss associated with the same collaborator
        doubleHolidayPlan1.Setup(hp1 => hp1.GetCollaborator()).Returns(doubleColab1.Object);
        doubleHolidayPlan2.Setup(hp2 => hp2.GetCollaborator()).Returns(doubleColab2.Object);
        doubleHolidayPlan3.Setup(hp3 => hp3.GetCollaborator()).Returns(doubleColab3.Object);

        // Act
        new HolidayPlanRepository(holidayPlansList);

        // Assert 
    }

    /**
    * Test for constructor that receives a list of holidayPlans that is not valid
    * -> Should throw exception
    *
    * One element being invalid is enough for the whole list to be invalid too 
    */
    [Fact]
    public void WhenPassingIncorrectHolidayPlansList_ThenShouldThrowException()
    {
        // Arrange 
        // HolidayPlan doubles - stubs
        Mock<IHolidayPlan> doubleHolidayPlan1 = new Mock<IHolidayPlan>();
        Mock<IHolidayPlan> doubleHolidayPlan2 = new Mock<IHolidayPlan>();
        Mock<IHolidayPlan> doubleHolidayPlan3 = new Mock<IHolidayPlan>();

        //Collaborator doubles for constructor verification
        Mock<ICollaborator> doubleColab1 = new Mock<ICollaborator>();
        Mock<ICollaborator> doubleColab2 = new Mock<ICollaborator>();

        // Create HolidayPlan List
        List<IHolidayPlan> holidayPlansList =
            new List<IHolidayPlan> { doubleHolidayPlan1.Object, doubleHolidayPlan2.Object, doubleHolidayPlan3.Object };

        // To be insertable, all holidayPlans must have distinct collaborators
        // no holidayPlan iss associated with the same collaborator
        doubleHolidayPlan1.Setup(hp1 => hp1.GetCollaborator()).Returns(doubleColab1.Object);
        doubleHolidayPlan2.Setup(hp2 => hp2.GetCollaborator()).Returns(doubleColab2.Object);
        doubleHolidayPlan3.Setup(hp3 => hp3.GetCollaborator()).Returns(doubleColab2.Object);

        // Assert
        var exception = Assert.Throws<ArgumentException>(() =>
            // Act
            new HolidayPlanRepository(holidayPlansList));

        Assert.Equal("Arguments are not valid!", exception.Message);
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
    public void WhenPassingCorrectValues_ThenObjectIsCreated()
    {
        //arrange
        Mock<IHolidayPlan> holidayPlanDouble = new Mock<IHolidayPlan>();
        List<IHolidayPlan> holidayPlans = new List<IHolidayPlan> { holidayPlanDouble.Object };

        //act
        new HolidayPlanRepository(holidayPlans);

        //assert
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


        HolidayPlanRepository repository = new HolidayPlanRepository(new List<IHolidayPlan> { holidayPlan.Object });

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


        HolidayPlanRepository repository = new HolidayPlanRepository(new List<IHolidayPlan> { holidayPlan.Object });

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

        var hpRepo = new HolidayPlanRepository(new List<IHolidayPlan> { holidayPlan.Object });

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

        var hpRepo = new HolidayPlanRepository(new List<IHolidayPlan> { holidayPlan.Object });

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

        var hpRepo = new HolidayPlanRepository(new List<IHolidayPlan> { holidayPlan.Object });

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

        var hpRepo = new HolidayPlanRepository(new List<IHolidayPlan> { holidayPlan.Object });

        //act
        var result = hpRepo.FindAllHolidayPeriodsLongerThanForCollaboratorBetweenDates(collab.Object, ini, end, 4);

        //assert
        Assert.Empty(result);
    }

    [Fact]
    public void GetHolidayPlansByAssociations_ReturnsCorrectPlans()
    {
        // Arrange
        Mock<ICollaborator> doubleCollaborator1 = new Mock<ICollaborator>();
        Mock<ICollaborator> doubleCollaborator2 = new Mock<ICollaborator>();
        Mock<IAssociationProjectCollaborator> mockAssociation = new Mock<IAssociationProjectCollaborator>();

        mockAssociation.Setup(a => a.GetCollaborator()).Returns(doubleCollaborator1.Object);

        Mock<IHolidayPlan> doubleHolidayPlan1 = new Mock<IHolidayPlan>();
        Mock<IHolidayPlan> doubleHolidayPlan2 = new Mock<IHolidayPlan>();

        doubleHolidayPlan1.Setup(h => h.GetCollaborator()).Returns(doubleCollaborator1.Object);
        doubleHolidayPlan2.Setup(h => h.GetCollaborator()).Returns(doubleCollaborator2.Object);

        var holidayPlans = new List<IHolidayPlan>
            {
                doubleHolidayPlan1.Object,
                doubleHolidayPlan2.Object
            };

        var repository = new HolidayPlanRepository(holidayPlans);

        // Act
        var result = repository.GetHolidayPlansByAssociations(mockAssociation.Object);

        // Assert
        Assert.Contains(doubleHolidayPlan1.Object, result);
        Assert.DoesNotContain(doubleHolidayPlan2.Object, result);
        Assert.Single(result);
    }

    [Fact]
    public void GetHolidayPlansByAssociations_WithNoMatchingCollaborator_ReturnsEmpty()
    {
        // Arrange
        var mockCollaborator = new Mock<ICollaborator>();
        var mockAssociation = new Mock<IAssociationProjectCollaborator>();
        mockAssociation.Setup(a => a.GetCollaborator()).Returns(mockCollaborator.Object);

        var mockHolidayPlan = new Mock<IHolidayPlan>();
        mockHolidayPlan.Setup(h => h.GetCollaborator()).Returns(new Mock<ICollaborator>().Object); // Outro colaborador

        var holidayPlans = new List<IHolidayPlan> { mockHolidayPlan.Object };
        var repository = new HolidayPlanRepository(holidayPlans);

        // Act
        var result = repository.GetHolidayPlansByAssociations(mockAssociation.Object);

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public void GetHolidayPlansByAssociations_WithNoHolidayPlans_ReturnsEmpty()
    {
        // Arrange
        var mockAssociation = new Mock<IAssociationProjectCollaborator>();
        var repository = new HolidayPlanRepository(new List<IHolidayPlan>());

        // Act
        var result = repository.GetHolidayPlansByAssociations(mockAssociation.Object);

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public void WhenHolidayPlansByAssociations_ThenReturnsCorrectPlans()
    {
        // Arrange
        var mockCollaborator = new Mock<ICollaborator>();
        var mockAssociation = new Mock<IAssociationProjectCollaborator>();
        mockAssociation.Setup(a => a.GetCollaborator()).Returns(mockCollaborator.Object);

        var mockHolidayPlan1 = new Mock<IHolidayPlan>();
        var mockHolidayPlan2 = new Mock<IHolidayPlan>();

        // Apenas um plano pode pertencer ao colaborador
        mockHolidayPlan1.Setup(h => h.GetCollaborator()).Returns(mockCollaborator.Object);

        mockHolidayPlan2.Setup(h => h.GetCollaborator()).Returns(new Mock<ICollaborator>().Object);

        var holidayPlans = new List<IHolidayPlan>
        {
            mockHolidayPlan1.Object,
            mockHolidayPlan2.Object
        };

        var repository = new HolidayPlanRepository(holidayPlans);

        // Act
        var result = repository.GetHolidayPlansByAssociations(mockAssociation.Object);

        // Assert
        var expectedPlans = new List<IHolidayPlan> { mockHolidayPlan1.Object };
        Assert.Equal(expectedPlans, result.ToList());
    }




    [Fact]
    public void WhenHolidayPlansByAssociations_WithNoMatchingCollaborator_ThenReturnsEmpty()
    {
        // Arrange
        var mockCollaborator = new Mock<ICollaborator>();
        var mockAssociation = new Mock<IAssociationProjectCollaborator>();
        mockAssociation.Setup(a => a.GetCollaborator()).Returns(mockCollaborator.Object);

        var mockHolidayPlan = new Mock<IHolidayPlan>();
        mockHolidayPlan.Setup(h => h.GetCollaborator()).Returns(new Mock<ICollaborator>().Object); // Outro colaborador

        var holidayPlans = new List<IHolidayPlan> { mockHolidayPlan.Object };
        var repository = new HolidayPlanRepository(holidayPlans);

        // Act
        var result = repository.GetHolidayPlansByAssociations(mockAssociation.Object);

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public void WhenHolidayPlansByAssociations_WithNoHolidayPlans_ThenReturnsEmpty()
    {
        // Arrange
        var mockAssociation = new Mock<IAssociationProjectCollaborator>();
        var repository = new HolidayPlanRepository(new List<IHolidayPlan>());

        // Act
        var result = repository.GetHolidayPlansByAssociations(mockAssociation.Object);

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public void WhenFindingHolidayPlansWithPeriodsLongerThan_ReturnsCorrectList()
    {
        //arrange
        int days = 5;

        Mock<ICollaborator> doubleCollab1 = new Mock<ICollaborator>();
        Mock<ICollaborator> doubleCollab2 = new Mock<ICollaborator>();

        Mock<IHolidayPlan> holidayPlanDouble1 = new Mock<IHolidayPlan>();
        holidayPlanDouble1.Setup(p => p.HasPeriodLongerThan(days)).Returns(true);
        holidayPlanDouble1.Setup(p => p.GetCollaborator()).Returns(doubleCollab1.Object);

        Mock<IHolidayPlan> holidayPlanDouble2 = new Mock<IHolidayPlan>();
        holidayPlanDouble2.Setup(p => p.HasPeriodLongerThan(days)).Returns(false);
        holidayPlanDouble2.Setup(p => p.GetCollaborator()).Returns(doubleCollab2.Object);

        IHolidayPlanRepository holidayPlanRepo = new HolidayPlanRepository(new List<IHolidayPlan> { holidayPlanDouble1.Object, holidayPlanDouble2.Object });

        //act
        var result = holidayPlanRepo.FindAllWithHolidayPeriodsLongerThan(days);

        //assert
        Assert.Contains(holidayPlanDouble1.Object, result);
        Assert.DoesNotContain(holidayPlanDouble2.Object, result);
    }

    [Fact]
    public void WhenFindingHolidayPlanByCollaborator_ThenReturnsCorrectCollaborator()
    {
        //arrange
        var collaboratorDouble1 = new Mock<ICollaborator>();
        var collaboratorDouble2 = new Mock<ICollaborator>();

        var holidayPlanDouble1 = new Mock<IHolidayPlan>();
        holidayPlanDouble1.Setup(hp => hp.GetCollaborator()).Returns(collaboratorDouble1.Object);

        var holidayPlanDouble2 = new Mock<IHolidayPlan>();
        holidayPlanDouble2.Setup(hp => hp.GetCollaborator()).Returns(collaboratorDouble2.Object);

        var repo = new HolidayPlanRepository(new List<IHolidayPlan> { holidayPlanDouble1.Object, holidayPlanDouble2.Object });

        //act
        var result = repo.FindHolidayPlanByCollaborator(collaboratorDouble1.Object);

        //assert
        Assert.Equal(holidayPlanDouble1.Object, result);
    }

    [Fact]
    public void WhenFindingHolidayPlanBCollaborator_ThenReturnsNull()
    {
        //arrange
        var collaboratorDouble1 = new Mock<ICollaborator>();

        var repo = new HolidayPlanRepository();

        //act
        var result = repo.FindHolidayPlanByCollaborator(collaboratorDouble1.Object);

        //assert
        Assert.Null(result);
    }

    /**
    * Method to add a holiday plan to repo
    * Happy Path
    */
    [Fact]
    public void WhenAddingCorrectHolidayPlanToRepository_ThenReturnTrue()
    {
        // Arrange 
        // Double for holidayPlan
        Mock<IHolidayPlan> doubleHolidayPlan = new Mock<IHolidayPlan>();

        // Double for holidayPlan Colllaborator
        Mock<ICollaborator> doubleCollab = new Mock<ICollaborator>();

        // Collab has the holidayPlan
        doubleHolidayPlan.Setup(hp => hp.GetCollaborator()).Returns(doubleCollab.Object);

        // Instatiate repository
        HolidayPlanRepository repo = new HolidayPlanRepository();

        // Act
        // add holiday plan to repository
        bool result = repo.AddHolidayPlan(doubleHolidayPlan.Object);

        // Assert
        Assert.True(result);
    }

    /**
    * Method to add a holiday plan to repo
    * There is an holiday plan for the same collaborator 
    * -> can't add it
    */
    [Fact]
    public void WhenAddingHolidayPlanWithRepeatedCollaboratorToRepository_ThenReturnFalse()
    {
        // Arrange 
        // Double for holidayPlan
        Mock<IHolidayPlan> doubleHolidayPlan = new Mock<IHolidayPlan>();
        Mock<IHolidayPlan> doubleHolidayPlanToAdd = new Mock<IHolidayPlan>();

        // Double for holidayPlan Colllaborator
        Mock<ICollaborator> doubleCollab = new Mock<ICollaborator>();

        // Collab has the holidayPlan
        doubleHolidayPlan.Setup(hp => hp.GetCollaborator()).Returns(doubleCollab.Object);
        doubleHolidayPlanToAdd.Setup(hp => hp.GetCollaborator()).Returns(doubleCollab.Object);

        // Instatiate repository
        HolidayPlanRepository repo = new HolidayPlanRepository(new List<IHolidayPlan> { doubleHolidayPlan.Object });

        // Act
        // add holiday plan to repository
        bool result = repo.AddHolidayPlan(doubleHolidayPlanToAdd.Object);

        // Assert
        Assert.False(result);
    }
}
