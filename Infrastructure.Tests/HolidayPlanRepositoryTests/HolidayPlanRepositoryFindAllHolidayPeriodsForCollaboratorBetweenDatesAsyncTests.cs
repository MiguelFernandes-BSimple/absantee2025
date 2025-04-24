//using Infrastructure.Repositories;
//using Domain.Interfaces;
//using Moq;
//using Domain.Models;
//using Domain.Visitor;
//using Infrastructure.DataModel;
//using AutoMapper;
//using Microsoft.EntityFrameworkCore;

//namespace Infrastructure.Tests.HolidayPlanRepositoryTests;

//public class HolidayPlanRepositoryFindAllHolidayPeriodsForCollaboratorBetweenDatesAsyncTests
//{
//    [Theory]
//    [InlineData("2020-01-01", "2020-01-02")]
//    [InlineData("2020-01-01", "2020-01-31")]
//    public async Task WhenPassinValidDatesAsync_ThenReturnsCorrectPeriod(string init1Str, string final1Str)
//    {
//        // Arrange
//        var options = new DbContextOptionsBuilder<AbsanteeContext>()
//            .UseInMemoryDatabase(Guid.NewGuid().ToString()) // ensure isolation per test
//            .Options;

//        using var context = new AbsanteeContext(options);

//        var mapper = new Mock<IMapper<IHolidayPlan, HolidayPlanDataModel>>();
//        var hmapper = new Mock<IMapper<IHolidayPeriod, HolidayPeriodDataModel>>();

//        var periodDate = new PeriodDate(DateOnly.Parse(init1Str), DateOnly.Parse(final1Str));

//        var hperiod = new Mock<IHolidayPeriod>();
//        hperiod.Setup(hperiod => hperiod._periodDate).Returns(periodDate);

//        var hperiods = new List<IHolidayPeriod> {hperiod.Object};

//        var hplan = new Mock<IHolidayPlan>();
//        hplan.Setup(hplan => hplan.GetCollaboratorId()).Returns(1);
//        hplan.Setup(hplan => hplan.GetHolidayPeriods()).Returns(hperiods);

//        var hperioddm = new HolidayPeriodDataModel(hperiod.Object);

//        hmapper.Setup(m => m.ToDataModel(hperiods)).Returns(new List<HolidayPeriodDataModel> {hperioddm});
//        hmapper.Setup(m => m.ToDomain(new List<HolidayPeriodDataModel> {hperioddm})).Returns(hperiods);
//        var hpdm = new HolidayPlanDataModel(hplan.Object, hmapper.Object);

//        context.HolidayPlans.Add(hpdm);
//        await context.SaveChangesAsync();

//        var repo = new HolidayPlanRepositoryEF(context, mapper.Object, hmapper.Object);

//        var searchingPeriodDate = new PeriodDate(new DateOnly(2020, 1, 1), new DateOnly(2020, 1, 31));
        
//        // Act
//        var result = await repo.FindAllHolidayPeriodsForAllCollaboratorsBetweenDatesAsync(new List<long> {1, 2}, searchingPeriodDate); 

//        // Assert
//        Assert.Equal(hperiods, result);
//    }

//    [Theory]
//    [InlineData("2019-01-01", "2020-12-31")]
//    [InlineData("2020-01-01", "2021-12-31")]
//    [InlineData("2019-01-02", "2021-12-30")]
//    public async Task WhenHolidayPeriodIsOutOfRangeThanTheOneBeingSearchedForAsync_ThenReturnsEmptyList(string init1Str, string final1Str)
//    {
//        // Arrange
//        var options = new DbContextOptionsBuilder<AbsanteeContext>()
//            .UseInMemoryDatabase(Guid.NewGuid().ToString()) // ensure isolation per test
//            .Options;

//        using var context = new AbsanteeContext(options);

//        var mapper = new Mock<IMapper<IHolidayPlan, HolidayPlanDataModel>>();
//        var hmapper = new Mock<IMapper<IHolidayPeriod, HolidayPeriodDataModel>>();

//        var periodDate = new PeriodDate(DateOnly.Parse(init1Str), DateOnly.Parse(final1Str));

//        var hperiod = new Mock<IHolidayPeriod>();
//        hperiod.Setup(hperiod => hperiod._periodDate).Returns(periodDate);

//        var hperiods = new List<IHolidayPeriod> {hperiod.Object};

//        var hplan = new Mock<IHolidayPlan>();
//        hplan.Setup(hplan => hplan.GetCollaboratorId()).Returns(1);
//        hplan.Setup(hplan => hplan.GetHolidayPeriods()).Returns(hperiods);

//        var hperioddm = new HolidayPeriodDataModel(hperiod.Object);

//        hmapper.Setup(m => m.ToDataModel(hperiods)).Returns(new List<HolidayPeriodDataModel> {hperioddm});
//        hmapper.Setup(m => m.ToDomain(new List<HolidayPeriodDataModel> {hperioddm})).Returns(hperiods);
//        var hpdm = new HolidayPlanDataModel(hplan.Object, hmapper.Object);

//        context.HolidayPlans.Add(hpdm);
//        await context.SaveChangesAsync();

//        var repo = new HolidayPlanRepositoryEF(context, mapper.Object, hmapper.Object);

//        var searchingPeriodDate = new PeriodDate(new DateOnly(2020, 1, 1), new DateOnly(2020, 1, 7));
        
//        // Act
//        var result = await repo.FindAllHolidayPeriodsForAllCollaboratorsBetweenDatesAsync(new List<long> {1, 2}, searchingPeriodDate); 

//        // Assert
//        Assert.Empty(result);
//    }

//    [Fact]
//    public async Task WhenCollaboratorHasNoHolidayPlansAsync_ThenReturnsEmptyList()
//    {
//        // Arrange
//        var options = new DbContextOptionsBuilder<AbsanteeContext>()
//            .UseInMemoryDatabase(Guid.NewGuid().ToString()) // ensure isolation per test
//            .Options;

//        using var context = new AbsanteeContext(options);

//        var mapper = new Mock<IMapper<IHolidayPlan, HolidayPlanDataModel>>();
//        var hmapper = new Mock<IMapper<IHolidayPeriod, HolidayPeriodDataModel>>();

//        var repo = new HolidayPlanRepositoryEF(context, mapper.Object, hmapper.Object);

//        var searchingPeriodDate = new PeriodDate(new DateOnly(2020, 1, 1), new DateOnly(2020, 1, 7));
        
//        // Act
//        var result = await repo.FindAllHolidayPeriodsForAllCollaboratorsBetweenDatesAsync(new List<long> {1, 2}, searchingPeriodDate); 

//        // Assert
//        Assert.Empty(result);
//    }
//}