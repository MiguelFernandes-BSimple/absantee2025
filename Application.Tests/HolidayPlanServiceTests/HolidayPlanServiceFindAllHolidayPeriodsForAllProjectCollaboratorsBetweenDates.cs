using Domain.Interfaces;
using Domain.IRepository;
using Application.Services;
using Moq;
using Domain.Models;
using Application.DTO;

namespace Application.Tests.HolidayPlanServiceTests;

public class HolidayPlanServiceFindAllHolidayPeriodsForAllProjectCollaboratorsBetweenDates : HolidayPlanServiceTestBase
{
    [Fact]
    public async Task WhenFindingHolidayPeriodsForProjectCollaborators_ThenReturnCorrectPeriods()
    {
        // Arrange
        var collaboratorsIdsList = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() };
        var period = new PeriodDate(
            DateOnly.FromDateTime(DateTime.Today),
            DateOnly.FromDateTime(DateTime.Today).AddDays(20));

        var association1 = new Mock<IAssociationProjectCollaborator>();
        association1.Setup(a => a.CollaboratorId).Returns(collaboratorsIdsList[0]);
        var association2 = new Mock<IAssociationProjectCollaborator>();
        association2.Setup(a => a.CollaboratorId).Returns(collaboratorsIdsList[1]);
        var associationsList = new List<IAssociationProjectCollaborator> {
            association1.Object,
            association2.Object
        };

        _associationProjectCollaboratorRepository
            .Setup(a =>
                a.FindAllByProjectAndIntersectingPeriodAsync(
                    It.IsAny<Guid>(),
                    period
                )
            )
            .ReturnsAsync(associationsList);

        var holidayPeriodMock1 = new Mock<IHolidayPeriod>();
        var holidayPeriodMock2 = new Mock<IHolidayPeriod>();
        var holidays = new List<IHolidayPeriod>() { holidayPeriodMock1.Object, holidayPeriodMock2.Object };
        _holidayPlanRepository.Setup(hr => hr.FindAllHolidayPeriodsForAllCollaboratorsIntersectingPeriodAsync(collaboratorsIdsList, period))
                                    .ReturnsAsync(holidays);

        var holidayDTO1 = new HolidayPeriodDTO
        {
            Id = Guid.NewGuid(),
            PeriodDate = new PeriodDate
            {
                InitDate = period.InitDate.AddDays(-3),
                FinalDate = period.FinalDate
            }
        };
        var holidayDTO2 = new HolidayPeriodDTO
        {
            Id = Guid.NewGuid(),
            PeriodDate = new PeriodDate
            {
                InitDate = period.InitDate,
                FinalDate = period.FinalDate.AddDays(5)
            }
        };
        var expected = new List<HolidayPeriodDTO>() { holidayDTO1, holidayDTO2 };

        _mapper.Setup(m => m.Map<HolidayPeriodDTO>(holidayPeriodMock1.Object)).Returns(holidayDTO1);
        _mapper.Setup(m => m.Map<HolidayPeriodDTO>(holidayPeriodMock2.Object)).Returns(holidayDTO2);

        // Act
        var result = (await _holidayPlanService.FindAllHolidayPeriodsForAllProjectCollaboratorsBetweenDatesAsync(
            It.IsAny<Guid>(),
            period
        )).Value;

        // Assert
        Assert.True(expected.SequenceEqual(result));
    }

    //[Fact]
    //public async Task WhenNoHolidayPeriodsForCollaboratorWithinInterval_ThenReturnEmptyList()
    //{
    //    // Arrange
    //    var collaboratorIdList = new List<long>(){
    //            1
    //        };

    //    var period = new PeriodDate(It.IsAny<DateOnly>(), It.IsAny<DateOnly>());

    //    var associationRepoMock = new Mock<IAssociationProjectCollaboratorRepository>();
    //    var associationMock = new Mock<IAssociationProjectCollaborator>();
    //    var associationsList = new List<IAssociationProjectCollaborator> {
    //            associationMock.Object
    //        };
    //    associationRepoMock
    //        .Setup(a =>
    //            a.FindAllByProjectAndBetweenPeriodAsync(
    //                It.IsAny<long>(),
    //                period
    //            )
    //        )
    //        .ReturnsAsync(associationsList);

    //    associationMock.Setup(a => a.GetCollaboratorId()).Returns(1);

    //    var expected = new List<IHolidayPeriod>();
    //    var holidayRepoMock = new Mock<IHolidayPlanRepository>();
    //    holidayRepoMock.Setup(hr => hr.FindAllHolidayPeriodsForAllCollaboratorsBetweenDatesAsync(collaboratorIdList, period))
    //                                .ReturnsAsync(expected);

    //    var holidayPlanService = new HolidayPlanService(associationRepoMock.Object, holidayRepoMock.Object);
    //    // Act
    //    var result = await holidayPlanService.FindAllHolidayPeriodsForAllProjectCollaboratorsBetweenDatesAsync(
    //        It.IsAny<long>(),
    //        period
    //    );

    //    Assert.Empty(result);
    //}
}