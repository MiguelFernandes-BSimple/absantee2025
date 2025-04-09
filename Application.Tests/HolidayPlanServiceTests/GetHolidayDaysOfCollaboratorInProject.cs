using Domain.Interfaces;
using Domain.IRepository;
using Application.Services;
using Moq;

namespace Application.Tests.HolidayPlanServiceTests;

public class GetHolidayDaysOfCollaboratorInProject
{
    [Fact]
    public void WhenCalculatingHolidayDaysOfCollaboratorInAProject_ThenReturnCorrectValue()
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
        holidayPlanRepositoryDouble.Setup(hpr => hpr.FindHolidayPlanByCollaborator(collaboratorId)).Returns(holidayPlanDouble.Object);

        Mock<IAssociationProjectCollaboratorRepository> associationProjectCollaboratorRepository = new Mock<IAssociationProjectCollaboratorRepository>();
        associationProjectCollaboratorRepository.Setup(a => a.FindByProjectAndCollaborator(projectId, collaboratorId)).Returns(associationDouble.Object);

        HolidayPlanService service = new HolidayPlanService(associationProjectCollaboratorRepository.Object, holidayPlanRepositoryDouble.Object);

        //act
        int result = service.GetHolidayDaysOfCollaboratorInProject(projectId, collaboratorId);

        //assert
        Assert.Equal(5, result);
    }

    [Fact]
    public void WhenCalculatingHolidayDaysOfCollaboratorInAProjectWithoutHolidayPlan_ThenReturnZero()
    {
        //arrange
        long projectId = 1;
        long collaboratorId = 1;

        Mock<IAssociationProjectCollaborator> associationDouble = new Mock<IAssociationProjectCollaborator>();

        Mock<IHolidayPlanRepository> holidayPlanRepositoryDouble = new Mock<IHolidayPlanRepository>();
        holidayPlanRepositoryDouble.Setup(hpr => hpr.FindHolidayPlanByCollaborator(collaboratorId)).Returns((IHolidayPlan?)null);

        Mock<IAssociationProjectCollaboratorRepository> associationProjectCollaboratorRepository = new Mock<IAssociationProjectCollaboratorRepository>();
        associationProjectCollaboratorRepository.Setup(a => a.FindByProjectAndCollaborator(projectId, collaboratorId)).Returns(associationDouble.Object);

        HolidayPlanService service = new HolidayPlanService(associationProjectCollaboratorRepository.Object, holidayPlanRepositoryDouble.Object);

        //act
        int result = service.GetHolidayDaysOfCollaboratorInProject(projectId, collaboratorId);

        //assert
        Assert.Equal(0, result);
    }

    [Fact]
    public void WhenCalculatingHolidayDaysOfCollaboratorInAProjectAndAssocitionsAreNull_ThenThrowExcepection()
    {

        //arrange
        long projectId = 1;
        long collaboratorId = 1;

        Mock<IAssociationProjectCollaboratorRepository> association = new Mock<IAssociationProjectCollaboratorRepository>();
        Mock<IHolidayPlanRepository> holidayPlanRepositoryDouble = new Mock<IHolidayPlanRepository>();

        association.Setup(hpr => hpr.FindByProjectAndCollaborator(projectId, collaboratorId)).Returns((IAssociationProjectCollaborator?)null);

        HolidayPlanService service = new HolidayPlanService(association.Object, holidayPlanRepositoryDouble.Object);

        // Act & Assert
        var exception = Assert.Throws<Exception>(() =>
            service.GetHolidayDaysOfCollaboratorInProject(projectId, collaboratorId));

        Assert.Equal("A associação com os parâmetros fornecidos não existe.", exception.Message);

    }
}