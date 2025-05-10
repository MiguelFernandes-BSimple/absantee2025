/* using Domain.Interfaces;
using Domain.IRepository;
using Application.Services;
using Moq;
using Domain.Models;
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

        //contains a weeends
        var searchPeriodDate = new PeriodDate(new DateOnly(2020,1,1), new DateOnly(2021,1,1));

        var holidayPeriodsList = new List<IHolidayPeriod> { holidayPeriod.Object };

        holidayPlanRepository.Setup(r => r.FindAllHolidayPeriodsForCollaboratorBetweenDatesAsync(It.IsAny<long>(), searchPeriodDate)).ReturnsAsync(holidayPeriodsList);
        holidayPeriod.Setup(hp => hp.ContainsWeekend()).Returns(true);

        HolidayPlanService service = new HolidayPlanService(associationRepository.Object, holidayPlanRepository.Object);

        // Act
        var result = await service.FindAllHolidayPeriodsForCollaboratorBetweenDatesThatIncludeWeekendsAsync(It.IsAny<long>(), searchPeriodDate);

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

        //dont contains weekends
        var searchPeriodDate = new PeriodDate(new DateOnly(2020, 1, 1), new DateOnly(2020, 1, 2));

        var holidayPeriodsList = new List<IHolidayPeriod> { holidayPeriod.Object };

        holidayPlanRepository.Setup(r => r.FindAllHolidayPeriodsForCollaboratorBetweenDatesAsync(It.IsAny<long>(), searchPeriodDate)).ReturnsAsync(holidayPeriodsList);

        HolidayPlanService service = new HolidayPlanService(associationRepository.Object, holidayPlanRepository.Object);

        // Act
        var result = await service.FindAllHolidayPeriodsForCollaboratorBetweenDatesThatIncludeWeekendsAsync(It.IsAny<long>(), searchPeriodDate);

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

        //contains a weeends
        var searchPeriodDate = new PeriodDate(new DateOnly(2020, 1, 1), new DateOnly(2021, 1, 1));

        var holidayPeriodsList = new List<IHolidayPeriod> { holidayPeriod.Object };

        holidayPlanRepository.Setup(r => r.FindAllHolidayPeriodsForCollaboratorBetweenDatesAsync(It.IsAny<long>(), searchPeriodDate)).ReturnsAsync(holidayPeriodsList);

        HolidayPlanService service = new HolidayPlanService(associationRepository.Object, holidayPlanRepository.Object);

        // Act
        var result = await service.FindAllHolidayPeriodsForCollaboratorBetweenDatesThatIncludeWeekendsAsync(It.IsAny<long>(), searchPeriodDate);

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

        //contains a weeends
        var searchPeriodDate = new PeriodDate(new DateOnly(2020, 1, 1), new DateOnly(2021, 1, 1));

        var holidayPeriodsList = new List<IHolidayPeriod> { holidayPeriod.Object };

        holidayPlanRepository.Setup(r => r.FindAllHolidayPeriodsForCollaboratorBetweenDatesAsync(It.IsAny<long>(), searchPeriodDate)).ReturnsAsync(new List<IHolidayPeriod>());

        HolidayPlanService service = new HolidayPlanService(associationRepository.Object, holidayPlanRepository.Object);

        // Act
        var result = await service.FindAllHolidayPeriodsForCollaboratorBetweenDatesThatIncludeWeekendsAsync(It.IsAny<long>(), searchPeriodDate);

        // Assert
        Assert.Empty(result);
    }
} */