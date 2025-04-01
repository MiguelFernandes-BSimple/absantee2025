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

        DateOnly initDate = new DateOnly(2025, 6, 1);
        DateOnly finalDate = new DateOnly(2025, 6, 10);

        associationDouble.Setup(a => a.GetCollaborator()).Returns(collaboratorDouble.Object);
        associationDouble.Setup(a => a.GetProject()).Returns(projectDouble.Object);
        associationDouble.Setup(a => a.GetInitDate()).Returns(initDate);
        associationDouble.Setup(a => a.GetFinalDate()).Returns(finalDate);

        holidayPlanDouble.Setup(hp => hp.GetCollaborator()).Returns(collaboratorDouble.Object);
        holidayPlanDouble.Setup(hp => hp.GetNumberOfHolidayDaysBetween(initDate, finalDate)).Returns(5);

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
}