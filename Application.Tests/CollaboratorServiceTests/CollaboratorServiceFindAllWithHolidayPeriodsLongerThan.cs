using Application.Services;
using Moq;
using Domain.Models;

namespace Application.Tests.CollaboratorServiceTests
{
    public class CollaboratorServiceFindAllWithHolidayPeriodsLongerThan : CollaboratorServiceTestBase
    {
        [Fact]
        public async Task FindAllWithHolidayPeriodsLongerThan_ReturnsCorrectCollaborators()
        {
            // arrange
            var days = 5;

            var userId1 = Guid.NewGuid();
            var collabId1 = Guid.NewGuid();
            var period1 = new PeriodDateTime(DateTime.UtcNow.AddDays(5), DateTime.UtcNow.AddDays(15));
            var collab1 = new Collaborator(collabId1, userId1, period1);

            var userId2 = Guid.NewGuid();
            var collabId2 = Guid.NewGuid();
            var period2 = new PeriodDateTime(DateTime.UtcNow.AddDays(5), DateTime.UtcNow.AddDays(15));
            var collab2 = new Collaborator(collabId2, userId2, period2);

            var holidayPlans = new List<HolidayPlan>{
                new HolidayPlan(collabId1, new List<HolidayPeriod> { It.IsAny<HolidayPeriod>() }),
                new HolidayPlan(collabId2, new List<HolidayPeriod> { It.IsAny<HolidayPeriod>() })
            };

            HolidayPlanRepositoryDouble.Setup(repo => repo.FindAllWithHolidayPeriodsLongerThanAsync(days)).ReturnsAsync(holidayPlans);

            var collabIdsList = new List<Guid> { collabId1, collabId2 };
            var expectedList = new List<Collaborator> { collab1, collab2 };

            CollaboratorRepositoryDouble.Setup(repo => repo.GetByIdsAsync(collabIdsList)).ReturnsAsync(expectedList);

            // act
            var result = await CollaboratorService.FindAllWithHolidayPeriodsLongerThan(days);

            // assert
            Assert.Equal(2, result.Count());
            Assert.Contains(result, c => c.Id == collab1.Id);
            Assert.Contains(result, c => c.Id == collab2.Id);
        }


        [Fact]
        public async Task FindAllWithHolidayPeriodsLongerThan_WhenCollaboratorsDontHavePeriosLongerThan_ReturnsEmptyList()
        {
            // arrange
            var days = 5;

            var holidayPlans = new List<HolidayPlan> { };

            HolidayPlanRepositoryDouble.Setup(repo => repo.FindAllWithHolidayPeriodsLongerThanAsync(days)).ReturnsAsync(holidayPlans);

            var collabIdsList = new List<Guid>();
            var expectedList = new List<Collaborator>();

            CollaboratorRepositoryDouble.Setup(repo => repo.GetByIdsAsync(collabIdsList)).ReturnsAsync(expectedList);

            // act
            var result = await CollaboratorService.FindAllWithHolidayPeriodsLongerThan(days);

            // assert
            Assert.Empty(result);
        }
    }
}