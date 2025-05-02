using System.Threading.Tasks;
using Application.DTO;
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
}

