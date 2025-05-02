using System.Threading.Tasks;
using Application.DTO;
using WebApi.IntegrationTests.Helpers;
using Domain.Models;
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

        var period = new PeriodDate(DateOnly.Parse("18/2/2045"),
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
}
