//using Moq;
//using Infrastructure.Repositories;
//using Domain.Interfaces;
//using AutoMapper;
//using Domain.Models;
//using Infrastructure.DataModel;
//using Domain.Visitor;
//using Microsoft.EntityFrameworkCore;

//namespace Infrastructure.Tests.HolidayPlanRepositoryTests;

//public class HolidayPlanRepositoryFindHolidayPeriodsByCollaboratorAsyncTests
//{
//    [Fact]
//    public async Task WhenFindingHolidayPeriodsByCollaboratorAsync_ThenReturnsCorrectPeriods()
//    {
//        // Arrange
//        var options = new DbContextOptionsBuilder<AbsanteeContext>()
//            .UseInMemoryDatabase(Guid.NewGuid().ToString()) // ensure isolation per test
//            .Options;

//        using var context = new AbsanteeContext(options);

//        var mapper = new Mock<IMapper<IHolidayPlan, HolidayPlanDataModel>>();
//        var hmapper = new Mock<IMapper<IHolidayPeriod, HolidayPeriodDataModel>>();

//        var hpId = 1;
//        var hp = new Mock<IHolidayPlan>();
//        hp.Setup(hp => hp.GetCollaboratorId()).Returns(hpId);
//        var expected = new Mock<List<IHolidayPeriod>>();
//        hp.Setup(hp => hp.GetHolidayPeriods()).Returns(expected.Object);

//        var hpdm = new HolidayPlanDataModel(hp.Object, hmapper.Object);
//        mapper.Setup(m => m.ToDomain(hpdm)).Returns(hp.Object);

//        context.HolidayPlans.Add(hpdm);
//        await context.SaveChangesAsync();

//        var repo = new HolidayPlanRepositoryEF(context, mapper.Object, hmapper.Object);        

//        // Act
//        var result = await repo.FindHolidayPeriodsByCollaboratorAsync(hpId);

//        // Assert
//        Assert.Equal(expected.Object, result);
//    }

//    [Fact]
//    public async Task WhenNotFindingHolidayPeriodsByCollaboratorAsync_ThenReturnsEmptyList()
//    {
//        // Arrange
//        var options = new DbContextOptionsBuilder<AbsanteeContext>()
//            .UseInMemoryDatabase(Guid.NewGuid().ToString()) // ensure isolation per test
//            .Options;

//        using var context = new AbsanteeContext(options);

//        var mapper = new Mock<IMapper<IHolidayPlan, HolidayPlanDataModel>>();
//        var hmapper = new Mock<IMapper<IHolidayPeriod, HolidayPeriodDataModel>>();

//        var hp1 = new Mock<IHolidayPlan>();
//        hp1.Setup(e => e.GetCollaboratorId()).Returns(1);
//        var hpdm1 = new HolidayPlanDataModel(hp1.Object, hmapper.Object);

//        var hp2 = new Mock<IHolidayPlan>();
//        hp2.Setup(e => e.GetCollaboratorId()).Returns(2);
//        var hpdm2 = new HolidayPlanDataModel(hp2.Object, hmapper.Object);

//        var hp3 = new Mock<IHolidayPlan>();
//        hp3.Setup(e => e.GetCollaboratorId()).Returns(3);
//        var hpdm3 = new HolidayPlanDataModel(hp3.Object, hmapper.Object);

//        context.HolidayPlans.Add(hpdm1);
//        context.HolidayPlans.Add(hpdm2);
//        context.HolidayPlans.Add(hpdm3);
//        await context.SaveChangesAsync();

//        var repo = new HolidayPlanRepositoryEF(context, mapper.Object, hmapper.Object);        

//        // Act
//        var result = await repo.FindHolidayPeriodsByCollaboratorAsync(4);

//        // Assert
//        Assert.Empty(result);
//    }
//}