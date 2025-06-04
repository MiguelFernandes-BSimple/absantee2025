using Application.DTO;
using Application.DTO.Collaborators;
using Domain.Interfaces;
using Domain.Models;
using System.Net;
using WebApi.IntegrationTests.Helpers;
using Xunit;

namespace WebApi.IntegrationTests.Tests;



public class CollaboratorControllerTests : IntegrationTestBase, IClassFixture<IntegrationTestsWebApplicationFactory<Program>>
{
    public CollaboratorControllerTests(IntegrationTestsWebApplicationFactory<Program> factory) : base(factory.CreateClient())
    { }

    [Fact]
    public async Task GetCollaborators_ReturnsOkWithList()
    {
        // Arrange: Cria ao menos um colaborador no sistema
        var collaborator = CollaboratorHelper.GenerateRandomCollaboratorDto();
        var collaboratorCreatedDTO = await PostAndDeserializeAsync<CollaboratorCreatedDto>("api/collaborators", collaborator);

        // Act: Faz a chamada GET
        var collaborators = await GetAndDeserializeAsync<List<Guid>>($"/api/collaborators");

        // Assert: Valida o retorno
        Assert.NotNull(collaborators);
        Assert.NotEmpty(collaborators);
        Assert.Contains(collaboratorCreatedDTO.CollabId, collaborators);
    }

    [Fact]
    public async Task CreateCollaborator_Returns201Created()
    {
        // arrange
        var collabDTO = CollaboratorHelper.GenerateRandomCollaboratorDto();

        // act
        var createdCollabDTO = await PostAndDeserializeAsync<CollaboratorCreatedDto>("/api/collaborators", collabDTO);

        // assert
        Assert.NotNull(createdCollabDTO);
        Assert.Equal(collabDTO.Names, createdCollabDTO.Names);
        Assert.Equal(collabDTO.Email, createdCollabDTO.Email);
    }

    [Fact]
    public async Task CreateCollaborator_WithEmptyName_ReturnsBadRequest()
    {
        // arrange
        var collabDTO = CollaboratorHelper.GenerateRandomCollaboratorDto();
        collabDTO.Names = "";

        // act
        var response = await PostAsync("/api/collaborators", collabDTO);

        // assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

        var body = await response.Content.ReadAsStringAsync();
        Assert.Contains("Error. Name required", body);
    }

    [Fact]
    public async Task CreateCollaborator_WithEmptySurname_ReturnsBadRequest()
    {
        // arrange
        var collabDTO = CollaboratorHelper.GenerateRandomCollaboratorDto();
        collabDTO.Surnames = "";

        // act
        var response = await PostAsync("/api/collaborators", collabDTO);

        // assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

        var body = await response.Content.ReadAsStringAsync();
        Assert.Contains("Error. Surname required", body);
    }

    [Fact]
    public async Task CreateCollaborator_WithEmptyEmail_ReturnsBadRequest()
    {
        // arrange
        var collabDTO = CollaboratorHelper.GenerateRandomCollaboratorDto();
        collabDTO.Email = "";

        // act
        var response = await PostAsync("/api/collaborators", collabDTO);

        // assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

        var body = await response.Content.ReadAsStringAsync();
        Assert.Contains("Error. Email required", body);
    }

    [Fact]
    public async Task SearchByName_ReturnsCollabId()
    {
        // arrange
        var collabDTO = CollaboratorHelper.GenerateRandomCollaboratorDto();
        var createdCollabDTO = await PostAndDeserializeAsync<CollaboratorCreatedDto>("/api/collaborators", collabDTO);


        // act
        var collabIdList = await GetAndDeserializeAsync<IEnumerable<Guid>>($"/api/collaborators/search?name={collabDTO.Names}");

        // assert
        Assert.NotNull(collabIdList);
        Assert.NotEmpty(collabIdList);
        Assert.Single(collabIdList);
        Assert.Equal(createdCollabDTO.CollabId, collabIdList.First());
    }

    [Fact]
    public async Task SearchByName_WithEmptyNameAndSurname_ReturnsBadRequest()
    {
        // arrange
        var collabDTO = CollaboratorHelper.GenerateRandomCollaboratorDto();
        var createdCollabDTO = await PostAndDeserializeAsync<CollaboratorDTO>("/api/collaborators", collabDTO);


        // act
        var response = await GetAsync($"/api/collaborators/search");

        // assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        var body = await response.Content.ReadAsStringAsync();
        Assert.Equal("Please insert at least a name or surname", body);
    }

    [Fact]
    public async Task SearchBySurname_ReturnsCollabId()
    {
        // arrange
        var collabDTO = CollaboratorHelper.GenerateRandomCollaboratorDto();
        var createdCollabDTO = await PostAndDeserializeAsync<CollaboratorCreatedDto>("/api/collaborators", collabDTO);


        // act
        var collabIdList = await GetAndDeserializeAsync<IEnumerable<Guid>>($"/api/collaborators/search?surname={collabDTO.Surnames}");

        // assert
        Assert.NotNull(collabIdList);
        Assert.NotEmpty(collabIdList);
        Assert.Single(collabIdList);
        Assert.Equal(createdCollabDTO.CollabId, collabIdList.First());
    }

    [Fact]
    public async Task SearchByNameAndSurname_ReturnsCollabId()
    {
        // arrange
        var collabDTO = CollaboratorHelper.GenerateRandomCollaboratorDto();
        var createdCollabDTO = await PostAndDeserializeAsync<CollaboratorCreatedDto>("/api/collaborators", collabDTO);


        // act
        var collabIdList = await GetAndDeserializeAsync<IEnumerable<Guid>>($"/api/collaborators/search?name={collabDTO.Names}&surname={collabDTO.Surnames}");

        // assert
        Assert.NotNull(collabIdList);
        Assert.NotEmpty(collabIdList);
        Assert.Single(collabIdList);
        Assert.Equal(createdCollabDTO.CollabId, collabIdList.First());
    }

    [Fact]
    public async Task Count_ReturnsLong()
    {
        // arrange
        var iniCount = await GetAndDeserializeAsync<long>("/api/collaborators/count");

        var collabDTO1 = CollaboratorHelper.GenerateRandomCollaboratorDto();
        await PostAndDeserializeAsync<CollaboratorDTO>("/api/collaborators", collabDTO1);

        var collabDTO2 = CollaboratorHelper.GenerateRandomCollaboratorDto();
        await PostAndDeserializeAsync<CollaboratorDTO>("/api/collaborators", collabDTO2);

        var collabDTO3 = CollaboratorHelper.GenerateRandomCollaboratorDto();
        await PostAndDeserializeAsync<CollaboratorDTO>("/api/collaborators", collabDTO3);

        var collabDTO4 = CollaboratorHelper.GenerateRandomCollaboratorDto();
        await PostAndDeserializeAsync<CollaboratorDTO>("/api/collaborators", collabDTO4);

        var collabDTO5 = CollaboratorHelper.GenerateRandomCollaboratorDto();
        await PostAndDeserializeAsync<CollaboratorDTO>("/api/collaborators", collabDTO5);

        // act
        var count = await GetAndDeserializeAsync<long>("/api/collaborators/count");

        // assert
        Assert.Equal(iniCount + 5, count);
    }

    [Fact]
    public async Task FindOverlappingHolidayPeriodsBetweenTwoCollaboratorsBetweenDates_ReturnsPeriods()
    {
        // Arrange
        // Create two collaborators and their holiday plans
        var collaborator1 = await PostAndDeserializeAsync<CollaboratorCreatedDto>("/api/collaborators", CollaboratorHelper.GenerateRandomCollaboratorDto());

        var collaborator2 = await PostAndDeserializeAsync<CollaboratorCreatedDto>("/api/collaborators", CollaboratorHelper.GenerateRandomCollaboratorDto());

        // Define holiday periods for holiday plans 
        var initDate = DateOnly.FromDateTime(new DateTime(2025, 7, 1));
        var finalDate = DateOnly.FromDateTime(new DateTime(2025, 7, 10));

        CreateHolidayPeriodDTO period = new CreateHolidayPeriodDTO();
        period.InitDate = initDate;
        period.FinalDate = finalDate;

        await PostAndDeserializeAsync<HolidayPeriodDTO>($"api/collaborators/{collaborator1.CollabId}/holidayPlan/holidayPeriod", period);
        await PostAndDeserializeAsync<HolidayPeriodDTO>($"api/collaborators/{collaborator2.CollabId}/holidayPlan/holidayPeriod", period);

        // act
        var returnedPeriods = await GetAndDeserializeAsync<IEnumerable<HolidayPeriodDTO>>($"/api/collaborators/holidayperiods/overlaps?collabId1={collaborator1.CollabId}&collabId2={collaborator2.CollabId}&InitDate={initDate:yyyy-MM-dd}&FinalDate={finalDate:yyyy-MM-dd}");

        // assert
        Assert.NotNull(returnedPeriods);
        Assert.NotEmpty(returnedPeriods);
        foreach (var p in returnedPeriods)
        {
            Assert.Equal(initDate, p.PeriodDate.InitDate);
            Assert.Equal(finalDate, p.PeriodDate.FinalDate);
        }
    }

    [Fact]
    public async Task ListHolidayPeriodContainingDay_Returns200AndObject()
    {
        // Arrange: Create a random Collaborator and respective HolidayPeriods
        var init = new DateTime(2045, 2, 1).ToUniversalTime();
        var end = new DateTime(2045, 3, 20).ToUniversalTime();
        var collaborator = CollaboratorHelper.GenerateRandomCollaboratorDtoWithDates(init, end);
        var collaboratorCreatedDTO = await PostAndDeserializeAsync<CollaboratorCreatedDto>("api/collaborators", collaborator);

        var period = new PeriodDate(DateOnly.Parse("18-02-2045"), DateOnly.Parse("18-03-2045"));

        var createdHolidayPeriodDTO = await PostAndDeserializeAsync<HolidayPeriodDTO>($"api/collaborators/{collaboratorCreatedDTO.CollabId}/holidayPlan/holidayPeriod", period);

        // Act
        var result = await GetAndDeserializeAsync<HolidayPeriodDTO>($"api/collaborators/{collaboratorCreatedDTO.CollabId}/holidayperiods/includes-date?date=2045-02-20");

        // Assert
        Assert.NotNull(result);
        Assert.Equal(period.InitDate, result.PeriodDate.InitDate);
        Assert.Equal(period.FinalDate, result.PeriodDate.FinalDate);
    }

    [Fact]
    public async Task ListHolidayPeriodLongerThan_Returns200AndObjects()
    {
        // Arrange: Create a random Collaborator and respective HolidayPeriods
        var init = new DateTime(2045, 2, 1).ToUniversalTime();
        var end = new DateTime(2045, 3, 20).ToUniversalTime();
        var collaborator = CollaboratorHelper.GenerateRandomCollaboratorDtoWithDates(init, end);
        var collaboratorCreatedDTO = await PostAndDeserializeAsync<CollaboratorCreatedDto>("api/collaborators", collaborator);

        var period = new PeriodDate(DateOnly.Parse("20/2/2045"),
                DateOnly.Parse("18/3/2045"));
        var period2 = new PeriodDate(DateOnly.Parse("18/2/2045"),
                DateOnly.Parse("19/2/2045"));

        var createdHolidayPeriod1DTO = await PostAndDeserializeAsync<HolidayPeriodDTO>($"api/collaborators/{collaboratorCreatedDTO.CollabId}/holidayPlan/holidayPeriod", period);
        var createdHolidayPeriod2DTO = await PostAndDeserializeAsync<HolidayPeriodDTO>($"api/collaborators/{collaboratorCreatedDTO.CollabId}/holidayPlan/holidayPeriod", period2);

        // Act
        var result = await GetAndDeserializeAsync<List<HolidayPeriodDTO>>($"api/collaborators/{collaboratorCreatedDTO.CollabId}/holidayperiods/longer-than?days=5");

        // Assert
        Assert.NotNull(result);
        Assert.Equal(period.InitDate, result.First().PeriodDate.InitDate);
        Assert.Equal(period.FinalDate, result.First().PeriodDate.FinalDate);
    }

    [Fact]
    public async Task ListCollaboratorsWithHolidayPeriodsLongerThan_Returns200AndObjects()
    {
        // Arrange
        var iniResult = await GetAndDeserializeAsync<IEnumerable<CollaboratorDTO>>($"api/collaborators/longer-than?days=5");

        var init = new DateTime(2045, 2, 1).ToUniversalTime();
        var end = new DateTime(2045, 3, 20).ToUniversalTime();
        var collaborator = CollaboratorHelper.GenerateRandomCollaboratorDtoWithDates(init, end);
        var collaboratorCreatedDTO = await PostAndDeserializeAsync<CollaboratorCreatedDto>("api/collaborators", collaborator);

        var period = new PeriodDate(DateOnly.Parse("5/2/2045"),
                DateOnly.Parse("18/2/2045"));

        var createdHolidayPeriod1DTO = await PostAndDeserializeAsync<HolidayPeriodDTO>($"api/collaborators/{collaboratorCreatedDTO.CollabId}/holidayPlan/holidayPeriod", period);

        //Act
        var result = await GetAndDeserializeAsync<IEnumerable<CollaboratorDTO>>($"api/collaborators/longer-than?days=5");

        // Assert
        Assert.NotNull(result);
        Assert.NotEmpty(result);
        Assert.Contains(result, c => c.Id == collaboratorCreatedDTO.CollabId);
        Assert.Equal(iniResult.Count() + 1, result.Count());

    }

    [Fact]
    public async Task GetCollaboratorsByPeriod_Returns200AndObjects()
    {
        // Arrange
        // Create two collaborators and their holiday plans
        var collaborator1 = await PostAndDeserializeAsync<CollaboratorCreatedDto>("/api/collaborators", CollaboratorHelper.GenerateRandomCollaboratorDto());

        var collaborator2 = await PostAndDeserializeAsync<CollaboratorCreatedDto>("/api/collaborators", CollaboratorHelper.GenerateRandomCollaboratorDto());

        // Define holiday periods for holiday plans 
        var initDate = DateOnly.FromDateTime(new DateTime(2025, 7, 1));
        var finalDate = DateOnly.FromDateTime(new DateTime(2025, 7, 10));

        CreateHolidayPeriodDTO period1 = new CreateHolidayPeriodDTO();
        period1.InitDate = initDate;
        period1.FinalDate = finalDate;

        CreateHolidayPeriodDTO outsidePeriod = new CreateHolidayPeriodDTO();
        outsidePeriod.InitDate = DateOnly.FromDateTime(new DateTime(2025, 8, 1));
        outsidePeriod.FinalDate = DateOnly.FromDateTime(new DateTime(2025, 8, 10));

        var createdHolidayPeriod1DTO = await PostAndDeserializeAsync<HolidayPeriodDTO>($"api/collaborators/{collaborator1.CollabId}/holidayPlan/holidayPeriod", period1);
        var createdHolidayPeriod2DTO = await PostAndDeserializeAsync<HolidayPeriodDTO>($"api/collaborators/{collaborator2.CollabId}/holidayPlan/holidayPeriod", outsidePeriod);

        var query = "/api/collaborators/holidayPlan/holidayPeriods/ByPeriod?InitDate=2025-07-01&FinalDate=2025-07-09";

        // Act
        var result = await GetAndDeserializeAsync<IEnumerable<CollaboratorDTO>>(query);

        // Assert
        Assert.NotNull(result);
        Assert.Contains(result, c => c.Id == collaborator1.CollabId);
        Assert.DoesNotContain(result, c => c.Id == collaborator2.CollabId);
    }
}
