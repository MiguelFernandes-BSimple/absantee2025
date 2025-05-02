using System.Threading.Tasks;
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
        Assert.Equal(5, count);
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
}

