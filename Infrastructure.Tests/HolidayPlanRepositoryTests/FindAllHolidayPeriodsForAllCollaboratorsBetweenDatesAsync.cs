using Domain.Interfaces;
using Infrastructure.Repositories;
using Moq;

namespace Infrastructure.Tests.HolidayPlanRepositoryTests;

public class FindAllHolidayPeriodsForAllCollaboratorsBetweenDatesAsync
{
    [Fact]
    public async Task WhenPassingValidData_ThenReturnsAllHolidayPeriodsForAllCollaboratorsBetweenDatesAsync()
    {
        // arrange
        var collabDouble = new Mock<ICollaborator>();
        var collabList = new List<ICollaborator> { collabDouble.Object };

        var holidayPeriodDouble = new Mock<IHolidayPeriod>();
        var expectedPeriods = new List<IHolidayPeriod> { holidayPeriodDouble.Object };

        var holidayPlanDouble = new Mock<IHolidayPlan>();
        holidayPlanDouble.Setup(hpd => hpd.GetCollaborator()).Returns(collabDouble.Object);
        holidayPlanDouble.Setup(hpd => hpd.GetHolidayPeriodsBetweenPeriod(It.IsAny<IPeriodDate>())).Returns(expectedPeriods);

        var hprepo = new HolidayPlanRepository(new List<IHolidayPlan> { holidayPlanDouble.Object });

        // act
        var result = await hprepo.FindAllHolidayPeriodsForAllCollaboratorsBetweenDatesAsync(collabList, It.IsAny<IPeriodDate>());

        // assert
        Assert.Equal(result, expectedPeriods);
    }
}
