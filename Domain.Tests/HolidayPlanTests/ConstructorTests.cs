using Domain;
using Moq;

public class ConstructorTests
{
    // Happy Path - Testing constructor with Single HolidayPeriod
    // Can Instatiate a HolidayPlan Object successfuly
    [Fact]
    public void WhenPassingValidSinglePeriod_ThenHolidayPlanIsCreated()
    {
        // Arrange
        // Test double for Holiday Period
        Mock<IHolidayPeriod> holidayPeriodDouble = new Mock<IHolidayPeriod>();

        // Test double for Collaborator
        Mock<ICollaborator> collaboratorDouble = new Mock<ICollaborator>();

        // Holiday period dates must be in the collaborator contract time frame

        collaboratorDouble
            .Setup(c => c.ContractContainsDates(It.IsAny<DateTime>(), It.IsAny<DateTime>()))
            .Returns(true);

        // Act
        HolidayPlan holidayPlan = new HolidayPlan(
            holidayPeriodDouble.Object,
            collaboratorDouble.Object
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

        // Test double for Collaborator
        Mock<ICollaborator> collaboratorDouble = new Mock<ICollaborator>();

        // Holiday period dates must be in the collaborator contract time frame

        collaboratorDouble
            .Setup(c => c.ContractContainsDates(It.IsAny<DateTime>(), It.IsAny<DateTime>()))
            .Returns(true);

        // Can't overlap with any other holiday periods
        holidayPeriodDouble1
            .Setup(hp1 => hp1.Contains(It.IsAny<IHolidayPeriod>()))
            .Returns(false);
        holidayPeriodDouble2
            .Setup(hp2 => hp2.Contains(It.IsAny<IHolidayPeriod>()))
            .Returns(false);
        holidayPeriodDouble3
            .Setup(hp3 => hp3.Contains(It.IsAny<IHolidayPeriod>()))
            .Returns(false);

        // Create Holiday Periods list
        List<IHolidayPeriod> holidayPeriods = new List<IHolidayPeriod>
        {
            holidayPeriodDouble1.Object,
            holidayPeriodDouble2.Object,
            holidayPeriodDouble3.Object,
        };

        // Act
        HolidayPlan holidayPlan = new HolidayPlan(holidayPeriods, collaboratorDouble.Object);

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

        // Test double for Collaborator
        Mock<ICollaborator> collaboratorDouble = new Mock<ICollaborator>();

        // Holiday period dates must be in the collaborator contract time frame

        collaboratorDouble
            .Setup(c => c.ContractContainsDates(It.IsAny<DateTime>(), It.IsAny<DateTime>()))
            .Returns(true);

        // There is overlap with other holiday periods
        // If there is two dates that overlap, the exception shall happen
        holidayPeriodDouble1.Setup(hp => hp.Contains(It.IsAny<IHolidayPeriod>())).Returns(true);

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
                new HolidayPlan(holidayPeriods, collaboratorDouble.Object)
        );

        Assert.Equal("Invalid Arguments", exception.Message);
    }

    // Validate exception - Holiday period dates are not in the collaborator contract time frame
    [Fact]
    public void WhenHolidayPeriodNotInCollabContractTimeFrame_ThenThrowException()
    {
        // Arrange
        // Test doubles for Holiday Period
        Mock<IHolidayPeriod> holidayPeriodDouble1 = new Mock<IHolidayPeriod>();
        Mock<IHolidayPeriod> holidayPeriodDouble2 = new Mock<IHolidayPeriod>();

        // Test double for Collaborator
        Mock<ICollaborator> collaboratorDouble = new Mock<ICollaborator>();

        // At least one holiday period is outside the collaborator contract time frame - ContractContainsDates = false

        // In order for the exception to be thrown
        collaboratorDouble
            .Setup(c => c.ContractContainsDates(It.IsAny<DateTime>(), It.IsAny<DateTime>()))
            .Returns(false);

        // There no overlap with other holiday periods
        holidayPeriodDouble1.Setup(hp1 => hp1.Contains(It.IsAny<IHolidayPeriod>())).Returns(false);
        holidayPeriodDouble2.Setup(hp2 => hp2.Contains(It.IsAny<IHolidayPeriod>())).Returns(false);

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
                new HolidayPlan(holidayPeriods, collaboratorDouble.Object)
        );

        Assert.Equal("Invalid Arguments", exception.Message);
    }

}