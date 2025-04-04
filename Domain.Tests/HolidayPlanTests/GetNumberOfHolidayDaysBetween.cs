using Domain.Models;
using Domain.Interfaces;
using Moq;

namespace Domain.Tests.HolidayPlanTests;

public class GetNumberOfHolidayDaysBetween
{
    public static IEnumerable<object[]> GetHolidayDaysBetweenData()
    {
        yield return new object[]
        {
            new List<int> { 3, 2 },
            5,
        };
        yield return new object[]
        {
            new List<int> { 0 },
            0,
        };
        yield return new object[]
        {
            new List<int> { 10, 0, 5 },
            15,
        };
    }

    [Theory]
    [MemberData(nameof(GetHolidayDaysBetweenData))]
    public void GetNumberOfHolidayDaysBetween_ShouldReturnCorrectSumValue(
        List<int> daysByPeriod,
        int expectedTotal
    )
    {
        // Arrange
        var holidayPeriods = new List<IHolidayPeriod>();

        var periodDate1 = new Mock<IPeriodDate>();
        foreach (var days in daysByPeriod)
        {
            var holidayPeriodDouble = new Mock<IHolidayPeriod>();
            holidayPeriodDouble.Setup(hp => hp.GetPeriodDate()).Returns(periodDate1.Object);

            holidayPeriodDouble
                .Setup(p =>
                    p.GetNumberOfCommonUtilDaysBetweenPeriods(
                        It.IsAny<IPeriodDate>()
                    )
                )
                .Returns(days);
            holidayPeriods.Add(holidayPeriodDouble.Object);
        }

        var collaboratorDouble = new Mock<ICollaborator>();
        collaboratorDouble
            .Setup(c => c.ContractContainsDates(It.IsAny<IPeriodDateTime>()))
            .Returns(true);

        var holidayPlan = new HolidayPlan(holidayPeriods, collaboratorDouble.Object);

        var periodDateDouble = new Mock<IPeriodDate>();

        // Act
        var result = holidayPlan.GetNumberOfHolidayDaysBetween(periodDateDouble.Object);

        // Assert
        Assert.Equal(expectedTotal, result);
    }
}