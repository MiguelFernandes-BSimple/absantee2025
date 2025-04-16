namespace Domain.Tests.HolidayPeriodTests;

using Domain.Interfaces;
using Domain.Models;
using Xunit;
using Moq;


public class GetId{

    [Fact]
    public void WhenGettingId_ThenReturnsTheId(){

        var expected = 2;
        

        //arrange

        var mockPeriodDate = new Mock<IPeriodDate>();

        
        var holidayPlan = new HolidayPeriod(expected,mockPeriodDate.Object);
        
        //act
        var result = holidayPlan.GetId();

        //Assert

        Assert.Equal(expected,result);

    }

    


    
    
}