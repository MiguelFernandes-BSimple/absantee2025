using Domain.Interfaces;
using Domain.IRepository;
using Application.Services;
using Moq;
using Domain.IRepository;
namespace Application.Tests.HolidayPlanServiceTests;

public class FindAllHolidayPeriodsForCollaboratorBetweenDatesThatIncludeWeekends
{
    [Fact]
    public void WhenRetrievingAllHolidayPeriodsForCollaboratorBetweenDatesThatIncludeWeekends_ThenReturnSucessfully()
    {
        // Arrange
        Mock<ICollaborator> collab = new Mock<ICollaborator>();

        Mock<IPeriodDate> holidayPeriodDate = new Mock<IPeriodDate>();
        Mock<IHolidayPeriod> holidayPeriod = new Mock<IHolidayPeriod>();

        Mock<IHolidayPlanRepository> holidayPlanRepository = new Mock<IHolidayPlanRepository>();
        Mock<IAssociationProjectCollaboratorRepository> associationRepository = new Mock<IAssociationProjectCollaboratorRepository>();

        Mock<IPeriodDate> searchPeriodDate = new Mock<IPeriodDate>();
        searchPeriodDate.Setup(p => p.ContainsWeekend()).Returns(true);

        var holidayPeriodsList = new List<IHolidayPeriod> { holidayPeriod.Object };

        holidayPlanRepository.Setup(r => r.FindAllHolidayPeriodsForCollaboratorBetweenDates(collab.Object, searchPeriodDate.Object)).Returns(holidayPeriodsList);
        holidayPeriod.Setup(hp => hp.ContainsWeekend()).Returns(true);
        HolidayPlanService service = new HolidayPlanService(associationRepository.Object, holidayPlanRepository.Object);

        // Act
        var result = service.FindAllHolidayPeriodsForCollaboratorBetweenDatesThatIncludeWeekends(collab.Object, searchPeriodDate.Object);

        // Assert
        Assert.True(result.SequenceEqual(holidayPeriodsList));
    }

    [Fact]
    public void WhenRetrievingAllHolidayPeriodsForCollaboratorBetweenWithSearchDatesThatDontIncludeWeekends_ThenReturnEmpty()
    {
        // Arrange
        Mock<ICollaborator> collab = new Mock<ICollaborator>();

        Mock<IPeriodDate> holidayPeriodDate = new Mock<IPeriodDate>();
        Mock<IHolidayPeriod> holidayPeriod = new Mock<IHolidayPeriod>();
        holidayPeriod.Setup(hp => hp.GetPeriodDate()).Returns(holidayPeriodDate.Object);
        holidayPeriod.Setup(hp => hp.ContainsWeekend()).Returns(true);

        Mock<IHolidayPlanRepository> holidayPlanRepository = new Mock<IHolidayPlanRepository>();
        Mock<IAssociationProjectCollaboratorRepository> associationRepository = new Mock<IAssociationProjectCollaboratorRepository>();

        Mock<IPeriodDate> searchPeriodDate = new Mock<IPeriodDate>();
        searchPeriodDate.Setup(spd => spd.ContainsWeekend()).Returns(false);

        var holidayPeriodsList = new List<IHolidayPeriod> { holidayPeriod.Object };

        holidayPlanRepository.Setup(r => r.FindAllHolidayPeriodsForCollaboratorBetweenDates(collab.Object, searchPeriodDate.Object)).Returns(holidayPeriodsList);

        HolidayPlanService service = new HolidayPlanService(associationRepository.Object, holidayPlanRepository.Object);

        // Act
        var result = service.FindAllHolidayPeriodsForCollaboratorBetweenDatesThatIncludeWeekends(collab.Object, searchPeriodDate.Object);

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public void WhenRetrievingAllHolidayPeriodsForCollaboratorBetweenWithHolidayDatesThatDontIncludeWeekends_ThenReturnEmpty()
    {
        // Arrange
        Mock<ICollaborator> collab = new Mock<ICollaborator>();

        Mock<IPeriodDate> holidayPeriodDate = new Mock<IPeriodDate>();
        Mock<IHolidayPeriod> holidayPeriod = new Mock<IHolidayPeriod>();
        holidayPeriod.Setup(hp => hp.GetPeriodDate()).Returns(holidayPeriodDate.Object);
        holidayPeriod.Setup(hp => hp.ContainsWeekend()).Returns(false);

        Mock<IHolidayPlanRepository> holidayPlanRepository = new Mock<IHolidayPlanRepository>();
        Mock<IAssociationProjectCollaboratorRepository> associationRepository = new Mock<IAssociationProjectCollaboratorRepository>();

        Mock<IPeriodDate> searchPeriodDate = new Mock<IPeriodDate>();
        searchPeriodDate.Setup(spd => spd.ContainsWeekend()).Returns(true);

        var holidayPeriodsList = new List<IHolidayPeriod> { holidayPeriod.Object };

        holidayPlanRepository.Setup(r => r.FindAllHolidayPeriodsForCollaboratorBetweenDates(collab.Object, searchPeriodDate.Object)).Returns(holidayPeriodsList);

        HolidayPlanService service = new HolidayPlanService(associationRepository.Object, holidayPlanRepository.Object);

        // Act
        var result = service.FindAllHolidayPeriodsForCollaboratorBetweenDatesThatIncludeWeekends(collab.Object, searchPeriodDate.Object);

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public void WhenRetrievingAllHolidayPeriodsForCollaboratorBetweenWithNonIntersectingPeriodsThatIncludeDifferentWeekends_ThenReturnEmpty()
    {
        // Arrange
        Mock<ICollaborator> collab = new Mock<ICollaborator>();

        Mock<IPeriodDate> holidayPeriodDate = new Mock<IPeriodDate>();
        Mock<IHolidayPeriod> holidayPeriod = new Mock<IHolidayPeriod>();
        holidayPeriod.Setup(hp => hp.GetPeriodDate()).Returns(holidayPeriodDate.Object);
        holidayPeriod.Setup(hp => hp.ContainsWeekend()).Returns(true);

        Mock<IHolidayPlanRepository> holidayPlanRepository = new Mock<IHolidayPlanRepository>();
        Mock<IAssociationProjectCollaboratorRepository> associationRepository = new Mock<IAssociationProjectCollaboratorRepository>();

        Mock<IPeriodDate> searchPeriodDate = new Mock<IPeriodDate>();
        searchPeriodDate.Setup(spd => spd.ContainsWeekend()).Returns(true);

        var holidayPeriodsList = new List<IHolidayPeriod> { holidayPeriod.Object };

        holidayPlanRepository.Setup(r => r.FindAllHolidayPeriodsForCollaboratorBetweenDates(collab.Object, searchPeriodDate.Object)).Returns(new List<IHolidayPeriod>());

        HolidayPlanService service = new HolidayPlanService(associationRepository.Object, holidayPlanRepository.Object);

        // Act
        var result = service.FindAllHolidayPeriodsForCollaboratorBetweenDatesThatIncludeWeekends(collab.Object, searchPeriodDate.Object);

        // Assert
        Assert.Empty(result);
    }
}