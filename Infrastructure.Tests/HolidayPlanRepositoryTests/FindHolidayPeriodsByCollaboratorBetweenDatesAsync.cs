using System.Threading.Tasks;
using Domain.Interfaces;
using Infrastructure.Repositories;
using Moq;

namespace Infrastructure.Tests.HolidayPlanRepositoryTests;

public class FindHolidayPeriodsByCollaboratorBetweenDatesAsync
{
    [Fact]
    public async Task WhenPassingCorrectDataAsync_ThenReturnsPeriodsByCollaboratorBetweenDates()
    {
        // arrange
        var collabDouble = new Mock<ICollaborator>();

        var holidayPeriodDouble = new Mock<IHolidayPeriod>();
        holidayPeriodDouble.Setup(hpd => hpd.Intersects(It.IsAny<IPeriodDate>())).Returns(true);

        var expectedPeriod = new List<IHolidayPeriod> { holidayPeriodDouble.Object };


        var holidayPlanDouble = new Mock<IHolidayPlan>();
        holidayPlanDouble.Setup(hpd => hpd.HasCollaborator(collabDouble.Object)).Returns(true);
        holidayPlanDouble.Setup(hpd => hpd.GetHolidayPeriodsBetweenPeriod(It.IsAny<IPeriodDate>())).Returns(expectedPeriod);


        var hprepo = new HolidayPlanRepository(new List<IHolidayPlan> { holidayPlanDouble.Object });

        // act
        var result = await hprepo.FindHolidayPeriodsByCollaboratorBetweenDatesAsync(collabDouble.Object, It.IsAny<IPeriodDate>());

        // assert
        Assert.Equal(result, expectedPeriod);
    }

    [Fact]
    public async Task WhenPassingCorrectDataAsync_ThenReturnsEmptyList()
    {
        // arrange
        var collabDouble = new Mock<ICollaborator>();

        var holidayPeriodDouble = new Mock<IHolidayPeriod>();
        holidayPeriodDouble.Setup(hpd => hpd.Intersects(It.IsAny<IPeriodDate>())).Returns(true);

        var expectedPeriod = new List<IHolidayPeriod>();

        var holidayPlanDouble = new Mock<IHolidayPlan>();
        holidayPlanDouble.Setup(hpd => hpd.HasCollaborator(collabDouble.Object)).Returns(false);


        var hprepo = new HolidayPlanRepository(new List<IHolidayPlan> { holidayPlanDouble.Object });

        // act
        var result = await hprepo.FindHolidayPeriodsByCollaboratorBetweenDatesAsync(collabDouble.Object, It.IsAny<IPeriodDate>());

        // assert
        Assert.Equal(result, expectedPeriod);
    }
}
