using System.Text;
using Newtonsoft.Json;
using Xunit;
using Application.DTO;
using Domain.Models;

namespace WebApi.IntegrationTests.Tests;

public class ProjectControllerTests : IClassFixture<IntegrationTestsWebApplicationFactory<Program>>
{
    private readonly HttpClient _client;
    private readonly Random _random;

    public ProjectControllerTests(IntegrationTestsWebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
        _random = new Random();
    }

    // Helper method to create a random project DTO
    private CreateProjectDTO CreateRandomProject()
    {
        var randomNumber = _random.Next(0, 999999);
        return new CreateProjectDTO
        {
            Title = $"teste {randomNumber}",
            Acronym = $"T{randomNumber}",
            PeriodDate = new PeriodDate
            {
                InitDate = DateOnly.FromDateTime(DateTime.Now.AddDays(_random.Next(1, 60))),  // InitDate between 1 and 60 days from now
                FinalDate = DateOnly.FromDateTime(DateTime.Now.AddDays(_random.Next(80, 120))) // FinalDate between 80 and 120 days from now
            }
        };
    }

    // Helper method to create a random collaborator DTO
    private CreateCollaboratorDto CreateRandomCollaborator()
    {
        var name = Faker.Name.First();
        var surname = Faker.Name.Last();
        var email = $"{name}-{surname}@test.com";
        var deactivationDate = DateTime.UtcNow.AddYears(_random.Next(1, 5));

        return new CreateCollaboratorDto
        {
            Names = name,
            Surnames = surname,
            Email = email,
            deactivationDate = deactivationDate,
            PeriodDateTime = new PeriodDateTime
            {
                _initDate = DateTime.UtcNow,
                _finalDate = deactivationDate
            }
        };
    }

    // Helper method to send a POST request and deserialize the response
    private async Task<T> PostAndDeserializeAsync<T>(string url, object payload)
    {
        var content = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");
        var response = await _client.PostAsync(url, content);
        response.EnsureSuccessStatusCode();
        var responseBody = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<T>(responseBody);
    }

    [Fact]
    public async Task CreateProject_Returns201Created()
    {
        // Arrange: Create a random project payload
        var projectDTO = CreateRandomProject();

        // Act: Send the POST request to create the project
        var createdProjectDTO = await PostAndDeserializeAsync<ProjectDTO>("/api/Project", projectDTO);

        // Assert: Check that the status code is 201 Created
        Assert.NotNull(createdProjectDTO);
        Assert.Equal(projectDTO.Title, createdProjectDTO.Title);
        Assert.Equal(projectDTO.Acronym, createdProjectDTO.Acronym);
        Assert.Equal(projectDTO.PeriodDate.InitDate, createdProjectDTO.PeriodDate.InitDate);
        Assert.Equal(projectDTO.PeriodDate.FinalDate, createdProjectDTO.PeriodDate.FinalDate);
    }

    [Fact]
    public async Task AssociateCollaboratorWithProject_Returns201Created()
    {
        // Arrange: Create a random project payload
        var projectDTO = CreateRandomProject();
        var createdProjectDTO = await PostAndDeserializeAsync<ProjectDTO>("/api/Project", projectDTO);

        // Create Collaborator
        var collaborator = CreateRandomCollaborator();
        var collaboratorCreatedDTO = await PostAndDeserializeAsync<CollaboratorCreatedDto>("api/Collaborator", collaborator);

        // Create Association
        var associationDTO = new CreateAssociationProjectCollaboratorDTO
        {
            CollaboratorId = collaboratorCreatedDTO.Id,
            PeriodDate = projectDTO.PeriodDate
        };
        var createdAssociationDTO = await PostAndDeserializeAsync<AssociationProjectCollaboratorDTO>($"/api/Project/{createdProjectDTO.Id}/collaborators", associationDTO);

        // Assert: Check that the status code is 201 Created
        Assert.NotNull(createdAssociationDTO);
        Assert.Equal(createdAssociationDTO.ProjectId, createdProjectDTO.Id);
        Assert.Equal(createdAssociationDTO.CollaboratorId, collaboratorCreatedDTO.Id);
    }
}