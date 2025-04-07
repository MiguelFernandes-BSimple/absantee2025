using Domain.Interfaces;
using Domain.IRepository;
using Application.Services;
using Domain.Models;
using Moq;
using Domain.IRepository;

namespace Application.Tests.HolidayPlanServiceTests;

public class FindAllHolidayPeriodsForAllProjectCollaboratorsBetweenDates
{
    [Fact]
    public void WhenFindingHolidayPeriodsForProjectCollaborators_ThenReturnCorrectPeriods()
    {
        // Arrange
        var projectMock = new Mock<IProject>();
        var collaboratorMock = new Mock<ICollaborator>();
        var collaboratorList = new List<ICollaborator>(){
                collaboratorMock.Object
            };

        var periodDouble = new Mock<IPeriodDate>();

        var associationRepoMock = new Mock<IAssociationProjectCollaboratorRepository>();
        var associationMock = new Mock<IAssociationProjectCollaborator>();
        var associationsList = new List<IAssociationProjectCollaborator> {
                associationMock.Object
            };
        associationRepoMock
            .Setup(a =>
                a.FindAllByProjectAndBetweenPeriod(
                    projectMock.Object,
                    periodDouble.Object
                )
            )
            .Returns(associationsList);

        associationMock.Setup(a => a.GetCollaborator()).Returns(collaboratorMock.Object);

        var holidayPeriodMock = new Mock<IHolidayPeriod>();
        var expected = new List<IHolidayPeriod>() { holidayPeriodMock.Object };
        var holidayRepoMock = new Mock<IHolidayPlanRepository>();
        holidayRepoMock.Setup(hr => hr.FindAllHolidayPeriodsForAllCollaboratorsBetweenDates(collaboratorList, periodDouble.Object))
                                    .Returns(expected);
        var holidayPlanService = new HolidayPlanService(associationRepoMock.Object, holidayRepoMock.Object);
        // Act
        var result = holidayPlanService.FindAllHolidayPeriodsForAllProjectCollaboratorsBetweenDates(
            projectMock.Object,
            periodDouble.Object
        );

        Assert.True(expected.SequenceEqual(result));
    }

    [Fact]
    public void WhenNoHolidayPeriodsForCollaboratorWithinInterval_ThenReturnEmptyList()
    {
        // Arrange
        var projectMock = new Mock<IProject>();
        var collaboratorMock = new Mock<ICollaborator>();
        var collaboratorList = new List<ICollaborator>(){
                collaboratorMock.Object
            };

        var periodDouble = new Mock<IPeriodDate>();

        var associationRepoMock = new Mock<IAssociationProjectCollaboratorRepository>();
        var associationMock = new Mock<IAssociationProjectCollaborator>();
        var associationsList = new List<IAssociationProjectCollaborator> {
                associationMock.Object
            };
        associationRepoMock
            .Setup(a =>
                a.FindAllByProjectAndBetweenPeriod(
                    projectMock.Object,
                    periodDouble.Object
                )
            )
            .Returns(associationsList);

        associationMock.Setup(a => a.GetCollaborator()).Returns(collaboratorMock.Object);

        var expected = new List<IHolidayPeriod>();
        var holidayRepoMock = new Mock<IHolidayPlanRepository>();
        holidayRepoMock.Setup(hr => hr.FindAllHolidayPeriodsForAllCollaboratorsBetweenDates(collaboratorList, periodDouble.Object))
                                    .Returns(expected);
        var holidayPlanService = new HolidayPlanService(associationRepoMock.Object, holidayRepoMock.Object);
        // Act
        var result = holidayPlanService.FindAllHolidayPeriodsForAllProjectCollaboratorsBetweenDates(
            projectMock.Object,
            periodDouble.Object
        );

        Assert.Empty(result);
    }
}