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
            hpRepoMock.Setup(hp => hp.GetHolidayPlansWithHolidayPeriodValid(initDate, endDate)).Returns(new List<IHolidayPlan> { holidayPlan.Object });

            var APCRepo = new Mock<IAssociationProjectCollaboratorRepository>();
            var colabService = new CollaboratorService(APCRepo.Object, hpRepoMock.Object);
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
            hpRepoMock.Setup(hp => hp.GetHolidayPlansWithHolidayPeriodValid(initDate, endDate)).Returns(new List<IHolidayPlan> { holidayPlan1.Object, holidayPlan2.Object });

            var APCRepo = new Mock<IAssociationProjectCollaboratorRepository>();
            var colabService = new CollaboratorService(APCRepo.Object, hpRepoMock.Object);

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
            hpRepoMock.Setup(hp => hp.GetHolidayPlansWithHolidayPeriodValid(initDate, endDate)).Returns(new List<IHolidayPlan> { holidayPlan.Object });

            var APCRepo = new Mock<IAssociationProjectCollaboratorRepository>();
            var colabService = new CollaboratorService(APCRepo.Object, hpRepoMock.Object);

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
            hpRepoMock.Setup(hp => hp.GetHolidayPlansWithHolidayPeriodValid(initDate, endDate)).Returns(new List<IHolidayPlan>());
            var APCRepo = new Mock<IAssociationProjectCollaboratorRepository>();

            var colabService = new CollaboratorService(APCRepo.Object, hpRepoMock.Object);

            // Act
            var result = colabService.FindAllCollaboratorsWithHolidayPeriodsBetweenDates(initDate, endDate);

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public void WhenPassingInitDateBiggerThanEndDate_ThenReturnsEmptyList()
        {
            // Arrange
            var initDate = new DateOnly(2025, 8, 15);
            var endDate = new DateOnly(2025, 7, 1);

            var hpRepoMock = new Mock<IHolidayPlanRepository>();
            hpRepoMock.Setup(hp => hp.GetHolidayPlansWithHolidayPeriodValid(initDate, endDate)).Returns(new List<IHolidayPlan>());
            var APCRepo = new Mock<IAssociationProjectCollaboratorRepository>();
            var colabService = new CollaboratorService(APCRepo.Object, hpRepoMock.Object);
            // Act
            var result = colabService.FindAllCollaboratorsWithHolidayPeriodsBetweenDates(initDate, endDate);

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public void WhenFindingCollaboratorsByProject_ThenReturnAllAssociatedCollaborators()
        {
            //arrange
            Mock<IProject> projectMock = new Mock<IProject>();

            Mock<IHolidayPlanRepository> holidayPlanRepoMock = new Mock<IHolidayPlanRepository>();
            Mock<IAssociationProjectCollaboratorRepository> assocRepoMock = new Mock<IAssociationProjectCollaboratorRepository>();

            Mock<IAssociationProjectCollaborator> assoc1 = new Mock<IAssociationProjectCollaborator>();
            Mock<IAssociationProjectCollaborator> assoc2 = new Mock<IAssociationProjectCollaborator>();

            List<IAssociationProjectCollaborator> associations = new List<IAssociationProjectCollaborator>()
            {
                assoc1.Object,
                assoc2.Object,
            };

            Mock<ICollaborator> collab1 = new Mock<ICollaborator>();
            Mock<ICollaborator> collab2 = new Mock<ICollaborator>();

            assoc1.Setup(a => a.GetCollaborator()).Returns(collab1.Object);
            assoc2.Setup(a => a.GetCollaborator()).Returns(collab2.Object);

            List<ICollaborator> expected = new List<ICollaborator>()
            {
                collab1.Object,
                collab2.Object
            };

            assocRepoMock.Setup(a => a.FindAllByProject(projectMock.Object)).Returns(associations);

            var assoc = new CollaboratorService(assocRepoMock.Object, holidayPlanRepoMock.Object);

            //act
            var result = assoc.FindAllByProject(projectMock.Object);

            //assert
            Assert.True(expected.SequenceEqual(result));
        }

        [Fact]
        public void WhenProjectHasNoCollaborators_ThenReturnEmptyList()
        {
            // Arrange
            Mock<IProject> projectMock = new Mock<IProject>();

            Mock<IHolidayPlanRepository> holidayPlanRepoMock = new Mock<IHolidayPlanRepository>();
            Mock<IAssociationProjectCollaboratorRepository> assocRepoMock = new Mock<IAssociationProjectCollaboratorRepository>();

            // No associations for the project
            List<IAssociationProjectCollaborator> emptyAssociations = new List<IAssociationProjectCollaborator>();

            assocRepoMock.Setup(a => a.FindAllByProject(projectMock.Object)).Returns(emptyAssociations);

            var assoc = new CollaboratorService(assocRepoMock.Object, holidayPlanRepoMock.Object);

            // Act
            var result = assoc.FindAllByProject(projectMock.Object);

            // Assert
            Assert.Empty(result);
        }


        [Fact]
        public void WhenProjectHasCollaboratorsInPeriod_ThenReturnAllCollaborators()
        {
            // Arrange
            Mock<IProject> projectMock = new Mock<IProject>();
            DateOnly initDate = new DateOnly(2023, 1, 1);
            DateOnly finalDate = new DateOnly(2023, 12, 31);

            Mock<IHolidayPlanRepository> holidayPlanRepoMock = new Mock<IHolidayPlanRepository>();
            Mock<IAssociationProjectCollaboratorRepository> assocRepoMock = new Mock<IAssociationProjectCollaboratorRepository>();

            Mock<IAssociationProjectCollaborator> assoc1 = new Mock<IAssociationProjectCollaborator>();
            Mock<IAssociationProjectCollaborator> assoc2 = new Mock<IAssociationProjectCollaborator>();

            List<IAssociationProjectCollaborator> associations = new List<IAssociationProjectCollaborator>()
            {
                assoc1.Object,
                assoc2.Object,
            };

            Mock<ICollaborator> collab1 = new Mock<ICollaborator>();
            Mock<ICollaborator> collab2 = new Mock<ICollaborator>();

            assoc1.Setup(a => a.GetCollaborator()).Returns(collab1.Object);
            assoc2.Setup(a => a.GetCollaborator()).Returns(collab2.Object);

            List<ICollaborator> expected = new List<ICollaborator>()
            {
                collab1.Object,
                collab2.Object
            };

            assocRepoMock.Setup(a => a.FindAllByProjectAndBetweenPeriod(projectMock.Object, initDate, finalDate)).Returns(associations);

            var service = new CollaboratorService(assocRepoMock.Object, holidayPlanRepoMock.Object);

            // Act
            var result = service.FindAllByProjectAndBetweenPeriod(projectMock.Object, initDate, finalDate);

            // Assert
            Assert.True(expected.SequenceEqual(result));
        }


        [Fact]
        public void WhenProjectHasNoCollaboratorsInPeriod_ThenReturnEmptyList()
        {
            // Arrange
            Mock<IProject> projectMock = new Mock<IProject>();
            DateOnly initDate = new DateOnly(2023, 1, 1);
            DateOnly finalDate = new DateOnly(2023, 12, 31);

            Mock<IHolidayPlanRepository> holidayPlanRepoMock = new Mock<IHolidayPlanRepository>();
            Mock<IAssociationProjectCollaboratorRepository> assocRepoMock = new Mock<IAssociationProjectCollaboratorRepository>();

            // No associations for the project in the period
            List<IAssociationProjectCollaborator> emptyAssociations = new List<IAssociationProjectCollaborator>();

            assocRepoMock.Setup(a => a.FindAllByProjectAndBetweenPeriod(projectMock.Object, initDate, finalDate)).Returns(emptyAssociations);

            var service = new CollaboratorService(assocRepoMock.Object, holidayPlanRepoMock.Object);

            // Act
            var result = service.FindAllByProjectAndBetweenPeriod(projectMock.Object, initDate, finalDate);

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public void WhenCreatingWithValidParameters_ThenObjectIsCreated()
        {
            //arrange
            Mock<IHolidayPlanRepository> holidayPlanRepositoryDouble = new Mock<IHolidayPlanRepository>();
            Mock<IAssociationProjectCollaboratorRepository> assocRepoMock = new Mock<IAssociationProjectCollaboratorRepository>();

            //act & assert
            new CollaboratorService(assocRepoMock.Object, holidayPlanRepositoryDouble.Object);

        }

        [Fact]
        public void WhenFindingAllCollaboratorsWithHolidayPeriodsLongerThan_ThenShouldReturnCorrectCollaborators()
        {
            //arrange
            int days = 5;

            Mock<ICollaborator> collaboratorDouble1 = new Mock<ICollaborator>();
            //Mock<ICollaborator> collaboratorDouble2 = new Mock<ICollaborator>();

            Mock<IHolidayPlan> holidayPlanDouble1 = new Mock<IHolidayPlan>();
            holidayPlanDouble1.Setup(p => p.GetCollaborator()).Returns(collaboratorDouble1.Object);
            //holidayPlanDouble1.Setup(p => p.HasPeriodLongerThan(days)).Returns(true);

            Mock<IAssociationProjectCollaboratorRepository> assocRepoMock = new Mock<IAssociationProjectCollaboratorRepository>();
            Mock<IHolidayPlanRepository> holidayPlanRepositoryDouble = new Mock<IHolidayPlanRepository>();
            holidayPlanRepositoryDouble.Setup(hpr => hpr.FindAllWithHolidayPeriodsLongerThan(days)).Returns(new List<IHolidayPlan> { holidayPlanDouble1.Object });

            CollaboratorService collaboratorService = new CollaboratorService(assocRepoMock.Object, holidayPlanRepositoryDouble.Object);

            //act
            var result = collaboratorService.FindAllWithHolidayPeriodsLongerThan(days).ToList();

            //assert
            var expected = new List<ICollaborator> { collaboratorDouble1.Object };
            Assert.Equal(expected, result);
        }

        [Fact]
        public void WhenNoCollaboratorHaveHolidayPeriodLongerThan_ThenShouldReturnEmptyList()
        {
            //arrange
            int days = 5;

            Mock<IAssociationProjectCollaboratorRepository> assocRepoMock = new Mock<IAssociationProjectCollaboratorRepository>();
            Mock<IHolidayPlanRepository> holidayPlanRepositoryDouble = new Mock<IHolidayPlanRepository>();
            holidayPlanRepositoryDouble.Setup(hpr => hpr.FindAllWithHolidayPeriodsLongerThan(days)).Returns(new List<IHolidayPlan> { });

            CollaboratorService service = new CollaboratorService(assocRepoMock.Object, holidayPlanRepositoryDouble.Object);

            //act
            var result = service.FindAllWithHolidayPeriodsLongerThan(days);

            //assert
            Assert.Empty(result);
        }
    }
}
