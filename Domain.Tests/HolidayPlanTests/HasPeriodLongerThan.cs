using Domain;
using Moq;

public class HasPeriodLongerThanTests
{
    [Theory]
    [InlineData(true, true)]
    [InlineData(true, false)]
    public void WhenCheckingIfHolidayPlanHasPeriodLongerThanGivenDays_ThenReturnTrueIfAtLeastOnePeriodIsLonger(
        bool methodResult1,
        bool methodResult2
    )
    {
        //arrange
        Mock<IHolidayPeriod> holidayPeriodDouble1 = new Mock<IHolidayPeriod>();
        Mock<IHolidayPeriod> holidayPeriodDouble2 = new Mock<IHolidayPeriod>();

        holidayPeriodDouble1.Setup(h => h.IsLongerThan(It.IsAny<int>())).Returns(methodResult1);
        holidayPeriodDouble1
            .Setup(p => p.Contains(It.IsAny<IHolidayPeriod>()))
            .Returns(false);

        holidayPeriodDouble2.Setup(h => h.IsLongerThan(It.IsAny<int>())).Returns(methodResult2);
        holidayPeriodDouble2
            .Setup(p => p.Contains(It.IsAny<IHolidayPeriod>()))
            .Returns(false);

        Mock<ICollaborator> collaboratorDouble = new Mock<ICollaborator>();
        collaboratorDouble
            .Setup(c => c.ContractContainsDates(It.IsAny<DateTime>(), It.IsAny<DateTime>()))
            .Returns(true);

        IHolidayPlan holidayPlan = new HolidayPlan(
            new List<IHolidayPeriod> { holidayPeriodDouble1.Object, holidayPeriodDouble2.Object },
            collaboratorDouble.Object
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

        holidayPeriodDouble1.Setup(h => h.IsLongerThan(It.IsAny<int>())).Returns(false);
        holidayPeriodDouble1
            .Setup(p => p.Contains(It.IsAny<IHolidayPeriod>()))
            .Returns(false);

        holidayPeriodDouble2.Setup(h => h.IsLongerThan(It.IsAny<int>())).Returns(false);
        holidayPeriodDouble2
            .Setup(p => p.Contains(It.IsAny<IHolidayPeriod>()))
            .Returns(false);

        Mock<ICollaborator> collaboratorDouble = new Mock<ICollaborator>();
        collaboratorDouble
            .Setup(c => c.ContractContainsDates(It.IsAny<DateTime>(), It.IsAny<DateTime>()))
            .Returns(true);

        IHolidayPlan holidayPlan = new HolidayPlan(
            new List<IHolidayPeriod> { holidayPeriodDouble1.Object, holidayPeriodDouble2.Object },
            collaboratorDouble.Object
        );

        //act
        bool result = holidayPlan.HasPeriodLongerThan(5);

        //assert
        Assert.False(result);
    }
}