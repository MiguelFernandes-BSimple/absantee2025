using Domain.Interfaces;
using Infrastructure.Interfaces;
using Application.Services;
using Moq;
using System.IO.Compression;
namespace Application.Tests.HolidayPlanServiceTests;

public class FindAllOverlappingHolidayPeriodsBetweenTwoCollaboratorsBetweenDates
{
    //UC20 Data
    public static IEnumerable<object[]> ValidPeriodToSearchOverlapping()
    {
        // intersection
        yield return new object[] {
            new DateOnly(2025, 04, 02), new DateOnly(2025, 04, 08),
            new DateOnly(2025, 04, 05), new DateOnly(2025, 04, 12),
            new DateOnly(2025, 04, 01), new DateOnly(2025, 04, 15),
        };
        // same holiday period
        yield return new object[] {
            new DateOnly(2025, 04, 05), new DateOnly(2025, 04, 12),
            new DateOnly(2025, 04, 05), new DateOnly(2025, 04, 12),
            new DateOnly(2025, 04, 01), new DateOnly(2025, 04, 15),
        };
        // same start holiday period date, end date after
        yield return new object[] {
                new DateOnly(2025, 04, 05), new DateOnly(2025, 04, 12),
                new DateOnly(2025, 04, 05), new DateOnly(2025, 04, 14),
                new DateOnly(2025, 04, 01), new DateOnly(2025, 04, 15),
        };
        // same end holiday period date, start date before
        yield return new object[] {
                new DateOnly(2025, 04, 05), new DateOnly(2025, 04, 11),
                new DateOnly(2025, 04, 04), new DateOnly(2025, 04, 11),
                new DateOnly(2025, 04, 01), new DateOnly(2025, 04, 15),
        };
        // one holiday period inside the other
        yield return new object[] {
                new DateOnly(2025, 04, 02), new DateOnly(2025, 04, 12),
                new DateOnly(2025, 04, 04), new DateOnly(2025, 04, 11),
                new DateOnly(2025, 04, 01), new DateOnly(2025, 04, 15),
        };
        // search for a specific day that contains both holiday periods
        yield return new object[] {
                new DateOnly(2025, 04, 02), new DateOnly(2025, 04, 12),
                new DateOnly(2025, 04, 04), new DateOnly(2025, 04, 11),
                new DateOnly(2025, 04, 10), new DateOnly(2025, 04, 10),
        };
        // holiday period 1 ends when holiday period 2 start
        yield return new object[] {
                new DateOnly(2025, 04, 04), new DateOnly(2025, 04, 10),
                new DateOnly(2025, 04, 10), new DateOnly(2025, 04, 15),
                new DateOnly(2025, 04, 01), new DateOnly(2025, 04, 15),
        };
        // holiday period 1 starts when holiday period 2 ends
        yield return new object[] {
                new DateOnly(2025, 04, 10), new DateOnly(2025, 04, 15),
                new DateOnly(2025, 04, 04), new DateOnly(2025, 04, 10),
                new DateOnly(2025, 04, 01), new DateOnly(2025, 04, 15),
        };
    }

    // UC20 
    [Theory]
    [MemberData(nameof(ValidPeriodToSearchOverlapping))]
    public void WhenGivenCorrectValues_ThenReturnOverlappingHolidayPeriodBetweenTwoCollabsInPeriod(
        DateOnly holidayPeriodStartDate1, DateOnly holidayPeriodFinalDate1,
        DateOnly holidayPeriodStartDate2, DateOnly holidayPeriodFinalDate2,
        DateOnly searchInitDate, DateOnly searchEndDate)
    {
        //arrange
        //collab1
        Mock<ICollaborator> collab1 = new Mock<ICollaborator>();

        var periodDateDouble1 = new Mock<IPeriodDate>();
        periodDateDouble1.Setup(pd => pd.GetInitDate()).Returns(holidayPeriodStartDate1);
        periodDateDouble1.Setup(pd => pd.GetFinalDate()).Returns(holidayPeriodFinalDate1);

        var periodDateDouble2 = new Mock<IPeriodDate>();
        periodDateDouble2.Setup(pd => pd.GetInitDate()).Returns(holidayPeriodStartDate2);
        periodDateDouble2.Setup(pd => pd.GetFinalDate()).Returns(holidayPeriodFinalDate2);

        Mock<IHolidayPeriod> holidayPeriod1 = new Mock<IHolidayPeriod>();
        holidayPeriod1.Setup(hp => hp.GetPeriodDate()).Returns(periodDateDouble1.Object);
        var holidayPeriodsList1 = new List<IHolidayPeriod> { holidayPeriod1.Object };

        var searchPeriodDateDouble = new Mock<IPeriodDate>();
        searchPeriodDateDouble.Setup(spd => spd.GetInitDate()).Returns(searchInitDate);
        searchPeriodDateDouble.Setup(spd => spd.GetFinalDate()).Returns(searchEndDate);

        //colab2
        Mock<ICollaborator> collab2 = new Mock<ICollaborator>();

        Mock<IHolidayPeriod> holidayPeriod2 = new Mock<IHolidayPeriod>();
        holidayPeriod2.Setup(hp => hp.GetPeriodDate()).Returns(periodDateDouble2.Object);
        var holidayPeriodsList2 = new List<IHolidayPeriod> { holidayPeriod2.Object };
        holidayPeriod1.Setup(hp => hp.Intersects(holidayPeriod2.Object)).Returns(true);

        Mock<IHolidayPlanRepository> holidayPlanRepository = new Mock<IHolidayPlanRepository>();
        Mock<IAssociationProjectCollaboratorRepository> associationRepository = new Mock<IAssociationProjectCollaboratorRepository>();
        holidayPlanRepository.Setup(r => r.FindAllHolidayPeriodsForCollaboratorBetweenDates(collab1.Object, searchPeriodDateDouble.Object)).Returns(holidayPeriodsList1);
        holidayPlanRepository.Setup(r => r.FindAllHolidayPeriodsForCollaboratorBetweenDates(collab2.Object, searchPeriodDateDouble.Object)).Returns(holidayPeriodsList2);

        HolidayPlanService service = new HolidayPlanService(associationRepository.Object, holidayPlanRepository.Object);

        //act
        var result = service.FindAllOverlappingHolidayPeriodsBetweenTwoCollaboratorsBetweenDates(collab1.Object, collab2.Object, searchPeriodDateDouble.Object);

        //assert
        Assert.Equal(holidayPeriodStartDate1, result.First().GetPeriodDate().GetInitDate());
        Assert.Equal(holidayPeriodFinalDate1, result.First().GetPeriodDate().GetFinalDate());
        Assert.Equal(holidayPeriodStartDate2, result.ToList()[1].GetPeriodDate().GetInitDate());
        Assert.Equal(holidayPeriodFinalDate2, result.ToList()[1].GetPeriodDate().GetFinalDate());
    }

    public static IEnumerable<object[]> SearchOverlappingPeriodsOutsideHolidayPeriod()
    {
        /* // dates intersect, but search date is outside
        yield return new object[] {
                    new DateOnly(2025, 04, 02), new DateOnly(2025, 04, 08),
                    new DateOnly(2025, 04, 05), new DateOnly(2025, 04, 12),
                    new DateOnly(2025, 04, 13), new DateOnly(2025, 04, 20),
            };
        // dates intersect, but search only contains 1 holiday period (1st date higher range)
        yield return new object[] {
                    new DateOnly(2025, 04, 02), new DateOnly(2025, 04, 12),
                    new DateOnly(2025, 04, 04), new DateOnly(2025, 04, 11),
                    new DateOnly(2025, 04, 12), new DateOnly(2025, 04, 12),
            };
        // dates intersect, but search only contains 1 holiday period (1st date lower range)
        yield return new object[] {
                    new DateOnly(2025, 04, 02), new DateOnly(2025, 04, 12),
                    new DateOnly(2025, 04, 04), new DateOnly(2025, 04, 11),
                    new DateOnly(2025, 04, 02), new DateOnly(2025, 04, 02),
            };
        // dates intersect, but search only contains 1 holiday period (2nd date higher range)
        yield return new object[] {
                    new DateOnly(2025, 04, 04), new DateOnly(2025, 04, 11),
                    new DateOnly(2025, 04, 02), new DateOnly(2025, 04, 12),
                    new DateOnly(2025, 04, 12), new DateOnly(2025, 04, 12),
            };
        // dates intersect, but search only contains 1 holiday period (2nd date lower range)
        yield return new object[] {
                    new DateOnly(2025, 04, 04), new DateOnly(2025, 04, 11),
                    new DateOnly(2025, 04, 02), new DateOnly(2025, 04, 12),
                    new DateOnly(2025, 04, 02), new DateOnly(2025, 04, 02),
            }; */
        // holiday periods don't intercept (2nd date after)
        yield return new object[] {
            new DateOnly(2025, 04, 02), new DateOnly(2025, 04, 10),
            new DateOnly(2025, 04, 11), new DateOnly(2025, 04, 15),
            new DateOnly(2025, 04, 01), new DateOnly(2025, 04, 15),
    };
        // holiday periods don't intercept  (1st date after)
        yield return new object[] {
            new DateOnly(2025, 04, 11), new DateOnly(2025, 04, 15),
            new DateOnly(2025, 04, 02), new DateOnly(2025, 04, 10),
            new DateOnly(2025, 04, 01), new DateOnly(2025, 04, 15),
        };
    }

    [Theory]
    [MemberData(nameof(SearchOverlappingPeriodsOutsideHolidayPeriod))]
    public void WhenGivenSearchPeriodOutsideOverlappingHoliadyPeriod_ThenReturnEmpty(
        DateOnly holidayPeriodStartDate1, DateOnly holidayPeriodFinalDate1,
        DateOnly holidayPeriodStartDate2, DateOnly holidayPeriodFinalDate2,
        DateOnly searchInitDate, DateOnly searchEndDate)
    {
        //arrange

        var searchPeriodDateDouble = new Mock<IPeriodDate>();
        searchPeriodDateDouble.Setup(spd => spd.GetInitDate()).Returns(searchInitDate);
        searchPeriodDateDouble.Setup(spd => spd.GetFinalDate()).Returns(searchEndDate);

        //collab1
        Mock<ICollaborator> collab1 = new Mock<ICollaborator>();

        var periodDateDouble = new Mock<IPeriodDate>();
        periodDateDouble.Setup(pd => pd.GetInitDate()).Returns(holidayPeriodStartDate1);
        periodDateDouble.Setup(pd => pd.GetFinalDate()).Returns(holidayPeriodFinalDate1);

        Mock<IHolidayPeriod> holidayPeriod1 = new Mock<IHolidayPeriod>();
        holidayPeriod1.Setup(hp => hp.GetPeriodDate()).Returns(periodDateDouble.Object);
        var holidayPeriodsList1 = new List<IHolidayPeriod> { holidayPeriod1.Object };

        //colab2
        Mock<ICollaborator> collab2 = new Mock<ICollaborator>();

        var periodDateDouble2 = new Mock<IPeriodDate>();
        periodDateDouble2.Setup(pd => pd.GetInitDate()).Returns(holidayPeriodStartDate2);
        periodDateDouble2.Setup(pd => pd.GetFinalDate()).Returns(holidayPeriodFinalDate2);

        Mock<IHolidayPeriod> holidayPeriod2 = new Mock<IHolidayPeriod>();
        holidayPeriod2.Setup(hp => hp.GetPeriodDate()).Returns(periodDateDouble2.Object);
        var holidayPeriodsList2 = new List<IHolidayPeriod> { holidayPeriod2.Object };

        Mock<IHolidayPlanRepository> holidayPlanRepository = new Mock<IHolidayPlanRepository>();
        Mock<IAssociationProjectCollaboratorRepository> associationRepository = new Mock<IAssociationProjectCollaboratorRepository>();
        holidayPlanRepository.Setup(r => r.FindAllHolidayPeriodsForCollaboratorBetweenDates(collab1.Object, searchPeriodDateDouble.Object)).Returns(holidayPeriodsList1);
        holidayPlanRepository.Setup(r => r.FindAllHolidayPeriodsForCollaboratorBetweenDates(collab2.Object, searchPeriodDateDouble.Object)).Returns(holidayPeriodsList2);

        HolidayPlanService service = new HolidayPlanService(associationRepository.Object, holidayPlanRepository.Object);

        //act
        var result = service.FindAllOverlappingHolidayPeriodsBetweenTwoCollaboratorsBetweenDates(collab1.Object, collab2.Object, searchPeriodDateDouble.Object);

        //assert
        Assert.Empty(result);
    }
}