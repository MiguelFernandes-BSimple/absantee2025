
using Domain.Models;
using Domain.Interfaces;
using Moq;

namespace Domain.Tests.HolidayPlanTests;


public class HolidayPlanGetCollaboratorIdTests{


    [Fact]

     public void WhenGettingCollaboratorId_ThenReturnsTheId(){

        var id = 2;
        var expected = 4;

        //arrange

        var holidayPeriod1 = new Mock<IHolidayPeriod>();
        var holidayPeriod2 = new Mock<IHolidayPeriod>();

        var list = new List<IHolidayPeriod> {
        holidayPeriod1.Object,
        holidayPeriod2.Object
        };

        
        var holidayPlan = new HolidayPlan(id,expected,list);
        
        //act
        var result = holidayPlan.GetCollaboratorId();

        //Assert

        Assert.Equal(expected,result);

    }
}