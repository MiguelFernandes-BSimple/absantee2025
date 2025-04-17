using Domain.Interfaces;
using Domain.IRepository;
using Application.Services;
using Moq;
using Domain.Models;

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

        var period = new PeriodDate(It.IsAny<DateOnly>(), It.IsAny<DateOnly>());

        var associationRepoMock = new Mock<IAssociationProjectCollaboratorRepository>();
        var associationMock = new Mock<IAssociationProjectCollaborator>();
        var associationsList = new List<IAssociationProjectCollaborator> {
            associationMock.Object
        };

        associationRepoMock
            .Setup(a =>
                a.FindAllByProjectAndBetweenPeriodAsync(
                    It.IsAny<long>(),
                    period
                )
            )
            .ReturnsAsync(associationsList);

        associationMock.Setup(a => a.GetCollaboratorId()).Returns(1);

        var holidayPeriodMock = new Mock<IHolidayPeriod>();
        var expected = new List<IHolidayPeriod>() { holidayPeriodMock.Object };
        var holidayRepoMock = new Mock<IHolidayPlanRepository>();
        holidayRepoMock.Setup(hr => hr.FindAllHolidayPeriodsForAllCollaboratorsBetweenDatesAsync(It.IsAny<List<long>>(), period))
                                    .ReturnsAsync(expected);
        var holidayPlanService = new HolidayPlanService(associationRepoMock.Object, holidayRepoMock.Object);

        // Act
        var result = await holidayPlanService.FindAllHolidayPeriodsForAllProjectCollaboratorsBetweenDatesAsync(
            It.IsAny<long>(),
            period
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

        var period = new PeriodDate(It.IsAny<DateOnly>(), It.IsAny<DateOnly>());

        var associationRepoMock = new Mock<IAssociationProjectCollaboratorRepository>();
        var associationMock = new Mock<IAssociationProjectCollaborator>();
        var associationsList = new List<IAssociationProjectCollaborator> {
                associationMock.Object
            };
        associationRepoMock
            .Setup(a =>
                a.FindAllByProjectAndBetweenPeriodAsync(
                    It.IsAny<long>(),
                    period
                )
            )
            .ReturnsAsync(associationsList);

        associationMock.Setup(a => a.GetCollaboratorId()).Returns(1);

        var expected = new List<IHolidayPeriod>();
        var holidayRepoMock = new Mock<IHolidayPlanRepository>();
        holidayRepoMock.Setup(hr => hr.FindAllHolidayPeriodsForAllCollaboratorsBetweenDatesAsync(collaboratorIdList, period))
                                    .ReturnsAsync(expected);

        var holidayPlanService = new HolidayPlanService(associationRepoMock.Object, holidayRepoMock.Object);
        // Act
        var result = await holidayPlanService.FindAllHolidayPeriodsForAllProjectCollaboratorsBetweenDatesAsync(
            It.IsAny<long>(),
            period
        );

        Assert.Empty(result);
    }
}
