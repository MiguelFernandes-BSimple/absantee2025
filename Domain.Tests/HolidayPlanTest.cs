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

        colaboratorDouble
            .Setup(c => c.ContainsDates(It.IsAny<DateTime>(), It.IsAny<DateTime>()))
            .Returns(true);


        // Act
        HolidayPlan holidayPlan = new HolidayPlan(
            holidayPeriodDouble.Object,
            colaboratorDouble.Object
        );

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

        colaboratorDouble
            .Setup(c => c.ContainsDates(It.IsAny<DateTime>(), It.IsAny<DateTime>()))
            .Returns(true);


        // Can't overlap with any other holiday periods
        holidayPeriodDouble1
            .Setup(hp1 => hp1.HolidayPeriodOverlap(It.IsAny<IHolidayPeriod>()))
            .Returns(false);
        holidayPeriodDouble2
            .Setup(hp2 => hp2.HolidayPeriodOverlap(It.IsAny<IHolidayPeriod>()))
            .Returns(false);
        holidayPeriodDouble3
            .Setup(hp3 => hp3.HolidayPeriodOverlap(It.IsAny<IHolidayPeriod>()))
            .Returns(false);

        // Create Holiday Periods list
        List<IHolidayPeriod> holidayPeriods = new List<IHolidayPeriod>
        {
            holidayPeriodDouble1.Object,
            holidayPeriodDouble2.Object,
            holidayPeriodDouble3.Object,
        };

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

        colaboratorDouble
            .Setup(c => c.ContainsDates(It.IsAny<DateTime>(), It.IsAny<DateTime>()))
            .Returns(true);


        // There is overlap with other holiday periods
        // If there is two dates that overlap, the exception shall happen
        holidayPeriodDouble1
            .Setup(hp => hp.HolidayPeriodOverlap(It.IsAny<IHolidayPeriod>()))
            .Returns(true);

        // Create Holiday Periods list
        List<IHolidayPeriod> holidayPeriods = new List<IHolidayPeriod>
        {
            holidayPeriodDouble1.Object,
            holidayPeriodDouble2.Object,
        };

        // Assert
        ArgumentException exception = Assert.Throws<ArgumentException>(
            () =>
                // Act
                new HolidayPlan(holidayPeriods, colaboratorDouble.Object)
        );

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
        colaboratorDouble
            .Setup(c => c.ContainsDates(It.IsAny<DateTime>(), It.IsAny<DateTime>()))
            .Returns(false);


        // There no overlap with other holiday periods
        holidayPeriodDouble1
            .Setup(hp1 => hp1.HolidayPeriodOverlap(It.IsAny<IHolidayPeriod>()))
            .Returns(false);
        holidayPeriodDouble2
            .Setup(hp2 => hp2.HolidayPeriodOverlap(It.IsAny<IHolidayPeriod>()))
            .Returns(false);

        // Create Holiday Periods list
        List<IHolidayPeriod> holidayPeriods = new List<IHolidayPeriod>
        {
            holidayPeriodDouble1.Object,
            holidayPeriodDouble2.Object,
        };

        // Assert
        ArgumentException exception = Assert.Throws<ArgumentException>(
            () =>
                // Act
                new HolidayPlan(holidayPeriods, colaboratorDouble.Object)
        );

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

        colaboratorDouble
            .Setup(c => c.ContainsDates(It.IsAny<DateTime>(), It.IsAny<DateTime>()))
            .Returns(true);


        // Can't overlap with any other holiday periods
        holidayPeriodDouble1
            .Setup(hp1 => hp1.HolidayPeriodOverlap(It.IsAny<IHolidayPeriod>()))
            .Returns(false);

        // Create Holiday Periods list (wth only one period)
        List<IHolidayPeriod> holidayPeriods = new List<IHolidayPeriod>
        {
            holidayPeriodDouble1.Object,
        };

        // Instatiate Holiday plan class object
        HolidayPlan holidayPlan = new HolidayPlan(holidayPeriods, colaboratorDouble.Object);

        // Test double for Holiday Period to be added
        Mock<IHolidayPeriod> holidayPeriodDoubleToAdd = new Mock<IHolidayPeriod>();

        // It doesn't overlap with any other period
        holidayPeriodDoubleToAdd
            .Setup(hp2 => hp2.HolidayPeriodOverlap(It.IsAny<IHolidayPeriod>()))
            .Returns(false);

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

        colaboratorDouble
            .Setup(c => c.ContainsDates(It.IsAny<DateTime>(), It.IsAny<DateTime>()))
            .Returns(true);


        // Create Holiday Periods list (wth only one period)
        List<IHolidayPeriod> holidayPeriods = new List<IHolidayPeriod>
        {
            holidayPeriodDouble1.Object,
        };

        // Instatiate Holiday plan class object
        HolidayPlan holidayPlan = new HolidayPlan(holidayPeriods, colaboratorDouble.Object);

        // Test double for holiday period to be added to plan
        Mock<IHolidayPeriod> holidayPeriodDoubleToAdd = new Mock<IHolidayPeriod>();

        // Holiday period to be added intersects - Therefore it's not added
        holidayPeriodDoubleToAdd
            .Setup(hp => hp.HolidayPeriodOverlap(It.IsAny<IHolidayPeriod>()))
            .Returns(true);

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
        colaboratorDouble
            .SetupSequence(c => c.ContainsDates(It.IsAny<DateTime>(), It.IsAny<DateTime>()))
            .Returns(true)
            .Returns(false);


        // Create Holiday Periods list (wth only one period)
        List<IHolidayPeriod> holidayPeriods = new List<IHolidayPeriod>
        {
            holidayPeriodDouble1.Object,
        };

        // Instatiate Holiday plan class object
        HolidayPlan holidayPlan = new HolidayPlan(holidayPeriods, colaboratorDouble.Object);

        // Test double for holiday period to be added to plan
        Mock<IHolidayPeriod> holidayPeriodDoubleToAdd = new Mock<IHolidayPeriod>();

        // holiday period to be added can't overlap with any other holiday periods
        holidayPeriodDoubleToAdd
            .Setup(hp => hp.HolidayPeriodOverlap(It.IsAny<IHolidayPeriod>()))
            .Returns(false);

        // Act
        // Add a seconda holiday period
        bool result = holidayPlan.AddHolidayPeriod(holidayPeriodDoubleToAdd.Object);

        // Assert
        Assert.False(result);
    }


    [Fact]
    public void WhenSameColaborator_ReturnsTrue()
    {
        var colaborator = new Mock<IColaborator>();
        var holidayPeriod = new Mock<IHolidayPeriod>();

        colaborator
            .Setup(c => c.ContainsDates(It.IsAny<DateTime>(), It.IsAny<DateTime>()))
            .Returns(true);

        holidayPeriod
            .Setup(hp => hp.HolidayPeriodOverlap(It.IsAny<IHolidayPeriod>()))
            .Returns(false);
        holidayPeriod.Setup(hp => hp.GetInitDate()).Returns(new DateOnly(2025, 1, 1));
        holidayPeriod.Setup(hp => hp.GetFinalDate()).Returns(new DateOnly(2025, 1, 10));

        var holidayPlan = new HolidayPlan(holidayPeriod.Object, colaborator.Object);

        // Act
        var result = holidayPlan.HasColaborator(colaborator.Object);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void WhenDifferentColaborator_ReturnsFalse()
    {
        // Arrange
        var colaborator1 = new Mock<IColaborator>();
        var colaborator2 = new Mock<IColaborator>();
        var holidayPeriod = new Mock<IHolidayPeriod>();

        colaborator1
            .Setup(c => c.ContainsDates(It.IsAny<DateTime>(), It.IsAny<DateTime>()))
            .Returns(true);

        holidayPeriod
            .Setup(hp => hp.HolidayPeriodOverlap(It.IsAny<IHolidayPeriod>()))
            .Returns(false);
        holidayPeriod.Setup(hp => hp.GetInitDate()).Returns(new DateOnly(2025, 1, 1));
        holidayPeriod.Setup(hp => hp.GetFinalDate()).Returns(new DateOnly(2025, 1, 10));

        var holidayPlan = new HolidayPlan(holidayPeriod.Object, colaborator1.Object);

        // Act
        var result = holidayPlan.HasColaborator(colaborator2.Object);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void GetColaborator_ReturnsSameReference()
    {
        // Arrange
        var colaborator = new Mock<IColaborator>();
        var holidayPeriod = new Mock<IHolidayPeriod>();

        colaborator
            .Setup(c => c.ContainsDates(It.IsAny<DateTime>(), It.IsAny<DateTime>()))
            .Returns(true);

        holidayPeriod
            .Setup(hp => hp.HolidayPeriodOverlap(It.IsAny<IHolidayPeriod>()))
            .Returns(false);
        holidayPeriod.Setup(hp => hp.GetInitDate()).Returns(new DateOnly(2025, 1, 1));
        holidayPeriod.Setup(hp => hp.GetFinalDate()).Returns(new DateOnly(2025, 1, 10));

        var holidayPlan = new HolidayPlan(holidayPeriod.Object, colaborator.Object);

        // Act
        var result = holidayPlan.GetColaborator();

        // Assert
        Assert.Same(colaborator.Object, result);
    }


    public static IEnumerable<object[]> GetHolidayDaysBetweenData()
    {
        yield return new object[] { new List<int> { 3, 2 }, 5 };
        yield return new object[] { new List<int> { 0 }, 0 };
        yield return new object[] { new List<int> { 10, 0, 5 }, 15 };
    }

    [Theory]
    [MemberData(nameof(GetHolidayDaysBetweenData))]
    public void GetNumberOfHolidayDaysBetween_ShouldReturnCorrectSumValue(List<int> daysByPeriod, int expectedTotal)
    {
        // Arrange
        var holidayPeriods = new List<IHolidayPeriod>();
        foreach (var days in daysByPeriod)
        {
            var holidayPeriodDouble = new Mock<IHolidayPeriod>();
            holidayPeriodDouble
                .Setup(p => p.GetNumberOfCommonDaysBetweenPeriods(It.IsAny<DateOnly>(), It.IsAny<DateOnly>()))
                .Returns(days);
            holidayPeriods.Add(holidayPeriodDouble.Object);
        }

        var collaboratorDouble = new Mock<IColaborator>();
        collaboratorDouble.Setup(c => c.ContractContainsDates(It.IsAny<DateTime>(), It.IsAny<DateTime>())).Returns(true);

        var holidayPlan = new HolidayPlan(holidayPeriods, collaboratorDouble.Object);

        var startDate = new DateOnly(2025, 01, 01);
        var finalDate = new DateOnly(2025, 01, 10);

        // Act
        var result = holidayPlan.GetNumberOfHolidayDaysBetween(startDate, finalDate);

        // Assert
        Assert.Equal(expectedTotal, result);
    }


    [Theory]
    [InlineData(true, true)]
    [InlineData(true, false)]
    public void WhenCheckingIfHolidayPlanHasPeriodLongerThanGivenDays_ThenReturnTrueIfAtLeastOnePeriodIsLonger(bool methodResult1, bool methodResult2)
    {
        //arrange
        Mock<IHolidayPeriod> holidayPeriodDouble1 = new Mock<IHolidayPeriod>();
        Mock<IHolidayPeriod> holidayPeriodDouble2 = new Mock<IHolidayPeriod>();

        holidayPeriodDouble1
            .Setup(h => h.IsLongerThan(It.IsAny<int>()))
            .Returns(methodResult1);
        holidayPeriodDouble1
        .Setup(p => p.HolidayPeriodOverlap(It.IsAny<IHolidayPeriod>()))
        .Returns(false);

        holidayPeriodDouble2
            .Setup(h => h.IsLongerThan(It.IsAny<int>()))
            .Returns(methodResult2);
        holidayPeriodDouble2
            .Setup(p => p.HolidayPeriodOverlap(It.IsAny<IHolidayPeriod>()))
            .Returns(false);

        Mock<IColaborator> colaboratorDouble = new Mock<IColaborator>();
        colaboratorDouble
            .Setup(c => c.ContractContainsDates(It.IsAny<DateTime>(), It.IsAny<DateTime>()))
            .Returns(true);

        IHolidayPlan holidayPlan = new HolidayPlan(
            new List<IHolidayPeriod> { holidayPeriodDouble1.Object, holidayPeriodDouble2.Object },
            colaboratorDouble.Object
        );

        //act
        bool result = holidayPlan.HasPeriodLongerThan(5);

        //assert
        Assert.True(result);
    }

    [Fact]
    public void WhenCheckingIfHolidayPlanHasPeriodLongerThanGivenDays_ThenReturnFalseIfNoPeriodIsLonger()
    {
        //arrange
        Mock<IHolidayPeriod> holidayPeriodDouble1 = new Mock<IHolidayPeriod>();
        Mock<IHolidayPeriod> holidayPeriodDouble2 = new Mock<IHolidayPeriod>();

        holidayPeriodDouble1
            .Setup(h => h.IsLongerThan(It.IsAny<int>()))
            .Returns(false);
        holidayPeriodDouble1
        .Setup(p => p.HolidayPeriodOverlap(It.IsAny<IHolidayPeriod>()))
        .Returns(false);

        holidayPeriodDouble2
            .Setup(h => h.IsLongerThan(It.IsAny<int>()))
            .Returns(false);
        holidayPeriodDouble2
            .Setup(p => p.HolidayPeriodOverlap(It.IsAny<IHolidayPeriod>()))
            .Returns(false);

        Mock<IColaborator> colaboratorDouble = new Mock<IColaborator>();
        colaboratorDouble
            .Setup(c => c.ContractContainsDates(It.IsAny<DateTime>(), It.IsAny<DateTime>()))
            .Returns(true);

        IHolidayPlan holidayPlan = new HolidayPlan(
            new List<IHolidayPeriod> { holidayPeriodDouble1.Object, holidayPeriodDouble2.Object },
            colaboratorDouble.Object
        );

        //act
        bool result = holidayPlan.HasPeriodLongerThan(5);

        //assert
        Assert.False(result);
    }
}
