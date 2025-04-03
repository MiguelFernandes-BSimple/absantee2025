using Infrastructure.Repositories;
using Domain.Interfaces;
using Moq;
using Domain.Models;
namespace Infrastructure.Tests.HolidayPlanRepositoryTests;
using Domain.Interfaces;
using Infrastructure.Interfaces;

using Moq;


public class GetHolidayPlansWithHolidayPeriodValid(){

    [Fact]
    public void GetHolidayPlansWithHolidayPeriodValid_ShouldReturnCorrectPlans()
    {
        // Arrange
        var periodDateMock = new Mock<IPeriodDate>();

        var holidayPeriodMock1 = new Mock<IHolidayPeriod>();
        holidayPeriodMock1.Setup(p => p.Intersects(periodDateMock.Object)).Returns(true);
        
        var holidayPeriodMock2 = new Mock<IHolidayPeriod>();
        holidayPeriodMock2.Setup(p => p.Intersects(periodDateMock.Object)).Returns(false);
        
        var holidayPlanMock1 = new Mock<IHolidayPlan>();
        holidayPlanMock1.Setup(h => h.GetHolidayPeriods()).Returns(new List<IHolidayPeriod> { holidayPeriodMock1.Object });
        
        var holidayPlanMock2 = new Mock<IHolidayPlan>();
        holidayPlanMock2.Setup(h => h.GetHolidayPeriods()).Returns(new List<IHolidayPeriod> { holidayPeriodMock2.Object });
        
        var holidayPlanMock3 = new Mock<IHolidayPlan>();
        holidayPlanMock3.Setup(h => h.GetHolidayPeriods()).Returns(new List<IHolidayPeriod> { holidayPeriodMock1.Object, holidayPeriodMock2.Object });
        
        var holidayPlans = new List<IHolidayPlan> { holidayPlanMock1.Object, holidayPlanMock2.Object, holidayPlanMock3.Object };
        HolidayPlanRepository hpr = new HolidayPlanRepository(holidayPlans);

        // Act
        var result = hpr.GetHolidayPlansWithHolidayPeriodValid(periodDateMock.Object);
        
        // Assert
        Assert.Contains(holidayPlanMock1.Object, result);
        Assert.Contains(holidayPlanMock3.Object, result);
        Assert.DoesNotContain(holidayPlanMock2.Object, result);
    }

}