using Application.DTO;
using Domain.Models;

namespace WebApi.IntegrationTests.Helpers;

public static class ProjectHelper
{
    private static readonly Random _random = new();

    public static CreateProjectDTO GenerateRandomProjectDto()
    {
        var number = _random.Next(0, 999999);
        return new CreateProjectDTO
        {
            Title = $"teste {number}",
            Acronym = $"T{number}",
            PeriodDate = new PeriodDate
            {
                InitDate = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(_random.Next(1, 60))),
                FinalDate = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(_random.Next(80, 120)))
            }
        };
    }

    public static CreateProjectDTO GenerateRandomProjectDto(DateOnly initDate, DateOnly finalDate)
    {
        var number = _random.Next(0, 999999);
        return new CreateProjectDTO
        {
            Title = $"teste {number}",
            Acronym = $"T{number}",
            PeriodDate = new PeriodDate
            {
                InitDate = initDate,
                FinalDate = finalDate
            }
        };
    }
}
