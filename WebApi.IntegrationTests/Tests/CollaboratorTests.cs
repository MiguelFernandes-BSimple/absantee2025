using Application.DTO;
using WebApi.IntegrationTests.Helpers;
using Xunit;

namespace WebApi.IntegrationTests.Tests;



public class CollaboratorControllerTests : IntegrationTestBase, IClassFixture<IntegrationTestsWebApplicationFactory<Program>>
{
    public CollaboratorControllerTests(IntegrationTestsWebApplicationFactory<Program> factory) : base(factory.CreateClient())
    {}

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

}

