using Domain.Interfaces;
using Domain.IRepository;
using Application.Services;
using Moq;
namespace Application.Tests.HolidayPlanServiceTests;

public class GetHolidayDaysForProjectAllCollaboratorBetwenDates
{
    [Fact]
    public void GetHolidayDaysForProjectCollaboratorBetweenDates_ReturnsCorrectDays()
    {
        // Arrange
        long projectId = 1;
        var mockAssociationRepo = new Mock<IAssociationProjectCollaboratorRepository>();
        var mockHolidayPlanRepo = new Mock<IHolidayPlanRepository>();
        long collaborador1Id = 1;
        long collaborador2Id = 1;
        var mockCollab2 = new Mock<ICollaborator>();
        var collabList = new List<long>() { collaborador1Id, collaborador2Id };
        var periodDouble = new Mock<IPeriodDate>();

        var mockAssociation1 = new Mock<IAssociationProjectCollaborator>();
        var mockAssociation2 = new Mock<IAssociationProjectCollaborator>();
        var mockHolidayPeriod1 = new Mock<IHolidayPeriod>();
        var mockHolidayPeriod2 = new Mock<IHolidayPeriod>();

        mockAssociationRepo
            .Setup(repo => repo.FindAllByProject(projectId))
            .Returns(new List<IAssociationProjectCollaborator> { mockAssociation1.Object, mockAssociation2.Object });

        mockAssociation1.Setup(a => a.GetCollaboratorId()).Returns(collaborador1Id);
        mockAssociation2.Setup(a => a.GetCollaboratorId()).Returns(collaborador2Id);

        mockHolidayPlanRepo
            .Setup(repo => repo.FindAllHolidayPeriodsForAllCollaboratorsBetweenDates(collabList, periodDouble.Object))
            .Returns(new List<IHolidayPeriod> {
                mockHolidayPeriod1.Object,
                mockHolidayPeriod2.Object
            });

        mockHolidayPeriod1.Setup(hp => hp.GetDuration()).Returns(6);
        mockHolidayPeriod2.Setup(hp => hp.GetDuration()).Returns(3);

        var service = new HolidayPlanService(mockAssociationRepo.Object, mockHolidayPlanRepo.Object);

        // Act
        var totalHolidayDays = service.GetHolidayDaysForProjectAllCollaboratorBetweenDates(projectId, periodDouble.Object);

        // Assert
        Assert.Equal(9, totalHolidayDays);
    }

    [Fact]
    public void GetHolidayDaysForProjectWithNoAssociations_ReturnsZeroDays()
    {
        // Arrange
        long projectId = 1;
        var mockAssociationRepo = new Mock<IAssociationProjectCollaboratorRepository>();
        var mockHolidayPlanRepo = new Mock<IHolidayPlanRepository>();
        var periodDouble = new Mock<IPeriodDate>();

        mockAssociationRepo
            .Setup(repo => repo.FindAllByProject(projectId))
            .Returns(new List<IAssociationProjectCollaborator>());

        var service = new HolidayPlanService(mockAssociationRepo.Object, mockHolidayPlanRepo.Object);

        // Act
        var totalHolidayDays = service.GetHolidayDaysForProjectAllCollaboratorBetweenDates(projectId, periodDouble.Object);

        // Assert
        Assert.Equal(0, totalHolidayDays);
    }

    [Fact]
    public void GetHolidayDaysForHolidayPlanWithNoPeriods_ReturnsZeroDays()
    {
        // Arrange
        long projectId = 1;
        var mockAssociationRepo = new Mock<IAssociationProjectCollaboratorRepository>();
        var mockHolidayPlanRepo = new Mock<IHolidayPlanRepository>();
        long collaboradorId = 1;
        var collabList = new List<long>() { collaboradorId };
        var periodDouble = new Mock<IPeriodDate>();

        var mockAssociation = new Mock<IAssociationProjectCollaborator>();

        mockAssociationRepo
            .Setup(repo => repo.FindAllByProject(projectId))
            .Returns(new List<IAssociationProjectCollaborator> { mockAssociation.Object });

        mockAssociation.Setup(a => a.GetCollaboratorId()).Returns(collaboradorId);

        mockHolidayPlanRepo
            .Setup(repo => repo.FindAllHolidayPeriodsForAllCollaboratorsBetweenDates(collabList, periodDouble.Object))
            .Returns(new List<IHolidayPeriod>());

        var service = new HolidayPlanService(mockAssociationRepo.Object, mockHolidayPlanRepo.Object);

        // Act
        var totalHolidayDays = service.GetHolidayDaysForProjectAllCollaboratorBetweenDates(projectId, periodDouble.Object);

        // Assert
        Assert.Equal(0, totalHolidayDays);
    }
}
