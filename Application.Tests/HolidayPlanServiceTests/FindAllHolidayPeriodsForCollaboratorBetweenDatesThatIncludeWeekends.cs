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
        // Arrange
        Mock<ICollaborator> collab = new Mock<ICollaborator>();
        Mock<IHolidayPeriod> holidayPeriod = new Mock<IHolidayPeriod>();
        // HolidayPeriod's period dates
        Mock<IPeriodDate> periodDate = new Mock<IPeriodDate>();

        // Establish period dates for PeriodDate
        DateOnly holidayPeriodStartDate = new DateOnly(2025, 04, 02);
        DateOnly holidayPeriodFinalDate = new DateOnly(2025, 04, 07);

        periodDate.Setup(pd => pd.GetInitDate()).Returns(holidayPeriodStartDate);
        periodDate.Setup(pd => pd.GetFinalDate()).Returns(holidayPeriodFinalDate);

        // Establish the HolidayPeriod's list period
        // that are between the dates
        holidayPeriod.Setup(hp => hp.GetPeriodDate()).Returns(periodDate.Object);

        // Setup holidayperiod list
        // that are between the dates
        var holidayPeriodsList = new List<IHolidayPeriod> { holidayPeriod.Object };

        Mock<IHolidayPlanRepository> holidayPlanRepository = new Mock<IHolidayPlanRepository>();
        Mock<IAssociationProjectCollaboratorRepository> associationRepository = new Mock<IAssociationProjectCollaboratorRepository>();

        // PeriodDate for input search dates
        Mock<IPeriodDate> inputPeriodDate = new Mock<IPeriodDate>();

        inputPeriodDate.Setup(pd => pd.GetInitDate()).Returns(searchInitDate);
        inputPeriodDate.Setup(pd => pd.GetFinalDate()).Returns(searchEndDate);

        holidayPlanRepository.Setup(r => r.FindAllHolidayPeriodsForCollaboratorBetweenDates(collab.Object, inputPeriodDate.Object)).Returns(holidayPeriodsList);

        // Instatiate HolidayPlanService
        HolidayPlanService service = new HolidayPlanService(associationRepository.Object, holidayPlanRepository.Object);

        // Act
        var result = service.FindAllHolidayPeriodsForCollaboratorBetweenDatesThatIncludeWeekends(collab.Object, inputPeriodDate.Object);

        // Assert
        Assert.Equal(holidayPeriodStartDate, result.First().GetPeriodDate().GetInitDate());
        Assert.Equal(holidayPeriodFinalDate, result.First().GetPeriodDate().GetFinalDate());
    }

    public static IEnumerable<object[]> ValidHolidayDatesWithoutWeekends()
    {
        yield return new object[] { new DateOnly(2025, 04, 01), new DateOnly(2025, 04, 04) };
        yield return new object[] { new DateOnly(2025, 04, 10), new DateOnly(2025, 04, 11) };
        yield return new object[] { new DateOnly(2025, 04, 07), new DateOnly(2025, 04, 14) };
        yield return new object[] { new DateOnly(2025, 03, 20), new DateOnly(2025, 04, 04) };
        yield return new object[] { DateOnly.MinValue, DateOnly.MinValue };
    }

    [Theory]
    [MemberData(nameof(ValidHolidayDatesWithoutWeekends))]
    public void WhenRetrievingAllHolidayPeriodsForCollaboratorBetweenWithDatesThatDontIncludeWeekends_ThenReturnEmpty(DateOnly searchInitDate, DateOnly searchEndDate)
    {
        // Arrange
        Mock<ICollaborator> collab = new Mock<ICollaborator>();
        Mock<IHolidayPeriod> holidayPeriod = new Mock<IHolidayPeriod>();
        // HolidayPeriod's period dates
        Mock<IPeriodDate> periodDate = new Mock<IPeriodDate>();

        DateOnly holidayPeriodStartDate = new DateOnly(2025, 04, 01);
        DateOnly holidayPeriodFinalDate = new DateOnly(2025, 04, 09);

        periodDate.Setup(pd => pd.GetInitDate()).Returns(holidayPeriodStartDate);
        periodDate.Setup(pd => pd.GetFinalDate()).Returns(holidayPeriodFinalDate);

        // Establish the HolidayPeriod's period
        holidayPeriod.Setup(hp => hp.GetPeriodDate()).Returns(periodDate.Object);

        // Establish the HolidayPeriod's list period
        // that are between the dates
        var holidayPeriodsList = new List<IHolidayPeriod> { holidayPeriod.Object };

        Mock<IHolidayPlanRepository> holidayPlanRepository = new Mock<IHolidayPlanRepository>();
        Mock<IAssociationProjectCollaboratorRepository> associationRepository = new Mock<IAssociationProjectCollaboratorRepository>();

        // PeriodDate for input search dates
        Mock<IPeriodDate> inputPeriodDate = new Mock<IPeriodDate>();

        inputPeriodDate.Setup(pd => pd.GetInitDate()).Returns(searchInitDate);
        inputPeriodDate.Setup(pd => pd.GetFinalDate()).Returns(searchEndDate);

        holidayPlanRepository.Setup(r => r.FindAllHolidayPeriodsForCollaboratorBetweenDates(collab.Object, inputPeriodDate.Object)).Returns(holidayPeriodsList);

        // Instatiate HolidayPlanService
        HolidayPlanService service = new HolidayPlanService(associationRepository.Object, holidayPlanRepository.Object);

        // Act
        var result = service.FindAllHolidayPeriodsForCollaboratorBetweenDatesThatIncludeWeekends(collab.Object, inputPeriodDate.Object);

        // Assert
        Assert.Empty(result);
    }
}