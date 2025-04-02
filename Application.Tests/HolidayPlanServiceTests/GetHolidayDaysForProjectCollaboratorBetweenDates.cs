using Domain.Interfaces;
using Infrastructure.Interfaces;
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
        associationRepoMock
            .Setup(a => a.FindByProjectAndCollaborator(It.IsAny<IProject>(), It.IsAny<ICollaborator>()))
            .Returns((IAssociationProjectCollaborator?)null);

        var holidayPlanService = new HolidayPlanService(associationRepoMock.Object, Mock.Of<IHolidayPlanRepository>());

        // Act & Assert
        var exception = Assert.Throws<Exception>(() =>
            holidayPlanService.GetHolidayDaysForProjectCollaboratorBetweenDates(
                projectMock.Object,
                collaboratorMock.Object,
                It.IsAny<IPeriodDate>()
            )
        );
        Assert.Equal("No association found for the project and collaborator", exception.Message);
    }



    [Fact]
    public void WhenGettingHolidayDaysForProjectCollaboratorBetweenDates_ThenReturnsCorrectHolidayDays()
    {
        // Arrange
        var collaboratorMock = new Mock<ICollaborator>();
        var projectMock = new Mock<IProject>();

        var associationRepoMock = new Mock<IAssociationProjectCollaboratorRepository>();
        var associationMock = new Mock<IAssociationProjectCollaborator>();
        associationRepoMock
            .Setup(a => a.FindByProjectAndCollaborator(projectMock.Object, collaboratorMock.Object))
            .Returns(associationMock.Object);

        var holidayPeriodMock = new Mock<IHolidayPeriod>();
        var holidarPeriodList = new List<IHolidayPeriod>(){
                holidayPeriodMock.Object
            };

        var holidayRepoMock = new Mock<IHolidayPlanRepository>();
        holidayRepoMock.Setup(hr => hr.FindHolidayPeriodsByCollaborator(collaboratorMock.Object))
                        .Returns(holidarPeriodList);

        var periodDouble = new Mock<IPeriodDate>();



        holidayPeriodMock.Setup(hp => hp.GetNumberOfCommonUtilDaysBetweenPeriods(periodDouble.Object))
                        .Returns(1);


        var holidayPlanService = new HolidayPlanService(associationRepoMock.Object, holidayRepoMock.Object);

        // Act
        var result = holidayPlanService.GetHolidayDaysForProjectCollaboratorBetweenDates(
            projectMock.Object,
            collaboratorMock.Object,
            periodDouble.Object
        );

        // Assert
        Assert.Equal(1, result);
    }

    [Fact]
    public void WhenInitDateIsGreaterThanEndDate_ThenReturnsZero()
    {
        // Arrange
        var collaboratorMock = new Mock<ICollaborator>();
        var projectMock = new Mock<IProject>();

        var associationRepoMock = new Mock<IAssociationProjectCollaboratorRepository>();
        var associationMock = new Mock<IAssociationProjectCollaborator>();
        associationRepoMock
            .Setup(a => a.FindByProjectAndCollaborator(projectMock.Object, collaboratorMock.Object))
            .Returns(associationMock.Object);

        var periodDouble = new Mock<IPeriodDate>();

        var holidayRepoMock = new Mock<IHolidayPlanRepository>();
        holidayRepoMock.Setup(hr => hr.FindHolidayPeriodsByCollaboratorBetweenDates(collaboratorMock.Object, periodDouble.Object))
                        .Returns(new List<IHolidayPeriod>());

        var holidayPlanService = new HolidayPlanService(associationRepoMock.Object, holidayRepoMock.Object);

        // Act
        var result = holidayPlanService.GetHolidayDaysForProjectCollaboratorBetweenDates(
           projectMock.Object,
           collaboratorMock.Object,
           periodDouble.Object
       );

        // Assert
        Assert.Equal(0, result);
    }


}