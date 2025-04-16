
using Domain.Models;
using Domain.Interfaces;
using Moq;

namespace Domain.Tests.HolidayPlanTests;


public class GetId{

    [Fact]
    public void WhenGettingId_ThenReturnsTheId(){

        var expected = 2;
        var collabId = 4;
        

        //arrange

        var holidayPeriod1 = new Mock<IHolidayPeriod>();
        var holidayPeriod2 = new Mock<IHolidayPeriod>();

        var list = new List<IHolidayPeriod> {
        holidayPeriod1.Object,
        holidayPeriod2.Object
        };

        
        var holidayPlan = new HolidayPlan(expected,collabId,list);
        
        //act
        var result = holidayPlan.GetId();

        //Assert

        Assert.Equal(expected,result);

    }
    
}