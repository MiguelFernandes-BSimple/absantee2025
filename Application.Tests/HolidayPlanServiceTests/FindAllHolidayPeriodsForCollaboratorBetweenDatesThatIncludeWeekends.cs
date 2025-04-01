using Domain.Interfaces;
using Infrastructure.Interfaces;
using Application.Services;
using Moq;
namespace Application.Tests.HolidayPlanServiceTests;

public class FindAllHolidayPeriodsForCollaboratorBetweenDatesThatIncludeWeekends
{
    public static IEnumerable<object[]> ValidHolidayDatesBetweenWeekends()
    {
        yield return new object[] { new DateOnly(2025, 04, 01), new DateOnly(2025, 04, 08) };
        yield return new object[] { new DateOnly(2025, 04, 01), new DateOnly(2025, 04, 07) };
        yield return new object[] { new DateOnly(2025, 04, 02), new DateOnly(2025, 04, 08) };
        yield return new object[] { new DateOnly(2025, 04, 02), new DateOnly(2025, 04, 07) };
        yield return new object[] { new DateOnly(2025, 04, 03), new DateOnly(2025, 04, 06) };
        yield return new object[] { new DateOnly(2025, 04, 03), new DateOnly(2025, 04, 07) };
        yield return new object[] { new DateOnly(2025, 04, 03), new DateOnly(2025, 04, 08) };
        yield return new object[] { new DateOnly(2025, 04, 05), new DateOnly(2025, 04, 05) };
        yield return new object[] { new DateOnly(2025, 04, 06), new DateOnly(2025, 04, 06) };
    }

    [Theory]
    [MemberData(nameof(ValidHolidayDatesBetweenWeekends))]
    public void WhenRetrievingAllHolidayPeriodsForCollaboratorBetweenDatesThatIncludeWeekends_ThenReturnSucessfully(DateOnly searchInitDate, DateOnly searchEndDate)
    {
        //arrange
        Mock<ICollaborator> collab = new Mock<ICollaborator>();

        Mock<IHolidayPeriod> holidayPeriod = new Mock<IHolidayPeriod>();
        DateOnly holidayPeriodStartDate = new DateOnly(2025, 04, 02);
        DateOnly holidayPeriodFinalDate = new DateOnly(2025, 04, 07);
        holidayPeriod.Setup(hp => hp.GetInitDate()).Returns(holidayPeriodStartDate);
        holidayPeriod.Setup(hp => hp.GetFinalDate()).Returns(holidayPeriodFinalDate);
        var holidayPeriodsList = new List<IHolidayPeriod> { holidayPeriod.Object };

        Mock<IHolidayPlanRepository> holidayPlanRepository = new Mock<IHolidayPlanRepository>();
        Mock<IAssociationProjectCollaboratorRepository> associationRepository = new Mock<IAssociationProjectCollaboratorRepository>();
        holidayPlanRepository.Setup(r => r.FindAllHolidayPeriodsForCollaboratorBetweenDates(collab.Object, searchInitDate, searchEndDate)).Returns(holidayPeriodsList);

        HolidayPlanService service = new HolidayPlanService(associationRepository.Object, holidayPlanRepository.Object);

        //act
        var result = service.FindAllHolidayPeriodsForCollaboratorBetweenDatesThatIncludeWeekends(collab.Object, searchInitDate, searchEndDate);

        //assert
        Assert.Equal(holidayPeriodStartDate, result.First().GetInitDate());
        Assert.Equal(holidayPeriodFinalDate, result.First().GetFinalDate());
    }

    public static IEnumerable<object[]> ValidHolidayDatesWithoutWeekends()
    {
        yield return new object[] { new DateOnly(2025, 04, 01), new DateOnly(2025, 04, 04) };
        yield return new object[] { new DateOnly(2025, 04, 10), new DateOnly(2025, 04, 11) };
        yield return new object[] { new DateOnly(2025, 04, 07), new DateOnly(2025, 04, 14) };
        yield return new object[] { new DateOnly(2025, 03, 20), new DateOnly(2025, 04, 04) };
    }

    [Theory]
    [MemberData(nameof(ValidHolidayDatesWithoutWeekends))]
    public void WhenRetrievingAllHolidayPeriodsForCollaboratorBetweenWithDatesThatDontIncludeWeekends_ThenReturnEmpty(DateOnly searchInitDate, DateOnly searchEndDate)
    {

        //arrange
        Mock<ICollaborator> collab = new Mock<ICollaborator>();

        Mock<IHolidayPeriod> holidayPeriod = new Mock<IHolidayPeriod>();
        DateOnly holidayPeriodStartDate = new DateOnly(2025, 04, 01);
        DateOnly holidayPeriodFinalDate = new DateOnly(2025, 04, 09);
        holidayPeriod.Setup(hp => hp.GetInitDate()).Returns(holidayPeriodStartDate);
        holidayPeriod.Setup(hp => hp.GetFinalDate()).Returns(holidayPeriodFinalDate);
        var holidayPeriodsList = new List<IHolidayPeriod> { holidayPeriod.Object };

        Mock<IHolidayPlanRepository> holidayPlanRepository = new Mock<IHolidayPlanRepository>();
        Mock<IAssociationProjectCollaboratorRepository> associationRepository = new Mock<IAssociationProjectCollaboratorRepository>();
        holidayPlanRepository.Setup(r => r.FindAllHolidayPeriodsForCollaboratorBetweenDates(collab.Object, searchInitDate, searchEndDate)).Returns(holidayPeriodsList);

        HolidayPlanService service = new HolidayPlanService(associationRepository.Object, holidayPlanRepository.Object);

        //act
        var result = service.FindAllHolidayPeriodsForCollaboratorBetweenDatesThatIncludeWeekends(collab.Object, searchInitDate, searchEndDate);

        //assert
        Assert.Empty(result);
    }
}