using System.Text;
using Newtonsoft.Json;
using Xunit;
using Application.DTO;
using Domain.Models;
using WebApi.IntegrationTests.Helpers;

namespace WebApi.IntegrationTests.Tests;

public class ProjectControllerTests : IntegrationTestBase, IClassFixture<IntegrationTestsWebApplicationFactory<Program>>
{
    public ProjectControllerTests(IntegrationTestsWebApplicationFactory<Program> factory)
        :base(factory.CreateClient())
    {
    }

    [Fact]
    public async Task CreateProject_Returns201Created()
    {
        // Arrange: Create a random project payload
        var projectDTO = ProjectHelper.GenerateRandomProjectDto();

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
        var projectDTO = ProjectHelper.GenerateRandomProjectDto();
        var createdProjectDTO = await PostAndDeserializeAsync<ProjectDTO>("/api/Project", projectDTO);

        // Create Collaborator
        var collaborator = CollaboratorHelper.GenerateRandomCollaboratorDto();
        var collaboratorCreatedDTO = await PostAndDeserializeAsync<CollaboratorCreatedDto>("api/collaborators", collaborator);

        // Create Association
        var associationDTO = new CreateAssociationProjectCollaboratorDTO
        {
            CollaboratorId = collaboratorCreatedDTO.Id,
            PeriodDate = projectDTO.PeriodDate
        };
        //Act
        var createdAssociationDTO = await PostAndDeserializeAsync<AssociationProjectCollaboratorDTO>($"/api/Project/{createdProjectDTO.Id}/collaborators", associationDTO);

        // Assert: Check that the status code is 201 Created
        Assert.NotNull(createdAssociationDTO);
        Assert.Equal(createdAssociationDTO.ProjectId, createdProjectDTO.Id);
        Assert.Equal(createdAssociationDTO.CollaboratorId, collaboratorCreatedDTO.Id);
    }


    [Fact]
    public async Task GetAllCollaborators_ReturnsAssociatedCollaborators()
    {
        // Arrange: Create a random project payload
        var projectDTO = ProjectHelper.GenerateRandomProjectDto();
        var createdProjectDTO = await PostAndDeserializeAsync<ProjectDTO>("/api/Project", projectDTO);

        // Create Collaborator
        var collaborator = CollaboratorHelper.GenerateRandomCollaboratorDto();
        var collaboratorCreatedDTO = await PostAndDeserializeAsync<CollaboratorCreatedDto>("api/collaborators", collaborator);

        // Create Association
        var associationDTO = new CreateAssociationProjectCollaboratorDTO
        {
            CollaboratorId = collaboratorCreatedDTO.Id,
            PeriodDate = projectDTO.PeriodDate
        };
        var createdAssociationDTO = await PostAndDeserializeAsync<AssociationProjectCollaboratorDTO>($"/api/Project/{createdProjectDTO.Id}/collaborators", associationDTO);

        var collaborators = await GetAndDeserializeAsync<List<CollaboratorDTO>>($"/api/Project/{createdProjectDTO.Id}/collaborators");

        // Assert: Check that the status code is 201 Created
        Assert.NotEmpty(collaborators);
        Assert.Equal(collaboratorCreatedDTO.Id, collaborators.First().Id);
    }
}