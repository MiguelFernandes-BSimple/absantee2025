using Domain.Interfaces;
using Infrastructure.Interfaces;
using Application.Services;
using Moq;
namespace Application.Tests.HolidayPlanServiceTests;

public class GetHolidayDaysOfCollaboratorInProject
{
    [Fact]
    public void WhenCalculatingHolidayDaysOfCollaboratorInAProject_ThenReturnCorrectValue()
    {
        //arrange
        Mock<IHolidayPlan> holidayPlanDouble = new Mock<IHolidayPlan>();
        Mock<ICollaborator> collaboratorDouble = new Mock<ICollaborator>();
        Mock<IProject> projectDouble = new Mock<IProject>();
        Mock<IAssociationProjectCollaborator> associationDouble = new Mock<IAssociationProjectCollaborator>();

        associationDouble.Setup(a => a.GetCollaborator()).Returns(collaboratorDouble.Object);

        holidayPlanDouble.Setup(hp => hp.GetCollaborator()).Returns(collaboratorDouble.Object);
        holidayPlanDouble.Setup(hp => hp.GetNumberOfHolidayDaysBetween(It.IsAny<IPeriodDate>())).Returns(5);

        Mock<IHolidayPlanRepository> holidayPlanRepositoryDouble = new Mock<IHolidayPlanRepository>();
        holidayPlanRepositoryDouble.Setup(hpr => hpr.FindHolidayPlanByCollaborator(collaboratorDouble.Object)).Returns(holidayPlanDouble.Object);

        Mock<IAssociationProjectCollaboratorRepository> associationProjectCollaboratorRepository = new Mock<IAssociationProjectCollaboratorRepository>();
        associationProjectCollaboratorRepository.Setup(a => a.FindByProjectAndCollaborator(projectDouble.Object, collaboratorDouble.Object)).Returns(associationDouble.Object);

        HolidayPlanService service = new HolidayPlanService(associationProjectCollaboratorRepository.Object, holidayPlanRepositoryDouble.Object);

        //act
        int result = service.GetHolidayDaysOfCollaboratorInProject(projectDouble.Object, collaboratorDouble.Object);

        //assert
        Assert.Equal(5, result);
    }

    [Fact]
    public void WhenCalculatingHolidayDaysOfCollaboratorInAProjectWithoutHolidayPlan_ThenReturnZero()
    {
        //arrange
        Mock<ICollaborator> collaboratorDouble = new Mock<ICollaborator>();
        Mock<IProject> projectDouble = new Mock<IProject>();
        Mock<IAssociationProjectCollaborator> associationDouble = new Mock<IAssociationProjectCollaborator>();

        Mock<IHolidayPlanRepository> holidayPlanRepositoryDouble = new Mock<IHolidayPlanRepository>();
        holidayPlanRepositoryDouble.Setup(hpr => hpr.FindHolidayPlanByCollaborator(collaboratorDouble.Object)).Returns((IHolidayPlan?)null);

        Mock<IAssociationProjectCollaboratorRepository> associationProjectCollaboratorRepository = new Mock<IAssociationProjectCollaboratorRepository>();
        associationProjectCollaboratorRepository.Setup(a => a.FindByProjectAndCollaborator(projectDouble.Object, collaboratorDouble.Object)).Returns(associationDouble.Object);

        HolidayPlanService service = new HolidayPlanService(associationProjectCollaboratorRepository.Object, holidayPlanRepositoryDouble.Object);

        //act
        int result = service.GetHolidayDaysOfCollaboratorInProject(projectDouble.Object, collaboratorDouble.Object);

        //assert
        Assert.Equal(0, result);
    }

    [Fact]
    public void WhenCalculatingHolidayDaysOfCollaboratorInAProjectAndAssocitionsAreNull_ThenThrowExcepection()
    {

        //arrange
        Mock<IProject> projectMock = new Mock <IProject>();
        Mock<ICollaborator> collaboratorMock = new Mock<ICollaborator>();
        Mock<IAssociationProjectCollaboratorRepository> association = new Mock<IAssociationProjectCollaboratorRepository>();
        Mock<IHolidayPlanRepository> holidayPlanRepositoryDouble = new Mock<IHolidayPlanRepository>(); 

        association.Setup(hpr => hpr.FindByProjectAndCollaborator(projectMock.Object, collaboratorMock.Object)).Returns((IAssociationProjectCollaborator?)null);

        HolidayPlanService service = new HolidayPlanService(association.Object, holidayPlanRepositoryDouble.Object);

        // Act & Assert
        var exception = Assert.Throws<Exception>(() => 
            service.GetHolidayDaysOfCollaboratorInProject(projectMock.Object, collaboratorMock.Object));

        Assert.Equal("A associação com os parâmetros fornecidos não existe.", exception.Message);
    
    }
}