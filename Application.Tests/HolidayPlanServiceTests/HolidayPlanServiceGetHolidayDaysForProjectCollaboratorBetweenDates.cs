using Domain.Interfaces;
using Domain.IRepository;
using Application.Services;
using Moq;
namespace Application.Tests.HolidayPlanServiceTests;

public class HolidayPlanServiceGetHolidayDaysForProjectCollaboratorBetweenDates
{
    [Fact]
    public async Task WhenNoAssociationForCollaborator_ThenThrowException()
    {
        // Arrangew
        var associationRepoMock = new Mock<IAssociationProjectCollaboratorRepository>();
        associationRepoMock.Setup(a => a.FindByProjectAndCollaboratorAsync(It.IsAny<long>(), It.IsAny<long>())).ReturnsAsync((IAssociationProjectCollaborator?)null);

        var holidayPlanService = new HolidayPlanService(associationRepoMock.Object, Mock.Of<IHolidayPlanRepository>());

        // Act & Assert
        var exception = await Assert.ThrowsAsync<Exception>(() => holidayPlanService.GetHolidayDaysForProjectCollaboratorBetweenDates(It.IsAny<long>(), It.IsAny<long>(), It.IsAny<IPeriodDate>()));

        Assert.Equal("No association found for the project and collaborator", exception.Message);
    }

    [Fact]
    public async Task WhenGettingHolidayDaysForProjectCollaboratorBetwennDates_ThenThrowsHolidayException()
    {
        //Arrange
        var periodDateMock = new Mock<IPeriodDate>();
        var associationRepoMock = new Mock<IAssociationProjectCollaboratorRepository>();
        var holidayPlanRepoMock = new Mock<IHolidayPlanRepository>();

        associationRepoMock.Setup(a => a.FindByProjectAndCollaboratorAsync(It.IsAny<long>(), It.IsAny<long>())).ReturnsAsync(Mock.Of<IAssociationProjectCollaborator>());

        holidayPlanRepoMock.Setup(h => h.FindHolidayPeriodsByCollaboratorBetweenDatesAsync(It.IsAny<long>(), It.IsAny<IPeriodDate>())).ReturnsAsync(new List<IHolidayPeriod>());

        var holidayPlanService = new HolidayPlanService(associationRepoMock.Object, holidayPlanRepoMock.Object);

        // Act
        var result = await holidayPlanService.GetHolidayDaysForProjectCollaboratorBetweenDates(It.IsAny<long>(), It.IsAny<long>(), periodDateMock.Object);


        //Assert
        Assert.Equal(0, result);

    }

    [Fact]
    public async Task WhenGettingHolidayDaysForProjectCollaboratorBetweenDates_ThenReturnsCorrectHolidayDays()
    {
        // Arrange
        var expectedHolidayDays = 5;
        var collaboratorMock = new Mock<ICollaborator>();
        collaboratorMock.Setup(c => c.ContractContainsDates(It.IsAny<IPeriodDateTime>())).Returns(true);

        var projectMock = new Mock<IProject>();

        var associationRepoMock = new Mock<IAssociationProjectCollaboratorRepository>();
        var associationMock = new Mock<IAssociationProjectCollaborator>();
        associationRepoMock.Setup(a => a.FindByProjectAndCollaboratorAsync(It.IsAny<long>(), It.IsAny<long>())).ReturnsAsync(associationMock.Object);

        var holidayPeriodDouble = new Mock<IPeriodDate>();
        var periodDouble = new Mock<IPeriodDate>();

        var holidayPeriodMock = new Mock<IHolidayPeriod>();
        holidayPeriodMock.Setup(hp => hp.GetPeriodDate()).Returns(holidayPeriodDouble.Object);

        var holidayPeriodList = new List<IHolidayPeriod>() { holidayPeriodMock.Object };

        var holidayRepoMock = new Mock<IHolidayPlanRepository>();
        holidayRepoMock.Setup(hr => hr.FindHolidayPeriodsByCollaboratorBetweenDatesAsync(It.IsAny<long>(), periodDouble.Object)).ReturnsAsync(holidayPeriodList);

        holidayPeriodMock.Setup(hp => hp.GetNumberOfCommonUtilDays()).Returns(expectedHolidayDays);

        var holidayPlanService = new HolidayPlanService(associationRepoMock.Object, holidayRepoMock.Object);

        // Act
        var result = await holidayPlanService.GetHolidayDaysForProjectCollaboratorBetweenDates(It.IsAny<long>(), It.IsAny<long>(), periodDouble.Object);

        // Assert
        Assert.Equal(expectedHolidayDays, result);
    }

    [Fact]
    public async Task WhenInitDateIsGreaterThanFinalDate_ThenReturnsZero()
    {
        // Arrange
        var associationRepoMock = new Mock<IAssociationProjectCollaboratorRepository>();
        var associationMock = new Mock<IAssociationProjectCollaborator>();

        associationRepoMock.Setup(a => a.FindByProjectAndCollaboratorAsync(It.IsAny<long>(), It.IsAny<long>())).ReturnsAsync(associationMock.Object);

        var periodDouble = new Mock<IPeriodDate>();

        var holidayRepoMock = new Mock<IHolidayPlanRepository>(); holidayRepoMock.Setup(hr => hr.FindHolidayPeriodsByCollaboratorBetweenDatesAsync(It.IsAny<long>(), periodDouble.Object)).ReturnsAsync(new List<IHolidayPeriod>());

        var holidayPlanService = new HolidayPlanService(associationRepoMock.Object, holidayRepoMock.Object);

        // Act
        var result = await holidayPlanService.GetHolidayDaysForProjectCollaboratorBetweenDates(It.IsAny<long>(), It.IsAny<long>(), periodDouble.Object);

        // Assert
        Assert.Equal(0, result);
    }
}