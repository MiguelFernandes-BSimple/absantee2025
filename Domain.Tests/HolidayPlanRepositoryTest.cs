using Domain;
using Moq;

namespace Domain.Tests;

public class HolidayPlanRepositoryTest
{
    [Fact]
    public void GivenProjectWithCollaborators_WhenGetHolidayDaysForAllCollaborators_ThenReturnsCorrectDays()
    {
        // Arrange
        var mockColaborator = new Mock<IColaborator>();
        var mockHolidayPlan = new Mock<IHolidayPlan>();

        // Configurando o colaborador
        mockColaborator.Setup(c => c.ContainsDates(It.IsAny<DateTime>(), It.IsAny<DateTime>())).Returns(true);

        // Período de férias
        var mockHolidayPeriod = new Mock<IHolidayPeriod>();
        mockHolidayPeriod.Setup(h => h.GetInitDate()).Returns(new DateOnly(2024, 6, 1));
        mockHolidayPeriod.Setup(h => h.GetFinalDate()).Returns(new DateOnly(2024, 6, 10));
        mockHolidayPeriod.Setup(h => h.GetDurationInDays(It.IsAny<DateOnly>(), It.IsAny<DateOnly>())).Returns(10);

        // Criando o plano de férias para o colaborador
        var holidayPlan = new HolidayPlan(new List<IHolidayPeriod> { mockHolidayPeriod.Object }, mockColaborator.Object);

        // Criar lista de colaboradores
        var collaboratorsList = new List<IColaborator>() { mockColaborator.Object };

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
}



