/* using Domain.Interfaces;
using Domain.IRepository;
using Application.Services;
using Moq;
using Domain.Models;
namespace Application.Tests.HolidayPlanServiceTests;

public class HolidayPlanServiceGetHolidayDaysForProjectAllCollaboratorBetwenDates
{
    [Fact]
    public async Task GetHolidayDaysForProjectCollaboratorBetweenDates_ReturnsCorrectDays()
    {
        // Arrange
        var mockAssociationRepo = new Mock<IAssociationProjectCollaboratorRepository>();
        var mockHolidayPlanRepo = new Mock<IHolidayPlanRepository>();

        var collabIdList = new List<long>() { 1, 2 };

        var period = new PeriodDate(It.IsAny<DateOnly>(), It.IsAny<DateOnly>());

        var mockAssociation1 = new Mock<IAssociationProjectCollaborator>();
        mockAssociation1.Setup(a => a.GetCollaboratorId()).Returns(1);

        var mockAssociation2 = new Mock<IAssociationProjectCollaborator>();
        mockAssociation2.Setup(a => a.GetCollaboratorId()).Returns(2);

        var mockHolidayPeriod1 = new Mock<IHolidayPeriod>();
        mockHolidayPeriod1.Setup(h => h.GetDuration()).Returns(5);
        var mockHolidayPeriod2 = new Mock<IHolidayPeriod>();
        mockHolidayPeriod2.Setup(h => h.GetDuration()).Returns(4);

        mockAssociationRepo
            .Setup(repo => repo.FindAllByProjectAsync(It.IsAny<long>()))
            .ReturnsAsync(new List<IAssociationProjectCollaborator> { mockAssociation1.Object, mockAssociation2.Object });

        mockHolidayPlanRepo
            .Setup(repo => repo.FindAllHolidayPeriodsForAllCollaboratorsBetweenDatesAsync(collabIdList, period))
            .ReturnsAsync(new List<IHolidayPeriod> {
                mockHolidayPeriod1.Object,
                mockHolidayPeriod2.Object
            });

        mockHolidayPeriod1.Setup(hp => hp.GetDuration()).Returns(6);
        mockHolidayPeriod2.Setup(hp => hp.GetDuration()).Returns(3);

        var service = new HolidayPlanService(mockAssociationRepo.Object, mockHolidayPlanRepo.Object);

        // Act
        var totalHolidayDays = await service.GetHolidayDaysForProjectAllCollaboratorBetweenDates(It.IsAny<long>(), period);

        // Assert
        Assert.Equal(9, totalHolidayDays);
    }

    [Fact]
    public async Task GetHolidayDaysForProjectWithNoAssociations_ReturnsZeroDays()
    {
        // Arrange
        var mockAssociationRepo = new Mock<IAssociationProjectCollaboratorRepository>();
        var mockHolidayPlanRepo = new Mock<IHolidayPlanRepository>();
        var period = new PeriodDate(It.IsAny<DateOnly>(), It.IsAny<DateOnly>());

        mockAssociationRepo
            .Setup(repo => repo.FindAllByProjectAsync(It.IsAny<long>()))
            .ReturnsAsync(new List<IAssociationProjectCollaborator>());

        var service = new HolidayPlanService(mockAssociationRepo.Object, mockHolidayPlanRepo.Object);

        // Act
        var totalHolidayDays = await service.GetHolidayDaysForProjectAllCollaboratorBetweenDates(It.IsAny<long>(), period);

        // Assert
        Assert.Equal(0, totalHolidayDays);
    }

    [Fact]
    public async Task GetHolidayDaysForHolidayPlanWithNoPeriods_ReturnsZeroDays()
    {
        // Arrange
        var mockAssociationRepo = new Mock<IAssociationProjectCollaboratorRepository>();
        var mockHolidayPlanRepo = new Mock<IHolidayPlanRepository>();

        var collabIdList = new List<long>() { 1, 2 };

        var period = new PeriodDate(It.IsAny<DateOnly>(), It.IsAny<DateOnly>());

        var mockAssociation = new Mock<IAssociationProjectCollaborator>();
        mockAssociation.Setup(a => a.GetCollaboratorId()).Returns(1);

        var mockHolidayPeriod = new Mock<IHolidayPeriod>();
        mockHolidayPeriod.Setup(h => h.GetDuration()).Returns(0);

        mockAssociationRepo
            .Setup(repo => repo.FindAllByProjectAsync(It.IsAny<long>()))
            .ReturnsAsync(new List<IAssociationProjectCollaborator> { mockAssociation.Object });

        mockHolidayPlanRepo
            .Setup(repo => repo.FindAllHolidayPeriodsForAllCollaboratorsBetweenDatesAsync(collabIdList, period))
            .ReturnsAsync(new List<IHolidayPeriod>() { mockHolidayPeriod.Object });

        var service = new HolidayPlanService(mockAssociationRepo.Object, mockHolidayPlanRepo.Object);

        // Act
        var totalHolidayDays = await service.GetHolidayDaysForProjectAllCollaboratorBetweenDates(It.IsAny<long>(), period);

        // Assert
        Assert.Equal(0, totalHolidayDays);
    }
}
 */