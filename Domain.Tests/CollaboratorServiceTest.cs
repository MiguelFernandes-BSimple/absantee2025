using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;

namespace Domain.Tests
{
    public class CollaboratorServiceTest
    {
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

            var hpRepoMock = new Mock<IHolidayPlanRepository>();
            hpRepoMock.Setup(hp => hp.FindAll()).Returns(new List<IHolidayPlan> { holidayPlan.Object });
            var colabService = new CollaboratorService(hpRepoMock.Object);
            // Act
            var result = colabService.FindAllCollaboratorsWithHolidayPeriodsBetweenDates(initDate, endDate);

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

            var hpRepoMock = new Mock<IHolidayPlanRepository>();
            hpRepoMock.Setup(hp => hp.FindAll()).Returns(new List<IHolidayPlan> { holidayPlan1.Object, holidayPlan2.Object });
            var colabService = new CollaboratorService(hpRepoMock.Object);
            // Act
            var result = colabService.FindAllCollaboratorsWithHolidayPeriodsBetweenDates(initDate, endDate);

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

            var hpRepoMock = new Mock<IHolidayPlanRepository>();
            hpRepoMock.Setup(hp => hp.FindAll()).Returns(new List<IHolidayPlan> { holidayPlan.Object });
            var colabService = new CollaboratorService(hpRepoMock.Object);
            // Act
            var result = colabService.FindAllCollaboratorsWithHolidayPeriodsBetweenDates(initDate, endDate);

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

            var hpRepoMock = new Mock<IHolidayPlanRepository>();
            hpRepoMock.Setup(hp => hp.FindAll()).Returns(new List<IHolidayPlan> { holidayPlan.Object });
            var colabService = new CollaboratorService(hpRepoMock.Object);
            // Act
            var result = colabService.FindAllCollaboratorsWithHolidayPeriodsBetweenDates(initDate, endDate);

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
            var hpRepoMock = new Mock<IHolidayPlanRepository>();
            hpRepoMock.Setup(hp => hp.FindAll()).Returns(new List<IHolidayPlan>());
            var colabService = new CollaboratorService(hpRepoMock.Object);
            // Act
            var result = colabService.FindAllCollaboratorsWithHolidayPeriodsBetweenDates(initDate, endDate);

            Assert.Empty(result);
        }
    }
}