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

        [Fact]
        public void GetHolidayDaysForProjectCollaboratorBetweenDates_ShouldReturnZero_WhenInitDateIsGreaterThanEndDate()
        {
            // Arrange
            var mockProject = new Mock<IProject>();
            var mockHolidayRepo = new Mock<IHolidayPlanRepository>();
            var mockAssociationRepo = new Mock<IAssociationProjectCollaboratorRepository>();

            var service = new HolidayPlanService(mockAssociationRepo.Object, mockHolidayRepo.Object);

            DateOnly initDate = new DateOnly(2024, 12, 31); // Data inicial maior
            DateOnly endDate = new DateOnly(2024, 01, 01);  // Data final menor

            // Act
            int result = service.GetHolidayDaysForProjectCollaboratorBetweenDates(mockProject.Object, initDate, endDate);

            // Assert
            Assert.Equal(0, result);
        }
    }

}
