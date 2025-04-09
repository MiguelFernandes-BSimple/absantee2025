using Domain.Interfaces;
using Domain.IRepository;
using Application.Services;
using Moq;

namespace Application.Tests.HolidayPlanServiceTests;

public class FindAllHolidayPeriodsForAllProjectCollaboratorsBetweenDates
{
    [Fact]
    public void WhenFindingHolidayPeriodsForProjectCollaborators_ThenReturnCorrectPeriods()
    {
        // Arrange
        long projectId = 1;
        long collaboratorId = 1;
        var collaboratorList = new List<long>(){
                collaboratorId
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
                    projectId,
                    periodDouble.Object
                )
            )
            .Returns(associationsList);

        associationMock.Setup(a => a.GetCollaboratorId()).Returns(collaboratorId);

        var holidayPeriodMock = new Mock<IHolidayPeriod>();
        var expected = new List<IHolidayPeriod>() { holidayPeriodMock.Object };
        var holidayRepoMock = new Mock<IHolidayPlanRepository>();
        holidayRepoMock.Setup(hr => hr.FindAllHolidayPeriodsForAllCollaboratorsBetweenDates(collaboratorList, periodDouble.Object))
                                    .Returns(expected);
        var holidayPlanService = new HolidayPlanService(associationRepoMock.Object, holidayRepoMock.Object);
        // Act
        var result = holidayPlanService.FindAllHolidayPeriodsForAllProjectCollaboratorsBetweenDates(
            projectId,
            periodDouble.Object
        );

        Assert.True(expected.SequenceEqual(result));
    }

    [Fact]
    public void WhenNoHolidayPeriodsForCollaboratorWithinInterval_ThenReturnEmptyList()
    {
        // Arrange
        long projectId = 1;
        long collaboratorId = 1;
        var collaboratorList = new List<long>(){
                collaboratorId
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
                    projectId,
                    periodDouble.Object
                )
            )
            .Returns(associationsList);

        associationMock.Setup(a => a.GetCollaboratorId()).Returns(collaboratorId);

        var expected = new List<IHolidayPeriod>();
        var holidayRepoMock = new Mock<IHolidayPlanRepository>();
        holidayRepoMock.Setup(hr => hr.FindAllHolidayPeriodsForAllCollaboratorsBetweenDates(collaboratorList, periodDouble.Object))
                                    .Returns(expected);
        var holidayPlanService = new HolidayPlanService(associationRepoMock.Object, holidayRepoMock.Object);
        // Act
        var result = holidayPlanService.FindAllHolidayPeriodsForAllProjectCollaboratorsBetweenDates(
            projectId,
            periodDouble.Object
        );

        Assert.Empty(result);
    }
}