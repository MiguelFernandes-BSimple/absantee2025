using Moq;

namespace Domain.Tests
{
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
                .Setup(a => a.FindByProjectandCollaborator(projectMock.Object, collaboratorMock.Object))
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

    private readonly Mock<IAssociationProjectCollaboratorRepository> _mockAssociationRepo;
    private readonly Mock<IHolidayPlanRepository> _mockHolidayRepo;
    private readonly HolidayPlanService _service;

    public HolidayPlanServiceTests()
    {
        _mockAssociationRepo = new Mock<IAssociationProjectCollaboratorRepository>();
        _mockHolidayRepo = new Mock<IHolidayPlanRepository>();
        _service = new HolidayPlanService(_mockAssociationRepo.Object, _mockHolidayRepo.Object);
    }

    // [Fact]
    // public void When_InitDateIsGreaterThanEndDate_Then_ReturnsZero()
    // {
    //     // Arrange
    //     var project = new Mock<IProject>().Object;
    //     var initDate = new DateOnly(2024, 12, 31);
    //     var endDate = new DateOnly(2024, 1, 1);

    //     // Act
    //     var result = _service.GetHolidayDaysForProjectCollaboratorBetweenDates(project, initDate, endDate);

    //     // Assert
    //     Assert.Equal(0, result);
    // }

    // [Fact]
    // public void When_RepositoriesAreNull_Then_ReturnsZero()
    // {
    //     // Arrange
    //     var project = new Mock<IProject>().Object;
    //     var service = new HolidayPlanService(null, null);
    //     var initDate = new DateOnly(2024, 1, 1);
    //     var endDate = new DateOnly(2024, 1, 10);

    //     // Act
    //     var result = service.GetHolidayDaysForProjectAllCollaboratorBetweenDates(project, initDate, endDate);

    //     // Assert
    //     Assert.Equal(0, result);
    // }

    // [Fact]
    // public void When_NoCollaboratorsInProject_Then_ReturnsZero()
    // {
    //     // Arrange
    //     var project = new Mock<IProject>().Object;
    //     _mockAssociationRepo.Setup(repo => repo.FindAllProjectCollaborators(project))
    //         .Returns(new List<ICollaborator>());

    //     // Act
    //     var result = _service.GetHolidayDaysForProjectCollaboratorBetweenDates(project, new DateOnly(2024, 1, 1), new DateOnly(2024, 1, 10));

    //     // Assert
    //     Assert.Equal(0, result);
    // }

    // [Fact]
    // public void When_CollaboratorsHaveNoHolidayPlans_Then_ReturnsZero()
    // {
    //     // Arrange
    //     var project = new Mock<IProject>().Object;
    //     var collaborator = new Mock<ICollaborator>().Object;

    //     _mockAssociationRepo.Setup(repo => repo.FindAllProjectCollaborators(project))
    //         .Returns(new List<ICollaborator> { collaborator });

    //     _mockHolidayRepo.Setup(repo => repo.GetHolidayPlansByCollaborator(collaborator))
    //         .Returns(new List<IHolidayPlan>());

    //     // Act
    //     var result = _service.GetHolidayDaysForProjectCollaboratorBetweenDates(project, new DateOnly(2024, 1, 1), new DateOnly(2024, 1, 10));

    //     // Assert
    //     Assert.Equal(0, result);
    // }

    // [Fact]
    // public void When_HolidayPeriodIsCompletelyOutsideDateRange_Then_ReturnsZero()
    // {
    //     // Arrange
    //     var project = new Mock<IProject>().Object;
    //     var collaborator = new Mock<ICollaborator>().Object;
    //     var holidayPeriod = new Mock<IHolidayPeriod>();

    //     holidayPeriod.Setup(h => h.GetInitDate()).Returns(new DateOnly(2023, 12, 1));
    //     holidayPeriod.Setup(h => h.GetFinalDate()).Returns(new DateOnly(2023, 12, 10));

    //     var holidayPlan = new Mock<IHolidayPlan>();
    //     holidayPlan.Setup(h => h.GetHolidayPeriods()).Returns(new List<IHolidayPeriod> { holidayPeriod.Object });

    //     _mockAssociationRepo.Setup(repo => repo.FindAllProjectCollaborators(project))
    //         .Returns(new List<ICollaborator> { collaborator });

    //     _mockHolidayRepo.Setup(repo => repo.GetHolidayPlansByCollaborator(collaborator))
    //         .Returns(new List<IHolidayPlan> { holidayPlan.Object });

    //     // Act
    //     var result = _service.GetHolidayDaysForProjectCollaboratorBetweenDates(project, new DateOnly(2024, 1, 1), new DateOnly(2024, 1, 10));

    //     // Assert
    //     Assert.Equal(0, result);
    // }

    // [Fact]
    // public void When_HolidayPeriodIsPartiallyWithinRange_Then_CalculatesCorrectDays()
    // {
    //     // Arrange
    //     var project = new Mock<IProject>().Object;
    //     var collaborator = new Mock<ICollaborator>().Object;
    //     var holidayPeriod = new Mock<IHolidayPeriod>();

    //     holidayPeriod.Setup(h => h.GetInitDate()).Returns(new DateOnly(2023, 12, 30));
    //     holidayPeriod.Setup(h => h.GetFinalDate()).Returns(new DateOnly(2024, 1, 5));

    //     var holidayPlan = new Mock<IHolidayPlan>();
    //     holidayPlan.Setup(h => h.GetHolidayPeriods()).Returns(new List<IHolidayPeriod> { holidayPeriod.Object });

    //     _mockAssociationRepo.Setup(repo => repo.FindAllProjectCollaborators(project))
    //         .Returns(new List<ICollaborator> { collaborator });

    //     _mockHolidayRepo.Setup(repo => repo.GetHolidayPlansByCollaborator(collaborator))
    //         .Returns(new List<IHolidayPlan> { holidayPlan.Object });

    //     // Act
    //     var result = _service.GetHolidayDaysForProjectCollaboratorBetweenDates(project, new DateOnly(2024, 1, 1), new DateOnly(2024, 1, 10));

    //     // Assert
    //     Assert.Equal(5, result); // De 01/01 a 05/01
    // }

    // [Fact]
    // public void When_HolidayPeriodIsFullyWithinRange_Then_SumsAllDays()
    // {
    //     // Arrange
    //     var project = new Mock<IProject>().Object;
    //     var collaborator = new Mock<ICollaborator>().Object;
    //     var holidayPeriod = new Mock<IHolidayPeriod>();

    //     holidayPeriod.Setup(h => h.GetInitDate()).Returns(new DateOnly(2024, 1, 2));
    //     holidayPeriod.Setup(h => h.GetFinalDate()).Returns(new DateOnly(2024, 1, 8));

    //     var holidayPlan = new Mock<IHolidayPlan>();
    //     holidayPlan.Setup(h => h.GetHolidayPeriods()).Returns(new List<IHolidayPeriod> { holidayPeriod.Object });

    //     _mockAssociationRepo.Setup(repo => repo.FindAllProjectCollaborators(project))
    //         .Returns(new List<ICollaborator> { collaborator });

    //     _mockHolidayRepo.Setup(repo => repo.GetHolidayPlansByCollaborator(collaborator))
    //         .Returns(new List<IHolidayPlan> { holidayPlan.Object });

    //     // Act
    //     var result = _service.GetHolidayDaysForProjectCollaboratorBetweenDates(project, new DateOnly(2024, 1, 1), new DateOnly(2024, 1, 10));

    //     // Assert
    //     Assert.Equal(7, result); // De 02/01 a 08/01
    // }


    //     [Fact]
    //     public void WhentHolidayDaysForProjectCollaboratorBetweenDates_ThenReturnCorrectDays()
    //     {
    //         // Arrange
    //         var mockAssociationRepo = new Mock<IAssociationProjectCollaboratorRepository>();
    //         var mockHolidayRepo = new Mock<IHolidayPlanRepository>();
    //         var project = Mock.Of<IProject>();
    //         var collaborator = Mock.Of<ICollaborator>();
    //         var holidayPlan = Mock.Of<IHolidayPlan>();
    //         var holidayPeriod = new Mock<IHolidayPeriod>();
            
    //         DateOnly initDate = new DateOnly(2024, 6, 1);
    //         DateOnly endDate = new DateOnly(2024, 6, 10);
            
    //         holidayPeriod.Setup(h => h.GetInitDate()).Returns(new DateOnly(2024, 6, 3));
    //         holidayPeriod.Setup(h => h.GetFinalDate()).Returns(new DateOnly(2024, 6, 8));
    //         holidayPeriod.Setup(h => h.GetDurationInDays(It.IsAny<DateOnly>(), It.IsAny<DateOnly>())).Returns(6);
            
    //         Mock.Get(holidayPlan).Setup(h => h.GetHolidayPeriods()).Returns(new List<IHolidayPeriod> { holidayPeriod.Object });
    //         mockHolidayRepo.Setup(h => h.GetHolidayPlansByCollaborator(collaborator)).Returns(new List<IHolidayPlan> { holidayPlan });
    //         mockAssociationRepo.Setup(a => a.FindAllProjectCollaborators(project)).Returns(new List<ICollaborator> { collaborator });
            
    //         var service = new HolidayPlanService(mockAssociationRepo.Object, mockHolidayRepo.Object);
            
    //         // Act
    //         int totalHolidayDays = service.GetHolidayDaysForProjectAllCollaboratorBetweenDates(project, initDate, endDate);
            
    //         // Assert
    //         Assert.Equal(6, totalHolidayDays);
    //     }

    //     [Fact]
    //     public void GivenHolidayDaysForProjectCollaboratorBetweenDates_WhenReturnZero_ThenNoCollaborators()
    //     {
    //         // Arrange
    //         var mockAssociationRepo = new Mock<IAssociationProjectCollaboratorRepository>();
    //         var mockHolidayRepo = new Mock<IHolidayPlanRepository>();
    //         var project = Mock.Of<IProject>();
            
    //         DateOnly initDate = new DateOnly(2024, 6, 1);
    //         DateOnly endDate = new DateOnly(2024, 6, 10);
            
    //         mockAssociationRepo.Setup(a => a.FindAllProjectCollaborators(project)).Returns(new List<ICollaborator>());
            
    //         var service = new HolidayPlanService(mockAssociationRepo.Object, mockHolidayRepo.Object);
            
    //         // Act
    //         int totalHolidayDays = service.GetHolidayDaysForProjectAllCollaboratorBetweenDates(project, initDate, endDate);
            
    //         // Assert
    //         Assert.Equal(0, totalHolidayDays);
    //     }

    //     [Fact]
    //     public void GivenHolidayDaysForProjectCollaboratorBetweenDates_WhenReturnZero_ThenDatesAreInvalid()
    //     {
    //         // Arrange
    //         var mockAssociationRepo = new Mock<IAssociationProjectCollaboratorRepository>();
    //         var mockHolidayRepo = new Mock<IHolidayPlanRepository>();
    //         var project = Mock.Of<IProject>();
            
    //         DateOnly initDate = new DateOnly(2024, 6, 10);
    //         DateOnly endDate = new DateOnly(2024, 6, 1);
            
    //         var service = new HolidayPlanService(mockAssociationRepo.Object, mockHolidayRepo.Object);
            
    //         // Act
    //         int totalHolidayDays = service.GetHolidayDaysForProjectAllCollaboratorBetweenDates(project, initDate, endDate);
            
    //         // Assert
    //         Assert.Equal(0, totalHolidayDays);
    //     }
    }
}
