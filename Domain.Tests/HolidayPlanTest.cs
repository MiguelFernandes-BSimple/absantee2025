using System.ComponentModel;
using Domain;
using Moq;

public class HolidayPlanTest
{
    // Happy Path - Testing constructor with Single HolidayPeriod
    // Can Instatiate a HolidayPlan Object successfuly
    [Fact]
    public void WhenPassingValidSinglePeriod_ThenHolidayPlanIsCreated()
    {
        // Arrange
        // Test double for Holiday Period
        Mock<IHolidayPeriod> holidayPeriodDouble = new Mock<IHolidayPeriod>();

        // Test double for Colaborator
        Mock<IColaborator> colaboratorDouble = new Mock<IColaborator>();

        // Holiday period dates must be in the colaborator contract time frame
        colaboratorDouble.Setup(c => c.ContainsDates(It.IsAny<DateTime>(), It.IsAny<DateTime>())).Returns(true);

        // Act
        HolidayPlan holidayPlan = new HolidayPlan(holidayPeriodDouble.Object, colaboratorDouble.Object);

        // Assert
    }

    // Happy Path - Testing constructor with Single HolidayPeriod
    // Can Instatiate a HolidayPlan Object successfuly
    [Fact]
    public void WhenPassingValidMultiplePeriods_ThenHolidayPlanIsCreated()
    {
        // Arrange
        // Test doubles for Holiday Period
        Mock<IHolidayPeriod> holidayPeriodDouble1 = new Mock<IHolidayPeriod>();
        Mock<IHolidayPeriod> holidayPeriodDouble2 = new Mock<IHolidayPeriod>();
        Mock<IHolidayPeriod> holidayPeriodDouble3 = new Mock<IHolidayPeriod>();

        // Test double for Colaborator
        Mock<IColaborator> colaboratorDouble = new Mock<IColaborator>();

        // Holiday period dates must be in the colaborator contract time frame
        colaboratorDouble.Setup(c => c.ContainsDates(It.IsAny<DateTime>(), It.IsAny<DateTime>())).Returns(true);

        // Can't overlap with any other holiday periods
        holidayPeriodDouble1.Setup(hp1 => hp1.HolidayPeriodOverlap(It.IsAny<IHolidayPeriod>())).Returns(false);
        holidayPeriodDouble2.Setup(hp2 => hp2.HolidayPeriodOverlap(It.IsAny<IHolidayPeriod>())).Returns(false);
        holidayPeriodDouble3.Setup(hp3 => hp3.HolidayPeriodOverlap(It.IsAny<IHolidayPeriod>())).Returns(false);

        // Create Holiday Periods list
        List<IHolidayPeriod> holidayPeriods =
            new List<IHolidayPeriod> { holidayPeriodDouble1.Object, holidayPeriodDouble2.Object, holidayPeriodDouble3.Object };

        // Act
        HolidayPlan holidayPlan = new HolidayPlan(holidayPeriods, colaboratorDouble.Object);

        // Assert
    }

    // Validate exception - Two holiday periods collide
    // Happens when date periods intersect
    [Fact]
    public void WhenHolidayPeriodDatesOverlap_ThenThrowException()
    {
        // Arrange
        // Test doubles for Holiday Period
        Mock<IHolidayPeriod> holidayPeriodDouble1 = new Mock<IHolidayPeriod>();
        Mock<IHolidayPeriod> holidayPeriodDouble2 = new Mock<IHolidayPeriod>();

        // Test double for Colaborator
        Mock<IColaborator> colaboratorDouble = new Mock<IColaborator>();

        // Holiday period dates must be in the colaborator contract time frame
        colaboratorDouble.Setup(c => c.ContainsDates(It.IsAny<DateTime>(), It.IsAny<DateTime>())).Returns(true);

        // There is overlap with other holiday periods
        // If there is two dates that overlap, the exception shall happen
        holidayPeriodDouble1.Setup(hp => hp.HolidayPeriodOverlap(It.IsAny<IHolidayPeriod>())).Returns(true);

        // Create Holiday Periods list
        List<IHolidayPeriod> holidayPeriods =
            new List<IHolidayPeriod> { holidayPeriodDouble1.Object, holidayPeriodDouble2.Object };

        // Assert
        ArgumentException exception = Assert.Throws<ArgumentException>(() =>
            // Act
            new HolidayPlan(holidayPeriods, colaboratorDouble.Object));

        Assert.Equal("Invalid Arguments", exception.Message);
    }

    // Validate exception - Holiday period dates are not in the colaborator contract time frame
    [Fact]
    public void WhenHolidayPeriodNotInColabContractTimeFrame_ThenThrowException()
    {
        // Arrange
        // Test doubles for Holiday Period
        Mock<IHolidayPeriod> holidayPeriodDouble1 = new Mock<IHolidayPeriod>();
        Mock<IHolidayPeriod> holidayPeriodDouble2 = new Mock<IHolidayPeriod>();

        // Test double for Colaborator
        Mock<IColaborator> colaboratorDouble = new Mock<IColaborator>();

        // At least one holiday period is outside the colaborator contract time frame - ContainsDates = false
        // In order for the exception to be thrown 
        colaboratorDouble.Setup(c => c.ContainsDates(It.IsAny<DateTime>(), It.IsAny<DateTime>())).Returns(false);

        // There no overlap with other holiday periods
        holidayPeriodDouble1.Setup(hp1 => hp1.HolidayPeriodOverlap(It.IsAny<IHolidayPeriod>())).Returns(false);
        holidayPeriodDouble2.Setup(hp2 => hp2.HolidayPeriodOverlap(It.IsAny<IHolidayPeriod>())).Returns(false);

        // Create Holiday Periods list
        List<IHolidayPeriod> holidayPeriods =
            new List<IHolidayPeriod> { holidayPeriodDouble1.Object, holidayPeriodDouble2.Object };

        // Assert
        ArgumentException exception = Assert.Throws<ArgumentException>(() =>
            // Act
            new HolidayPlan(holidayPeriods, colaboratorDouble.Object));

        Assert.Equal("Invalid Arguments", exception.Message);
    }

    // Validate if a valid Holiday period can be added to a holiday plan
    // If its successfully added, returns true
    [Fact]
    public void WhenPassingValidPeriod_ThenAddHolidayPeriodReturnTrue()
    {
        // Arrange
        // Test double for Holiday Period
        Mock<IHolidayPeriod> holidayPeriodDouble1 = new Mock<IHolidayPeriod>();

        // Test double for Colaborator
        Mock<IColaborator> colaboratorDouble = new Mock<IColaborator>();

        // Holiday period dates must be in the colaborator contract time frame
        colaboratorDouble.Setup(c => c.ContainsDates(It.IsAny<DateTime>(), It.IsAny<DateTime>())).Returns(true);

        // Can't overlap with any other holiday periods
        holidayPeriodDouble1.Setup(hp1 => hp1.HolidayPeriodOverlap(It.IsAny<IHolidayPeriod>())).Returns(false);

        // Create Holiday Periods list (wth only one period)
        List<IHolidayPeriod> holidayPeriods =
            new List<IHolidayPeriod> { holidayPeriodDouble1.Object };

        // Instatiate Holiday plan class object
        HolidayPlan holidayPlan =
            new HolidayPlan(holidayPeriods, colaboratorDouble.Object);

        // Test double for Holiday Period to be added
        Mock<IHolidayPeriod> holidayPeriodDoubleToAdd = new Mock<IHolidayPeriod>();

        // It doesn't overlap with any other period
        holidayPeriodDoubleToAdd.Setup(hp2 => hp2.HolidayPeriodOverlap(It.IsAny<IHolidayPeriod>())).Returns(false);

        // Act
        // Add a seconda holiday period
        bool result = holidayPlan.AddHolidayPeriod(holidayPeriodDoubleToAdd.Object);

        // Assert
        Assert.True(result);
    }

    // Validate bool result when the Holiday period to be added to holiday plan
    // intercects other holiday periods
    // If its not added, returns false
    [Fact]
    public void WhenPassingIntersectingPeriod_ThenAddHolidayPeriodReturnFalse()
    {
        // Arrange
        // Test doubles for Holiday Period
        Mock<IHolidayPeriod> holidayPeriodDouble1 = new Mock<IHolidayPeriod>();

        // Test double for Colaborator
        Mock<IColaborator> colaboratorDouble = new Mock<IColaborator>();

        // Holiday period dates must be in the colaborator contract time frame
        colaboratorDouble.Setup(c => c.ContainsDates(It.IsAny<DateTime>(), It.IsAny<DateTime>())).Returns(true);

        // Create Holiday Periods list (wth only one period)
        List<IHolidayPeriod> holidayPeriods =
            new List<IHolidayPeriod> { holidayPeriodDouble1.Object };

        // Instatiate Holiday plan class object
        HolidayPlan holidayPlan =
            new HolidayPlan(holidayPeriods, colaboratorDouble.Object);

        // Test double for holiday period to be added to plan
        Mock<IHolidayPeriod> holidayPeriodDoubleToAdd = new Mock<IHolidayPeriod>();

        // Holiday period to be added intersects - Therefore it's not added
        holidayPeriodDoubleToAdd.Setup(hp => hp.HolidayPeriodOverlap(It.IsAny<IHolidayPeriod>())).Returns(true);

        // Act
        // Add a seconda holiday period
        bool result = holidayPlan.AddHolidayPeriod(holidayPeriodDoubleToAdd.Object);

        // Assert
        Assert.False(result);
    }

    // Validate bool result when the Holiday period to be added to holiday plan
    // isn't in the colaborator contract time frame
    // If its not added, returns false
    [Fact]
    public void WhenPassingPeriodNotInColabContractTimeFrame_ThenAddHolidayPeriodReturnFalse()
    {
        // Arrange
        // Test doubles for Holiday Period
        Mock<IHolidayPeriod> holidayPeriodDouble1 = new Mock<IHolidayPeriod>();

        // Test double for Colaborator
        Mock<IColaborator> colaboratorDouble = new Mock<IColaborator>();

        // The first Holiday period date will be in the colaborator contract time frame
        // Therefore it will be added
        // The second one shall not be, to verify if it returns false
        colaboratorDouble.SetupSequence(c => c.ContainsDates(It.IsAny<DateTime>(), It.IsAny<DateTime>()))
            .Returns(true).Returns(false);

        // Create Holiday Periods list (wth only one period)
        List<IHolidayPeriod> holidayPeriods =
            new List<IHolidayPeriod> { holidayPeriodDouble1.Object };

        // Instatiate Holiday plan class object
        HolidayPlan holidayPlan =
            new HolidayPlan(holidayPeriods, colaboratorDouble.Object);

        // Test double for holiday period to be added to plan
        Mock<IHolidayPeriod> holidayPeriodDoubleToAdd = new Mock<IHolidayPeriod>();

        // holiday period to be added can't overlap with any other holiday periods
        holidayPeriodDoubleToAdd.Setup(hp => hp.HolidayPeriodOverlap(It.IsAny<IHolidayPeriod>())).Returns(false);

        // Act
        // Add a seconda holiday period
        bool result = holidayPlan.AddHolidayPeriod(holidayPeriodDoubleToAdd.Object);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void WhenGivenCorrectDate_ThenReturnPeriod() {
        //arrange
        var colab = new Mock<IColaborator>();
        colab.Setup(a => a.ContainsDates(It.IsAny<DateTime>(), It.IsAny<DateTime>())).Returns(true);

        var holidayPeriod = new Mock<IHolidayPeriod>();
        DateOnly date = new DateOnly(2020, 1, 1);
        holidayPeriod.Setup(a => a.ContainsDate(date)).Returns(true);

        var holidayPlan = new HolidayPlan(holidayPeriod.Object, colab.Object);

        //act
        var result = holidayPlan.GetHolidayPeriodContainingDate(date);

        //assert
        Assert.Equal(holidayPeriod.Object, result);
    }

    [Fact]
    public void WhenGivenIncorrectDate_ThenReturnNull() {
        //arrange
        var colab = new Mock<IColaborator>();
        colab.Setup(a => a.ContainsDates(It.IsAny<DateTime>(), It.IsAny<DateTime>())).Returns(true);

        var holidayPeriod = new Mock<IHolidayPeriod>();
        DateOnly date = new DateOnly(2020, 1, 1);
        holidayPeriod.Setup(a => a.ContainsDate(date)).Returns(false);

        var holidayPlan = new HolidayPlan(holidayPeriod.Object, colab.Object);

        //act
        var result = holidayPlan.GetHolidayPeriodContainingDate(date);

        //assert
        Assert.Null(result);
    }

    [Fact]
    public void WhenGivenValidDatesAndLength_ThenReturnPeriods() {
        //arrange
        var colab = new Mock<IColaborator>();
        DateOnly ini = new DateOnly(2020, 1, 1);
        DateOnly end = ini.AddDays(5);
        colab.Setup(a => a.ContainsDates(It.IsAny<DateTime>(), It.IsAny<DateTime>())).Returns(true);
        DateOnly ini2 = ini.AddMonths(1);
        DateOnly end2 = end.AddMonths(1);
        
        var holidayPeriod1 = new Mock<IHolidayPeriod>();
        holidayPeriod1.Setup(a => a.ContainedBetween(ini, end)).Returns(true);
        holidayPeriod1.Setup(a => a.Length()).Returns(5);

        var holidayPeriod2 = new Mock<IHolidayPeriod>();
        holidayPeriod2.Setup(a => a.ContainedBetween(ini2, end2)).Returns(false);
        holidayPeriod2.Setup(a => a.Length()).Returns(5);

        var holidayPeriod3 = new Mock<IHolidayPeriod>();
        holidayPeriod3.Setup(a => a.ContainedBetween(ini, ini)).Returns(true);
        holidayPeriod3.Setup(a => a.Length()).Returns(4);

        var holidayPeriod4 = new Mock<IHolidayPeriod>();
        holidayPeriod4.Setup(a => a.ContainedBetween(ini, ini)).Returns(true);
        holidayPeriod4.Setup(a => a.Length()).Returns(3);

        var holidayPeriods = new List<IHolidayPeriod> {holidayPeriod1.Object, holidayPeriod2.Object};
        HolidayPlan holidayPlan = new HolidayPlan(holidayPeriods, colab.Object);
        
        //act
        var result = holidayPlan.FindAllHolidayPeriodsBetweenDatesLongerThan(ini, end, 4);
        
        //assert
        Assert.Single(result);
    }
}