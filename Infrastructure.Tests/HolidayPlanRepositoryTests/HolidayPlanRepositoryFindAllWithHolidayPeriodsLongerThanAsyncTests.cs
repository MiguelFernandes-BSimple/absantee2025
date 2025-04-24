//using Infrastructure.Repositories;
//using Domain.Interfaces;
//using Moq;
//using Domain.Visitor;
//using Infrastructure.DataModel;
//using Microsoft.EntityFrameworkCore;
//using AutoMapper;
//using Domain.Models;

//namespace Infrastructure.Tests.HolidayPlanRepositoryTests;

//public class HolidayPlanRepositoryFindAllWithHolidayPeriodsLongerThanAsyncTests
//{
//    [Fact]
//    public async Task WhenFindingHolidayPlansWithPeriodsLongerThanAsync_ReturnsCorrectList()
//    {
//        // Arrange
//        var options = new DbContextOptionsBuilder<AbsanteeContext>()
//            .UseInMemoryDatabase(Guid.NewGuid().ToString()) // ensure isolation per test
//            .Options;

//        using var context = new AbsanteeContext(options);

//        var mapper = new Mock<IMapper<IHolidayPlan, HolidayPlanDataModel>>();
//        var hmapper = new Mock<IMapper<IHolidayPeriod, HolidayPeriodDataModel>>();

//        var hperiod1 = new Mock<IHolidayPeriod>();
//        var periodDate1 = new PeriodDate(new DateOnly(2000, 1, 1), new DateOnly(2000, 1, 7));
//        hperiod1.Setup(h => h._periodDate).Returns(periodDate1);

//        var hperiod2 = new Mock<IHolidayPeriod>();
//        var periodDate2 = new PeriodDate(new DateOnly(2000, 1, 1), new DateOnly(2000, 1, 4));
//        hperiod2.Setup(h => h._periodDate).Returns(periodDate2);

//        var hplan = new Mock<IHolidayPlan>();
//        hplan.Setup(hp => hp.GetHolidayPeriods()).Returns(
//            new List<IHolidayPeriod> {
//                hperiod1.Object, hperiod2.Object
//            }
//        );

//        var hperiods = new List<HolidayPeriodDataModel> { 
//            new HolidayPeriodDataModel(hperiod1.Object),
//            new HolidayPeriodDataModel(hperiod2.Object)
//        };
        
//        hmapper.Setup(m => m.ToDataModel(hplan.Object.GetHolidayPeriods())).Returns(hperiods);
    
//        var hpdm = new HolidayPlanDataModel(hplan.Object, hmapper.Object);

//        mapper.Setup(m => m.ToDomain(new List<HolidayPlanDataModel> {hpdm})).Returns(new List<IHolidayPlan> {hplan.Object});

//        context.Add(hpdm);
//        await context.SaveChangesAsync();

//        var expected = new List<IHolidayPlan> {hplan.Object};

//        var repo = new HolidayPlanRepositoryEF(context, mapper.Object, hmapper.Object);        

//        // Act
//        var result = await repo.FindAllWithHolidayPeriodsLongerThanAsync(5);

//        // Assert
//        Assert.Equal(expected, result);
//    }
//}