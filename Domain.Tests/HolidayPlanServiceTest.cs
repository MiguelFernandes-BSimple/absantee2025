using System;
using System.Collections.Generic;
using Moq;
using Xunit;

namespace Domain.Tests
{
    public class HolidayPlanServiceTests
    {

    private readonly Mock<IAssociationProjectCollaboratorRepository> _mockAssociationRepo;
    private readonly Mock<IHolidayPlanRepository> _mockHolidayRepo;
    private readonly HolidayPlanService _service;

    public HolidayPlanServiceTests()
    {
        _mockAssociationRepo = new Mock<IAssociationProjectCollaboratorRepository>();
        _mockHolidayRepo = new Mock<IHolidayPlanRepository>();
        _service = new HolidayPlanService(_mockAssociationRepo.Object, _mockHolidayRepo.Object);
    }

    [Fact]
    public void When_InitDateIsGreaterThanEndDate_Then_ReturnsZero()
    {
        // Arrange
        var project = new Mock<IProject>().Object;
        var initDate = new DateOnly(2024, 12, 31);
        var endDate = new DateOnly(2024, 1, 1);

        // Act
        var result = _service.GetHolidayDaysForProjectCollaboratorBetweenDates(project, initDate, endDate);

        // Assert
        Assert.Equal(0, result);
    }

    [Fact]
    public void When_RepositoriesAreNull_Then_ReturnsZero()
    {
        // Arrange
        var project = new Mock<IProject>().Object;
        var service = new HolidayPlanService(null, null);
        var initDate = new DateOnly(2024, 1, 1);
        var endDate = new DateOnly(2024, 1, 10);

        // Act
        var result = service.GetHolidayDaysForProjectCollaboratorBetweenDates(project, initDate, endDate);

        // Assert
        Assert.Equal(0, result);
    }

    [Fact]
    public void When_NoCollaboratorsInProject_Then_ReturnsZero()
    {
        // Arrange
        var project = new Mock<IProject>().Object;
        _mockAssociationRepo.Setup(repo => repo.FindAllProjectCollaborators(project))
            .Returns(new List<ICollaborator>());

        // Act
        var result = _service.GetHolidayDaysForProjectCollaboratorBetweenDates(project, new DateOnly(2024, 1, 1), new DateOnly(2024, 1, 10));

        // Assert
        Assert.Equal(0, result);
    }

    [Fact]
    public void When_CollaboratorsHaveNoHolidayPlans_Then_ReturnsZero()
    {
        // Arrange
        var project = new Mock<IProject>().Object;
        var collaborator = new Mock<ICollaborator>().Object;

        _mockAssociationRepo.Setup(repo => repo.FindAllProjectCollaborators(project))
            .Returns(new List<ICollaborator> { collaborator });

        _mockHolidayRepo.Setup(repo => repo.GetHolidayPlansByCollaborator(collaborator))
            .Returns(new List<IHolidayPlan>());

        // Act
        var result = _service.GetHolidayDaysForProjectCollaboratorBetweenDates(project, new DateOnly(2024, 1, 1), new DateOnly(2024, 1, 10));

        // Assert
        Assert.Equal(0, result);
    }

    [Fact]
    public void When_HolidayPeriodIsCompletelyOutsideDateRange_Then_ReturnsZero()
    {
        // Arrange
        var project = new Mock<IProject>().Object;
        var collaborator = new Mock<ICollaborator>().Object;
        var holidayPeriod = new Mock<IHolidayPeriod>();

        holidayPeriod.Setup(h => h.GetInitDate()).Returns(new DateOnly(2023, 12, 1));
        holidayPeriod.Setup(h => h.GetFinalDate()).Returns(new DateOnly(2023, 12, 10));

        var holidayPlan = new Mock<IHolidayPlan>();
        holidayPlan.Setup(h => h.GetHolidayPeriods()).Returns(new List<IHolidayPeriod> { holidayPeriod.Object });

        _mockAssociationRepo.Setup(repo => repo.FindAllProjectCollaborators(project))
            .Returns(new List<ICollaborator> { collaborator });

        _mockHolidayRepo.Setup(repo => repo.GetHolidayPlansByCollaborator(collaborator))
            .Returns(new List<IHolidayPlan> { holidayPlan.Object });

        // Act
        var result = _service.GetHolidayDaysForProjectCollaboratorBetweenDates(project, new DateOnly(2024, 1, 1), new DateOnly(2024, 1, 10));

        // Assert
        Assert.Equal(0, result);
    }

    [Fact]
    public void When_HolidayPeriodIsPartiallyWithinRange_Then_CalculatesCorrectDays()
    {
        // Arrange
        var project = new Mock<IProject>().Object;
        var collaborator = new Mock<ICollaborator>().Object;
        var holidayPeriod = new Mock<IHolidayPeriod>();

        holidayPeriod.Setup(h => h.GetInitDate()).Returns(new DateOnly(2023, 12, 30));
        holidayPeriod.Setup(h => h.GetFinalDate()).Returns(new DateOnly(2024, 1, 5));

        var holidayPlan = new Mock<IHolidayPlan>();
        holidayPlan.Setup(h => h.GetHolidayPeriods()).Returns(new List<IHolidayPeriod> { holidayPeriod.Object });

        _mockAssociationRepo.Setup(repo => repo.FindAllProjectCollaborators(project))
            .Returns(new List<ICollaborator> { collaborator });

        _mockHolidayRepo.Setup(repo => repo.GetHolidayPlansByCollaborator(collaborator))
            .Returns(new List<IHolidayPlan> { holidayPlan.Object });

        // Act
        var result = _service.GetHolidayDaysForProjectCollaboratorBetweenDates(project, new DateOnly(2024, 1, 1), new DateOnly(2024, 1, 10));

        // Assert
        Assert.Equal(5, result); // De 01/01 a 05/01
    }

    [Fact]
    public void When_HolidayPeriodIsFullyWithinRange_Then_SumsAllDays()
    {
        // Arrange
        var project = new Mock<IProject>().Object;
        var collaborator = new Mock<ICollaborator>().Object;
        var holidayPeriod = new Mock<IHolidayPeriod>();

        holidayPeriod.Setup(h => h.GetInitDate()).Returns(new DateOnly(2024, 1, 2));
        holidayPeriod.Setup(h => h.GetFinalDate()).Returns(new DateOnly(2024, 1, 8));

        var holidayPlan = new Mock<IHolidayPlan>();
        holidayPlan.Setup(h => h.GetHolidayPeriods()).Returns(new List<IHolidayPeriod> { holidayPeriod.Object });

        _mockAssociationRepo.Setup(repo => repo.FindAllProjectCollaborators(project))
            .Returns(new List<ICollaborator> { collaborator });

        _mockHolidayRepo.Setup(repo => repo.GetHolidayPlansByCollaborator(collaborator))
            .Returns(new List<IHolidayPlan> { holidayPlan.Object });

        // Act
        var result = _service.GetHolidayDaysForProjectCollaboratorBetweenDates(project, new DateOnly(2024, 1, 1), new DateOnly(2024, 1, 10));

        // Assert
        Assert.Equal(7, result); // De 02/01 a 08/01
    }


        [Fact]
        public void WhentHolidayDaysForProjectCollaboratorBetweenDates_ThenReturnCorrectDays()
        {
            // Arrange
            var mockAssociationRepo = new Mock<IAssociationProjectCollaboratorRepository>();
            var mockHolidayRepo = new Mock<IHolidayPlanRepository>();
            var project = Mock.Of<IProject>();
            var collaborator = Mock.Of<ICollaborator>();
            var holidayPlan = Mock.Of<IHolidayPlan>();
            var holidayPeriod = new Mock<IHolidayPeriod>();
            
            DateOnly initDate = new DateOnly(2024, 6, 1);
            DateOnly endDate = new DateOnly(2024, 6, 10);
            
            holidayPeriod.Setup(h => h.GetInitDate()).Returns(new DateOnly(2024, 6, 3));
            holidayPeriod.Setup(h => h.GetFinalDate()).Returns(new DateOnly(2024, 6, 8));
            holidayPeriod.Setup(h => h.GetDurationInDays(It.IsAny<DateOnly>(), It.IsAny<DateOnly>())).Returns(6);
            
            Mock.Get(holidayPlan).Setup(h => h.GetHolidayPeriods()).Returns(new List<IHolidayPeriod> { holidayPeriod.Object });
            mockHolidayRepo.Setup(h => h.GetHolidayPlansByCollaborator(collaborator)).Returns(new List<IHolidayPlan> { holidayPlan });
            mockAssociationRepo.Setup(a => a.FindAllProjectCollaborators(project)).Returns(new List<ICollaborator> { collaborator });
            
            var service = new HolidayPlanService(mockAssociationRepo.Object, mockHolidayRepo.Object);
            
            // Act
            int totalHolidayDays = service.GetHolidayDaysForProjectCollaboratorBetweenDates(project, initDate, endDate);
            
            // Assert
            Assert.Equal(6, totalHolidayDays);
        }

        [Fact]
        public void GivenHolidayDaysForProjectCollaboratorBetweenDates_WhenReturnZero_ThenNoCollaborators()
        {
            // Arrange
            var mockAssociationRepo = new Mock<IAssociationProjectCollaboratorRepository>();
            var mockHolidayRepo = new Mock<IHolidayPlanRepository>();
            var project = Mock.Of<IProject>();
            
            DateOnly initDate = new DateOnly(2024, 6, 1);
            DateOnly endDate = new DateOnly(2024, 6, 10);
            
            mockAssociationRepo.Setup(a => a.FindAllProjectCollaborators(project)).Returns(new List<ICollaborator>());
            
            var service = new HolidayPlanService(mockAssociationRepo.Object, mockHolidayRepo.Object);
            
            // Act
            int totalHolidayDays = service.GetHolidayDaysForProjectCollaboratorBetweenDates(project, initDate, endDate);
            
            // Assert
            Assert.Equal(0, totalHolidayDays);
        }

        [Fact]
        public void GivenHolidayDaysForProjectCollaboratorBetweenDates_WhenReturnZero_ThenDatesAreInvalid()
        {
            // Arrange
            var mockAssociationRepo = new Mock<IAssociationProjectCollaboratorRepository>();
            var mockHolidayRepo = new Mock<IHolidayPlanRepository>();
            var project = Mock.Of<IProject>();
            
            DateOnly initDate = new DateOnly(2024, 6, 10);
            DateOnly endDate = new DateOnly(2024, 6, 1);
            
            var service = new HolidayPlanService(mockAssociationRepo.Object, mockHolidayRepo.Object);
            
            // Act
            int totalHolidayDays = service.GetHolidayDaysForProjectCollaboratorBetweenDates(project, initDate, endDate);
            
            // Assert
            Assert.Equal(0, totalHolidayDays);
        }
    }
}
