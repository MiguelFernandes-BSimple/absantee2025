using Infrastructure.Repositories;
using Domain.Interfaces;
using Moq;
using Domain.Visitor;
using Infrastructure.DataModel;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Mapper;
using Domain.Models;

namespace Infrastructure.Tests.HolidayPlanRepositoryTests;

public class FindAllWithHolidayPeriodsLongerThanAsync
{
    [Fact]
    public async Task WhenFindingHolidayPlansWithPeriodsLongerThanAsync_ReturnsCorrectList()
    {
        // arrange
        var holidayPlanDM1 = new Mock<IHolidayPlanVisitor>();
        var holidayPlanDM2 = new Mock<IHolidayPlanVisitor>();
        var holidayPlanDM3 = new Mock<IHolidayPlanVisitor>();

        var holidayPlans = new List<HolidayPlanDataModel>{
            (HolidayPlanDataModel)holidayPlanDM1.Object,
            (HolidayPlanDataModel)holidayPlanDM2.Object,
            (HolidayPlanDataModel)holidayPlanDM3.Object
        }.AsQueryable();

        var mockSet = new Mock<DbSet<HolidayPlanDataModel>>();
        mockSet.As<IQueryable<HolidayPlanDataModel>>().Setup(m => m.Provider).Returns(holidayPlans.Provider);
        mockSet.As<IQueryable<HolidayPlanDataModel>>().Setup(m => m.Expression).Returns(holidayPlans.Expression);
        mockSet.As<IQueryable<HolidayPlanDataModel>>().Setup(m => m.ElementType).Returns(holidayPlans.ElementType);
        mockSet.As<IQueryable<HolidayPlanDataModel>>().Setup(m => m.GetEnumerator()).Returns(() => holidayPlans.GetEnumerator());

        var mockContext = new Mock<IAbsanteeContext>();
        mockContext.Setup(mc => mc.HolidayPlans).Returns(mockSet.Object);

        var days = 5;
        var holidayPeriodDouble1 = new Mock<IHolidayPeriod>();
        holidayPeriodDouble1.Setup(hpd => hpd.IsLongerThan(days)).Returns(true);
        var holidayPeriodDouble2 = new Mock<IHolidayPeriod>();
        holidayPeriodDouble2.Setup(hpd => hpd.IsLongerThan(days)).Returns(true);

        var holidayPeriodsDouble = new List<IHolidayPeriod>(){
            holidayPeriodDouble1.Object, holidayPeriodDouble2.Object
        };

        holidayPlanDM1.Setup(hp => hp.GetHolidayPeriods()).Returns(holidayPeriodsDouble);

        var expected = new List<IHolidayPlan> { (HolidayPlan)holidayPlanDM1.Object };

        var mapperDouble = new Mock<IMapper<IHolidayPlan, HolidayPlanDataModel>>();
        mapperDouble.Setup(md => md.ToDomain(holidayPlans)).Returns(expected);

        var holidayPlanRepo = new HolidayPlanRepositoryEF((AbsanteeContext)mockContext.Object, (HolidayPlanMapper)mapperDouble.Object);

        // act
        var result = await holidayPlanRepo.FindAllWithHolidayPeriodsLongerThanAsync(days);

        // assert
        Assert.Equal(result, expected);
    }
}