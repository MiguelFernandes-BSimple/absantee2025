using Domain.Interfaces;
using Infrastructure.Interfaces;
using Application.Services;
using Moq;
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

        var mockAssociation = new Mock<IAssociationProjectCollaborator>();
        var mockHolidayPlan = new Mock<IHolidayPlan>();
        var mockHolidayPeriod = new Mock<IHolidayPeriod>();

        mockAssociationRepo
            .Setup(repo => repo.FindAllByProject(mockProject.Object))
            .Returns(new List<IAssociationProjectCollaborator> { mockAssociation.Object });

        mockHolidayPlanRepo
            .Setup(repo => repo.GetHolidayPlansByAssociations(mockAssociation.Object))
            .Returns(new List<IHolidayPlan> { mockHolidayPlan.Object });

        var periodDouble = new Mock<IPeriodDate>();
        mockHolidayPlan.Setup(hp => hp.GetHolidayPeriods()).Returns(new List<IHolidayPeriod> { mockHolidayPeriod.Object });
        mockHolidayPeriod.Setup(hperiod => hperiod.Intersects(periodDouble.Object)).Returns(true);

        mockHolidayPeriod.Setup(hp => hp.GetDuration()).Returns(6);

        var service = new HolidayPlanService(mockAssociationRepo.Object, mockHolidayPlanRepo.Object);

        // Act
        var totalHolidayDays = service.GetHolidayDaysForProjectAllCollaboratorBetweenDates(mockProject.Object, periodDouble.Object);

        // Assert
        Assert.Equal(6, totalHolidayDays);
    }
}
