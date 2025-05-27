using Application.DTO.Collaborators;
using Domain.Models;

namespace WebApi.IntegrationTests.Helpers;

public static class CollaboratorHelper
{
    private static readonly Random _random = new();

    public static CreateCollaboratorDto GenerateRandomCollaboratorDto()
    {
        var deactivationDate = DateTime.UtcNow.AddYears(_random.Next(5, 10));
        var name = Faker.Name.First();
        var surname = Faker.Name.Last();
        return new CreateCollaboratorDto
        {
            Names = name,
            Surnames = surname,
            Email = $"{name}-{surname}@test.com",
            deactivationDate = deactivationDate,
            PeriodDateTime = new PeriodDateTime
            {
                _initDate = DateTime.UtcNow,
                _finalDate = deactivationDate
            }
        };
    }

    public static CreateCollaboratorDto GenerateRandomCollaboratorDtoWithDates(DateTime ini, DateTime end)
    {
        var name = Faker.Name.First();
        var surname = Faker.Name.Last();
        return new CreateCollaboratorDto
        {
            Names = name,
            Surnames = surname,
            Email = $"{name}-{surname}@test.com",
            deactivationDate = end,
            PeriodDateTime = new PeriodDateTime
            {
                _initDate = ini,
                _finalDate = end
            }
        };
    }
}
