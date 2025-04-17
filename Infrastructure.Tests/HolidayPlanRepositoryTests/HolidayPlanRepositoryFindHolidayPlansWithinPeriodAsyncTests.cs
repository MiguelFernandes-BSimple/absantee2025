using System.Runtime.CompilerServices;
using Domain.Interfaces;
using Domain.Models;
using Domain.Visitor;
using Infrastructure.DataModel;
using Infrastructure.Mapper;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Infrastructure.Tests.HolidayPlanRepositoryTests;

public class HolidayPlanRepositoryFindHolidayPlansWithinPeriodTests
{
    [Theory]
    [InlineData("2020-01-01", "2020-12-31")]
    [InlineData("2020-01-02", "2020-12-30")]
    public async Task WhenCollaboratorHasHolidayPeriodWithinDateRangeAsync_ThenReturnsHolidayPlan(string init1Str, string final1Str)
    {
        // Arrange
        var options = new DbContextOptionsBuilder<AbsanteeContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()) // ensure isolation per test
            .Options;

        using var context = new AbsanteeContext(options);

        var mapper = new Mock<IMapper<IHolidayPlan, HolidayPlanDataModel>>();

        var hplan = new Mock<IHolidayPlan>();
        var hperiod = new Mock<IHolidayPeriod>();
        var period = new PeriodDate(DateOnly.Parse(init1Str), DateOnly.Parse(final1Str));
        hperiod.Setup(h => h._periodDate).Returns(period);

        var hlist = new List<IHolidayPeriod> {hperiod.Object};
        hplan.Setup(hp => hp.GetHolidayPeriods()).Returns(hlist);

        var hmapper = new Mock<IMapper<IHolidayPeriod, HolidayPeriodDataModel>>();
        hmapper.Setup(h => h.ToDataModel(hplan.Object.GetHolidayPeriods())).Returns(
            new List<HolidayPeriodDataModel>{
                new HolidayPeriodDataModel(hperiod.Object)
            });

        var hpdm = new HolidayPlanDataModel(hplan.Object, hmapper.Object);
        context.HolidayPlans.Add(hpdm);

        await context.SaveChangesAsync();

        var searchingPeriodDate = new PeriodDate(new DateOnly(2020, 1, 1), new DateOnly(2020, 12, 31));

        var repo = new HolidayPlanRepositoryEF(context, mapper.Object, hmapper.Object);

        var hpdmExpected = new List<HolidayPlanDataModel> {hpdm};
        var expected = new List<IHolidayPlan> {hplan.Object};

        mapper.Setup(hp => hp.ToDomain(hpdmExpected)).Returns(expected);

        // Act
        var result = await repo.FindHolidayPlansWithinPeriodAsync(searchingPeriodDate);

        // Assert
        Assert.True(result.SequenceEqual(expected));
    }

    [Theory]
    [InlineData("2019-01-01", "2020-12-31")]
    [InlineData("2020-01-01", "2021-12-31")]
    [InlineData("2019-01-02", "2021-12-30")]
    public async Task WhenNoCollaboratorsHaveHolidayPeriodsInDateRangeAsync_ThenReturnsEmptyList(string init1Str, string final1Str)
    {
        // Arrange
        var searchingPeriodDate = new Mock<PeriodDate>();
        searchingPeriodDate.Setup(pd => pd.GetInitDate()).Returns(new DateOnly(2020, 1, 1));
        searchingPeriodDate.Setup(pd => pd.GetFinalDate()).Returns(new DateOnly(2020, 12, 31));
        var holidayPlanDM1 = new Mock<IHolidayPlanVisitor>();
        var holidayPlans = new List<HolidayPlanDataModel>
            {
                (HolidayPlanDataModel)holidayPlanDM1.Object,
            }.AsQueryable();

        var mockSet = new Mock<DbSet<HolidayPlanDataModel>>();
        mockSet.As<IQueryable<HolidayPlanDataModel>>().Setup(m => m.Provider).Returns(holidayPlans.Provider);
        mockSet.As<IQueryable<HolidayPlanDataModel>>().Setup(m => m.Expression).Returns(holidayPlans.Expression);
        mockSet.As<IQueryable<HolidayPlanDataModel>>().Setup(m => m.ElementType).Returns(holidayPlans.ElementType);
        mockSet.As<IQueryable<HolidayPlanDataModel>>().Setup(m => m.GetEnumerator()).Returns(holidayPlans.GetEnumerator());

        var absanteeMock = new Mock<IAbsanteeContext>();
        absanteeMock.Setup(a => a.HolidayPlans).Returns(mockSet.Object);

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

        holidayPlanDM1.Setup(hp => hp.GetHolidayPeriods()).Returns(holidayPeriods1);

        var expected = new List<IHolidayPlan>();

        var holidayPlanMapperMock = new Mock<IMapper<HolidayPlan, HolidayPlanDataModel>>();
        holidayPlanMapperMock.Setup(hpMap => hpMap.ToDomain(holidayPlans));
        var holidayPeriodMapper = new Mock<HolidayPeriodMapper>();

        var holidayPlanRepo = new HolidayPlanRepositoryEF((AbsanteeContext)absanteeMock.Object, (HolidayPlanMapper)holidayPlanMapperMock.Object, holidayPeriodMapper.Object);

        // Act
        var result = await holidayPlanRepo.FindHolidayPlansWithinPeriodAsync(searchingPeriodDate.Object);

        // Assert
        Assert.True(result.SequenceEqual(expected));
    }
}

