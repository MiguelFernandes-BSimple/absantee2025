using Infrastructure.Repositories;
using Domain.Interfaces;
using Moq;
using Domain.Models;
using Infrastructure.DataModel;
using Infrastructure.Mapper;

namespace Infrastructure.Tests.HolidayPlanRepositoryTests;

public class HolidayPlanRepositoryConstructorTests
{
    [Fact]
    public void WhenNotPassingAnyArguments_ThenObjectIsCreated()
    {
        //Arrange
        var holidayPlanMapperMock = new Mock<IMapper<HolidayPlan, HolidayPlanDataModel>>();
        var absanteeMock = new Mock<IAbsanteeContext>();

        var holidayPeriodMapper = new Mock<HolidayPeriodMapper>();

        //Act
        new HolidayPlanRepositoryEF((AbsanteeContext)absanteeMock.Object, (HolidayPlanMapper)holidayPlanMapperMock.Object, holidayPeriodMapper.Object);

        //Assert
    }
}