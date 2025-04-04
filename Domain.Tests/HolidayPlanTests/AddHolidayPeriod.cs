using Domain.Models;
using Domain.Interfaces;
using Moq;

namespace Domain.Tests.HolidayPlanTests;

public class AddHolidayPlan
{
    // Validate if a valid Holiday period can be added to a holiday plan
    // If its successfully added, returns true
    [Fact]
    public void WhenPassingValidPeriod_ThenAddHolidayPeriodReturnTrue()
    {
        // Arrange
        // Test double for Holiday Period
        Mock<IHolidayPeriod> holidayPeriodDouble1 = new Mock<IHolidayPeriod>();
        Mock<IPeriodDate> periodDateDouble1 = new Mock<IPeriodDate>();

        holidayPeriodDouble1.Setup(hp => hp.GetPeriodDate()).Returns(periodDateDouble1.Object);

        // Test double for Collaborator
        Mock<ICollaborator> collaboratorDouble = new Mock<ICollaborator>();

        // Holiday period dates must be in the collaborator contract time frame

        collaboratorDouble
            .Setup(c => c.ContractContainsDates(It.IsAny<IPeriodDateTime>()))
            .Returns(true);

        // Can't overlap with any other holiday periods
        holidayPeriodDouble1.Setup(hp1 => hp1.Contains(It.IsAny<IHolidayPeriod>())).Returns(false);

        // Create Holiday Periods list (wth only one period)
        List<IHolidayPeriod> holidayPeriods = new List<IHolidayPeriod>
        {
            holidayPeriodDouble1.Object,
        };

        // Instatiate Holiday plan class object
        HolidayPlan holidayPlan = new HolidayPlan(holidayPeriods, collaboratorDouble.Object);

        // Test double for Holiday Period to be added
        Mock<IHolidayPeriod> holidayPeriodDoubleToAdd = new Mock<IHolidayPeriod>();

        // It doesn't overlap with any other period
        holidayPeriodDoubleToAdd.Setup(hp2 => hp2.Contains(It.IsAny<IHolidayPeriod>())).Returns(false);
        holidayPeriodDoubleToAdd.Setup(hp => hp.GetPeriodDate()).Returns(periodDateDouble1.Object);

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
        Mock<IPeriodDate> periodDateDouble1 = new Mock<IPeriodDate>();
        holidayPeriodDouble1.Setup(hp => hp.GetPeriodDate()).Returns(periodDateDouble1.Object);

        // Test double for Collaborator
        Mock<ICollaborator> collaboratorDouble = new Mock<ICollaborator>();

        // Holiday period dates must be in the collaborator contract time frame

        collaboratorDouble
            .Setup(c => c.ContractContainsDates(It.IsAny<IPeriodDateTime>()))
            .Returns(true);

        // Create Holiday Periods list (wth only one period)
        List<IHolidayPeriod> holidayPeriods = new List<IHolidayPeriod>
        {
            holidayPeriodDouble1.Object,
        };

        // Instatiate Holiday plan class object
        HolidayPlan holidayPlan = new HolidayPlan(holidayPeriods, collaboratorDouble.Object);

        // Test double for holiday period to be added to plan
        Mock<IHolidayPeriod> holidayPeriodDoubleToAdd = new Mock<IHolidayPeriod>();
        holidayPeriodDoubleToAdd.Setup(hp => hp.GetPeriodDate()).Returns(periodDateDouble1.Object);

        // Holiday period to be added intersects - Therefore it's not added
        holidayPeriodDoubleToAdd.Setup(hp => hp.Contains(It.IsAny<IHolidayPeriod>())).Returns(true);

        // Act
        // Add a seconda holiday period
        bool result = holidayPlan.AddHolidayPeriod(holidayPeriodDoubleToAdd.Object);

        // Assert
        Assert.False(result);
    }

    // Validate bool result when the Holiday period to be added to holiday plan
    // isn't in the collaborator contract time frame
    // If its not added, returns false
    [Fact]
    public void WhenPassingPeriodNotInCollabContractTimeFrame_ThenAddHolidayPeriodReturnFalse()
    {
        // Arrange
        // Test doubles for Holiday Period
        Mock<IHolidayPeriod> holidayPeriodDouble1 = new Mock<IHolidayPeriod>();
        Mock<IPeriodDate> periodDateDouble1 = new Mock<IPeriodDate>();

        holidayPeriodDouble1.Setup(hp => hp.GetPeriodDate()).Returns(periodDateDouble1.Object);

        // Test double for Collaborator
        Mock<ICollaborator> collaboratorDouble = new Mock<ICollaborator>();

        // The first Holiday period date will be in the collaborator contract time frame
        // Therefore it will be added
        // The second one shall not be, to verify if it returns false
        collaboratorDouble
            .SetupSequence(c => c.ContractContainsDates(It.IsAny<IPeriodDateTime>()))
            .Returns(true)
            .Returns(false);

        // Create Holiday Periods list (wth only one period)
        List<IHolidayPeriod> holidayPeriods = new List<IHolidayPeriod>
        {
            holidayPeriodDouble1.Object,
        };

        // Instatiate Holiday plan class object
        HolidayPlan holidayPlan = new HolidayPlan(holidayPeriods, collaboratorDouble.Object);

        // Test double for holiday period to be added to plan
        Mock<IHolidayPeriod> holidayPeriodDoubleToAdd = new Mock<IHolidayPeriod>();

        // holiday period to be added can't overlap with any other holiday periods
        holidayPeriodDoubleToAdd.Setup(hp => hp.Contains(It.IsAny<IHolidayPeriod>())).Returns(false);
        holidayPeriodDoubleToAdd.Setup(hp => hp.GetPeriodDate()).Returns(periodDateDouble1.Object);

        // Act
        // Add a seconda holiday period
        bool result = holidayPlan.AddHolidayPeriod(holidayPeriodDoubleToAdd.Object);

        // Assert
        Assert.False(result);
    }
}
