//using Infrastructure.Repositories;
//using Domain.Interfaces;
//using Moq;
//using Infrastructure.DataModel;
//using Microsoft.EntityFrameworkCore;
//using Domain.Visitor;
//using AutoMapper;
//using System.Diagnostics.CodeAnalysis;
//using Domain.IRepository;
//using Microsoft.EntityFrameworkCore.Query;

//namespace Infrastructure.Tests.HolidayPlanRepositoryTests;

//public class HolidayPlanRepositoryFindHolidayPlanByCollaboratorAsyncTests
//{
//    [Fact]
//    public async Task WhenPassingValidCollabId_ThenReturnsCorrectHolidayPlan()
//    {
//        // Arrange
//        var options = new DbContextOptionsBuilder<AbsanteeContext>()
//            .UseInMemoryDatabase(Guid.NewGuid().ToString()) // ensure isolation per test
//            .Options;

//        using var context = new AbsanteeContext(options);

//        var mapper = new Mock<IMapper<IHolidayPlan, HolidayPlanDataModel>>();
//        var hmapper = new Mock<IMapper<IHolidayPeriod, HolidayPeriodDataModel>>();

//        var expectedId = 1;
//        var expected = new Mock<IHolidayPlan>();
//        expected.Setup(e => e.GetCollaboratorId()).Returns(expectedId);

//        var hpdm = new HolidayPlanDataModel(expected.Object, hmapper.Object);
//        mapper.Setup(m => m.ToDomain(hpdm)).Returns(expected.Object);

//        context.HolidayPlans.Add(hpdm);
//        await context.SaveChangesAsync();

//        var repo = new HolidayPlanRepositoryEF(context, mapper.Object, hmapper.Object);        

//        // Act
//        var result = await repo.FindHolidayPlanByCollaboratorAsync(expectedId);

//        // Assert
//        Assert.Equal(expected.Object, result);
//    }

//    [Fact]
//    public async Task WhenPassingValidCollabId_ThenReturnsNull()
//    {
//         // Arrange
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
//        var result = await repo.FindHolidayPlanByCollaboratorAsync(4);

//        // Assert
//        Assert.Null(result);
//    }
//}