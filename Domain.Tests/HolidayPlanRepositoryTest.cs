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

}
