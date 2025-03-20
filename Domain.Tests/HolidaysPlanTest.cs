using Domain;
using Moq;

public class HolidaysPlanTest
{
    // Happy Path - Testing constructor with Single HolidayPeriod
    // Can Instatiate a HolidaysPlan Object successfuly
    [Fact]
    public void WhenPassingValidSinglePeriod_ThenHolidaysPlanIsCreated()
    {
        // Arrange
        // Test double for Holiday Period
        Mock<IHolidayPeriod> holidayPeriodDouble = new Mock<IHolidayPeriod>();

        // Can't overlap with any other holiday periods
        holidayPeriodDouble.Setup(hp => hp.HolidayPeriodOverlap(It.IsAny<IHolidayPeriod>())).Returns(false);

        // Test double for Colaborator
        Mock<IColaborator> colaboratorDouble = new Mock<IColaborator>();

        // Holiday period dates must be in the colaborator contract time frame
        colaboratorDouble.Setup(c => c.IsInside(It.IsAny<DateTime>(), It.IsAny<DateTime>())).Returns(true);

        // Act
        HolidaysPlan holidaysPlan = new HolidaysPlan(holidayPeriodDouble.Object, colaboratorDouble.Object);

        // Assert
        Assert.True(holidaysPlan.IsSizeList(1));
    }

    // Happy Path - Testing constructor with Single HolidayPeriod
    // Can Instatiate a HolidaysPlan Object successfuly
    [Fact]
    public void WhenPassingValidMultiplePeriods_ThenHolidaysPlanIsCreated()
    {
        // Arrange
        // Test doubles for Holiday Period
        Mock<IHolidayPeriod> holidayPeriodDouble1 = new Mock<IHolidayPeriod>();
        Mock<IHolidayPeriod> holidayPeriodDouble2 = new Mock<IHolidayPeriod>();
        Mock<IHolidayPeriod> holidayPeriodDouble3 = new Mock<IHolidayPeriod>();

        // Can't overlap with any other holiday periods
        holidayPeriodDouble1.Setup(hp => hp.HolidayPeriodOverlap(It.IsAny<IHolidayPeriod>())).Returns(false);
        holidayPeriodDouble2.Setup(hp => hp.HolidayPeriodOverlap(It.IsAny<IHolidayPeriod>())).Returns(false);
        holidayPeriodDouble3.Setup(hp => hp.HolidayPeriodOverlap(It.IsAny<IHolidayPeriod>())).Returns(false);

        // Create Holiday Periods list
        List<IHolidayPeriod> holidayPeriods =
            new List<IHolidayPeriod> { holidayPeriodDouble1.Object, holidayPeriodDouble2.Object, holidayPeriodDouble3.Object };

        // Test double for Colaborator
        Mock<IColaborator> colaboratorDouble = new Mock<IColaborator>();

        // Holiday period dates must be in the colaborator contract time frame
        colaboratorDouble.Setup(c => c.IsInside(It.IsAny<DateTime>(), It.IsAny<DateTime>())).Returns(true);

        // Act
        HolidaysPlan holidaysPlan = new HolidaysPlan(holidayPeriods, colaboratorDouble.Object);

        // Assert
        Assert.True(holidaysPlan.IsSizeList(holidayPeriods.Count));

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

        // There is overlap with other holiday periods
        // If there is two dates that overlap, the exception shall happen
        holidayPeriodDouble1.Setup(hp => hp.HolidayPeriodOverlap(It.IsAny<IHolidayPeriod>())).Returns(true);
        holidayPeriodDouble2.Setup(hp => hp.HolidayPeriodOverlap(It.IsAny<IHolidayPeriod>())).Returns(true);

        // Create Holiday Periods list
        List<IHolidayPeriod> holidayPeriods =
            new List<IHolidayPeriod> { holidayPeriodDouble1.Object, holidayPeriodDouble2.Object };

        // Test double for Colaborator
        Mock<IColaborator> colaboratorDouble = new Mock<IColaborator>();

        // Holiday period dates must be in the colaborator contract time frame
        colaboratorDouble.Setup(c => c.IsInside(It.IsAny<DateTime>(), It.IsAny<DateTime>())).Returns(true);

        // Assert
        ArgumentException exception = Assert.Throws<ArgumentException>(() =>
            // Act
            new HolidaysPlan(holidayPeriods, colaboratorDouble.Object));

        Assert.Equal("Invalid Arguments", exception.Message);
    }

    // Validate exception - Holiday period dates are not in the colaborator contract time frame
    [Fact]
    public void WhenHolidayPeriodDatesNotInColabContractTimeFrame_ThenThrowException()
    {
        // Arrange
        // Test doubles for Holiday Period
        Mock<IHolidayPeriod> holidayPeriodDouble1 = new Mock<IHolidayPeriod>();
        Mock<IHolidayPeriod> holidayPeriodDouble2 = new Mock<IHolidayPeriod>();

        // There is overlap with other holiday periods
        // If there is two dates that overlap, the exception shall happen
        holidayPeriodDouble1.Setup(hp => hp.HolidayPeriodOverlap(It.IsAny<IHolidayPeriod>())).Returns(true);
        holidayPeriodDouble2.Setup(hp => hp.HolidayPeriodOverlap(It.IsAny<IHolidayPeriod>())).Returns(true);

        // Create Holiday Periods list
        List<IHolidayPeriod> holidayPeriods =
            new List<IHolidayPeriod> { holidayPeriodDouble1.Object, holidayPeriodDouble2.Object };

        // Test double for Colaborator
        Mock<IColaborator> colaboratorDouble = new Mock<IColaborator>();

        // At least one holiday period dates have to be in the colaborator contract time frame
        // In order for the exception to be thrown
        colaboratorDouble.Setup(c => c.IsInside(It.IsAny<DateTime>(), It.IsAny<DateTime>())).Returns(false);

        // Assert
        ArgumentException exception = Assert.Throws<ArgumentException>(() =>
            // Act
            new HolidaysPlan(holidayPeriods, colaboratorDouble.Object));

        Assert.Equal("Invalid Arguments", exception.Message);
    }

    // Validate if a valid Holiday period can be added to a holiday plan
    // If its successfully added, returns true
    [Fact]
    public void AddHolidayPeriod_WhenPassingValidPeriod_ThenReturnTrue()
    {
        // Arrange
        // Test doubles for Holiday Period
        Mock<IHolidayPeriod> holidayPeriodDouble1 = new Mock<IHolidayPeriod>();
        Mock<IHolidayPeriod> holidayPeriodDoubleToAdd = new Mock<IHolidayPeriod>();

        // Can't overlap with any other holiday periods
        holidayPeriodDouble1.Setup(hp => hp.HolidayPeriodOverlap(It.IsAny<IHolidayPeriod>())).Returns(false);
        holidayPeriodDoubleToAdd.Setup(hp => hp.HolidayPeriodOverlap(It.IsAny<IHolidayPeriod>())).Returns(false);

        // Create Holiday Periods list (wth only one period)
        List<IHolidayPeriod> holidayPeriods =
            new List<IHolidayPeriod> { holidayPeriodDouble1.Object };

        // Test double for Colaborator
        Mock<IColaborator> colaboratorDouble = new Mock<IColaborator>();

        // Holiday period dates must be in the colaborator contract time frame
        colaboratorDouble.Setup(c => c.IsInside(It.IsAny<DateTime>(), It.IsAny<DateTime>())).Returns(true);

        // Instatiate Holidays plan class object
        HolidaysPlan holidaysPlan =
            new HolidaysPlan(holidayPeriods, colaboratorDouble.Object);

        // Act
        // Add a seconda holiday period
        bool result = holidaysPlan.AddHolidayPeriod(holidayPeriodDoubleToAdd.Object);

        // Assert
        Assert.True(result);

        //Verify if list has the new holiday period
        Assert.True(holidaysPlan.IsSizeList(holidayPeriods.Count + 1));
    }

    // Validate bool result when the Holiday period to be added to holiday plan
    // intercects other holiday periods
    // If its not added, returns false
    [Fact]
    public void AddHolidayPeriod_WhenPassingIntersectingPeriod_ThenReturnFalse()
    {
        // Arrange
        // Test doubles for Holiday Period
        Mock<IHolidayPeriod> holidayPeriodDouble1 = new Mock<IHolidayPeriod>();
        Mock<IHolidayPeriod> holidayPeriodDoubleToAdd = new Mock<IHolidayPeriod>();

        // 1st holiday period can't overlap with any other holiday periods to allow instatiate
        holidayPeriodDouble1.Setup(hp => hp.HolidayPeriodOverlap(It.IsAny<IHolidayPeriod>())).Returns(false);

        // Holiday period to be added intersects - Therefore it's not added
        holidayPeriodDoubleToAdd.Setup(hp => hp.HolidayPeriodOverlap(It.IsAny<IHolidayPeriod>())).Returns(true);

        // Create Holiday Periods list (wth only one period)
        List<IHolidayPeriod> holidayPeriods =
            new List<IHolidayPeriod> { holidayPeriodDouble1.Object };

        // Test double for Colaborator
        Mock<IColaborator> colaboratorDouble = new Mock<IColaborator>();

        // Holiday period dates must be in the colaborator contract time frame
        colaboratorDouble.Setup(c => c.IsInside(It.IsAny<DateTime>(), It.IsAny<DateTime>())).Returns(true);

        // Instatiate Holidays plan class object
        HolidaysPlan holidaysPlan =
            new HolidaysPlan(holidayPeriods, colaboratorDouble.Object);

        // Act
        // Add a seconda holiday period
        bool result = holidaysPlan.AddHolidayPeriod(holidayPeriodDoubleToAdd.Object);

        // Assert
        Assert.False(result);

        //Verify if list has the new holiday period
        Assert.True(holidaysPlan.IsSizeList(holidayPeriods.Count));
    }

    // Validate bool result when the Holiday period to be added to holiday plan
    // isn't in the colaborator contract time frame
    // If its not added, returns false
    [Fact]
    public void AddHolidayPeriod_WhenPassingPeriodNotInColabContractTimeFrame_ThenReturnFalse()
    {
        // Arrange
        // Test doubles for Holiday Period
        Mock<IHolidayPeriod> holidayPeriodDouble1 = new Mock<IHolidayPeriod>();
        Mock<IHolidayPeriod> holidayPeriodDoubleToAdd = new Mock<IHolidayPeriod>();

        // holiday periods can't overlap with any other holiday periods
        holidayPeriodDouble1.Setup(hp => hp.HolidayPeriodOverlap(It.IsAny<IHolidayPeriod>())).Returns(false);
        holidayPeriodDoubleToAdd.Setup(hp => hp.HolidayPeriodOverlap(It.IsAny<IHolidayPeriod>())).Returns(false);

        // Create Holiday Periods list (wth only one period)
        List<IHolidayPeriod> holidayPeriods =
            new List<IHolidayPeriod> { holidayPeriodDouble1.Object };

        // Test double for Colaborator
        Mock<IColaborator> colaboratorDouble = new Mock<IColaborator>();

        // The first Holiday period date will be in the colaborator contract time frame
        // Therefore it will be added
        // The second one shall not be, to verify if it returns false
        colaboratorDouble.SetupSequence(c => c.IsInside(It.IsAny<DateTime>(), It.IsAny<DateTime>()))
            .Returns(true).Returns(false);

        // Instatiate Holidays plan class object
        HolidaysPlan holidaysPlan =
            new HolidaysPlan(holidayPeriods, colaboratorDouble.Object);

        // Act
        // Add a seconda holiday period
        bool result = holidaysPlan.AddHolidayPeriod(holidayPeriodDoubleToAdd.Object);

        // Assert
        Assert.False(result);

        //Verify if list has the new holiday period
        Assert.True(holidaysPlan.IsSizeList(holidayPeriods.Count));
    }

    // Testing if comparrison is well done - Size should be correct
    [Fact]
    public void IsSizeList_WhenPassingCorrectInput_ThenReturnTrue()
    {
        // Arrange
        // Test doubles for Holiday Period
        Mock<IHolidayPeriod> holidayPeriodDouble1 = new Mock<IHolidayPeriod>();

        // Can't overlap with any other holiday periods
        holidayPeriodDouble1.Setup(hp => hp.HolidayPeriodOverlap(It.IsAny<IHolidayPeriod>())).Returns(false);

        // Create Holiday Periods list - Only has ONE element
        List<IHolidayPeriod> holidayPeriods =
            new List<IHolidayPeriod> { holidayPeriodDouble1.Object };

        // Test double for Colaborator
        Mock<IColaborator> colaboratorDouble = new Mock<IColaborator>();

        // Holiday period dates must be in the colaborator contract time frame
        colaboratorDouble.Setup(c => c.IsInside(It.IsAny<DateTime>(), It.IsAny<DateTime>())).Returns(true);

        HolidaysPlan holidaysPlan =
            new HolidaysPlan(holidayPeriods, colaboratorDouble.Object);

        // Assert
        Assert.True(
            // Act
            // Because list only has one element - size should be one
            holidaysPlan.IsSizeList(1)
        );
    }
}