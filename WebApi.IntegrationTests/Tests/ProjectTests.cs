using System.Text;
using Newtonsoft.Json;
using Xunit;
using Application.DTO;
using Domain.Models;
using WebApi.IntegrationTests.Helpers;
using Domain.Interfaces;
using System.Net;
using Application.DTO.Collaborators;

namespace WebApi.IntegrationTests.Tests;

public class ProjectControllerTests : IntegrationTestBase, IClassFixture<IntegrationTestsWebApplicationFactory<Program>>
{
    public ProjectControllerTests(IntegrationTestsWebApplicationFactory<Program> factory)
        : base(factory.CreateClient())
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

        // Assert
        Assert.NotNull(createdProjectDTO);
        Assert.Equal(projectDTO.Title, createdProjectDTO.Title);
        Assert.Equal(projectDTO.Acronym, createdProjectDTO.Acronym);
        Assert.Equal(projectDTO.PeriodDate.InitDate, createdProjectDTO.PeriodDate.InitDate);
        Assert.Equal(projectDTO.PeriodDate.FinalDate, createdProjectDTO.PeriodDate.FinalDate);
    }

    [Fact]
    public async Task CreateProject_WithInvalidAcronym_Returns400BadRequest()
    {
        // Arrange
        var invalidProjectDTO = ProjectHelper.GenerateRandomProjectDto();
        invalidProjectDTO.Acronym = "Invalid"; // contains lowercase letters

        // Act
        var response = await PostAsync("/api/Project", invalidProjectDTO);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

        var body = await response.Content.ReadAsStringAsync();
        Assert.Equal("Acronym must be 1 to 10 characters long and contain only uppercase letters and digits.", body);
    }

    [Fact]
    public async Task AssociateCollaboratorWithProject_CollaboratorDontExists_Returns400BadRequest()
    {
        // Arrange
        // Create a random project payload
        var projectDTO = ProjectHelper.GenerateRandomProjectDto();
        var projectCreatedDTO = await PostAndDeserializeAsync<ProjectDTO>("/api/Project", projectDTO);

        //Create a random collaborator Id
        var collaboradorId = Guid.NewGuid();
        // Create Association
        var associationDTO = AssociationProjectCollaboratorHelper.
            GenerateCreateAssociationProjectCollaboratorDto(collaboradorId, projectCreatedDTO.PeriodDate);
        //Act
        var response = await PostAsync($"/api/Project/{projectCreatedDTO.Id}/collaborators", associationDTO);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        var body = await response.Content.ReadAsStringAsync();
        Assert.Equal("Invalid arguments", body);
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
            GenerateCreateAssociationProjectCollaboratorDto(collaboratorCreatedDTO.CollabId, projectCreatedDTO.PeriodDate);
        //Act
        var createdAssociationDTO = await PostAndDeserializeAsync<AssociationProjectCollaboratorDTO>($"/api/Project/{projectCreatedDTO.Id}/collaborators", associationDTO);

        // Assert: Check that the status code is 201 Created
        Assert.NotNull(createdAssociationDTO);
        Assert.Equal(createdAssociationDTO.ProjectId, projectCreatedDTO.Id);
        Assert.Equal(createdAssociationDTO.CollaboratorId, collaboratorCreatedDTO.CollabId);
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
            GenerateCreateAssociationProjectCollaboratorDto(collaboratorCreatedDTO.CollabId, projectCreatedDTO.PeriodDate);
        var createdAssociationDTO = await PostAndDeserializeAsync<AssociationProjectCollaboratorDTO>($"/api/Project/{projectCreatedDTO.Id}/collaborators", associationDTO);

        //Act
        var collaborators = await GetAndDeserializeAsync<List<CollaboratorDTO>>($"/api/Project/{projectCreatedDTO.Id}/collaborators");

        // Assert: Check that the status code is 201 Created
        Assert.NotEmpty(collaborators);
        Assert.Equal(collaboratorCreatedDTO.CollabId, collaborators.First().Id);
    }

    [Fact]
    public async Task GetAllCollaboratorsByPeriod_ReturnsAssociatedCollaborators()
    {
        // Arrange
        // Create a random project payload
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
            GenerateCreateAssociationProjectCollaboratorDto(collaboratorCreatedDTO1.CollabId, periodDate1);
        var createdAssociationDTO1 = await PostAndDeserializeAsync<AssociationProjectCollaboratorDTO>(
            $"/api/Project/{projectCreatedDTO.Id}/collaborators", associationDTO1);

        var periodDate2 = new PeriodDate(
            DateOnly.FromDateTime(DateTime.Today.AddYears(2)),
            DateOnly.FromDateTime(DateTime.Today.AddYears(3))
            );
        var associationDTO2 = AssociationProjectCollaboratorHelper.
            GenerateCreateAssociationProjectCollaboratorDto(collaboratorCreatedDTO2.CollabId, periodDate2);
        var createdAssociationDTO2 = await PostAndDeserializeAsync<AssociationProjectCollaboratorDTO>(
            $"/api/Project/{projectCreatedDTO.Id}/collaborators", associationDTO2);

        //Act: Search by periodDate2
        var collaborators = await GetAndDeserializeAsync<List<CollaboratorDTO>>(
            $"api/Project/{projectCreatedDTO.Id}/collaborators/byPeriod?InitDate={periodDate2.InitDate.ToString("yyyy-MM-dd")}&FinalDate={periodDate2.FinalDate.ToString("yyyy-MM-dd")}");

        // Assert: List should not be empty and only have collaboratorCreatedDTO2
        Assert.NotEmpty(collaborators);
        Assert.Equal(collaboratorCreatedDTO2.CollabId, collaborators.First().Id);
    }

    [Fact]
    public async Task GetHolidayCountByCollaborator_ReturnsNumberOfDays()
    {
        // Arrange
        // Create a random project payload
        var projectDTO = ProjectHelper.GenerateRandomProjectDto(
            DateOnly.FromDateTime(DateTime.Today.AddDays(1)),
            DateOnly.FromDateTime(DateTime.Today.AddYears(1))
            );
        var projectCreatedDTO = await PostAndDeserializeAsync<ProjectDTO>("/api/Project", projectDTO);

        // Create Collaborator
        var collaborator = CollaboratorHelper.GenerateRandomCollaboratorDto();
        var collaboratorCreatedDTO = await PostAndDeserializeAsync<CollaboratorCreatedDto>("api/collaborators", collaborator);

        // Create Association
        var associationDTO = AssociationProjectCollaboratorHelper.
            GenerateCreateAssociationProjectCollaboratorDto(collaboratorCreatedDTO.CollabId, projectCreatedDTO.PeriodDate);
        var createdAssociationDTO = await PostAndDeserializeAsync<AssociationProjectCollaboratorDTO>($"/api/Project/{projectCreatedDTO.Id}/collaborators", associationDTO);

        //Create Holiday Plan with HolidayPeriods
        var holidayPeriod1 = new CreateHolidayPeriodDTO() {
            InitDate = DateOnly.FromDateTime(DateTime.Today.AddMonths(1)),
            FinalDate = DateOnly.FromDateTime(DateTime.Today.AddMonths(1)).AddDays(5)
            };

        var holidayPeriod2 = new CreateHolidayPeriodDTO()
        {
            InitDate = DateOnly.FromDateTime(DateTime.Today.AddMonths(6)),
            FinalDate = DateOnly.FromDateTime(DateTime.Today.AddMonths(6)).AddDays(10)
        };

        var holidayPeriodResponseDTO1 = await PostAndDeserializeAsync<HolidayPeriodDTO>(
            $"/api/collaborators/{collaboratorCreatedDTO.CollabId}/holidayPlan/holidayPeriod", holidayPeriod1);

        var holidayPeriodResponseDTO2 = await PostAndDeserializeAsync<HolidayPeriodDTO>(
            $"/api/collaborators/{collaboratorCreatedDTO.CollabId}/holidayPlan/holidayPeriod", holidayPeriod2);

        //expected result is the number of utils days in holiday periods
        var expected = holidayPeriodResponseDTO1.PeriodDate.GetNumberOfCommonUtilDays();
        expected += holidayPeriodResponseDTO2.PeriodDate.GetNumberOfCommonUtilDays();

        //Act
        var result = await GetAndDeserializeAsync<int>(
            $"/api/Project/{projectCreatedDTO.Id}/collaborators/{collaboratorCreatedDTO.CollabId}/holidays/count");

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public async Task GetHolidaysByProjectAndBetweenPeriod_ReturnsHolidayPeriods()
    {
        // Arrange
        // Create a random project payload
        var projectDTO = ProjectHelper.GenerateRandomProjectDto(
            DateOnly.FromDateTime(DateTime.Today.AddDays(1)),
            DateOnly.FromDateTime(DateTime.Today.AddYears(1))
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
        var associationDTO1 = AssociationProjectCollaboratorHelper.
            GenerateCreateAssociationProjectCollaboratorDto(collaboratorCreatedDTO1.CollabId, projectCreatedDTO.PeriodDate);
        var createdAssociationDTO1 = await PostAndDeserializeAsync<AssociationProjectCollaboratorDTO>($"/api/Project/{projectCreatedDTO.Id}/collaborators", associationDTO1);

        var associationDTO2 = AssociationProjectCollaboratorHelper.
            GenerateCreateAssociationProjectCollaboratorDto(collaboratorCreatedDTO2.CollabId, projectCreatedDTO.PeriodDate);
        var createdAssociationDTO2 = await PostAndDeserializeAsync<AssociationProjectCollaboratorDTO>($"/api/Project/{projectCreatedDTO.Id}/collaborators", associationDTO2);

        //Create Holiday Plans with HolidayPeriods
        var holidayPeriod1 = new CreateHolidayPeriodDTO()
        {
            InitDate = DateOnly.FromDateTime(DateTime.Today.AddMonths(1)),
            FinalDate = DateOnly.FromDateTime(DateTime.Today.AddMonths(1)).AddDays(5)
        };

        var holidayPeriod2 = new CreateHolidayPeriodDTO()
        {
            InitDate = DateOnly.FromDateTime(DateTime.Today.AddMonths(6)),
            FinalDate = DateOnly.FromDateTime(DateTime.Today.AddMonths(6)).AddDays(10)
        };

        var holidayPeriodResponseDTO1 = await PostAndDeserializeAsync<HolidayPeriodDTO>(
            $"/api/collaborators/{collaboratorCreatedDTO1.CollabId}/holidayPlan/holidayPeriod", holidayPeriod1);

        var holidayPeriodResponseDTO2 = await PostAndDeserializeAsync<HolidayPeriodDTO>(
            $"/api/collaborators/{collaboratorCreatedDTO2.CollabId}/holidayPlan/holidayPeriod", holidayPeriod2);

        //Act : Search holiday Periods by searchPeriod
        var searchPeriod = new PeriodDate
        {
            InitDate = holidayPeriod2.InitDate.AddDays(-3),
            FinalDate = holidayPeriod2.FinalDate.AddDays(-2),
        };
        //expected is the intersection between searchPeriod and holidayPeriod2
        var expected =
            new PeriodDate
            {
                InitDate = holidayPeriod2.InitDate,
                FinalDate = holidayPeriod2.FinalDate,
            };
        var result = await GetAndDeserializeAsync<IEnumerable<HolidayPeriodDTO>>(
            $"api/Project/{projectCreatedDTO.Id}/collaborators/holidays/byPeriod" +
            $"?InitDate={searchPeriod.InitDate.ToString("yyyy-MM-dd")}" +
            $"&FinalDate={searchPeriod.FinalDate.ToString("yyyy-MM-dd")}");

        // Assert : List should only contain holidayPeriods2
        Assert.Single(result);
        Assert.Equal(expected.InitDate, result.First().PeriodDate.InitDate);
        Assert.Equal(expected.FinalDate, result.First().PeriodDate.FinalDate);
    }

    [Fact]
    public async Task GetHolidayCountByCollaboratorAndPeriod_ReturnsNumberOfDays()
    {
        // Arrange
        // Create a random project payload
        var projectDTO = ProjectHelper.GenerateRandomProjectDto(
            DateOnly.FromDateTime(DateTime.Today.AddDays(1)),
            DateOnly.FromDateTime(DateTime.Today.AddYears(1))
            );
        var projectCreatedDTO = await PostAndDeserializeAsync<ProjectDTO>("/api/Project", projectDTO);

        // Create Collaborator
        var collaborator = CollaboratorHelper.GenerateRandomCollaboratorDto();
        var collaboratorCreatedDTO = await PostAndDeserializeAsync<CollaboratorCreatedDto>("api/collaborators", collaborator);

        // Create Association
        var associationDTO = AssociationProjectCollaboratorHelper.
            GenerateCreateAssociationProjectCollaboratorDto(collaboratorCreatedDTO.CollabId, projectCreatedDTO.PeriodDate);
        var createdAssociationDTO = await PostAndDeserializeAsync<AssociationProjectCollaboratorDTO>($"/api/Project/{projectCreatedDTO.Id}/collaborators", associationDTO);

        //Create Holiday Plan with HolidayPeriods
        var holidayPeriod1 = new CreateHolidayPeriodDTO()
        {
            InitDate = DateOnly.FromDateTime(DateTime.Today.AddMonths(1)),
            FinalDate = DateOnly.FromDateTime(DateTime.Today.AddMonths(1)).AddDays(5)
        };

        var holidayPeriod2 = new CreateHolidayPeriodDTO()
        {
            InitDate = DateOnly.FromDateTime(DateTime.Today.AddMonths(6)),
            FinalDate = DateOnly.FromDateTime(DateTime.Today.AddMonths(6)).AddDays(10)
        };

        var holidayPeriodResponseDTO1 = await PostAndDeserializeAsync<HolidayPeriodDTO>(
            $"/api/collaborators/{collaboratorCreatedDTO.CollabId}/holidayPlan/holidayPeriod", holidayPeriod1);

        var holidayPeriodResponseDTO2 = await PostAndDeserializeAsync<HolidayPeriodDTO>(
            $"/api/collaborators/{collaboratorCreatedDTO.CollabId}/holidayPlan/holidayPeriod", holidayPeriod2);

        //expected result is number of common days in searchingPeriod
        var searchingPeriod = new PeriodDate(
            holidayPeriod2.InitDate,
            holidayPeriod2.FinalDate.AddDays(-3));
        var expected = holidayPeriodResponseDTO2.PeriodDate.GetNumberOfCommonUtilDaysBetweenPeriods(searchingPeriod);

        //Act : Filter by PeriodDate of HolidayPeriod2
        var result = await GetAndDeserializeAsync<int>(
            $"/api/Project/{projectCreatedDTO.Id}/collaborators/{collaboratorCreatedDTO.CollabId}/holidays/count/byPeriod?InitDate={searchingPeriod.InitDate.ToString("yyyy-MM-dd")}&FinalDate={searchingPeriod.FinalDate.ToString("yyyy-MM-dd")}");

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public async Task GetHolidayCountForAllCollaboratorsByPeriod_ReturnsExpectedDays()
    {
        // Arrange
        // Create a random project payload
        var projectDTO = ProjectHelper.GenerateRandomProjectDto(
            DateOnly.FromDateTime(DateTime.Today.AddDays(1)),
            DateOnly.FromDateTime(DateTime.Today.AddYears(1))
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
        var associationDTO1 = AssociationProjectCollaboratorHelper.
            GenerateCreateAssociationProjectCollaboratorDto(collaboratorCreatedDTO1.CollabId, projectCreatedDTO.PeriodDate);
        var createdAssociationDTO1 = await PostAndDeserializeAsync<AssociationProjectCollaboratorDTO>($"/api/Project/{projectCreatedDTO.Id}/collaborators", associationDTO1);

        var associationDTO2 = AssociationProjectCollaboratorHelper.
            GenerateCreateAssociationProjectCollaboratorDto(collaboratorCreatedDTO2.CollabId, projectCreatedDTO.PeriodDate);
        var createdAssociationDTO2 = await PostAndDeserializeAsync<AssociationProjectCollaboratorDTO>($"/api/Project/{projectCreatedDTO.Id}/collaborators", associationDTO2);

        //Create Holiday Plans with HolidayPeriods
        var holidayPeriod1 = new CreateHolidayPeriodDTO()
        {
            InitDate = DateOnly.FromDateTime(DateTime.Today.AddMonths(1)),
            FinalDate = DateOnly.FromDateTime(DateTime.Today.AddMonths(1)).AddDays(5)
        };

        var holidayPeriod2 = new CreateHolidayPeriodDTO()
        {
            InitDate = DateOnly.FromDateTime(DateTime.Today.AddMonths(6)),
            FinalDate = DateOnly.FromDateTime(DateTime.Today.AddMonths(6)).AddDays(10)
        };

        var holidayPeriodResponseDTO1 = await PostAndDeserializeAsync<HolidayPeriodDTO>(
            $"/api/collaborators/{collaboratorCreatedDTO1.CollabId}/holidayPlan/holidayPeriod", holidayPeriod1);

        var holidayPeriodResponseDTO2 = await PostAndDeserializeAsync<HolidayPeriodDTO>(
            $"/api/collaborators/{collaboratorCreatedDTO2.CollabId}/holidayPlan/holidayPeriod", holidayPeriod2);

        //Act : Search holiday Periods days by searchPeriod
        var searchPeriod = new PeriodDate
        {
            InitDate = holidayPeriod2.InitDate.AddDays(-3),
            FinalDate = holidayPeriod2.FinalDate.AddDays(-2),
        };
        //expected is the intersection between searchPeriod and holidayPeriod2
        var expected =
            searchPeriod.GetNumberOfCommonUtilDaysBetweenPeriods(holidayPeriodResponseDTO2.PeriodDate);

        var result = await GetAndDeserializeAsync<int>(
            $"api/Project/{projectCreatedDTO.Id}/collaborators/holidays/count/byPeriod" +
            $"?InitDate={searchPeriod.InitDate.ToString("yyyy-MM-dd")}" +
            $"&FinalDate={searchPeriod.FinalDate.ToString("yyyy-MM-dd")}");

        // Assert
        Assert.Equal(expected, result);
    }
}