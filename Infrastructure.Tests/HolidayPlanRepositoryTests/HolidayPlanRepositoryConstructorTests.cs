//using Infrastructure.Repositories;
//using Microsoft.EntityFrameworkCore;
//using Moq;
//using Domain.Models;
//using Infrastructure.DataModel;
//using AutoMapper;
//using Domain.Interfaces;
//using Domain.Visitor;

//namespace Infrastructure.Tests.HolidayPlanRepositoryTests;

//public class HolidayPlanRepositoryConstructorTests
//{
//    [Fact]
//    public void WhenNotPassingAnyArguments_ThenObjectIsCreated()
//    {
//        //Arrange
//        DbContextOptions<AbsanteeContext> options = new DbContextOptions<AbsanteeContext>();
//        Mock<AbsanteeContext> contextDouble = new Mock<AbsanteeContext>(options);
//        Mock<IMapper<IHolidayPlan, HolidayPlanDataModel>> holidayPlanMapperMock = new Mock<IMapper<IHolidayPlan, HolidayPlanDataModel>>();
//        Mock<IMapper<IHolidayPeriod, HolidayPeriodDataModel>> holidayPeriodMapperMock = new Mock<IMapper<IHolidayPeriod, HolidayPeriodDataModel>>();

//        //Act
//        new HolidayPlanRepositoryEF(contextDouble.Object, holidayPlanMapperMock.Object, holidayPeriodMapperMock.Object);

//        //Assert
//    }
//}