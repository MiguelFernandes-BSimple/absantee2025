using Application.DTO;
using Domain.Models;
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
        Assert.Contains(collaboratorCreatedDTO.Id, collaborators);
    }

    [Fact]
    public async Task CreateCollaborator_Returns201Created()
    {
        // arrange
        var collabDTO = CollaboratorHelper.GenerateRandomCollaboratorDto();

        // act
        var createdCollabDTO = await PostAndDeserializeAsync<CollaboratorDTO>("/api/collaborators", collabDTO);

        // assert
        Assert.NotNull(createdCollabDTO);
        Assert.Equal(collabDTO.PeriodDateTime._initDate, createdCollabDTO.PeriodDateTime._initDate);
        Assert.Equal(collabDTO.PeriodDateTime._finalDate, createdCollabDTO.PeriodDateTime._finalDate);
    }

    [Fact]
    public async Task SearchByName_ReturnsCollabId()
    {
        // arrange
        var collabDTO = CollaboratorHelper.GenerateRandomCollaboratorDto();
        var createdCollabDTO = await PostAndDeserializeAsync<CollaboratorDTO>("/api/collaborators", collabDTO);


        // act
        var collabIdList = await GetAndDeserializeAsync<IEnumerable<Guid>>($"/api/collaborators/search?name={collabDTO.Names}");

        // assert
        Assert.NotNull(collabIdList);
        Assert.NotEmpty(collabIdList);
        Assert.Single(collabIdList);
        Assert.Equal(createdCollabDTO.Id, collabIdList.First());
    }

    [Fact]
    public async Task SearchBySurname_ReturnsCollabId()
    {
        // arrange
        var collabDTO = CollaboratorHelper.GenerateRandomCollaboratorDto();
        var createdCollabDTO = await PostAndDeserializeAsync<CollaboratorDTO>("/api/collaborators", collabDTO);


        // act
        var collabIdList = await GetAndDeserializeAsync<IEnumerable<Guid>>($"/api/collaborators/search?surname={collabDTO.Surnames}");

        // assert
        Assert.NotNull(collabIdList);
        Assert.NotEmpty(collabIdList);
        Assert.Single(collabIdList);
        Assert.Equal(createdCollabDTO.Id, collabIdList.First());
    }

    [Fact]
    public async Task SearchByNameAndSurname_ReturnsCollabId()
    {
        // arrange
        var collabDTO = CollaboratorHelper.GenerateRandomCollaboratorDto();
        var createdCollabDTO = await PostAndDeserializeAsync<CollaboratorDTO>("/api/collaborators", collabDTO);


        // act
        var collabIdList = await GetAndDeserializeAsync<IEnumerable<Guid>>($"/api/collaborators/search?name={collabDTO.Names}&surname{collabDTO.Surnames}");

        // assert
        Assert.NotNull(collabIdList);
        Assert.NotEmpty(collabIdList);
        Assert.Single(collabIdList);
        Assert.Equal(createdCollabDTO.Id, collabIdList.First());
    }

    // este teste falha se todos os testes forem corridos ao mesmo tempo
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
        // arrange
        var initDate = DateOnly.FromDateTime(new DateTime(2025, 5, 10));
        var finalDate = DateOnly.FromDateTime(new DateTime(2025, 5, 20));

        var collabDTO1 = CollaboratorHelper.GenerateRandomCollaboratorDto();
        var createdCollab1 = await PostAndDeserializeAsync<CollaboratorDTO>("/api/collaborators", collabDTO1);
        var periodsForCollab1 = new List<PeriodDate> { new PeriodDate(initDate, finalDate) };
        var holidayPlan1 = HolidayPlanHelper.GenerateCreateHolidayPlanDto(createdCollab1.Id, periodsForCollab1);
        var createdHolidayPlan1 = await PostAndDeserializeAsync<HolidayPlanDTO>("api/holidayplans", holidayPlan1);

        var collabDTO2 = CollaboratorHelper.GenerateRandomCollaboratorDto();
        var createdCollab2 = await PostAndDeserializeAsync<CollaboratorDTO>("/api/collaborators", collabDTO2);
        var periodsForCollab2 = new List<PeriodDate> { new PeriodDate(initDate, finalDate) };
        var holidayPlan2 = HolidayPlanHelper.GenerateCreateHolidayPlanDto(createdCollab2.Id, periodsForCollab2);
        var createdHolidayPlan2 = await PostAndDeserializeAsync<HolidayPlanDTO>("api/holidayplans", holidayPlan2);

        // act
        var returnedPeriods = await GetAndDeserializeAsync<IEnumerable<HolidayPeriodDTO>>($"/api/collaborators/holidayperiods/overlaps?collabId1={createdCollab1.Id}&collabId2={createdCollab2.Id}&InitDate={initDate:yyyy-MM-dd}&FinalDate={finalDate:yyyy-MM-dd}");

        // assert
        Assert.NotNull(returnedPeriods);
        Assert.NotEmpty(returnedPeriods);
        foreach (var period in returnedPeriods)
        {
            Assert.Equal(initDate, period.PeriodDate.InitDate);
            Assert.Equal(finalDate, period.PeriodDate.FinalDate);
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

        var period = new PeriodDate(DateOnly.Parse("18/2/2045"),
                DateOnly.Parse("18/3/2045"));

        var holidayPlan = HolidayPlanHelper.GenerateCreateHolidayPlanDto(
            collaboratorCreatedDTO.Id,
            new List<PeriodDate> {
                period
            }
        );

        var holidayPlanDTO = await PostAndDeserializeAsync<CreateHolidayPlanDTO>("api/holidayplans", holidayPlan);

        // Act
        var result = await GetAndDeserializeAsync<HolidayPeriodDTO>($"api/collaborators/{collaboratorCreatedDTO.Id}/holidayperiods/includes-date?date=20/2/2045");

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

        var holidayPlan = HolidayPlanHelper.GenerateCreateHolidayPlanDto(
            collaboratorCreatedDTO.Id,
            new List<PeriodDate> {
                period, period2
            }
        );

        var holidayPlanDTO = await PostAndDeserializeAsync<CreateHolidayPlanDTO>("api/holidayplans", holidayPlan);

        // Act
        var result = await GetAndDeserializeAsync<List<HolidayPeriodDTO>>($"api/collaborators/{collaboratorCreatedDTO.Id}/holidayperiods/longer-than?days=5");

        // Assert
        Assert.NotNull(result);
        Assert.Equal(period.InitDate, result.First().PeriodDate.InitDate);
        Assert.Equal(period.FinalDate, result.First().PeriodDate.FinalDate);
    }

    [Fact]
    public async Task ListCollaboratorHolidayPeriodsBetweenPeriod()
    {
        // Arrange: Create a random Collaborator and respective HolidayPeriods
        var init = new DateTime(2045, 2, 1).ToUniversalTime();
        var end = new DateTime(2047, 3, 20).ToUniversalTime();
        var collaborator = CollaboratorHelper.GenerateRandomCollaboratorDtoWithDates(init, end);
        var collaboratorCreatedDTO = await PostAndDeserializeAsync<CollaboratorCreatedDto>("api/collaborators", collaborator);

        var period = new PeriodDate(DateOnly.Parse("18/2/2045"),
               DateOnly.Parse("18/3/2045"));
        var period2 = new PeriodDate(DateOnly.Parse("5/8/2045"),
                DateOnly.Parse("10/8/2045"));

        var holidayPlan = HolidayPlanHelper.GenerateCreateHolidayPlanDto(
            collaboratorCreatedDTO.Id,
            new List<PeriodDate> {
                period, period2
            }
        );

        var holidayPlanDTO = await PostAndDeserializeAsync<CreateHolidayPlanDTO>("api/holidayplans", holidayPlan);

        var query = $"/api/collaborators/{collaboratorCreatedDTO.Id}/holidayPlan/holidayPeriods/ByPeriod?InitDate=2045-01-1&FinalDate=2045-4-1";


        // Act
        var result = await GetAndDeserializeAsync<IEnumerable<HolidayPeriodDTO>>(query);

        // Assert
        var resultList = result.ToList();
        Assert.Single(resultList);

        var returnedPeriod = resultList.First();

        Assert.Equal(DateOnly.Parse("18/2/2045"), returnedPeriod.PeriodDate.InitDate);
        Assert.Equal(DateOnly.Parse("18/3/2045"), returnedPeriod.PeriodDate.FinalDate);
    }

    [Fact]
    public async Task ListCollaboratorsWithHolidayPeriodsLongerThan_Returns200AndObjects()
    {
        // Arrange: Create a random Collaborator and respective HolidayPeriods
        var iniResult = await GetAndDeserializeAsync<List<CollaboratorDTO>>($"api/collaborators/longer-than?days=5");

        var init = new DateTime(2045, 2, 1).ToUniversalTime();
        var end = new DateTime(2045, 3, 20).ToUniversalTime();
        var collaborator = CollaboratorHelper.GenerateRandomCollaboratorDtoWithDates(init, end);
        var collaboratorCreatedDTO = await PostAndDeserializeAsync<CollaboratorCreatedDto>("api/collaborators", collaborator);

        var period = new PeriodDate(DateOnly.Parse("5/2/2045"),
                DateOnly.Parse("18/2/2045"));

        var holidayPlan = HolidayPlanHelper.GenerateCreateHolidayPlanDto(
           collaboratorCreatedDTO.Id,
           new List<PeriodDate> {
                period
           }
       );

        var holidayPlanDTO = await PostAndDeserializeAsync<CreateHolidayPlanDTO>("api/holidayplans", holidayPlan);

        var result = await GetAndDeserializeAsync<List<CollaboratorDTO>>($"api/collaborators/longer-than?days=5");

        // Assert
        Assert.NotNull(result);
        Assert.NotEmpty(result);
        Assert.Contains(result, c => c.Id == collaboratorCreatedDTO.Id);
        Assert.Equal(iniResult.Count + 1, result.Count);

    }

    [Fact]
    public async Task GetCollaboratorsByPeriod_ReturnsCollaboratorsOnHolidayDuringPeriod()
    {
        // Arrange
        var initDate = DateOnly.FromDateTime(new DateTime(2025, 7, 1));
        var finalDate = DateOnly.FromDateTime(new DateTime(2025, 7, 10));

        // Criação de dois colaboradores
        var collaborator1 = await PostAndDeserializeAsync<CollaboratorDTO>(
            "/api/collaborators",
            CollaboratorHelper.GenerateRandomCollaboratorDto());

        var collaborator2 = await PostAndDeserializeAsync<CollaboratorDTO>(
            "/api/collaborators",
            CollaboratorHelper.GenerateRandomCollaboratorDto());

        // Define períodos de férias
        var period1 = new PeriodDate(initDate, finalDate);
        var outsidePeriod = new PeriodDate(
            DateOnly.FromDateTime(new DateTime(2025, 8, 1)),
            DateOnly.FromDateTime(new DateTime(2025, 8, 10)));

        // Cria planos de férias
        var holidayPlan1 = HolidayPlanHelper.GenerateCreateHolidayPlanDto(collaborator1.Id, new List<PeriodDate> { period1 });
        var holidayPlan2 = HolidayPlanHelper.GenerateCreateHolidayPlanDto(collaborator2.Id, new List<PeriodDate> { outsidePeriod });


        var createdHolidayPlan1 = await PostAndDeserializeAsync<HolidayPlanDTO>("api/holidayplans", holidayPlan1);
        var createdHolidayPlan2 = await PostAndDeserializeAsync<HolidayPlanDTO>("api/holidayplans", holidayPlan2);


        var query = "/api/collaborators/holidayPlan/holidayPeriods/ByPeriod?InitDate=2025-07-1&FinalDate=2025-07-09";

        // Act
        var result = await GetAndDeserializeAsync<IEnumerable<CollaboratorDTO>>(query);

        // Assert
        Assert.NotNull(result);
        Assert.Contains(result, c => c.Id == collaborator1.Id);
        Assert.DoesNotContain(result, c => c.Id == collaborator2.Id);
    }
}
