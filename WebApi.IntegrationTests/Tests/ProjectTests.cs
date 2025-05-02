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
        // Arrange
        // Create a random project payload
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
        // Arrange
        // Create a random project payload
        var projectDTO = ProjectHelper.GenerateRandomProjectDto();
        var projectCreatedDTO = await PostAndDeserializeAsync<ProjectDTO>("/api/Project", projectDTO);

        // Create Collaborator
        var collaborator = CollaboratorHelper.GenerateRandomCollaboratorDto();
        var collaboratorCreatedDTO = await PostAndDeserializeAsync<CollaboratorCreatedDto>("api/collaborators", collaborator);

        // Create Association
        var associationDTO = AssociationProjectCollaboratorHelper.
            GenerateCreateAssociationProjectCollaboratorDto(collaboratorCreatedDTO.Id, projectCreatedDTO.PeriodDate);
        //Act
        var createdAssociationDTO = await PostAndDeserializeAsync<AssociationProjectCollaboratorDTO>($"/api/Project/{projectCreatedDTO.Id}/collaborators", associationDTO);

        // Assert: Check that the status code is 201 Created
        Assert.NotNull(createdAssociationDTO);
        Assert.Equal(createdAssociationDTO.ProjectId, projectCreatedDTO.Id);
        Assert.Equal(createdAssociationDTO.CollaboratorId, collaboratorCreatedDTO.Id);
    }


    [Fact]
    public async Task GetAllCollaborators_ReturnsAssociatedCollaborators()
    {
        // Arrange
        // Create a random project payload
        var projectDTO = ProjectHelper.GenerateRandomProjectDto();
        var projectCreatedDTO = await PostAndDeserializeAsync<ProjectDTO>("/api/Project", projectDTO);

        // Create Collaborator
        var collaborator = CollaboratorHelper.GenerateRandomCollaboratorDto();
        var collaboratorCreatedDTO = await PostAndDeserializeAsync<CollaboratorCreatedDto>("api/collaborators", collaborator);

        // Create Association
        var associationDTO = AssociationProjectCollaboratorHelper.
            GenerateCreateAssociationProjectCollaboratorDto(collaboratorCreatedDTO.Id, projectCreatedDTO.PeriodDate);
        var createdAssociationDTO = await PostAndDeserializeAsync<AssociationProjectCollaboratorDTO>($"/api/Project/{projectCreatedDTO.Id}/collaborators", associationDTO);

        //Act
        var collaborators = await GetAndDeserializeAsync<List<CollaboratorDTO>>($"/api/Project/{projectCreatedDTO.Id}/collaborators");

        // Assert: Check that the status code is 201 Created
        Assert.NotEmpty(collaborators);
        Assert.Equal(collaboratorCreatedDTO.Id, collaborators.First().Id);
    }

    [Fact]
    public async Task GetAllCollaboratorsByPeriod_ReturnsAssociatedCollaborators()
    {
        // Arrange: Create a random project payload
        var projectDTO = ProjectHelper.GenerateRandomProjectDto(
            DateOnly.FromDateTime(DateTime.Today),
            DateOnly.FromDateTime(DateTime.Today.AddYears(4))
            );
        var projectCreatedDTO = await PostAndDeserializeAsync<ProjectDTO>("/api/Project", projectDTO);

        // Create Collaborators
        var collaborator1 = CollaboratorHelper.GenerateRandomCollaboratorDto();
        var collaboratorCreatedDTO1 = await PostAndDeserializeAsync<CollaboratorCreatedDto>(
            "api/collaborators", collaborator1);

        var collaborator2 = CollaboratorHelper.GenerateRandomCollaboratorDto();
        var collaboratorCreatedDTO2 = await PostAndDeserializeAsync<CollaboratorCreatedDto>(
            "api/collaborators", collaborator2);

        // Create Associations
        var periodDate1 = new PeriodDate(
            DateOnly.FromDateTime(DateTime.Today.AddDays(1)),
            DateOnly.FromDateTime(DateTime.Today.AddYears(1))
            );
        var associationDTO1 = AssociationProjectCollaboratorHelper.
            GenerateCreateAssociationProjectCollaboratorDto(collaboratorCreatedDTO1.Id, periodDate1);
        var createdAssociationDTO1 = await PostAndDeserializeAsync<AssociationProjectCollaboratorDTO>(
            $"/api/Project/{projectCreatedDTO.Id}/collaborators", associationDTO1);

        var periodDate2 = new PeriodDate(
            DateOnly.FromDateTime(DateTime.Today.AddYears(2)),
            DateOnly.FromDateTime(DateTime.Today.AddYears(3))
            );
        var associationDTO2 = AssociationProjectCollaboratorHelper.
            GenerateCreateAssociationProjectCollaboratorDto(collaboratorCreatedDTO2.Id, periodDate2);
        var createdAssociationDTO2 = await PostAndDeserializeAsync<AssociationProjectCollaboratorDTO>(
            $"/api/Project/{projectCreatedDTO.Id}/collaborators", associationDTO2);

        //Act: Search by periodDate2
        var collaborators = await GetAndDeserializeAsync<List<CollaboratorDTO>>(
            $"/api/Project/{projectCreatedDTO.Id}/collaborators/byPeriod?InitDate={periodDate2.InitDate}&FinalDate={periodDate2.FinalDate}");

        // Assert: List should not be empty and only have collaboratorCreatedDTO2
        Assert.NotEmpty(collaborators);
        Assert.Equal(collaboratorCreatedDTO2.Id, collaborators.First().Id);
    }
}