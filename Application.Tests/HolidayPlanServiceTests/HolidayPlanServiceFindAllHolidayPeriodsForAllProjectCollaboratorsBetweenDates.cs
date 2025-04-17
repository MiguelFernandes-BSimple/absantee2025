using Domain.Interfaces;
using Domain.IRepository;
using Application.Services;
using Moq;

namespace Application.Tests.HolidayPlanServiceTests;

public class HolidayPlanServiceFindAllHolidayPeriodsForAllProjectCollaboratorsBetweenDates
{
    [Fact]
    public async Task WhenFindingHolidayPeriodsForProjectCollaborators_ThenReturnCorrectPeriods()
    {
        // Arrange
        var collaboratorIdList = new List<long>(){
            1
        };

        var periodDouble = new Mock<PeriodDate>();

        var associationRepoMock = new Mock<IAssociationProjectCollaboratorRepository>();
        var associationMock = new Mock<IAssociationProjectCollaborator>();
        var associationsList = new List<IAssociationProjectCollaborator> {
            associationMock.Object
        };

        associationRepoMock
            .Setup(a =>
                a.FindAllByProjectAndBetweenPeriodAsync(
                    It.IsAny<long>(),
                    periodDouble.Object
                )
            )
            .ReturnsAsync(associationsList);

        associationMock.Setup(a => a.GetCollaboratorId()).Returns(1);

        var holidayPeriodMock = new Mock<IHolidayPeriod>();
        var expected = new List<IHolidayPeriod>() { holidayPeriodMock.Object };
        var holidayRepoMock = new Mock<IHolidayPlanRepository>();
        holidayRepoMock.Setup(hr => hr.FindAllHolidayPeriodsForAllCollaboratorsBetweenDatesAsync(It.IsAny<List<long>>(), periodDouble.Object))
                                    .ReturnsAsync(expected);
        var holidayPlanService = new HolidayPlanService(associationRepoMock.Object, holidayRepoMock.Object);

        // Act
        var result = await holidayPlanService.FindAllHolidayPeriodsForAllProjectCollaboratorsBetweenDatesAsync(
            It.IsAny<long>(),
            periodDouble.Object
        );

        // Assert
        Assert.True(expected.SequenceEqual(result));
    }

    [Fact]
    public async Task WhenNoHolidayPeriodsForCollaboratorWithinInterval_ThenReturnEmptyList()
    {
        // Arrange
        var collaboratorIdList = new List<long>(){
                1
            };

        var periodDouble = new Mock<PeriodDate>();

        var associationRepoMock = new Mock<IAssociationProjectCollaboratorRepository>();
        var associationMock = new Mock<IAssociationProjectCollaborator>();
        var associationsList = new List<IAssociationProjectCollaborator> {
                associationMock.Object
            };
        associationRepoMock
            .Setup(a =>
                a.FindAllByProjectAndBetweenPeriodAsync(
                    It.IsAny<long>(),
                    periodDouble.Object
                )
            )
            .ReturnsAsync(associationsList);

        associationMock.Setup(a => a.GetCollaboratorId()).Returns(1);

        var expected = new List<IHolidayPeriod>();
        var holidayRepoMock = new Mock<IHolidayPlanRepository>();
        holidayRepoMock.Setup(hr => hr.FindAllHolidayPeriodsForAllCollaboratorsBetweenDatesAsync(collaboratorIdList, periodDouble.Object))
                                    .ReturnsAsync(expected);

        var holidayPlanService = new HolidayPlanService(associationRepoMock.Object, holidayRepoMock.Object);
        // Act
        var result = await holidayPlanService.FindAllHolidayPeriodsForAllProjectCollaboratorsBetweenDatesAsync(
            It.IsAny<long>(),
            periodDouble.Object
        );

        Assert.Empty(result);
    }
}
