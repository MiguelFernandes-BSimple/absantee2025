//using System.Runtime.InteropServices.Marshalling;
//using Domain.Interfaces;
//using Domain.Models;
//using Domain.Visitor;
//using Infrastructure.DataModel;
//using AutoMapper;
//using Infrastructure.Repositories;
//using Microsoft.EntityFrameworkCore;
//using Moq;

//namespace Infrastructure.Tests.HolidayPlanRepositoryTests;

//public class HolidayPlanRepositoryFindAllHolidayPeriodsForAllCollaboratorsBetweenDatesAsyncTests
//{


//    [Theory]
//    [InlineData("2020-01-01", "2020-12-31")]
//    [InlineData("2020-01-02", "2020-12-30")]
//    public async Task WhenPassingValidData_ThenReturnsAllHolidayPeriodsForAllCollaboratorsBetweenDates(string init1Str, string final1Str)
//    {
//        //arrange
//        var options = new DbContextOptionsBuilder<AbsanteeContext>()
//            .UseInMemoryDatabase(Guid.NewGuid().ToString())
//            .Options;

//        using var context = new AbsanteeContext(options);

//        var holidayPeriodMapper = new Mock<IMapper<IHolidayPeriod, HolidayPeriodDataModel>>();

//        //holiday plan 1
//        var holidayPlan1 = new Mock<IHolidayPlan>();
//        holidayPlan1.Setup(hp => hp.GetCollaboratorId()).Returns(1);

//        var holidayPeriod1 = new Mock<IHolidayPeriod>();
//        var holidayStart1 = new DateOnly(2020, 1, 2);
//        var holidayEnd1 = new DateOnly(2020, 1, 28);
//        var holidayPeriodDate1 = new PeriodDate(holidayStart1, holidayEnd1);
//        holidayPeriod1.Setup(hperiod => hperiod._periodDate).Returns(holidayPeriodDate1);
//        var holidayPeriods1 = new List<IHolidayPeriod>() { holidayPeriod1.Object };
//        holidayPlan1.Setup(hp => hp.GetHolidayPeriods()).Returns(holidayPeriods1);

//        var holidayPeriodDM1 = new HolidayPeriodDataModel(holidayPeriod1.Object);
//        var holidayPeriodsDM1 = new List<HolidayPeriodDataModel>() { holidayPeriodDM1 };
//        holidayPeriodMapper.Setup(hpMapper => hpMapper.ToDataModel(holidayPeriods1)).Returns(holidayPeriodsDM1);
//        var holidayPlanDM1 = new HolidayPlanDataModel(holidayPlan1.Object, holidayPeriodMapper.Object);
//        context.HolidayPlans.Add(holidayPlanDM1);

//        //holiday plan 2
//        var holidayPlan2 = new Mock<IHolidayPlan>();
//        holidayPlan2.Setup(hp => hp.GetCollaboratorId()).Returns(2);

//        var holidayPeriod2 = new Mock<IHolidayPeriod>();
//        var holidayStart2 = new DateOnly(2019, 11, 1);
//        var holidayEnd2 = new DateOnly(2020, 1, 15);
//        var holidayPeriodDate2 = new PeriodDate(holidayStart2, holidayEnd2);
//        holidayPeriod2.Setup(hperiod => hperiod._periodDate).Returns(holidayPeriodDate2);
//        var holidayPeriods2 = new List<IHolidayPeriod>() { holidayPeriod2.Object };
//        holidayPlan2.Setup(hp => hp.GetHolidayPeriods()).Returns(holidayPeriods2);

//        var holidayPeriodDM2 = new HolidayPeriodDataModel(holidayPeriod2.Object);
//        var holidayPeriodsDM2 = new List<HolidayPeriodDataModel>() { holidayPeriodDM2 };
//        holidayPeriodMapper.Setup(hpMapper => hpMapper.ToDataModel(holidayPeriods2)).Returns(holidayPeriodsDM2);
//        var holidayPlanDM2 = new HolidayPlanDataModel(holidayPlan2.Object, holidayPeriodMapper.Object);
//        context.HolidayPlans.Add(holidayPlanDM2);

//        await context.SaveChangesAsync();

//        var searchStart = DateOnly.Parse(init1Str);
//        var searchEnd = DateOnly.Parse(final1Str);
//        var searchPeriod = new PeriodDate(searchStart, searchEnd);

//        var filteredHolidayPeriod = new List<HolidayPeriodDataModel>() { holidayPeriodDM1 };

//        var expected = holidayPeriods1;

//        var mapperMock = new Mock<IMapper<IHolidayPlan, HolidayPlanDataModel>>();
//        holidayPeriodMapper.Setup(hpMapper => hpMapper.ToDomain(filteredHolidayPeriod)).Returns(expected);

//        var holidayPlanRepoEF = new HolidayPlanRepositoryEF(context, mapperMock.Object, holidayPeriodMapper.Object);

//        var repo = new HolidayPlanRepositoryEF(context, mapperMock.Object, holidayPeriodMapper.Object);

//        //act
//        var result = await repo.FindAllHolidayPeriodsForAllCollaboratorsBetweenDatesAsync([1, 2], searchPeriod);

//        //assert
//        Assert.Equal(expected, result);
//    }


//    [Theory]
//    [InlineData("2019-01-01", "2019-12-31")]
//    [InlineData("2020-01-01", "2021-12-31")]
//    [InlineData("2019-01-02", "2021-12-30")]
//    public async Task WhenPassingDataOutOfRange_ThenReturnsEmptyList(string init1Str, string final1Str)
//    {
//        // Arrange
//        var options = new DbContextOptionsBuilder<AbsanteeContext>()
//            .UseInMemoryDatabase(Guid.NewGuid().ToString())
//            .Options;

//        using var context = new AbsanteeContext(options);

//        var holidayPeriodMapper = new Mock<IMapper<IHolidayPeriod, HolidayPeriodDataModel>>();

//        //holiday plan 1
//        var holidayPlan1 = new Mock<IHolidayPlan>();
//        holidayPlan1.Setup(hp => hp.GetCollaboratorId()).Returns(1);

//        var holidayPeriod1 = new Mock<IHolidayPeriod>();
//        var holidayStart1 = new DateOnly(2018, 12, 29);
//        var holidayEnd1 = new DateOnly(2018, 12, 30);
//        var holidayPeriodDate1 = new PeriodDate(holidayStart1, holidayEnd1);
//        holidayPeriod1.Setup(hperiod => hperiod._periodDate).Returns(holidayPeriodDate1);
//        var holidayPeriods1 = new List<IHolidayPeriod>() { holidayPeriod1.Object };
//        holidayPlan1.Setup(hp => hp.GetHolidayPeriods()).Returns(holidayPeriods1);

//        var holidayPeriodDM1 = new HolidayPeriodDataModel(holidayPeriod1.Object);
//        var holidayPeriodsDM1 = new List<HolidayPeriodDataModel>() { holidayPeriodDM1 };
//        holidayPeriodMapper.Setup(hpMapper => hpMapper.ToDataModel(holidayPeriods1)).Returns(holidayPeriodsDM1);
//        var holidayPlanDM1 = new HolidayPlanDataModel(holidayPlan1.Object, holidayPeriodMapper.Object);
//        context.HolidayPlans.Add(holidayPlanDM1);

//        //holiday plan 2
//        var holidayPlan2 = new Mock<IHolidayPlan>();
//        holidayPlan2.Setup(hp => hp.GetCollaboratorId()).Returns(2);

//        var holidayPeriod2 = new Mock<IHolidayPeriod>();
//        var holidayStart2 = new DateOnly(2018, 11, 1);
//        var holidayEnd2 = new DateOnly(2019, 1, 15);
//        var holidayPeriodDate2 = new PeriodDate(holidayStart2, holidayEnd2);
//        holidayPeriod2.Setup(hperiod => hperiod._periodDate).Returns(holidayPeriodDate2);
//        var holidayPeriods2 = new List<IHolidayPeriod>() { holidayPeriod2.Object };
//        holidayPlan2.Setup(hp => hp.GetHolidayPeriods()).Returns(holidayPeriods2);

//        var holidayPeriodDM2 = new HolidayPeriodDataModel(holidayPeriod2.Object);
//        var holidayPeriodsDM2 = new List<HolidayPeriodDataModel>() { holidayPeriodDM2 };
//        holidayPeriodMapper.Setup(hpMapper => hpMapper.ToDataModel(holidayPeriods2)).Returns(holidayPeriodsDM2);
//        var holidayPlanDM2 = new HolidayPlanDataModel(holidayPlan2.Object, holidayPeriodMapper.Object);
//        context.HolidayPlans.Add(holidayPlanDM2);

//        await context.SaveChangesAsync();

//        var searchStart = DateOnly.Parse(init1Str);
//        var searchEnd = DateOnly.Parse(final1Str);
//        var searchPeriod = new PeriodDate(searchStart, searchEnd);

//        var filteredHolidayPeriod = new List<HolidayPeriodDataModel>();

//        var expected = new List<IHolidayPeriod>();

//        var mapperMock = new Mock<IMapper<IHolidayPlan, HolidayPlanDataModel>>();

//        holidayPeriodMapper.Setup(m => m.ToDomain(It.IsAny<IEnumerable<HolidayPeriodDataModel>>())).Returns<IEnumerable<HolidayPeriodDataModel>>(seq =>
//        {
//            Assert.Empty(seq);
//            return Enumerable.Empty<IHolidayPeriod>();
//        });

//        var holidayPlanRepoEF = new HolidayPlanRepositoryEF(context, mapperMock.Object, holidayPeriodMapper.Object);

//        var repo = new HolidayPlanRepositoryEF(context, mapperMock.Object, holidayPeriodMapper.Object);

//        //act
//        var result = await repo.FindAllHolidayPeriodsForAllCollaboratorsBetweenDatesAsync([1, 2], searchPeriod);

//        //assert
//        Assert.Equal(expected, result);
//    }
//}
