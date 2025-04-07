using Domain.Interfaces;
using Domain.IRepository;
using Application.Services;
using Moq;
using Domain.IRepository;
namespace Application.Tests.HolidayPlanServiceTests;

public class GetHolidayDaysForProjectAllCollaboratorBetwenDates
{
    [Fact]
    public void GetHolidayDaysForProjectCollaboratorBetweenDates_ReturnsCorrectDays()
    {
        // Arrange
        var mockProject = new Mock<IProject>();
        var mockAssociationRepo = new Mock<IAssociationProjectCollaboratorRepository>();
        var mockHolidayPlanRepo = new Mock<IHolidayPlanRepository>();
        var mockCollab1 = new Mock<ICollaborator>();
        var mockCollab2 = new Mock<ICollaborator>();
        var collabList = new List<ICollaborator>() { mockCollab1.Object, mockCollab2.Object };
        var periodDouble = new Mock<IPeriodDate>();

        var mockAssociation1 = new Mock<IAssociationProjectCollaborator>();
        var mockAssociation2 = new Mock<IAssociationProjectCollaborator>();
        var mockHolidayPeriod1 = new Mock<IHolidayPeriod>();
        var mockHolidayPeriod2 = new Mock<IHolidayPeriod>();

        mockAssociationRepo
            .Setup(repo => repo.FindAllByProject(mockProject.Object))
            .Returns(new List<IAssociationProjectCollaborator> { mockAssociation1.Object, mockAssociation2.Object });

        mockAssociation1.Setup(a => a.GetCollaborator()).Returns(mockCollab1.Object);
        mockAssociation2.Setup(a => a.GetCollaborator()).Returns(mockCollab2.Object);

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
        var totalHolidayDays = service.GetHolidayDaysForProjectAllCollaboratorBetweenDates(mockProject.Object, periodDouble.Object);

        // Assert
        Assert.Equal(9, totalHolidayDays);
    }

    [Fact]
    public void GetHolidayDaysForProjectWithNoAssociations_ReturnsZeroDays()
    {
        // Arrange
        var mockProject = new Mock<IProject>();
        var mockAssociationRepo = new Mock<IAssociationProjectCollaboratorRepository>();
        var mockHolidayPlanRepo = new Mock<IHolidayPlanRepository>();
        var mockCollab = new Mock<ICollaborator>();
        var periodDouble = new Mock<IPeriodDate>();

        mockAssociationRepo
            .Setup(repo => repo.FindAllByProject(mockProject.Object))
            .Returns(new List<IAssociationProjectCollaborator>());

        var service = new HolidayPlanService(mockAssociationRepo.Object, mockHolidayPlanRepo.Object);

        // Act
        var totalHolidayDays = service.GetHolidayDaysForProjectAllCollaboratorBetweenDates(mockProject.Object, periodDouble.Object);

        // Assert
        Assert.Equal(0, totalHolidayDays);
    }

    [Fact]
    public void GetHolidayDaysForHolidayPlanWithNoPeriods_ReturnsZeroDays()
    {
        // Arrange
        var mockProject = new Mock<IProject>();
        var mockAssociationRepo = new Mock<IAssociationProjectCollaboratorRepository>();
        var mockHolidayPlanRepo = new Mock<IHolidayPlanRepository>();
        var mockCollab = new Mock<ICollaborator>();
        var collabList = new List<ICollaborator>() { mockCollab.Object };
        var periodDouble = new Mock<IPeriodDate>();

        var mockAssociation = new Mock<IAssociationProjectCollaborator>();

        mockAssociationRepo
            .Setup(repo => repo.FindAllByProject(mockProject.Object))
            .Returns(new List<IAssociationProjectCollaborator> { mockAssociation.Object });

        mockAssociation.Setup(a => a.GetCollaborator()).Returns(mockCollab.Object);

        mockHolidayPlanRepo
            .Setup(repo => repo.FindAllHolidayPeriodsForAllCollaboratorsBetweenDates(collabList, periodDouble.Object))
            .Returns(new List<IHolidayPeriod>());

        var service = new HolidayPlanService(mockAssociationRepo.Object, mockHolidayPlanRepo.Object);

        // Act
        var totalHolidayDays = service.GetHolidayDaysForProjectAllCollaboratorBetweenDates(mockProject.Object, periodDouble.Object);

        // Assert
        Assert.Equal(0, totalHolidayDays);
    }
}
