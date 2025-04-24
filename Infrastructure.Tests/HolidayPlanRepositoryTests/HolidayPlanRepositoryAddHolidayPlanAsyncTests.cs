//using Domain.Interfaces;
//using Moq;
//using Infrastructure.Repositories;
//using Infrastructure.DataModel;
//using Microsoft.EntityFrameworkCore;
//using Domain.Visitor;
//using AutoMapper;
//using Domain.Models;


//namespace Infrastructure.Tests.HolidayPlanRepositoryTests;

//public class HolidayPlanRepositoryAddHolidayPlanAsyncTests
//{
//    [Fact]
//    public async Task WhenAddingCorrectHolidayPlanToRepositoryAsync_ThenReturnTrue()
//    {
//        // Arrange
//        var options = new DbContextOptionsBuilder<AbsanteeContext>()
//            .UseInMemoryDatabase(Guid.NewGuid().ToString()) // ensure isolation per test
//            .Options;

//        using var context = new AbsanteeContext(options);
        
//        var doubleHolidayPlanMapper = new Mock<IMapper<IHolidayPlan, HolidayPlanDataModel>>();
//        var holidayPeriodMapper = new Mock<IMapper<IHolidayPeriod, HolidayPeriodDataModel>>();

//        long collabId = 1;
        
//        var doubleHolidayPlan = new Mock<IHolidayPlan>();
//        doubleHolidayPlan.Setup(hp => hp.GetCollaboratorId()).Returns(collabId);

//        var HolidayPlanDM = new HolidayPlanDataModel(doubleHolidayPlan.Object, holidayPeriodMapper.Object);

//        doubleHolidayPlanMapper.Setup(hpm => hpm.ToDataModel(doubleHolidayPlan.Object)).Returns(HolidayPlanDM);

//        var HolidayPlanRepositoryEF = new HolidayPlanRepositoryEF(context, doubleHolidayPlanMapper.Object, holidayPeriodMapper.Object);

//        // Act
//        bool result = await HolidayPlanRepositoryEF.AddHolidayPlanAsync(doubleHolidayPlan.Object);

//        // Assert
//        Assert.True(result);
//    }

//    [Fact]
//    public async Task WhenAddingHolidayPlanWithRepeatedCollaboratorToRepositoryAsync_ThenReturnFalse()
//    {
//        // Arrange
//        var options = new DbContextOptionsBuilder<AbsanteeContext>()
//            .UseInMemoryDatabase(Guid.NewGuid().ToString()) // ensure isolation per test
//            .Options;

//        using var context = new AbsanteeContext(options);
        
//        var doubleHolidayPlanMapper = new Mock<IMapper<IHolidayPlan, HolidayPlanDataModel>>();
//        var holidayPeriodMapper = new Mock<IMapper<IHolidayPeriod, HolidayPeriodDataModel>>();

        
//        var hplan1 = new Mock<IHolidayPlan>();
//        hplan1.Setup(hp => hp.GetCollaboratorId()).Returns(1);
//        var hpdm1 = new HolidayPlanDataModel(hplan1.Object, holidayPeriodMapper.Object);
//        doubleHolidayPlanMapper.Setup(hpm => hpm.ToDataModel(hplan1.Object)).Returns(hpdm1);
        
//        var hplan2 = new Mock<IHolidayPlan>();
//        hplan2.Setup(hp => hp.GetCollaboratorId()).Returns(2);
//        var hpdm2 = new HolidayPlanDataModel(hplan2.Object, holidayPeriodMapper.Object);
//        doubleHolidayPlanMapper.Setup(hpm => hpm.ToDataModel(hplan2.Object)).Returns(hpdm2);
        
//        var hplan3 = new Mock<IHolidayPlan>();
//        hplan3.Setup(hp => hp.GetCollaboratorId()).Returns(1);
//        var hpdm3 = new HolidayPlanDataModel(hplan3.Object, holidayPeriodMapper.Object);
//        doubleHolidayPlanMapper.Setup(hpm => hpm.ToDataModel(hplan3.Object)).Returns(hpdm3);

//        context.HolidayPlans.Add(hpdm1);
//        context.HolidayPlans.Add(hpdm2);
//        await context.SaveChangesAsync();

//        var HolidayPlanRepositoryEF = new HolidayPlanRepositoryEF(context, doubleHolidayPlanMapper.Object, holidayPeriodMapper.Object);

//        // Act
//        bool result = await HolidayPlanRepositoryEF.AddHolidayPlanAsync(hplan3.Object);

//        // Assert
//        Assert.False(result);
//    }
//}