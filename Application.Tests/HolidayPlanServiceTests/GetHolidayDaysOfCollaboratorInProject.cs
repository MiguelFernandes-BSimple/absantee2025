using Domain.Interfaces;
using Domain.IRepository;
using Application.Services;
using Moq;
using Xunit.Sdk;

namespace Application.Tests.HolidayPlanServiceTests;

public class GetHolidayDaysOfCollaboratorInProject
{
    [Fact]
    public async Task WhenCalculatingHolidayDaysOfCollaboratorInAProject_ThenReturnCorrectValue()
    {
        //arrange

        long projectId = 1;
        long collaboratorId = 1;

        Mock<IPeriodDate> periodDouble = new Mock<IPeriodDate>();

        Mock<IAssociationProjectCollaborator> associationDouble = new Mock<IAssociationProjectCollaborator>();
        associationDouble.Setup(a => a.GetPeriodDate()).Returns(periodDouble.Object);

        Mock<IHolidayPlan> holidayPlanDouble = new Mock<IHolidayPlan>();
        holidayPlanDouble.Setup(hp => hp.GetNumberOfHolidayDaysBetween(periodDouble.Object)).Returns(5);

        Mock<IHolidayPlanRepository> holidayPlanRepositoryDouble = new Mock<IHolidayPlanRepository>();
        holidayPlanRepositoryDouble.Setup(hpr => hpr.FindHolidayPlanByCollaboratorAsync(collaboratorId)).ReturnsAsync(holidayPlanDouble.Object);

        Mock<IAssociationProjectCollaboratorRepository> associationProjectCollaboratorRepository = new Mock<IAssociationProjectCollaboratorRepository>();
        associationProjectCollaboratorRepository.Setup(a => a.FindByProjectAndCollaboratorAsync(projectId, collaboratorId)).ReturnsAsync(associationDouble.Object);

        HolidayPlanService service = new HolidayPlanService(associationProjectCollaboratorRepository.Object, holidayPlanRepositoryDouble.Object);

        //act
        int result = await service.GetHolidayDaysOfCollaboratorInProjectAsync(projectId, collaboratorId);

        //assert
        Assert.Equal(5, result);
    }

    [Fact]
    public async Task WhenCalculatingHolidayDaysOfCollaboratorInAProjectWithoutHolidayPlan_ThenReturnZero()
    {
        //arrange
        long projectId = 1;
        long collaboratorId = 1;

        Mock<IAssociationProjectCollaborator> associationDouble = new Mock<IAssociationProjectCollaborator>();

        Mock<IHolidayPlanRepository> holidayPlanRepositoryDouble = new Mock<IHolidayPlanRepository>();
        holidayPlanRepositoryDouble.Setup(hpr => hpr.FindHolidayPlanByCollaboratorAsync(collaboratorId)).ReturnsAsync((IHolidayPlan?)null);

        Mock<IAssociationProjectCollaboratorRepository> associationProjectCollaboratorRepository = new Mock<IAssociationProjectCollaboratorRepository>();
        associationProjectCollaboratorRepository.Setup(a => a.FindByProjectAndCollaboratorAsync(projectId, collaboratorId)).ReturnsAsync(associationDouble.Object);

        HolidayPlanService service = new HolidayPlanService(associationProjectCollaboratorRepository.Object, holidayPlanRepositoryDouble.Object);

        //act
        int result = await service.GetHolidayDaysOfCollaboratorInProjectAsync(projectId, collaboratorId);

        //assert
        Assert.Equal(0, result);
    }

    [Fact]
    public async Task WhenCalculatingHolidayDaysOfCollaboratorInAProjectAndAssocitionsAreNull_ThenThrowExcepection()
    {

        //arrange
        long projectId = 1;
        long collaboratorId = 1;

        Mock<IAssociationProjectCollaboratorRepository> association = new Mock<IAssociationProjectCollaboratorRepository>();
        Mock<IHolidayPlanRepository> holidayPlanRepositoryDouble = new Mock<IHolidayPlanRepository>();

        association.Setup(hpr => hpr.FindByProjectAndCollaboratorAsync(projectId, collaboratorId)).ReturnsAsync((IAssociationProjectCollaborator?)null);

        HolidayPlanService service = new HolidayPlanService(association.Object, holidayPlanRepositoryDouble.Object);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<Exception>(() =>
            service.GetHolidayDaysOfCollaboratorInProjectAsync(projectId, collaboratorId));

        Assert.Equal("A associação com os parâmetros fornecidos não existe.", exception.Message);

    }
}