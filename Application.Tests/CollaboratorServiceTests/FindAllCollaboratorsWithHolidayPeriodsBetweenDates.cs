using Moq;
using Domain.Interfaces;
using Infrastructure.Interfaces;
using Application.Services;

namespace Application.Tests.CollaboratorServiceTests;

public class FindAllCollaboratorsWithHolidayPeriodsBetweenDates
{
    [Fact]
    public void WhenCollaboratorHasHolidayPeriodWithinDateRange_ThenReturnsCollaborator()
    {
        // Arrange
        var collaborator = new Mock<ICollaborator>();

        var periodDate = new Mock<IPeriodDate>();

        var holidayPlan = new Mock<IHolidayPlan>();
        holidayPlan.Setup(hp => hp.GetCollaborator()).Returns(collaborator.Object);

        var hpRepoMock = new Mock<IHolidayPlanRepository>();
        hpRepoMock.Setup(hp => hp.GetHolidayPlansWithHolidayPeriodValid(periodDate.Object)).Returns(new List<IHolidayPlan> { holidayPlan.Object });

        var APCRepo = new Mock<IAssociationProjectCollaboratorRepository>();
        var colabService = new CollaboratorService(APCRepo.Object, hpRepoMock.Object);
        // Act
        var result = colabService.FindAllCollaboratorsWithHolidayPeriodsBetweenDates(periodDate.Object);

        // Assert
        Assert.Single(result);
        Assert.Contains(collaborator.Object, result);
    }


    [Fact]
    public void WhenMultipleCollaboratorsHaveHolidayPeriodsWithinDateRange_ThenReturnsAllCollaborators()
    {
        // Arrange
        var collaborator1 = new Mock<ICollaborator>();
        var collaborator2 = new Mock<ICollaborator>();
        
        var periodDate = new Mock<IPeriodDate>();

        var holidayPlan1 = new Mock<IHolidayPlan>();
        holidayPlan1.Setup(hp => hp.GetCollaborator()).Returns(collaborator1.Object);

        var holidayPlan2 = new Mock<IHolidayPlan>();
        holidayPlan2.Setup(hp => hp.GetCollaborator()).Returns(collaborator2.Object);

        var hpRepoMock = new Mock<IHolidayPlanRepository>();
        hpRepoMock.Setup(hp => hp.GetHolidayPlansWithHolidayPeriodValid(periodDate.Object)).Returns(new List<IHolidayPlan> { holidayPlan1.Object, holidayPlan2.Object });

        var APCRepo = new Mock<IAssociationProjectCollaboratorRepository>();
        var colabService = new CollaboratorService(APCRepo.Object, hpRepoMock.Object);

        // Act
        var result = colabService.FindAllCollaboratorsWithHolidayPeriodsBetweenDates(periodDate.Object);

        // Assert
        Assert.Equal(2, result.Count());
        Assert.Contains(collaborator1.Object, result);
        Assert.Contains(collaborator2.Object, result);
    }

    [Fact]
    public void WhenCollaboratorHasPeriodsBothInsideAndOutsideRange_ThenReturnsCollaborator()
    {
        // Arrange
        var collaborator = new Mock<ICollaborator>();

        var periodDate = new Mock<IPeriodDate>();

        var holidayPlan = new Mock<IHolidayPlan>();
        holidayPlan.Setup(hp => hp.GetCollaborator()).Returns(collaborator.Object);

        var hpRepoMock = new Mock<IHolidayPlanRepository>();
        hpRepoMock.Setup(hp => hp.GetHolidayPlansWithHolidayPeriodValid(periodDate.Object)).Returns(new List<IHolidayPlan> { holidayPlan.Object });

        var APCRepo = new Mock<IAssociationProjectCollaboratorRepository>();
        var colabService = new CollaboratorService(APCRepo.Object, hpRepoMock.Object);

        // Act
        var result = colabService.FindAllCollaboratorsWithHolidayPeriodsBetweenDates(periodDate.Object);

        // Assert
        Assert.Single(result);
        Assert.Contains(collaborator.Object, result);
    }

    [Fact]
    public void WhenNoCollaboratorsHaveHolidayPeriodsInDateRange_ThenReturnsEmptyList()
    {
        // Arrange
        var collaborator = new Mock<ICollaborator>();
        var initDate = new DateOnly(2025, 7, 15);
        var endDate = new DateOnly(2025, 8, 1);
        
        var periodDate = new Mock<IPeriodDate>();

        var holidayPeriod = new Mock<IHolidayPeriod>();

        var holidayPlan = new Mock<IHolidayPlan>();
        holidayPlan.Setup(hp => hp.HasCollaborator(collaborator.Object)).Returns(true);
        holidayPlan
            .Setup(hp => hp.GetHolidayPeriods())
            .Returns(new List<IHolidayPeriod> { holidayPeriod.Object });
        holidayPlan.Setup(hp => hp.GetCollaborator()).Returns(collaborator.Object);

        var hpRepoMock = new Mock<IHolidayPlanRepository>();
        hpRepoMock.Setup(hp => hp.GetHolidayPlansWithHolidayPeriodValid(periodDate.Object)).Returns(new List<IHolidayPlan>());
        var APCRepo = new Mock<IAssociationProjectCollaboratorRepository>();

        var colabService = new CollaboratorService(APCRepo.Object, hpRepoMock.Object);

        // Act
        var result = colabService.FindAllCollaboratorsWithHolidayPeriodsBetweenDates(periodDate.Object);

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public void WhenPassingInitDateBiggerThanEndDate_ThenReturnsEmptyList()
    {
        // Arrange
        var periodDate = new Mock<IPeriodDate>();

        var hpRepoMock = new Mock<IHolidayPlanRepository>();
        hpRepoMock.Setup(hp => hp.GetHolidayPlansWithHolidayPeriodValid(periodDate.Object)).Returns(new List<IHolidayPlan>());
        var APCRepo = new Mock<IAssociationProjectCollaboratorRepository>();
        var colabService = new CollaboratorService(APCRepo.Object, hpRepoMock.Object);
        // Act
        var result = colabService.FindAllCollaboratorsWithHolidayPeriodsBetweenDates(periodDate.Object);

        // Assert
        Assert.Empty(result);
    }
}

