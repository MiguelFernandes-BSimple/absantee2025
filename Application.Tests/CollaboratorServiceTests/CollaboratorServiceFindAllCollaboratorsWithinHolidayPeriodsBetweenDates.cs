using Moq;
using Application.Services;
using Domain.Models;
using Application.DTO.Collaborators;
using Domain.Interfaces;

namespace Application.Tests.CollaboratorServiceTests;

public class CollaboratorServiceFindAllCollaboratorsWithinHolidayPeriodsBetweenDates : CollaboratorServiceTestBase
{
    [Fact]
    public async Task FindAllWithHolidayPeriodsBetweenDates_ReturnsCollaboratorList()
    {
        // arrange
        var period = new PeriodDate(new DateOnly(2025, 05, 10), new DateOnly(2025, 05, 20));

        var userId1 = Guid.NewGuid();
        var collabId1 = Guid.NewGuid();
        var period1 = new PeriodDateTime(DateTime.UtcNow.AddDays(5), DateTime.UtcNow.AddDays(15));
        var collab1 = new Collaborator(collabId1, userId1, period1);

        var userId2 = Guid.NewGuid();
        var collabId2 = Guid.NewGuid();
        var period2 = new PeriodDateTime(DateTime.UtcNow.AddDays(5), DateTime.UtcNow.AddDays(15));
        var collab2 = new Collaborator(collabId2, userId2, period2);

        var holidayPlans = new List<HolidayPlan>{
                new HolidayPlan(collabId1, new List<IHolidayPeriod> { It.IsAny<IHolidayPeriod>() }),
                new HolidayPlan(collabId2, new List<IHolidayPeriod> { It.IsAny<IHolidayPeriod>() })
            };

        HolidayPlanRepositoryDouble.Setup(repo => repo.FindHolidayPlansWithinPeriodAsync(period)).ReturnsAsync(holidayPlans);

        var collabIdsList = new List<Guid> { collabId1, collabId2 };
        var collabList = new List<Collaborator> { collab1, collab2 };
        CollaboratorRepositoryDouble.Setup(repo => repo.GetByIdsAsync(collabIdsList)).ReturnsAsync(collabList);

        MapperDouble.Setup(m => m.Map<CollaboratorDTO>(It.IsAny<Collaborator>()))
               .Returns((Collaborator c) => new CollaboratorDTO(c.Id, c.UserId, c.PeriodDateTime));

        // act
        var result = await CollaboratorService.FindAllWithHolidayPeriodsBetweenDates(period);

        // assert
        Assert.True(result.IsSuccess);
        Assert.Equal(2, result.Value.Count());
        Assert.Contains(result.Value, r => r.Id == collabId1);
        Assert.Contains(result.Value, r => r.UserId == userId1);
        Assert.Contains(result.Value, r => r.Id == collabId2);
        Assert.Contains(result.Value, r => r.UserId == userId2);
    }

    [Fact]
    public async Task FindAllWithHolidayPeriodsBetweenDates_WhenNoCollaboratorsHavePeriodsInRange_ReturnsEmptyList()
    {
        // arrange
        var period = new PeriodDate(new DateOnly(2025, 05, 10), new DateOnly(2025, 05, 20));

        var holidayPlans = new List<HolidayPlan>();

        HolidayPlanRepositoryDouble.Setup(repo => repo.FindHolidayPlansWithinPeriodAsync(period)).ReturnsAsync(holidayPlans);

        var collabIdsList = new List<Guid>();
        var collabList = new List<Collaborator>();
        CollaboratorRepositoryDouble.Setup(repo => repo.GetByIdsAsync(collabIdsList)).ReturnsAsync(collabList);

        MapperDouble.Setup(m => m.Map<CollaboratorDTO>(It.IsAny<Collaborator>()))
               .Returns((Collaborator c) => new CollaboratorDTO(c.Id, c.UserId, c.PeriodDateTime));

        // act
        var result = await CollaboratorService.FindAllWithHolidayPeriodsBetweenDates(period);

        // assert
        Assert.True(result.IsSuccess);
        Assert.Empty(result.Value);
    }
}

