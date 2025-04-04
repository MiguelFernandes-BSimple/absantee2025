using Domain.Interfaces;
using Domain.Models;
using Moq;

namespace Domain.Tests.HolidayPeriodTests;

public class Intersects
{
    [Fact]
    public void WhenPassingValidPeriodDate_ThenReturnsTrue()
    {
        //arrange
        var periodDouble = new Mock<IPeriodDate>();
        periodDouble.Setup(pd => pd.Intersects(It.IsAny<IPeriodDate>())).Returns(true);

        var holidayPeriod = new HolidayPeriod(periodDouble.Object);

        //act
        var result = holidayPeriod.Intersects(periodDouble.Object);

        //assert
        Assert.True(result);
    }

    [Fact]
    public void WhenPassingValidIncorrectPeriodDate_ThenReturnsFalse()
    {
        //arrange
        var periodDouble = new Mock<IPeriodDate>();
        periodDouble.Setup(pd => pd.Intersects(It.IsAny<IPeriodDate>())).Returns(false);

        var holidayPeriod = new HolidayPeriod(periodDouble.Object);

        //act
        var result = holidayPeriod.Intersects(periodDouble.Object);

        //assert
        Assert.False(result);
    }

    [Fact]
    public void WhenPassingValidHolidayPeriod_ThenReturnsTrue()
    {
        //arrange
        var periodDate = new Mock<IPeriodDate>();
        var holidayPeriod = new HolidayPeriod(periodDate.Object);

        var periodDate2 = new Mock<IPeriodDate>();
        var holidayPeriod2 = new HolidayPeriod(periodDate2.Object);

        periodDate.Setup(pd => pd.Intersects(periodDate2.Object)).Returns(true);

        //act
        var result = holidayPeriod.Intersects(holidayPeriod2);

        //assert
        Assert.True(result);
    }

    [Fact]
    public void WhenPassingValidIncorrectHolidayPeriod_ThenReturnsFalse()
    {
        //arrange
        var periodDate = new Mock<IPeriodDate>();
        var holidayPeriod = new HolidayPeriod(periodDate.Object);

        var periodDate2 = new Mock<IPeriodDate>();
        var holidayPeriod2 = new HolidayPeriod(periodDate2.Object);

        periodDate.Setup(pd => pd.Intersects(periodDate2.Object)).Returns(false);

        //act
        var result = holidayPeriod.Intersects(holidayPeriod2);

        //assert
        Assert.False(result);
    }
}
