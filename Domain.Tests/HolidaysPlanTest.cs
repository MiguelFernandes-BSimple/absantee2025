using Domain;
using Moq;

public class HolidaysPlanTest
{
    // Happy Path - Testing constructor with Single HolidayPeriod
    // Can Instatiate a HolidaysPlan Object successfuly
    [Fact]
    public void WhenPassingValidSinglePeriod_ThenHolidayPlanIsCreated()
    {
        // Arrange
        // Test double for Holiday Period
        Mock<IHolidayPeriod> holidayPeriodDouble = new Mock<IHolidayPeriod>();

        // Test double for Colaborator
        Mock<IColaborator> colaboratorDouble = new Mock<IColaborator>();

        // Holiday period dates must be in the colaborator contract time frame
        colaboratorDouble.Setup(c => c.IsInside(It.IsAny<DateTime>(), It.IsAny<DateTime>())).Returns(true);

        // Act
        HolidaysPlan holidayPlan = new HolidaysPlan(holidayPeriodDouble.Object, colaboratorDouble.Object);

        // Assert
        Assert.True(holidayPlan.IsSizeList(1));
    }

    // Happy Path - Testing constructor with Single HolidayPeriod
    // Can Instatiate a HolidaysPlan Object successfuly
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
        colaboratorDouble.Setup(c => c.IsInside(It.IsAny<DateTime>(), It.IsAny<DateTime>())).Returns(true);

        // Can't overlap with any other holiday periods
        holidayPeriodDouble1.Setup(hp1 => hp1.HolidayPeriodOverlap(It.IsAny<IHolidayPeriod>())).Returns(false);
        holidayPeriodDouble2.Setup(hp2 => hp2.HolidayPeriodOverlap(It.IsAny<IHolidayPeriod>())).Returns(false);
        holidayPeriodDouble3.Setup(hp3 => hp3.HolidayPeriodOverlap(It.IsAny<IHolidayPeriod>())).Returns(false);

        // Create Holiday Periods list
        List<IHolidayPeriod> holidayPeriods =
            new List<IHolidayPeriod> { holidayPeriodDouble1.Object, holidayPeriodDouble2.Object, holidayPeriodDouble3.Object };

        // Act
        HolidaysPlan holidayPlan = new HolidaysPlan(holidayPeriods, colaboratorDouble.Object);

        // Assert
        Assert.True(holidayPlan.IsSizeList(holidayPeriods.Count));

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
        colaboratorDouble.Setup(c => c.IsInside(It.IsAny<DateTime>(), It.IsAny<DateTime>())).Returns(true);

        // There is overlap with other holiday periods
        // If there is two dates that overlap, the exception shall happen
        holidayPeriodDouble1.Setup(hp => hp.HolidayPeriodOverlap(It.IsAny<IHolidayPeriod>())).Returns(true);

        // Create Holiday Periods list
        List<IHolidayPeriod> holidayPeriods =
            new List<IHolidayPeriod> { holidayPeriodDouble1.Object, holidayPeriodDouble2.Object };

        // Assert
        ArgumentException exception = Assert.Throws<ArgumentException>(() =>
            // Act
            new HolidaysPlan(holidayPeriods, colaboratorDouble.Object));

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

        // At least one holiday period is outside the colaborator contract time frame - IsInside = false
        // In order for the exception to be thrown 
        colaboratorDouble.Setup(c => c.IsInside(It.IsAny<DateTime>(), It.IsAny<DateTime>())).Returns(false);

        // There no overlap with other holiday periods
        holidayPeriodDouble1.Setup(hp1 => hp1.HolidayPeriodOverlap(It.IsAny<IHolidayPeriod>())).Returns(false);
        holidayPeriodDouble2.Setup(hp2 => hp2.HolidayPeriodOverlap(It.IsAny<IHolidayPeriod>())).Returns(false);

        // Create Holiday Periods list
        List<IHolidayPeriod> holidayPeriods =
            new List<IHolidayPeriod> { holidayPeriodDouble1.Object, holidayPeriodDouble2.Object };

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
        // Test double for Holiday Period
        Mock<IHolidayPeriod> holidayPeriodDouble1 = new Mock<IHolidayPeriod>();

        // Test double for Colaborator
        Mock<IColaborator> colaboratorDouble = new Mock<IColaborator>();

        // Holiday period dates must be in the colaborator contract time frame
        colaboratorDouble.Setup(c => c.IsInside(It.IsAny<DateTime>(), It.IsAny<DateTime>())).Returns(true);

        // Can't overlap with any other holiday periods
        holidayPeriodDouble1.Setup(hp1 => hp1.HolidayPeriodOverlap(It.IsAny<IHolidayPeriod>())).Returns(false);

        // Create Holiday Periods list (wth only one period)
        List<IHolidayPeriod> holidayPeriods =
            new List<IHolidayPeriod> { holidayPeriodDouble1.Object };

        // Instatiate Holidays plan class object
        HolidaysPlan holidayPlan =
            new HolidaysPlan(holidayPeriods, colaboratorDouble.Object);

        // Test double for Holiday Period to be added
        Mock<IHolidayPeriod> holidayPeriodDoubleToAdd = new Mock<IHolidayPeriod>();

        // It doesn't overlap with any other period
        holidayPeriodDoubleToAdd.Setup(hp2 => hp2.HolidayPeriodOverlap(It.IsAny<IHolidayPeriod>())).Returns(false);

        // Act
        // Add a seconda holiday period
        bool result = holidayPlan.AddHolidayPeriod(holidayPeriodDoubleToAdd.Object);

        // Assert
        Assert.True(result);

        //Verify if list has the new holiday period
        Assert.True(holidayPlan.IsSizeList(holidayPeriods.Count + 1));
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

        // Test double for Colaborator
        Mock<IColaborator> colaboratorDouble = new Mock<IColaborator>();

        // Holiday period dates must be in the colaborator contract time frame
        colaboratorDouble.Setup(c => c.IsInside(It.IsAny<DateTime>(), It.IsAny<DateTime>())).Returns(true);

        // Create Holiday Periods list (wth only one period)
        List<IHolidayPeriod> holidayPeriods =
            new List<IHolidayPeriod> { holidayPeriodDouble1.Object };

        // Instatiate Holidays plan class object
        HolidaysPlan holidayPlan =
            new HolidaysPlan(holidayPeriods, colaboratorDouble.Object);

        // Test double for holiday period to be added to plan
        Mock<IHolidayPeriod> holidayPeriodDoubleToAdd = new Mock<IHolidayPeriod>();

        // Holiday period to be added intersects - Therefore it's not added
        holidayPeriodDoubleToAdd.Setup(hp => hp.HolidayPeriodOverlap(It.IsAny<IHolidayPeriod>())).Returns(true);

        // Act
        // Add a seconda holiday period
        bool result = holidayPlan.AddHolidayPeriod(holidayPeriodDoubleToAdd.Object);

        // Assert
        Assert.False(result);

        //Verify if list has the new holiday period
        Assert.True(holidayPlan.IsSizeList(holidayPeriods.Count));
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

        // Test double for Colaborator
        Mock<IColaborator> colaboratorDouble = new Mock<IColaborator>();

        // The first Holiday period date will be in the colaborator contract time frame
        // Therefore it will be added
        // The second one shall not be, to verify if it returns false
        colaboratorDouble.SetupSequence(c => c.IsInside(It.IsAny<DateTime>(), It.IsAny<DateTime>()))
            .Returns(true).Returns(false);

        // Create Holiday Periods list (wth only one period)
        List<IHolidayPeriod> holidayPeriods =
            new List<IHolidayPeriod> { holidayPeriodDouble1.Object };

        // Instatiate Holidays plan class object
        HolidaysPlan holidayPlan =
            new HolidaysPlan(holidayPeriods, colaboratorDouble.Object);

        // Test double for holiday period to be added to plan
        Mock<IHolidayPeriod> holidayPeriodDoubleToAdd = new Mock<IHolidayPeriod>();

        // holiday period to be added can't overlap with any other holiday periods
        holidayPeriodDoubleToAdd.Setup(hp => hp.HolidayPeriodOverlap(It.IsAny<IHolidayPeriod>())).Returns(false);

        // Act
        // Add a seconda holiday period
        bool result = holidayPlan.AddHolidayPeriod(holidayPeriodDoubleToAdd.Object);

        // Assert
        Assert.False(result);

        //Verify if list has the new holiday period
        Assert.True(holidayPlan.IsSizeList(holidayPeriods.Count));
    }

    // Testing if comparrison is well done - Size should be correct
    [Fact]
    public void IsSizeList_WhenPassingCorrectInput_ThenReturnTrue()
    {
        // Arrange
        // Test doubles for Holiday Period
        Mock<IHolidayPeriod> holidayPeriodDouble1 = new Mock<IHolidayPeriod>();

        // Test double for Colaborator
        Mock<IColaborator> colaboratorDouble = new Mock<IColaborator>();

        // Holiday period dates must be in the colaborator contract time frame
        colaboratorDouble.Setup(c => c.IsInside(It.IsAny<DateTime>(), It.IsAny<DateTime>())).Returns(true);

        // Can't overlap with any other holiday periods
        holidayPeriodDouble1.Setup(hp => hp.HolidayPeriodOverlap(It.IsAny<IHolidayPeriod>())).Returns(false);

        // Create Holiday Periods list - Only has ONE element
        List<IHolidayPeriod> holidayPeriods =
            new List<IHolidayPeriod> { holidayPeriodDouble1.Object };

        HolidaysPlan holidayPlan =
            new HolidaysPlan(holidayPeriods, colaboratorDouble.Object);

        // Assert
        Assert.True(
            // Act
            // we have access to the list here, so we know its count
            // The function must return true, if properly implemented
            holidayPlan.IsSizeList(holidayPeriods.Count)
        );
    }

    // Testing if comparrison is well done - It asserts false
    [Fact]
    public void IsSizeList_WhenPassingIncorrectInput_ThenReturnFalse()
    {
        // Arrange
        // Test doubles for Holiday Period
        Mock<IHolidayPeriod> holidayPeriodDouble1 = new Mock<IHolidayPeriod>();

        // Test double for Colaborator
        Mock<IColaborator> colaboratorDouble = new Mock<IColaborator>();

        // Holiday period dates must be in the colaborator contract time frame
        colaboratorDouble.Setup(c => c.IsInside(It.IsAny<DateTime>(), It.IsAny<DateTime>())).Returns(true);

        // Can't overlap with any other holiday periods
        holidayPeriodDouble1.Setup(hp => hp.HolidayPeriodOverlap(It.IsAny<IHolidayPeriod>())).Returns(false);

        // Create Holiday Periods list - Only has ONE element
        List<IHolidayPeriod> holidayPeriods =
            new List<IHolidayPeriod> { holidayPeriodDouble1.Object };

        HolidaysPlan holidayPlan =
            new HolidaysPlan(holidayPeriods, colaboratorDouble.Object);

        Random random = new Random();
        int randomInt = random.Next(1, 101);

        // Assert
        Assert.False(
            // Act
            // we have access to the list here, so we know its count
            // The function must return true, if properly implemented
            holidayPlan.IsSizeList(holidayPeriods.Count + randomInt)
        );
    }
}