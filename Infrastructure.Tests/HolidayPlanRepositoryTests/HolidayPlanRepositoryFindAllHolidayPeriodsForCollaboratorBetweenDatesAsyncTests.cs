using Infrastructure.Repositories;
using Domain.Interfaces;
using Moq;
using Domain.Models;
using Domain.Visitor;
using Infrastructure.DataModel;
using Infrastructure.Mapper;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Tests.HolidayPlanRepositoryTests;

public class HolidayPlanRepositoryFindAllHolidayPeriodsForCollaboratorBetweenDatesAsyncTests
{
    [Theory]
    [InlineData("2020-01-01", "2020-12-31")]
    [InlineData("2020-01-02", "2020-12-30")]
    public async Task WhenPassinValidDatesAsync_ThenReturnsCorrectPeriod(string init1Str, string final1Str)
    {
        // Arrange
        var searchingPeriodDate = new Mock<PeriodDate>();
        searchingPeriodDate.Setup(pd => pd.GetInitDate()).Returns(new DateOnly(2020, 1, 1));
        searchingPeriodDate.Setup(pd => pd.GetFinalDate()).Returns(new DateOnly(2020, 12, 31));
        var holidayPlanDM1 = new Mock<IHolidayPlanVisitor>();
        var holidayPlanDM2 = new Mock<IHolidayPlanVisitor>();
        var holidayPlans = new List<HolidayPlanDataModel>
            {
                (HolidayPlanDataModel)holidayPlanDM1.Object,
                (HolidayPlanDataModel)holidayPlanDM2.Object,
            }.AsQueryable();

        var mockSet = new Mock<DbSet<HolidayPlanDataModel>>();
        mockSet.As<IQueryable<HolidayPlanDataModel>>().Setup(m => m.Provider).Returns(holidayPlans.Provider);
        mockSet.As<IQueryable<HolidayPlanDataModel>>().Setup(m => m.Expression).Returns(holidayPlans.Expression);
        mockSet.As<IQueryable<HolidayPlanDataModel>>().Setup(m => m.ElementType).Returns(holidayPlans.ElementType);
        mockSet.As<IQueryable<HolidayPlanDataModel>>().Setup(m => m.GetEnumerator()).Returns(holidayPlans.GetEnumerator());

        var absanteeMock = new Mock<IAbsanteeContext>();
        absanteeMock.Setup(a => a.HolidayPlans).Returns(mockSet.Object);

        holidayPlanDM1.Setup(hp => hp.CollaboratorId).Returns(0);
        holidayPlanDM2.Setup(hp => hp.CollaboratorId).Returns(1);

        var holidayPeriod = new Mock<IHolidayPeriod>();
        var periodDate = new Mock<PeriodDate>();
        holidayPeriod.Setup(hperiod => hperiod._periodDate).Returns(periodDate.Object);

        var init1 = DateOnly.Parse(init1Str);
        var final1 = DateOnly.Parse(final1Str);
        periodDate.Setup(pdate => pdate.GetInitDate()).Returns(init1);
        periodDate.Setup(pdate => pdate.GetFinalDate()).Returns(final1);
        var holidayPeriods1 = new List<IHolidayPeriod>()
        {
            holidayPeriod.Object
        };
        var holidayPeriods2 = new List<IHolidayPeriod>()
        {
            holidayPeriod.Object
        };

        holidayPlanDM1.Setup(hp => hp.GetHolidayPeriods()).Returns(holidayPeriods1);
        holidayPlanDM2.Setup(hp => hp.GetHolidayPeriods()).Returns(holidayPeriods2);

        var expected = new List<IHolidayPeriod>()
        {
            holidayPeriod.Object,
            holidayPeriod.Object
        };

        var holidayPlanMapperMock = new Mock<IMapper<HolidayPlan, HolidayPlanDataModel>>();
        holidayPlanMapperMock.Setup(hpMap => hpMap.ToDomain(holidayPlans));
        var holidayPlanRepo = new HolidayPlanRepositoryEF((AbsanteeContext)absanteeMock.Object, (HolidayPlanMapper)holidayPlanMapperMock.Object);

        // Act
        var result = await holidayPlanRepo.FindAllHolidayPeriodsForCollaboratorBetweenDatesAsync(1, periodDate.Object);

        // Assert
        Assert.True(result.SequenceEqual(expected));
    }

    [Theory]
    [InlineData("2019-01-01", "2020-12-31")]
    [InlineData("2020-01-01", "2021-12-31")]
    [InlineData("2019-01-02", "2021-12-30")]
    public async Task WhenHolidayPeriodIsOutOfRangeThanTheOneBeingSearchedForAsync_ThenReturnsEmptyList(string init1Str, string final1Str)
    {
        // Arrange
        var searchingPeriodDate = new Mock<PeriodDate>();
        searchingPeriodDate.Setup(pd => pd.GetInitDate()).Returns(new DateOnly(2020, 1, 1));
        searchingPeriodDate.Setup(pd => pd.GetFinalDate()).Returns(new DateOnly(2020, 12, 31));
        var holidayPlanDM1 = new Mock<IHolidayPlanVisitor>();
        var holidayPlanDM2 = new Mock<IHolidayPlanVisitor>();
        var holidayPlans = new List<HolidayPlanDataModel>
            {
                (HolidayPlanDataModel)holidayPlanDM1.Object,
                (HolidayPlanDataModel)holidayPlanDM2.Object,
            }.AsQueryable();

        var mockSet = new Mock<DbSet<HolidayPlanDataModel>>();
        mockSet.As<IQueryable<HolidayPlanDataModel>>().Setup(m => m.Provider).Returns(holidayPlans.Provider);
        mockSet.As<IQueryable<HolidayPlanDataModel>>().Setup(m => m.Expression).Returns(holidayPlans.Expression);
        mockSet.As<IQueryable<HolidayPlanDataModel>>().Setup(m => m.ElementType).Returns(holidayPlans.ElementType);
        mockSet.As<IQueryable<HolidayPlanDataModel>>().Setup(m => m.GetEnumerator()).Returns(holidayPlans.GetEnumerator());

        var absanteeMock = new Mock<IAbsanteeContext>();
        absanteeMock.Setup(a => a.HolidayPlans).Returns(mockSet.Object);

        holidayPlanDM1.Setup(hp => hp.CollaboratorId).Returns(0);
        holidayPlanDM2.Setup(hp => hp.CollaboratorId).Returns(1);

        var holidayPeriod = new Mock<IHolidayPeriod>();
        var periodDate = new Mock<PeriodDate>();
        holidayPeriod.Setup(hperiod => hperiod._periodDate).Returns(periodDate.Object);

        var init1 = DateOnly.Parse(init1Str);
        var final1 = DateOnly.Parse(final1Str);
        periodDate.Setup(pdate => pdate.GetInitDate()).Returns(init1);
        periodDate.Setup(pdate => pdate.GetFinalDate()).Returns(final1);
        var holidayPeriods1 = new List<IHolidayPeriod>()
        {
            holidayPeriod.Object
        };
        var holidayPeriods2 = new List<IHolidayPeriod>()
        {
            holidayPeriod.Object
        };

        holidayPlanDM1.Setup(hp => hp.GetHolidayPeriods()).Returns(holidayPeriods1);
        holidayPlanDM2.Setup(hp => hp.GetHolidayPeriods()).Returns(holidayPeriods2);

        var expected = new List<IHolidayPeriod>();

        var holidayPlanMapperMock = new Mock<IMapper<HolidayPlan, HolidayPlanDataModel>>();
        holidayPlanMapperMock.Setup(hpMap => hpMap.ToDomain(holidayPlans));
        var holidayPlanRepo = new HolidayPlanRepositoryEF((AbsanteeContext)absanteeMock.Object, (HolidayPlanMapper)holidayPlanMapperMock.Object);

        // Act
        var result = await holidayPlanRepo.FindAllHolidayPeriodsForCollaboratorBetweenDatesAsync(1, periodDate.Object);

        // Assert
        Assert.True(result.SequenceEqual(expected));
    }

    [Fact]
    public async Task WhenCollaboratorHasNoHolidayPlansAsync_ThenReturnsEmptyList()
    {
        // Arrange
        var searchingPeriodDate = new Mock<PeriodDate>();
        searchingPeriodDate.Setup(pd => pd.GetInitDate()).Returns(new DateOnly(2020, 1, 1));
        searchingPeriodDate.Setup(pd => pd.GetFinalDate()).Returns(new DateOnly(2020, 12, 31));
        var holidayPlanDM1 = new Mock<IHolidayPlanVisitor>();
        var holidayPlanDM2 = new Mock<IHolidayPlanVisitor>();
        var holidayPlans = new List<HolidayPlanDataModel>
            {
                (HolidayPlanDataModel)holidayPlanDM1.Object,
                (HolidayPlanDataModel)holidayPlanDM2.Object,
            }.AsQueryable();

        var mockSet = new Mock<DbSet<HolidayPlanDataModel>>();
        mockSet.As<IQueryable<HolidayPlanDataModel>>().Setup(m => m.Provider).Returns(holidayPlans.Provider);
        mockSet.As<IQueryable<HolidayPlanDataModel>>().Setup(m => m.Expression).Returns(holidayPlans.Expression);
        mockSet.As<IQueryable<HolidayPlanDataModel>>().Setup(m => m.ElementType).Returns(holidayPlans.ElementType);
        mockSet.As<IQueryable<HolidayPlanDataModel>>().Setup(m => m.GetEnumerator()).Returns(holidayPlans.GetEnumerator());

        var absanteeMock = new Mock<IAbsanteeContext>();
        absanteeMock.Setup(a => a.HolidayPlans).Returns(mockSet.Object);

        holidayPlanDM1.Setup(hp => hp.CollaboratorId).Returns(0);
        holidayPlanDM2.Setup(hp => hp.CollaboratorId).Returns(1);

        var holidayPlanMapperMock = new Mock<IMapper<HolidayPlan, HolidayPlanDataModel>>();
        holidayPlanMapperMock.Setup(hpMap => hpMap.ToDomain(holidayPlans));
        var holidayPlanRepo = new HolidayPlanRepositoryEF((AbsanteeContext)absanteeMock.Object, (HolidayPlanMapper)holidayPlanMapperMock.Object);

        // Act
        var result = await holidayPlanRepo.FindAllHolidayPeriodsForCollaboratorBetweenDatesAsync(2, It.IsAny<PeriodDate>());

        // Assert
        Assert.Empty(result);
    }
}