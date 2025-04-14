using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.Visitor;
using Infrastructure.DataModel;
using Infrastructure.Mapper;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Infrastructure.Tests.HolidayPlanRepositoryTests;

public class FindHolidayPeriodsByCollaboratorBetweenDatesAsync
{
    [Fact]
    public async Task WhenPassingCorrectDataAsync_ThenReturnsPeriodsByCollaboratorBetweenDates()
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

        var expectedId = 1;
        var searchPeriod = new Mock<IPeriodDate>();

        var holidayPeriodMock = new Mock<IHolidayPeriod>();
        holidayPeriodMock.Setup(hpd => hpd.Intersects(searchPeriod.Object)).Returns(true);

        var expectedPeriod = new List<IHolidayPeriod> { holidayPeriodMock.Object };

        var mapperMock = new Mock<HolidayPlanMapper>();

        var holidayPlanRepoEF = new HolidayPlanRepositoryEF((AbsanteeContext)contextMock.Object, mapperMock.Object);

        // act
        var result = await holidayPlanRepoEF.FindHolidayPeriodsByCollaboratorBetweenDatesAsync(expectedId, searchPeriod.Object);

        // assert
        Assert.Equal(result, expectedPeriod);
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

        var expectedId = 1;
        var searchPeriod = new Mock<IPeriodDate>();

        var holidayPeriodMock = new Mock<IHolidayPeriod>();
        holidayPeriodMock.Setup(hpd => hpd.Intersects(searchPeriod.Object)).Returns(false);

        var expectedPeriod = new List<IHolidayPeriod>();

        var mapperMock = new Mock<HolidayPlanMapper>();

        var holidayPlanRepoEF = new HolidayPlanRepositoryEF((AbsanteeContext)contextMock.Object, mapperMock.Object);

        // act
        var result = await holidayPlanRepoEF.FindHolidayPeriodsByCollaboratorBetweenDatesAsync(expectedId, searchPeriod.Object);

        // assert
        Assert.Equal(result, expectedPeriod);
    }
}
