using Infrastructure.Repositories;
using Domain.Interfaces;
using Moq;
using Infrastructure.Mapper;
using Infrastructure.DataModel;
using Domain.Visitor;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Tests.HolidayPlanRepositoryTests;

public class HolidayPlanRepositoryFindAllHolidayPeriodsLongerThanForCollaboratorBetweenDatesAsyncTests
{
    [Fact]
    public async Task WhenGivenBadCollaboratorAndDatesAndLengthAsync_ThenReturnEmptyLists()
    {
        // Arrange
        Mock<IAbsanteeContext> contextDouble = new Mock<IAbsanteeContext>();
        Mock<IMapper<IHolidayPlan, HolidayPlanDataModel>> mapper =
            new Mock<IMapper<IHolidayPlan, HolidayPlanDataModel>>();

        // Dbset data
        Mock<IHolidayPlanVisitor> hpDM1 = new Mock<IHolidayPlanVisitor>();
        Mock<IHolidayPlanVisitor> hpDM2 = new Mock<IHolidayPlanVisitor>();
        var holidayPlans = new List<HolidayPlanDataModel>
        {
            (HolidayPlanDataModel)hpDM1.Object,
            (HolidayPlanDataModel)hpDM2.Object
        }.AsQueryable();

        var mockSet = new Mock<DbSet<HolidayPlanDataModel>>();
        mockSet.As<IQueryable<HolidayPlanDataModel>>().Setup(m => m.Provider).Returns(holidayPlans.Provider);
        mockSet.As<IQueryable<HolidayPlanDataModel>>().Setup(m => m.Expression).Returns(holidayPlans.Expression);
        mockSet.As<IQueryable<HolidayPlanDataModel>>().Setup(m => m.ElementType).Returns(holidayPlans.ElementType);
        mockSet.As<IQueryable<HolidayPlanDataModel>>().Setup(m => m.GetEnumerator()).Returns(holidayPlans.GetEnumerator());

        // Period to search
        Mock<PeriodDate> periodDate = new Mock<PeriodDate>();

        long collab1Id = 1;
        long collab2Id = 2;
        int days = 4;

        // Id to seach - no holidayplan with collab id
        long toSearch = 3;

        // Define collab id for each holiday plan
        hpDM1.Setup(hp => hp.CollaboratorId).Returns(collab1Id);
        hpDM2.Setup(hp => hp.CollaboratorId).Returns(collab2Id);

        // Instantiate repository
        var hpRepo = new HolidayPlanRepositoryEF((AbsanteeContext)contextDouble.Object, (HolidayPlanMapper)mapper.Object);

        // Act
        var result = await hpRepo.FindAllHolidayPeriodsLongerThanForCollaboratorBetweenDatesAsync(toSearch, periodDate.Object, days);

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public async Task WhenGivenGoodCollaboratorAndDatesAndLengthAsync_ThenReturnPeriods()
    {
        // Arrange
        Mock<IAbsanteeContext> contextDouble = new Mock<IAbsanteeContext>();
        Mock<IMapper<IHolidayPlan, HolidayPlanDataModel>> mapper =
            new Mock<IMapper<IHolidayPlan, HolidayPlanDataModel>>();

        // Dbset data
        Mock<IHolidayPlanVisitor> hpDM1 = new Mock<IHolidayPlanVisitor>();
        Mock<IHolidayPlanVisitor> hpDM2 = new Mock<IHolidayPlanVisitor>();

        var holidayPlans = new List<HolidayPlanDataModel>
        {
            (HolidayPlanDataModel)hpDM1.Object,
            (HolidayPlanDataModel)hpDM2.Object
        }.AsQueryable();

        var mockSet = new Mock<DbSet<HolidayPlanDataModel>>();
        mockSet.As<IQueryable<HolidayPlanDataModel>>().Setup(m => m.Provider).Returns(holidayPlans.Provider);
        mockSet.As<IQueryable<HolidayPlanDataModel>>().Setup(m => m.Expression).Returns(holidayPlans.Expression);
        mockSet.As<IQueryable<HolidayPlanDataModel>>().Setup(m => m.ElementType).Returns(holidayPlans.ElementType);
        mockSet.As<IQueryable<HolidayPlanDataModel>>().Setup(m => m.GetEnumerator()).Returns(holidayPlans.GetEnumerator());

        long collab1Id = 1;
        long collab2Id = 2;

        int searchDays = 4;
        int days = 5;

        // Define collab id for each holiday plan
        hpDM1.Setup(hp => hp.CollaboratorId).Returns(collab1Id);
        hpDM2.Setup(hp => hp.CollaboratorId).Returns(collab2Id);

        Mock<IHolidayPeriod> hp1Period = new Mock<IHolidayPeriod>();
        Mock<PeriodDate> hpPeriodDate = new Mock<PeriodDate>();

        hp1Period.Setup(hp => hp.GetPeriodDate()).Returns(hpPeriodDate.Object);

        // Setup both hoiday plans with the same holidayPeriod list
        List<IHolidayPeriod> holidayPeriods = new List<IHolidayPeriod> { hp1Period.Object };

        hpDM1.Setup(hp => hp.GetHolidayPeriods()).Returns(holidayPeriods);
        hpDM2.Setup(hp => hp.GetHolidayPeriods()).Returns(holidayPeriods);

        // Period to search
        Mock<PeriodDate> periodDate = new Mock<PeriodDate>();

        // Restrictions
        periodDate.Setup(pd => pd.Contains(hpPeriodDate.Object)).Returns(true);
        // It has to be bigger than days
        periodDate.Setup(pd => pd.Duration()).Returns(days);

        // Instantiate repository
        var hpRepo = new HolidayPlanRepositoryEF((AbsanteeContext)contextDouble.Object, (HolidayPlanMapper)mapper.Object);

        // Act
        var result = await hpRepo.FindAllHolidayPeriodsLongerThanForCollaboratorBetweenDatesAsync(collab2Id, periodDate.Object, searchDays);

        // Assert
        Assert.Equal(holidayPeriods, result);
    }
}