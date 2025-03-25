using Domain;
using Moq;

namespace Domain.Tests;

public class HolidayPlanRepositoryTest
{
    // US13 Tests - Como gestor de RH, quero listar os períodos de férias dum colaborador num período

    [Fact]
    public void WhenHolidayPeriodIsTheSameAsTheOneBeingSearchedFor_ThenReturnsCorrectPeriod()
    {
        // Arrange
        var hpRepo = new HolidayPlanRepository();
        var colaborator = new Mock<IColaborator>();
        var initDate = new DateOnly(2025, 7, 15);
        var endDate = new DateOnly(2025, 8, 1);

        var holidayPeriod = new Mock<IHolidayPeriod>();
        holidayPeriod.Setup(p => p.GetInitDate()).Returns(initDate);
        holidayPeriod.Setup(p => p.GetFinalDate()).Returns(endDate);

        var holidayPlan = new Mock<IHolidayPlan>();
        holidayPlan.Setup(hp => hp.HasColaborator(colaborator.Object)).Returns(true);
        holidayPlan
            .Setup(hp => hp.GetHolidayPeriods())
            .Returns(new List<IHolidayPeriod> { holidayPeriod.Object });

        // Adicionar o plano de férias ao repositório
        hpRepo.AddHolidayPlan(holidayPlan.Object);

        // Act
        var result = hpRepo.FindAllHolidayPeriodsForCollaboratorBetweenDates(
            colaborator.Object,
            initDate,
            endDate
        );

        // Assert
        Assert.Single(result);
    }

    [Fact]
    public void WhenHolidayPeriodIsBiggerThanTheOneBeingSearchedFor_ThenReturnsCorrectPeriod()
    {
        // Arrange
        var hpRepo = new HolidayPlanRepository();
        var colaborator = new Mock<IColaborator>();
        var initDateForSearching = new DateOnly(2025, 7, 15);
        var endDateForSearching = new DateOnly(2025, 8, 1);
        var initDateForPeriod = new DateOnly(2025, 7, 20);
        var endDateForPeriod = new DateOnly(2025, 7, 25);

        var holidayPeriod = new Mock<IHolidayPeriod>();
        holidayPeriod.Setup(p => p.GetInitDate()).Returns(initDateForPeriod);
        holidayPeriod.Setup(p => p.GetFinalDate()).Returns(endDateForPeriod);

        var holidayPlan = new Mock<IHolidayPlan>();
        holidayPlan.Setup(hp => hp.HasColaborator(colaborator.Object)).Returns(true);
        holidayPlan
            .Setup(hp => hp.GetHolidayPeriods())
            .Returns(new List<IHolidayPeriod> { holidayPeriod.Object });

        // Adicionar o plano de férias ao repositório
        hpRepo.AddHolidayPlan(holidayPlan.Object);

        // Act
        var result = hpRepo.FindAllHolidayPeriodsForCollaboratorBetweenDates(
            colaborator.Object,
            initDateForSearching,
            endDateForSearching
        );

        // Assert
        Assert.Single(result);
    }

    // De acordo com a resposta do professor:
    // quando o periodo procurado é mais pequeno do que o periodo de ferias
    // quando o periodo procurado coincide com o periodo de ferias, mas nao na totalidade (inicio/fim)

    [Fact]
    public void WhenHolidayPeriodIsOutOfRangeThanTheOneBeingSearchedFor_ThenReturnsCorrectPeriod()
    {
        // Arrange
        var hpRepo = new HolidayPlanRepository();
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
        hpRepo.AddHolidayPlan(holidayPlan.Object);

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
    public void WhenColaboratorHolidayPeriodsStartAndEndAtSearchedStartAndEndPeriod_ThenThenReturnsCorrectPeriods()
    {
        // dados dois periodos, quando o primeiro começa exatamente no mesmo dia que o primeiro dia procurado
        // e o segundo termina exatamente no ultimo dia procurado, então retorna dois periodos.
        // Arrange
        var hpRepo = new HolidayPlanRepository();
        var colaborator = new Mock<IColaborator>();
        var initDateForSearching = new DateOnly(2025, 7, 15);
        var endDateForSearching = new DateOnly(2025, 8, 1);

        var initDateForPeriod_1 = new DateOnly(2025, 7, 15);
        var endDateForPeriod_1 = new DateOnly(2025, 7, 20);
        var initDateForPeriod_2 = new DateOnly(2025, 7, 25);
        var endDateForPeriod_2 = new DateOnly(2025, 8, 1);

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
        hpRepo.AddHolidayPlan(holidayPlan.Object);

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

    // US14 - Como gestor de RH, quero listar os colaboradores que têm de férias num período
    [Fact]
    public void WhenCollaboratorHasHolidayPeriodWithinDateRange_ThenReturnsCollaborator()
    {
        // Arrange
        var hpRepo = new HolidayPlanRepository();
        var colaborator = new Mock<IColaborator>();
        var initDate = new DateOnly(2025, 7, 15);
        var endDate = new DateOnly(2025, 8, 1);

        var holidayPeriod = new Mock<IHolidayPeriod>();
        holidayPeriod.Setup(p => p.GetInitDate()).Returns(new DateOnly(2025, 7, 10));
        holidayPeriod.Setup(p => p.GetFinalDate()).Returns(new DateOnly(2025, 7, 20));

        var holidayPlan = new Mock<IHolidayPlan>();
        holidayPlan.Setup(hp => hp.HasColaborator(colaborator.Object)).Returns(true);
        holidayPlan
            .Setup(hp => hp.GetHolidayPeriods())
            .Returns(new List<IHolidayPeriod> { holidayPeriod.Object });
        holidayPlan.Setup(hp => hp.GetColaborator()).Returns(colaborator.Object);

        hpRepo.AddHolidayPlan(holidayPlan.Object);

        // Act
        var result = hpRepo.FindAllCollaboratorsWithHolidayPeriodsBetweenDates(initDate, endDate);

        // Assert
        Assert.Single(result);
        Assert.Contains(colaborator.Object, result);
    }

    [Fact]
    public void WhenMultipleCollaboratorsHaveHolidayPeriodsWithinDateRange_ThenReturnsAllCollaborators()
    {
        // Arrange
        var hpRepo = new HolidayPlanRepository();
        var colaborator1 = new Mock<IColaborator>();
        var colaborator2 = new Mock<IColaborator>();
        var initDate = new DateOnly(2025, 7, 15);
        var endDate = new DateOnly(2025, 8, 1);

        var holidayPeriod1 = new Mock<IHolidayPeriod>();
        holidayPeriod1.Setup(p => p.GetInitDate()).Returns(new DateOnly(2025, 7, 10));
        holidayPeriod1.Setup(p => p.GetFinalDate()).Returns(new DateOnly(2025, 7, 20));

        var holidayPeriod2 = new Mock<IHolidayPeriod>();
        holidayPeriod2.Setup(p => p.GetInitDate()).Returns(new DateOnly(2025, 7, 18));
        holidayPeriod2.Setup(p => p.GetFinalDate()).Returns(new DateOnly(2025, 7, 25));

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

        hpRepo.AddHolidayPlan(holidayPlan1.Object);
        hpRepo.AddHolidayPlan(holidayPlan2.Object);

        // Act
        var result = hpRepo.FindAllCollaboratorsWithHolidayPeriodsBetweenDates(initDate, endDate);

        // Assert
        Assert.Equal(2, result.Count());
        Assert.Contains(colaborator1.Object, result);
        Assert.Contains(colaborator2.Object, result);
    }

    [Fact]
    public void WhenNoCollaboratorsHaveHolidayPeriodsInDateRange_ThenReturnsEmptyList()
    {
        // Arrange
        var hpRepo = new HolidayPlanRepository();
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

        hpRepo.AddHolidayPlan(holidayPlan.Object);

        // Act
        var result = hpRepo.FindAllCollaboratorsWithHolidayPeriodsBetweenDates(initDate, endDate);

        // Assert
        Assert.Empty(result);
    }

    // sobreposições a espera de resposta do professor
}
