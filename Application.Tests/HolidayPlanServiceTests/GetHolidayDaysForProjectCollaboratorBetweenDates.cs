using Domain.Interfaces;
using Domain.IRepository;
using Application.Services;
using Moq;
namespace Application.Tests.HolidayPlanServiceTests;

public class GetHolidayDaysForProjectCollaboratorBetweenDates
{
    [Fact]
    public void WhenNoAssociationForCollaborator_ThenThrowException()
    {
        // Arrange
        var projectMock = new Mock<IProject>();
        var collaboratorMock = new Mock<ICollaborator>();

        var associationRepoMock = new Mock<IAssociationProjectCollaboratorRepository>();
        associationRepoMock.Setup(a => a.FindByProjectAndCollaborator(It.IsAny<IProject>(), It.IsAny<ICollaborator>())).Returns((IAssociationProjectCollaborator?)null);

        var holidayPlanService = new HolidayPlanService(associationRepoMock.Object, Mock.Of<IHolidayPlanRepository>());

        // Act & Assert
        var exception = Assert.Throws<Exception>(() => holidayPlanService.GetHolidayDaysForProjectCollaboratorBetweenDates(projectMock.Object, collaboratorMock.Object, It.IsAny<IPeriodDate>()));

        Assert.Equal("No association found for the project and collaborator", exception.Message);
    }

    [Fact]
    public void WhenGettingHolidayDaysForProjectCollaboratorBetwennDates_ThenThrowsHolidayException()
    {
        //Arrange

        var collaboratorMock = new Mock<ICollaborator>();
        var projectMock = new Mock<IProject>();
        var periodDateMock = new Mock<IPeriodDate>();
        var associationRepoMock = new Mock<IAssociationProjectCollaboratorRepository>();
        var holidayPlanRepoMock = new Mock<IHolidayPlanRepository>();

        associationRepoMock.Setup(a => a.FindByProjectAndCollaborator(It.IsAny<IProject>(), It.IsAny<ICollaborator>())).Returns(Mock.Of<IAssociationProjectCollaborator>());

        holidayPlanRepoMock.Setup(h => h.FindHolidayPeriodsByCollaboratorBetweenDates(It.IsAny<ICollaborator>(), It.IsAny<IPeriodDate>())).Returns(new List<IHolidayPeriod>());

        var holidayPlanService = new HolidayPlanService(associationRepoMock.Object, holidayPlanRepoMock.Object);

        // Act
        var result = holidayPlanService.GetHolidayDaysForProjectCollaboratorBetweenDates(projectMock.Object, collaboratorMock.Object, periodDateMock.Object);


        //Assert
        Assert.Equal(0, result);

    }

    [Fact]
    public void WhenGettingHolidayDaysForProjectCollaboratorBetweenDates_ThenReturnsCorrectHolidayDays()
    {
        // Arrange
        var expectedHolidayDays = 5;
        var collaboratorMock = new Mock<ICollaborator>();
        collaboratorMock.Setup(c => c.ContractContainsDates(It.IsAny<IPeriodDateTime>())).Returns(true);

        var projectMock = new Mock<IProject>();

        var associationRepoMock = new Mock<IAssociationProjectCollaboratorRepository>();
        var associationMock = new Mock<IAssociationProjectCollaborator>();
        associationRepoMock.Setup(a => a.FindByProjectAndCollaborator(projectMock.Object, collaboratorMock.Object)).Returns(associationMock.Object);

        var holidayPeriodDouble = new Mock<IPeriodDate>();
        var periodDouble = new Mock<IPeriodDate>();

        var holidayPeriodMock = new Mock<IHolidayPeriod>();
        holidayPeriodMock.Setup(hp => hp.GetPeriodDate()).Returns(holidayPeriodDouble.Object);

        var holidayPeriodList = new List<IHolidayPeriod>() { holidayPeriodMock.Object };

        var holidayRepoMock = new Mock<IHolidayPlanRepository>();
        holidayRepoMock.Setup(hr => hr.FindHolidayPeriodsByCollaboratorBetweenDates(collaboratorMock.Object, periodDouble.Object)).Returns(holidayPeriodList);

        holidayPeriodMock.Setup(hp => hp.GetNumberOfCommonUtilDays()).Returns(expectedHolidayDays);

        var holidayPlanService = new HolidayPlanService(associationRepoMock.Object, holidayRepoMock.Object);

        // Act
        var result = holidayPlanService.GetHolidayDaysForProjectCollaboratorBetweenDates(projectMock.Object, collaboratorMock.Object, periodDouble.Object);

        // Assert
        Assert.Equal(expectedHolidayDays, result);
    }

    [Fact]
    public void WhenInitDateIsGreaterThanFinalDate_ThenReturnsZero()
    {
        // Arrange
        var collaboratorMock = new Mock<ICollaborator>();
        var projectMock = new Mock<IProject>();

        var associationRepoMock = new Mock<IAssociationProjectCollaboratorRepository>();
        var associationMock = new Mock<IAssociationProjectCollaborator>();

        associationRepoMock.Setup(a => a.FindByProjectAndCollaborator(projectMock.Object, collaboratorMock.Object)).Returns(associationMock.Object);

        var periodDouble = new Mock<IPeriodDate>();

        var holidayRepoMock = new Mock<IHolidayPlanRepository>(); holidayRepoMock.Setup(hr => hr.FindHolidayPeriodsByCollaboratorBetweenDates(collaboratorMock.Object, periodDouble.Object)).Returns(new List<IHolidayPeriod>());

        var holidayPlanService = new HolidayPlanService(associationRepoMock.Object, holidayRepoMock.Object);

        // Act
        var result = holidayPlanService.GetHolidayDaysForProjectCollaboratorBetweenDates(projectMock.Object, collaboratorMock.Object, periodDouble.Object);

        // Assert
        Assert.Equal(0, result);
    }

    [Fact]
    public void WhenNoAssociationForCollaborator_ThenThrowException_Long()
    {
        // Arrange
        long projectId = 1;
        long collaboratorId = 1;

        var associationRepoMock = new Mock<IAssociationProjectCollaboratorRepository>();
        associationRepoMock.Setup(a => a.FindByProjectAndCollaborator(It.IsAny<IProject>(), It.IsAny<ICollaborator>())).Returns((IAssociationProjectCollaborator?)null);

        var holidayPlanService = new HolidayPlanService(associationRepoMock.Object, Mock.Of<IHolidayPlanRepository>());

        // Act & Assert
        var exception = Assert.Throws<Exception>(() => holidayPlanService.GetHolidayDaysForProjectCollaboratorBetweenDates(projectId, collaboratorId, It.IsAny<IPeriodDate>()));

        Assert.Equal("No association found for the project and collaborator", exception.Message);
    }

    [Fact]
    public void WhenGettingHolidayDaysForProjectCollaboratorBetwennDates_ThenThrowsHolidayException_Long()
    {
        //Arrange

        long projectId = 1;
        long collaboratorId = 1;
        var periodDateMock = new Mock<IPeriodDate>();
        var associationRepoMock = new Mock<IAssociationProjectCollaboratorRepository>();
        var holidayPlanRepoMock = new Mock<IHolidayPlanRepository>();

        associationRepoMock.Setup(a => a.FindByProjectAndCollaborator(projectId, collaboratorId)).Returns(Mock.Of<IAssociationProjectCollaborator>());

        holidayPlanRepoMock.Setup(h => h.FindHolidayPeriodsByCollaboratorBetweenDates(collaboratorId, It.IsAny<IPeriodDate>())).Returns(new List<IHolidayPeriod>());

        var holidayPlanService = new HolidayPlanService(associationRepoMock.Object, holidayPlanRepoMock.Object);

        // Act
        var result = holidayPlanService.GetHolidayDaysForProjectCollaboratorBetweenDates(projectId, collaboratorId, periodDateMock.Object);


        //Assert
        Assert.Equal(0, result);

    }

    [Fact]
    public void WhenGettingHolidayDaysForProjectCollaboratorBetweenDates_ThenReturnsCorrectHolidayDays_Long()
    {
        // Arrange
        var expectedHolidayDays = 5;
        long projectId = 1;
        long collaboratorId = 1;

        var associationRepoMock = new Mock<IAssociationProjectCollaboratorRepository>();
        var associationMock = new Mock<IAssociationProjectCollaborator>();
        associationRepoMock.Setup(a => a.FindByProjectAndCollaborator(projectId, collaboratorId)).Returns(associationMock.Object);

        var holidayPeriodDouble = new Mock<IPeriodDate>();
        var periodDouble = new Mock<IPeriodDate>();

        var holidayPeriodMock = new Mock<IHolidayPeriod>();
        holidayPeriodMock.Setup(hp => hp.GetPeriodDate()).Returns(holidayPeriodDouble.Object);

        var holidayPeriodList = new List<IHolidayPeriod>() { holidayPeriodMock.Object };

        var holidayRepoMock = new Mock<IHolidayPlanRepository>();
        holidayRepoMock.Setup(hr => hr.FindHolidayPeriodsByCollaboratorBetweenDates(collaboratorId, periodDouble.Object)).Returns(holidayPeriodList);

        holidayPeriodMock.Setup(hp => hp.GetNumberOfCommonUtilDays()).Returns(expectedHolidayDays);

        var holidayPlanService = new HolidayPlanService(associationRepoMock.Object, holidayRepoMock.Object);

        // Act
        var result = holidayPlanService.GetHolidayDaysForProjectCollaboratorBetweenDates(projectId, collaboratorId, periodDouble.Object);

        // Assert
        Assert.Equal(expectedHolidayDays, result);
    }

    [Fact]
    public void WhenInitDateIsGreaterThanFinalDate_ThenReturnsZero_Long()
    {
        // Arrange
        long projectId = 1;
        long collaboratorId = 1;

        var associationRepoMock = new Mock<IAssociationProjectCollaboratorRepository>();
        var associationMock = new Mock<IAssociationProjectCollaborator>();

        associationRepoMock.Setup(a => a.FindByProjectAndCollaborator(projectId, collaboratorId)).Returns(associationMock.Object);

        var periodDouble = new Mock<IPeriodDate>();

        var holidayRepoMock = new Mock<IHolidayPlanRepository>(); holidayRepoMock.Setup(hr => hr.FindHolidayPeriodsByCollaboratorBetweenDates(collaboratorId, periodDouble.Object)).Returns(new List<IHolidayPeriod>());

        var holidayPlanService = new HolidayPlanService(associationRepoMock.Object, holidayRepoMock.Object);

        // Act
        var result = holidayPlanService.GetHolidayDaysForProjectCollaboratorBetweenDates(projectId, collaboratorId, periodDouble.Object);

        // Assert
        Assert.Equal(0, result);
    }
}