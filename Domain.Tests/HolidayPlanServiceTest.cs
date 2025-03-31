using System;
using System.Collections.Generic;
using Moq;
using Xunit;

namespace Domain.Tests
{
    public class HolidayPlanServiceTests
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

        var initDate = new DateOnly(2024, 6, 1);
        var endDate = new DateOnly(2024, 6, 10);

        mockHolidayPeriod.Setup(p => p.GetInitDate()).Returns(new DateOnly(2024, 6, 3));
        mockHolidayPeriod.Setup(p => p.GetFinalDate()).Returns(new DateOnly(2024, 6, 8));
        mockHolidayPeriod.Setup(p => p.GetDurationInDays(initDate, endDate)).Returns(6);

        mockHolidayPlan.Setup(hp => hp.GetHolidayPeriods()).Returns(new List<IHolidayPeriod> { mockHolidayPeriod.Object });

        mockHolidayPlanRepo
            .Setup(repo => repo.GetHolidayPlansByAssociations(mockAssociation.Object))
            .Returns(new List<IHolidayPlan> { mockHolidayPlan.Object });

        mockAssociationRepo
            .Setup(repo => repo.FindAllByProject(mockProject.Object))
            .Returns(new List<IAssociationProjectCollaborator> { mockAssociation.Object });

        var service = new HolidayPlanService(mockAssociationRepo.Object, mockHolidayPlanRepo.Object);

        // Act
        var totalHolidayDays = service.GetHolidayDaysForProjectCollaboratorBetweenDates(mockProject.Object, initDate, endDate);

        // Assert
        Assert.Equal(6, totalHolidayDays);
    }

    [Fact]
    public void GetHolidayDaysForProjectCollaboratorBetweenDates_ShouldReturnZero_WhenInitDateIsGreaterThanEndDate()
    {
        // Arrange
        var mockProject = new Mock<IProject>();
        var mockHolidayRepo = new Mock<IHolidayPlanRepository>();
        var mockAssociationRepo = new Mock<IAssociationProjectCollaboratorRepository>();

        var service = new HolidayPlanService(mockAssociationRepo.Object, mockHolidayRepo.Object);

        DateOnly initDate = new DateOnly(2024, 12, 31); // Data inicial maior
        DateOnly endDate = new DateOnly(2024, 01, 01);  // Data final menor

        // Act
        int result = service.GetHolidayDaysForProjectCollaboratorBetweenDates(mockProject.Object, initDate, endDate);

        // Assert
        Assert.Equal(0, result);
    }
    }

}
