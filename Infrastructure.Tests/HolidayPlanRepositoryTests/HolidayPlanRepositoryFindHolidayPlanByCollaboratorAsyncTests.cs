using Infrastructure.Repositories;
using Domain.Interfaces;
using Moq;
using Infrastructure.DataModel;
using Microsoft.EntityFrameworkCore;
using Domain.Visitor;
using Infrastructure.Mapper;
using System.Diagnostics.CodeAnalysis;
using Domain.IRepository;
using Microsoft.EntityFrameworkCore.Query;

namespace Infrastructure.Tests.HolidayPlanRepositoryTests;

public class HolidayPlanRepositoryFindHolidayPlanByCollaboratorAsyncTests
{
    [Fact]
    public async Task WhenPassingValidCollabId_ThenReturnsCorrectHolidayPlan()
    {
        var options = new DbContextOptionsBuilder<AbsanteeContext>()
            .UseInMemoryDatabase(databaseName: "TestDb")
            .Options;

        using var context = new AbsanteeContext(options);
    }

    [Fact]
    public async Task WhenPassingValidCollabIsdfsdd_ThenReturnsCorrectHolidayPlan()
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

        holidayPlanDM1.Setup(hp => hp.CollaboratorId).Returns(1);
        holidayPlanDM2.Setup(hp => hp.CollaboratorId).Returns(2);
        holidayPlanDM3.Setup(hp => hp.CollaboratorId).Returns(3);

        var expectedId = 1;
        var expected = new Mock<IHolidayPlan>();
        expected.Setup(e => e.GetCollaboratorId()).Returns(expectedId);

        var mapperDouble = new Mock<IMapper<IHolidayPlan, HolidayPlanDataModel>>();
        mapperDouble.Setup(md => md.ToDomain((HolidayPlanDataModel)holidayPlanDM1.Object)).Returns(expected.Object);

        var holidayPlanRepoDouble = new HolidayPlanRepositoryEF((AbsanteeContext)mockContext.Object, (HolidayPlanMapper)mapperDouble.Object);

        // act
        var result = await holidayPlanRepoDouble.FindHolidayPlanByCollaboratorAsync(expectedId);

        // assert
        Assert.Equal(result, expected.Object);
    }

    [Fact]
    public async Task WhenPassingValidCollabId_ThenReturnsNull()
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

        holidayPlanDM1.Setup(hp => hp.CollaboratorId).Returns(1);
        holidayPlanDM2.Setup(hp => hp.CollaboratorId).Returns(2);
        holidayPlanDM3.Setup(hp => hp.CollaboratorId).Returns(3);

        var expectedId = 4;
        var expected = new Mock<IHolidayPlan>();
        expected.Setup(e => e.GetCollaboratorId()).Returns(expectedId);

        var mapperDouble = new Mock<IMapper<IHolidayPlan, HolidayPlanDataModel>>();

        var holidayPlanRepoDouble = new HolidayPlanRepositoryEF((AbsanteeContext)mockContext.Object, (HolidayPlanMapper)mapperDouble.Object);

        // act
        var result = await holidayPlanRepoDouble.FindHolidayPlanByCollaboratorAsync(expectedId);

        // assert
        Assert.Null(result);
    }
}