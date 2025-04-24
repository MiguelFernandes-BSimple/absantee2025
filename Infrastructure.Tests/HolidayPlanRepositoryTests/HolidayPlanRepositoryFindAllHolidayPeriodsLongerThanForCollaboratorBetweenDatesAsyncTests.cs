//using Infrastructure.Repositories;
//using Domain.Interfaces;
//using Moq;
//using AutoMapper;
//using Infrastructure.DataModel;
//using Domain.Visitor;
//using Microsoft.EntityFrameworkCore;
//using Domain.Models;

//namespace Infrastructure.Tests.HolidayPlanRepositoryTests;

//public class HolidayPlanRepositoryFindAllHolidayPeriodsLongerThanForCollaboratorBetweenDatesAsyncTests
//{
//    [Fact]
//    public async Task WhenGivenBadCollaboratorAndDatesAndLengthAsync_ThenReturnEmptyLists()
//    {
//        // Arrange
//        var options = new DbContextOptionsBuilder<AbsanteeContext>()
//            .UseInMemoryDatabase(Guid.NewGuid().ToString()) // ensure isolation per test
//            .Options;

//        using var context = new AbsanteeContext(options);

//        var mapper = new Mock<IMapper<IHolidayPlan, HolidayPlanDataModel>>();
//        var hmapper = new Mock<IMapper<IHolidayPeriod, HolidayPeriodDataModel>>();

//        long collab1Id = 1;
//        long collab2Id = 2;

//        // Define collab id for each holiday plan
//        var hplan1 = new Mock<IHolidayPlan>();
//        hplan1.Setup(hp => hp.GetCollaboratorId()).Returns(collab1Id);
//        var hplan2 = new Mock<IHolidayPlan>();
//        hplan2.Setup(hp => hp.GetCollaboratorId()).Returns(collab2Id);

//        Mock<IHolidayPeriod> hp1Period = new Mock<IHolidayPeriod>();
//        PeriodDate hpPeriodDate = new PeriodDate(new DateOnly(2000, 1, 1), new DateOnly(2000, 1, 6));

//        hp1Period.Setup(hp => hp._periodDate).Returns(hpPeriodDate);

//        // Setup both hoiday plans with the same holidayPeriod list
//        List<IHolidayPeriod> holidayPeriods = new List<IHolidayPeriod> { hp1Period.Object };

//        var hperioddm = new HolidayPeriodDataModel(hp1Period.Object);

//        hplan1.Setup(hp => hp.GetHolidayPeriods()).Returns(holidayPeriods);
//        hplan2.Setup(hp => hp.GetHolidayPeriods()).Returns(holidayPeriods);

//        // Period to search
//        PeriodDate periodDate = new PeriodDate(new DateOnly(2000, 1, 1), new DateOnly(2000, 1, 4));

//        // Dbset data
//        hmapper.Setup(m => m.ToDataModel(holidayPeriods)).Returns(new List<HolidayPeriodDataModel> {hperioddm});
//        hmapper.Setup(m => m.ToDomain(new List<HolidayPeriodDataModel> {hperioddm})).Returns(holidayPeriods);
//        var hpDM1 = new HolidayPlanDataModel(hplan1.Object, hmapper.Object);
//        var hpDM2 = new HolidayPlanDataModel(hplan2.Object, hmapper.Object);

//        context.HolidayPlans.Add(hpDM1);
//        context.HolidayPlans.Add(hpDM2);
//        await context.SaveChangesAsync();

//        // Instantiate repository
//        var hpRepo = new HolidayPlanRepositoryEF(context, mapper.Object, hmapper.Object);

//        // Act
//        var result = await hpRepo.FindAllHolidayPeriodsLongerThanForCollaboratorBetweenDatesAsync(collab2Id, periodDate, 5);

//        // Assert
//        Assert.Empty(result);
//    }

//    [Fact]
//    public async Task WhenGivenGoodCollaboratorAndDatesAndLengthAsync_ThenReturnPeriods()
//    {
//        // Arrange
//        var options = new DbContextOptionsBuilder<AbsanteeContext>()
//            .UseInMemoryDatabase(Guid.NewGuid().ToString()) // ensure isolation per test
//            .Options;

//        using var context = new AbsanteeContext(options);

//        var mapper = new Mock<IMapper<IHolidayPlan, HolidayPlanDataModel>>();
//        var hmapper = new Mock<IMapper<IHolidayPeriod, HolidayPeriodDataModel>>();

//        long collab1Id = 1;
//        long collab2Id = 2;

//        // Define collab id for each holiday plan
//        var hplan1 = new Mock<IHolidayPlan>();
//        hplan1.Setup(hp => hp.GetCollaboratorId()).Returns(collab1Id);
//        var hplan2 = new Mock<IHolidayPlan>();
//        hplan2.Setup(hp => hp.GetCollaboratorId()).Returns(collab2Id);

//        Mock<IHolidayPeriod> hp1Period = new Mock<IHolidayPeriod>();
//        PeriodDate hpPeriodDate = new PeriodDate(new DateOnly(2000, 1, 1), new DateOnly(2000, 1, 6));

//        hp1Period.Setup(hp => hp._periodDate).Returns(hpPeriodDate);

//        // Setup both hoiday plans with the same holidayPeriod list
//        List<IHolidayPeriod> holidayPeriods = new List<IHolidayPeriod> { hp1Period.Object };

//        var hperioddm = new HolidayPeriodDataModel(hp1Period.Object);

//        hplan1.Setup(hp => hp.GetHolidayPeriods()).Returns(holidayPeriods);
//        hplan2.Setup(hp => hp.GetHolidayPeriods()).Returns(holidayPeriods);

//        // Period to search
//        PeriodDate periodDate = new PeriodDate(new DateOnly(2000, 1, 1), new DateOnly(2000, 1, 7));

//        // Dbset data
//        hmapper.Setup(m => m.ToDataModel(holidayPeriods)).Returns(new List<HolidayPeriodDataModel> {hperioddm});
//        hmapper.Setup(m => m.ToDomain(new List<HolidayPeriodDataModel> {hperioddm})).Returns(holidayPeriods);
//        var hpDM1 = new HolidayPlanDataModel(hplan1.Object, hmapper.Object);
//        var hpDM2 = new HolidayPlanDataModel(hplan2.Object, hmapper.Object);

//        context.HolidayPlans.Add(hpDM1);
//        context.HolidayPlans.Add(hpDM2);
//        await context.SaveChangesAsync();

//        // Instantiate repository
//        var hpRepo = new HolidayPlanRepositoryEF(context, mapper.Object, hmapper.Object);

//        // Act
//        var result = await hpRepo.FindAllHolidayPeriodsLongerThanForCollaboratorBetweenDatesAsync(collab2Id, periodDate, 4);

//        // Assert
//        Assert.Equal(holidayPeriods, result);
//    }
//}