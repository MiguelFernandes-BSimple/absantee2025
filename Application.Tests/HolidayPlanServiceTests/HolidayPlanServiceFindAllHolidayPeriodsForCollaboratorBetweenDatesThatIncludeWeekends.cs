using Domain.Interfaces;
using Domain.IRepository;
using Application.Services;
using Moq;
namespace Application.Tests.HolidayPlanServiceTests;

public class HolidayPlanServiceFindAllHolidayPeriodsForCollaboratorBetweenDatesThatIncludeWeekends
{
    [Fact]
    public async Task WhenRetrievingAllHolidayPeriodsForCollaboratorBetweenDatesThatIncludeWeekends_ThenReturnSucessfully()
    {
        // Arrange
        Mock<IHolidayPeriod> holidayPeriod = new Mock<IHolidayPeriod>();

        Mock<IHolidayPlanRepository> holidayPlanRepository = new Mock<IHolidayPlanRepository>();
        Mock<IAssociationProjectCollaboratorRepository> associationRepository = new Mock<IAssociationProjectCollaboratorRepository>();

        Mock<IPeriodDate> searchPeriodDate = new Mock<IPeriodDate>();
        searchPeriodDate.Setup(p => p.ContainsWeekend()).Returns(true);

        var holidayPeriodsList = new List<IHolidayPeriod> { holidayPeriod.Object };

        holidayPlanRepository.Setup(r => r.FindAllHolidayPeriodsForCollaboratorBetweenDatesAsync(It.IsAny<long>(), searchPeriodDate.Object)).ReturnsAsync(holidayPeriodsList);
        holidayPeriod.Setup(hp => hp.ContainsWeekend()).Returns(true);

        HolidayPlanService service = new HolidayPlanService(associationRepository.Object, holidayPlanRepository.Object);

        // Act
        var result = await service.FindAllHolidayPeriodsForCollaboratorBetweenDatesThatIncludeWeekendsAsync(It.IsAny<long>(), searchPeriodDate.Object);

        // Assert
        Assert.True(result.SequenceEqual(holidayPeriodsList));
    }

    [Fact]
    public async Task WhenRetrievingAllHolidayPeriodsForCollaboratorBetweenWithSearchDatesThatDontIncludeWeekends_ThenReturnEmpty()
    {
        // Arrange
        Mock<IHolidayPeriod> holidayPeriod = new Mock<IHolidayPeriod>();
        holidayPeriod.Setup(hp => hp.ContainsWeekend()).Returns(true);

        Mock<IHolidayPlanRepository> holidayPlanRepository = new Mock<IHolidayPlanRepository>();
        Mock<IAssociationProjectCollaboratorRepository> associationRepository = new Mock<IAssociationProjectCollaboratorRepository>();

        Mock<IPeriodDate> searchPeriodDate = new Mock<IPeriodDate>();
        searchPeriodDate.Setup(spd => spd.ContainsWeekend()).Returns(false);

        var holidayPeriodsList = new List<IHolidayPeriod> { holidayPeriod.Object };

        holidayPlanRepository.Setup(r => r.FindAllHolidayPeriodsForCollaboratorBetweenDatesAsync(It.IsAny<long>(), searchPeriodDate.Object)).ReturnsAsync(holidayPeriodsList);

        HolidayPlanService service = new HolidayPlanService(associationRepository.Object, holidayPlanRepository.Object);

        // Act
        var result = await service.FindAllHolidayPeriodsForCollaboratorBetweenDatesThatIncludeWeekendsAsync(It.IsAny<long>(), searchPeriodDate.Object);

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public async Task WhenRetrievingAllHolidayPeriodsForCollaboratorBetweenWithHolidayDatesThatDontIncludeWeekends_ThenReturnEmpty()
    {
        // Arrange
        Mock<IHolidayPeriod> holidayPeriod = new Mock<IHolidayPeriod>();
        holidayPeriod.Setup(hp => hp.ContainsWeekend()).Returns(false);

        Mock<IHolidayPlanRepository> holidayPlanRepository = new Mock<IHolidayPlanRepository>();
        Mock<IAssociationProjectCollaboratorRepository> associationRepository = new Mock<IAssociationProjectCollaboratorRepository>();

        Mock<IPeriodDate> searchPeriodDate = new Mock<IPeriodDate>();
        searchPeriodDate.Setup(spd => spd.ContainsWeekend()).Returns(true);

        var holidayPeriodsList = new List<IHolidayPeriod> { holidayPeriod.Object };

        holidayPlanRepository.Setup(r => r.FindAllHolidayPeriodsForCollaboratorBetweenDatesAsync(It.IsAny<long>(), searchPeriodDate.Object)).ReturnsAsync(holidayPeriodsList);

        HolidayPlanService service = new HolidayPlanService(associationRepository.Object, holidayPlanRepository.Object);

        // Act
        var result = await service.FindAllHolidayPeriodsForCollaboratorBetweenDatesThatIncludeWeekendsAsync(It.IsAny<long>(), searchPeriodDate.Object);

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public async Task WhenRetrievingAllHolidayPeriodsForCollaboratorBetweenWithNonIntersectingPeriodsThatIncludeDifferentWeekends_ThenReturnEmpty()
    {
        // Arrange
        Mock<IHolidayPeriod> holidayPeriod = new Mock<IHolidayPeriod>();
        holidayPeriod.Setup(hp => hp.ContainsWeekend()).Returns(true);

        Mock<IHolidayPlanRepository> holidayPlanRepository = new Mock<IHolidayPlanRepository>();
        Mock<IAssociationProjectCollaboratorRepository> associationRepository = new Mock<IAssociationProjectCollaboratorRepository>();

        Mock<IPeriodDate> searchPeriodDate = new Mock<IPeriodDate>();
        searchPeriodDate.Setup(spd => spd.ContainsWeekend()).Returns(true);

        var holidayPeriodsList = new List<IHolidayPeriod> { holidayPeriod.Object };

        holidayPlanRepository.Setup(r => r.FindAllHolidayPeriodsForCollaboratorBetweenDatesAsync(It.IsAny<long>(), searchPeriodDate.Object)).ReturnsAsync(new List<IHolidayPeriod>());

        HolidayPlanService service = new HolidayPlanService(associationRepository.Object, holidayPlanRepository.Object);

        // Act
        var result = await service.FindAllHolidayPeriodsForCollaboratorBetweenDatesThatIncludeWeekendsAsync(It.IsAny<long>(), searchPeriodDate.Object);

        // Assert
        Assert.Empty(result);
    }
}