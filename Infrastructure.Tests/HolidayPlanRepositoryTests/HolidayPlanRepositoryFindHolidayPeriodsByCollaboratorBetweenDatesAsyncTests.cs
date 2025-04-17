using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.Models;
using Domain.Visitor;
using Infrastructure.DataModel;
using Infrastructure.Mapper;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Infrastructure.Tests.HolidayPlanRepositoryTests;

public class HolidayPlanRepositoryFindHolidayPeriodsByCollaboratorBetweenDatesAsyncTests
{
    [Fact]
    public async Task WhenPassingCorrectDataAsync_ThenReturnsPeriodsByCollaboratorBetweenDates()
    {
        // arrange
        var options = new DbContextOptionsBuilder<AbsanteeContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()) // ensure isolation per test
                .Options;

        using var context = new AbsanteeContext(options);

        var holidayPeriodMapper = new Mock<IMapper<IHolidayPeriod, HolidayPeriodDataModel>>();

        var holidayPlan1 = new Mock<IHolidayPlan>();
        holidayPlan1.Setup(hp => hp.GetCollaboratorId()).Returns(1);

        var holidayPeriod = new Mock<IHolidayPeriod>();
        var holidayStart = new DateOnly(2025, 4, 10);
        var holidayEnd = new DateOnly(2025, 4, 20);
        var holidayPeriodDate = new PeriodDate(holidayStart, holidayEnd);
        holidayPeriod.Setup(hperiod => hperiod._periodDate).Returns(holidayPeriodDate);
        var holidayPeriods = new List<IHolidayPeriod>() { holidayPeriod.Object };
        holidayPlan1.Setup(hp => hp.GetHolidayPeriods()).Returns(holidayPeriods);

        var holidayPeriodDM1 = new HolidayPeriodDataModel(holidayPeriod.Object);
        var holidayPeriodsDM1 = new List<HolidayPeriodDataModel>() { holidayPeriodDM1 };
        holidayPeriodMapper.Setup(hpMapper => hpMapper.ToDataModel(holidayPeriods)).Returns(holidayPeriodsDM1);
        var holidayPlanDM1 = new HolidayPlanDataModel(holidayPlan1.Object, holidayPeriodMapper.Object);
        context.HolidayPlans.Add(holidayPlanDM1);

        var holidayPlan2 = new Mock<IHolidayPlan>();
        holidayPlan2.Setup(hp => hp.GetCollaboratorId()).Returns(2);

        holidayPlan2.Setup(hp => hp.GetHolidayPeriods()).Returns(holidayPeriods);

        var holidayPlanDM2 = new HolidayPlanDataModel(holidayPlan2.Object, holidayPeriodMapper.Object);
        context.HolidayPlans.Add(holidayPlanDM2);

        await context.SaveChangesAsync();

        var searchStart = new DateOnly(2025, 4, 15);
        var searchEnd = new DateOnly(2025, 4, 25);
        var searchPeriod = new PeriodDate(searchStart, searchEnd);


        var filteredHolidayPeriod = new List<HolidayPeriodDataModel>() { holidayPeriodDM1 };

        var expected = holidayPeriods;

        var mapperMock = new Mock<IMapper<IHolidayPlan, HolidayPlanDataModel>>();
        holidayPeriodMapper.Setup(hpMapper => hpMapper.ToDomain(filteredHolidayPeriod)).Returns(expected);

        var holidayPlanRepoEF = new HolidayPlanRepositoryEF(context, mapperMock.Object, holidayPeriodMapper.Object);

        // act
        var result = await holidayPlanRepoEF.FindHolidayPeriodsByCollaboratorBetweenDatesAsync(1, searchPeriod);

        // assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public async Task WhenPassingCorrectDataAsync_ThenReturnsEmptyList()
    {
        // arrange
        var holidayPlanDM1 = new Mock<IHolidayPlanVisitor>();
        var holidayPlanDM2 = new Mock<IHolidayPlanVisitor>();
        var holidayPlans = new List<HolidayPlanDataModel>
        {
            (HolidayPlanDataModel)holidayPlanDM1.Object,
            (HolidayPlanDataModel)holidayPlanDM2.Object
        }.AsQueryable();

        var mockSet = new Mock<DbSet<HolidayPlanDataModel>>();
        mockSet.As<IQueryable<HolidayPlanDataModel>>().Setup(m => m.Provider).Returns(holidayPlans.Provider);
        mockSet.As<IQueryable<HolidayPlanDataModel>>().Setup(m => m.Expression).Returns(holidayPlans.Expression);
        mockSet.As<IQueryable<HolidayPlanDataModel>>().Setup(m => m.ElementType).Returns(holidayPlans.ElementType);
        mockSet.As<IQueryable<HolidayPlanDataModel>>().Setup(m => m.GetEnumerator()).Returns(holidayPlans.GetEnumerator());

        var contextMock = new Mock<IAbsanteeContext>();
        contextMock.Setup(a => a.HolidayPlans).Returns(mockSet.Object);

        holidayPlanDM1.Setup(hp => hp.CollaboratorId).Returns(1);
        holidayPlanDM2.Setup(hp => hp.CollaboratorId).Returns(2);

        var searchPeriod = new Mock<PeriodDate>();

        var holidayPeriodMock = new Mock<IHolidayPeriod>();
        holidayPeriodMock.Setup(hpd => hpd.Intersects(searchPeriod.Object)).Returns(true);

        var expectedPeriod = new List<IHolidayPeriod>();

        var mapperMock = new Mock<HolidayPlanMapper>();
        var holidayPeriodMapper = new Mock<HolidayPeriodMapper>();

        var holidayPlanRepoEF = new HolidayPlanRepositoryEF((AbsanteeContext)contextMock.Object, mapperMock.Object, holidayPeriodMapper.Object);

        // act
        var result = await holidayPlanRepoEF.FindHolidayPeriodsByCollaboratorBetweenDatesAsync(3, searchPeriod.Object);

        // assert
        Assert.Equal(result, expectedPeriod);
    }
}
