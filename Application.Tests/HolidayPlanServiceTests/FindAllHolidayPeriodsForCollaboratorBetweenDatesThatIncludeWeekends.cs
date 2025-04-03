using Domain.Interfaces;
using Infrastructure.Interfaces;
using Application.Services;
using Moq;
namespace Application.Tests.HolidayPlanServiceTests;

public class FindAllHolidayPeriodsForCollaboratorBetweenDatesThatIncludeWeekends
{
    public static IEnumerable<object[]> ValidHolidayDatesBetweenWeekends()
    {
        yield return new object[] {
            new DateOnly(2025, 04, 01), new DateOnly(2025, 04, 08),
            new DateOnly(2025, 04, 02), new DateOnly(2025, 04, 07)
        };
        yield return new object[] {
            new DateOnly(2025, 04, 01), new DateOnly(2025, 04, 08),
            new DateOnly(2025, 04, 01), new DateOnly(2025, 04, 08)
        };
        yield return new object[] {
            new DateOnly(2025, 04, 01), new DateOnly(2025, 04, 08),
            new DateOnly(2025, 04, 01), new DateOnly(2025, 04, 07)
        };
        yield return new object[] {
            new DateOnly(2025, 04, 01), new DateOnly(2025, 04, 08),
            new DateOnly(2025, 04, 02), new DateOnly(2025, 04, 08)
        };
        yield return new object[] {
            new DateOnly(2025, 04, 01), new DateOnly(2025, 04, 08),
            new DateOnly(2025, 04, 03), new DateOnly(2025, 04, 06)
        };
        yield return new object[] {
            new DateOnly(2025, 04, 01), new DateOnly(2025, 04, 08),
            new DateOnly(2025, 04, 03), new DateOnly(2025, 04, 07)
        };
        yield return new object[] {
            new DateOnly(2025, 04, 01), new DateOnly(2025, 04, 08),
            new DateOnly(2025, 04, 03), new DateOnly(2025, 04, 08)
        };
        yield return new object[] {
            new DateOnly(2025, 04, 01), new DateOnly(2025, 04, 08),
            new DateOnly(2025, 04, 05), new DateOnly(2025, 04, 05)
        };
        yield return new object[] {
            new DateOnly(2025, 04, 01), new DateOnly(2025, 04, 08),
            new DateOnly(2025, 04, 06), new DateOnly(2025, 04, 06)
        };
    }

    [Theory]
    [MemberData(nameof(ValidHolidayDatesBetweenWeekends))]
    public void WhenRetrievingAllHolidayPeriodsForCollaboratorBetweenDatesThatIncludeWeekends_ThenReturnSucessfully(
        DateOnly holidayInitDate, DateOnly holidayEndDate,
        DateOnly searchInitDate, DateOnly searchEndDate
    )
    {
        // Arrange
        Mock<ICollaborator> collab = new Mock<ICollaborator>();

        Mock<IPeriodDate> holidayPeriodDate = new Mock<IPeriodDate>();
        holidayPeriodDate.Setup(hpd => hpd.GetInitDate()).Returns(holidayInitDate);
        holidayPeriodDate.Setup(hpd => hpd.GetFinalDate()).Returns(holidayEndDate);
        Mock<IHolidayPeriod> holidayPeriod = new Mock<IHolidayPeriod>();
        holidayPeriod.Setup(hp => hp.GetPeriodDate()).Returns(holidayPeriodDate.Object);
        holidayPeriod.Setup(hp => hp.ContainsWeekend()).Returns(true);

        Mock<IHolidayPlanRepository> holidayPlanRepository = new Mock<IHolidayPlanRepository>();
        Mock<IAssociationProjectCollaboratorRepository> associationRepository = new Mock<IAssociationProjectCollaboratorRepository>();

        Mock<IPeriodDate> searchPeriodDate = new Mock<IPeriodDate>();
        searchPeriodDate.Setup(spd => spd.GetInitDate()).Returns(searchInitDate);
        searchPeriodDate.Setup(spd => spd.GetFinalDate()).Returns(searchEndDate);
        searchPeriodDate.Setup(spd => spd.ContainsWeekend()).Returns(true);

        var holidayPeriodsList = new List<IHolidayPeriod> { holidayPeriod.Object };

        holidayPlanRepository.Setup(r => r.FindAllHolidayPeriodsForCollaboratorBetweenDates(collab.Object, searchPeriodDate.Object)).Returns(holidayPeriodsList);

        HolidayPlanService service = new HolidayPlanService(associationRepository.Object, holidayPlanRepository.Object);

        // Act
        var result = service.FindAllHolidayPeriodsForCollaboratorBetweenDatesThatIncludeWeekends(collab.Object, searchPeriodDate.Object);

        // Assert
        Assert.True(result.SequenceEqual(holidayPeriodsList));
    }

    public static IEnumerable<object[]> SearchPeriodDatesWithoutWeekends()
    {
        yield return new object[] { // same dates
            new DateOnly(2025, 04, 01), new DateOnly(2025, 04, 04),
            new DateOnly(2025, 04, 01), new DateOnly(2025, 04, 04)
        };
        yield return new object[] { // holiday after outside search period
            new DateOnly(2025, 04, 05), new DateOnly(2025, 04, 10),
            new DateOnly(2025, 04, 01), new DateOnly(2025, 04, 04)
        };
        yield return new object[] { // holiday period starting before, ending inside search period
            new DateOnly(2025, 03, 25), new DateOnly(2025, 04, 03),
            new DateOnly(2025, 04, 01), new DateOnly(2025, 04, 04)
        };
        yield return new object[] { // holiday period starting in the middle of search period, ending after
            new DateOnly(2025, 04, 02), new DateOnly(2025, 04, 10),
            new DateOnly(2025, 04, 01), new DateOnly(2025, 04, 04)
        };
        yield return new object[] { // holiday period starting before and ending after search period
            new DateOnly(2025, 03, 25), new DateOnly(2025, 04, 05),
            new DateOnly(2025, 04, 01), new DateOnly(2025, 04, 04)
        };
    }

    [Theory]
    [MemberData(nameof(SearchPeriodDatesWithoutWeekends))]
    public void WhenRetrievingAllHolidayPeriodsForCollaboratorBetweenWithSearchDatesThatDontIncludeWeekends_ThenReturnEmpty(
        DateOnly holidayInitDate, DateOnly holidayEndDate,
        DateOnly searchInitDate, DateOnly searchEndDate
    )
    {
        // Arrange
        Mock<ICollaborator> collab = new Mock<ICollaborator>();

        Mock<IPeriodDate> holidayPeriodDate = new Mock<IPeriodDate>();
        holidayPeriodDate.Setup(hpd => hpd.GetInitDate()).Returns(holidayInitDate);
        holidayPeriodDate.Setup(hpd => hpd.GetFinalDate()).Returns(holidayEndDate);
        Mock<IHolidayPeriod> holidayPeriod = new Mock<IHolidayPeriod>();
        holidayPeriod.Setup(hp => hp.GetPeriodDate()).Returns(holidayPeriodDate.Object);
        holidayPeriod.Setup(hp => hp.ContainsWeekend()).Returns(true);

        Mock<IHolidayPlanRepository> holidayPlanRepository = new Mock<IHolidayPlanRepository>();
        Mock<IAssociationProjectCollaboratorRepository> associationRepository = new Mock<IAssociationProjectCollaboratorRepository>();

        Mock<IPeriodDate> searchPeriodDate = new Mock<IPeriodDate>();
        searchPeriodDate.Setup(spd => spd.GetInitDate()).Returns(searchInitDate);
        searchPeriodDate.Setup(spd => spd.GetFinalDate()).Returns(searchEndDate);
        searchPeriodDate.Setup(spd => spd.ContainsWeekend()).Returns(false);

        var holidayPeriodsList = new List<IHolidayPeriod> { holidayPeriod.Object };

        holidayPlanRepository.Setup(r => r.FindAllHolidayPeriodsForCollaboratorBetweenDates(collab.Object, searchPeriodDate.Object)).Returns(holidayPeriodsList);

        HolidayPlanService service = new HolidayPlanService(associationRepository.Object, holidayPlanRepository.Object);

        // Act
        var result = service.FindAllHolidayPeriodsForCollaboratorBetweenDatesThatIncludeWeekends(collab.Object, searchPeriodDate.Object);

        // Assert
        Assert.Empty(result);
    }

    public static IEnumerable<object[]> HolidayDatesWithoutWeekend()
    {
        yield return new object[] { // same dates
            new DateOnly(2025, 04, 01), new DateOnly(2025, 04, 04),
            new DateOnly(2025, 04, 01), new DateOnly(2025, 04, 04)
        };
        yield return new object[] { // holiday period totally inside search period
            new DateOnly(2025, 04, 07), new DateOnly(2025, 04, 11),
            new DateOnly(2025, 04, 01), new DateOnly(2025, 04, 15)
        };
        yield return new object[] { // holiday period partially(upper) inside search period
            new DateOnly(2025, 04, 14), new DateOnly(2025, 04, 18),
            new DateOnly(2025, 04, 01), new DateOnly(2025, 04, 15)
        };
        yield return new object[] { // holiday period partially(lower) inside search period
            new DateOnly(2025, 03, 31), new DateOnly(2025, 04, 04),
            new DateOnly(2025, 04, 01), new DateOnly(2025, 04, 15)
        };
    }

    [Theory]
    [MemberData(nameof(HolidayDatesWithoutWeekend))]
    public void WhenRetrievingAllHolidayPeriodsForCollaboratorBetweenWithHolidayDatesThatDontIncludeWeekends_ThenReturnEmpty(
        DateOnly holidayInitDate, DateOnly holidayEndDate,
        DateOnly searchInitDate, DateOnly searchEndDate
    )
    {
        // Arrange
        Mock<ICollaborator> collab = new Mock<ICollaborator>();

        Mock<IPeriodDate> holidayPeriodDate = new Mock<IPeriodDate>();
        holidayPeriodDate.Setup(hpd => hpd.GetInitDate()).Returns(holidayInitDate);
        holidayPeriodDate.Setup(hpd => hpd.GetFinalDate()).Returns(holidayEndDate);
        Mock<IHolidayPeriod> holidayPeriod = new Mock<IHolidayPeriod>();
        holidayPeriod.Setup(hp => hp.GetPeriodDate()).Returns(holidayPeriodDate.Object);
        holidayPeriod.Setup(hp => hp.ContainsWeekend()).Returns(false);

        Mock<IHolidayPlanRepository> holidayPlanRepository = new Mock<IHolidayPlanRepository>();
        Mock<IAssociationProjectCollaboratorRepository> associationRepository = new Mock<IAssociationProjectCollaboratorRepository>();

        Mock<IPeriodDate> searchPeriodDate = new Mock<IPeriodDate>();
        searchPeriodDate.Setup(spd => spd.GetInitDate()).Returns(searchInitDate);
        searchPeriodDate.Setup(spd => spd.GetFinalDate()).Returns(searchEndDate);
        searchPeriodDate.Setup(spd => spd.ContainsWeekend()).Returns(true);

        var holidayPeriodsList = new List<IHolidayPeriod> { holidayPeriod.Object };

        holidayPlanRepository.Setup(r => r.FindAllHolidayPeriodsForCollaboratorBetweenDates(collab.Object, searchPeriodDate.Object)).Returns(holidayPeriodsList);

        HolidayPlanService service = new HolidayPlanService(associationRepository.Object, holidayPlanRepository.Object);

        // Act
        var result = service.FindAllHolidayPeriodsForCollaboratorBetweenDatesThatIncludeWeekends(collab.Object, searchPeriodDate.Object);

        // Assert
        Assert.Empty(result);
    }

    /* public static IEnumerable<object[]> IntersectingDifferentWeekendDates()
        {
            yield return new object[] {
                new DateOnly(2025, 04, 07), new DateOnly(2025, 04, 14),
                new DateOnly(2025, 04, 01), new DateOnly(2025, 04, 08)
            };
            yield return new object[] {
                new DateOnly(2025, 04, 01), new DateOnly(2025, 04, 08),
                new DateOnly(2025, 04, 07), new DateOnly(2025, 04, 14)
            };
        }

        [Theory]
        [MemberData(nameof(IntersectingDifferentWeekendDates))]
        public void WhenRetrievingAllHolidayPeriodsForCollaboratorBetweenWithIntersectingPeriodsThatIncludeDifferentWeekends_ThenReturnEmpty(
            DateOnly holidayInitDate, DateOnly holidayEndDate,
            DateOnly searchInitDate, DateOnly searchEndDate
        )
        {
            // Arrange
            Mock<ICollaborator> collab = new Mock<ICollaborator>();

            Mock<IPeriodDate> holidayPeriodDate = new Mock<IPeriodDate>();
            holidayPeriodDate.Setup(hpd => hpd.GetInitDate()).Returns(holidayInitDate);
            holidayPeriodDate.Setup(hpd => hpd.GetFinalDate()).Returns(holidayEndDate);
            Mock<IHolidayPeriod> holidayPeriod = new Mock<IHolidayPeriod>();
            holidayPeriod.Setup(hp => hp.GetPeriodDate()).Returns(holidayPeriodDate.Object);
            holidayPeriod.Setup(hp => hp.ContainsWeekend()).Returns(true);

            Mock<IHolidayPlanRepository> holidayPlanRepository = new Mock<IHolidayPlanRepository>();
            Mock<IAssociationProjectCollaboratorRepository> associationRepository = new Mock<IAssociationProjectCollaboratorRepository>();

            Mock<IPeriodDate> searchPeriodDate = new Mock<IPeriodDate>();
            searchPeriodDate.Setup(spd => spd.GetInitDate()).Returns(searchInitDate);
            searchPeriodDate.Setup(spd => spd.GetFinalDate()).Returns(searchEndDate);
            searchPeriodDate.Setup(spd => spd.ContainsWeekend()).Returns(true);

            var holidayPeriodsList = new List<IHolidayPeriod> { holidayPeriod.Object };

            holidayPlanRepository.Setup(r => r.FindAllHolidayPeriodsForCollaboratorBetweenDates(collab.Object, searchPeriodDate.Object)).Returns(holidayPeriodsList);

            HolidayPlanService service = new HolidayPlanService(associationRepository.Object, holidayPlanRepository.Object);

            // Act
            var result = service.FindAllHolidayPeriodsForCollaboratorBetweenDatesThatIncludeWeekends(collab.Object, searchPeriodDate.Object);

            // Assert
            Assert.Empty(result);
        } */

    public static IEnumerable<object[]> NonIntersectingDifferentWeekendDates()
    {
        yield return new object[] {
            new DateOnly(2025, 04, 07), new DateOnly(2025, 04, 14),
            new DateOnly(2025, 04, 01), new DateOnly(2025, 04, 08)
        };
        yield return new object[] {
            new DateOnly(2025, 04, 01), new DateOnly(2025, 04, 08),
            new DateOnly(2025, 04, 07), new DateOnly(2025, 04, 14)
        };
    }

    [Theory]
    [MemberData(nameof(NonIntersectingDifferentWeekendDates))]
    public void WhenRetrievingAllHolidayPeriodsForCollaboratorBetweenWithNonIntersectingPeriodsThatIncludeDifferentWeekends_ThenReturnEmpty(
        DateOnly holidayInitDate, DateOnly holidayEndDate,
        DateOnly searchInitDate, DateOnly searchEndDate
    )
    {
        // Arrange
        Mock<ICollaborator> collab = new Mock<ICollaborator>();

        Mock<IPeriodDate> holidayPeriodDate = new Mock<IPeriodDate>();
        holidayPeriodDate.Setup(hpd => hpd.GetInitDate()).Returns(holidayInitDate);
        holidayPeriodDate.Setup(hpd => hpd.GetFinalDate()).Returns(holidayEndDate);
        Mock<IHolidayPeriod> holidayPeriod = new Mock<IHolidayPeriod>();
        holidayPeriod.Setup(hp => hp.GetPeriodDate()).Returns(holidayPeriodDate.Object);
        holidayPeriod.Setup(hp => hp.ContainsWeekend()).Returns(true);

        Mock<IHolidayPlanRepository> holidayPlanRepository = new Mock<IHolidayPlanRepository>();
        Mock<IAssociationProjectCollaboratorRepository> associationRepository = new Mock<IAssociationProjectCollaboratorRepository>();

        Mock<IPeriodDate> searchPeriodDate = new Mock<IPeriodDate>();
        searchPeriodDate.Setup(spd => spd.GetInitDate()).Returns(searchInitDate);
        searchPeriodDate.Setup(spd => spd.GetFinalDate()).Returns(searchEndDate);
        searchPeriodDate.Setup(spd => spd.ContainsWeekend()).Returns(true);

        var holidayPeriodsList = new List<IHolidayPeriod> { holidayPeriod.Object };

        holidayPlanRepository.Setup(r => r.FindAllHolidayPeriodsForCollaboratorBetweenDates(collab.Object, searchPeriodDate.Object)).Returns(new List<IHolidayPeriod>());

        HolidayPlanService service = new HolidayPlanService(associationRepository.Object, holidayPlanRepository.Object);

        // Act
        var result = service.FindAllHolidayPeriodsForCollaboratorBetweenDatesThatIncludeWeekends(collab.Object, searchPeriodDate.Object);

        // Assert
        Assert.Empty(result);
    }
}