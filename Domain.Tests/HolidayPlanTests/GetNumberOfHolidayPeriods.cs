using Domain.Models;
using Domain.Interfaces;
using Moq;

namespace Domain.Tests.HolidayPlanTests;

public class GetNumberOfHolidayPeriods
{
    [Fact]
    public void WhenCalculatingNumberOfHolidayPeriods_ThenReturnsCorrectNumber()
    {
        //arrange
        var collaboratorDouble = new Mock<ICollaborator>();

        collaboratorDouble.Setup(c => c.ContractContainsDates(It.IsAny<IPeriodDateTime>())).Returns(true);

        var periodDateDouble1 = new Mock<IPeriodDate>();
        var periodDateDouble2 = new Mock<IPeriodDate>();
        var periodDateDouble3 = new Mock<IPeriodDate>();

        var holidayPeriodDouble1 = new Mock<IHolidayPeriod>();
        var holidayPeriodDouble2 = new Mock<IHolidayPeriod>();
        var holidayPeriodDouble3 = new Mock<IHolidayPeriod>();

        holidayPeriodDouble1.Setup(hp => hp.GetPeriodDate()).Returns(periodDateDouble1.Object);
        holidayPeriodDouble2.Setup(hp => hp.GetPeriodDate()).Returns(periodDateDouble2.Object);
        holidayPeriodDouble3.Setup(hp => hp.GetPeriodDate()).Returns(periodDateDouble3.Object);

        List<IHolidayPeriod> holidayPeriods = new List<IHolidayPeriod>
        {
            holidayPeriodDouble1.Object,
            holidayPeriodDouble2.Object,
            holidayPeriodDouble3.Object
        };

        holidayPeriodDouble1.Setup(hp1 => hp1.Contains(It.IsAny<IHolidayPeriod>())).Returns(false);
        holidayPeriodDouble2.Setup(hp1 => hp1.Contains(It.IsAny<IHolidayPeriod>())).Returns(false);
        holidayPeriodDouble3.Setup(hp1 => hp1.Contains(It.IsAny<IHolidayPeriod>())).Returns(false);


        HolidayPlan holidayPlan = new HolidayPlan(holidayPeriods, collaboratorDouble.Object);

        //act
        int result = holidayPlan.GetNumberOfHolidayPeriods();

        //assert
        Assert.Equal(3, result);
    }
}
