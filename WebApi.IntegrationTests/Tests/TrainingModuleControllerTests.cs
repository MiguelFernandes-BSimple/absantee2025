/* using Application.DTO.TrainingModule;
using Application.DTO.TrainingSubject;
using WebApi.IntegrationTests.Helpers;
using Xunit;

namespace WebApi.IntegrationTests.Tests;

public class TrainingModuleControllerTests : IntegrationTestBase, IClassFixture<IntegrationTestsWebApplicationFactory<Program>>
{
    public TrainingModuleControllerTests(IntegrationTestsWebApplicationFactory<Program> factory)
        : base(factory.CreateClient())
    {
    }

    [Fact]
    public async Task AddTrainingModule_Return201Created()
    {
        // Arrange
        // Create a random training subject payload
        var trainingSubjectDTO =
                    TrainingSubjectHelper.GenerateRandomAddTrainingSubjectDTO();

        // Post new subject and get Id
        var createdTrainingSubjectDTO =
                    await PostAndDeserializeAsync<TrainingSubjectDTO>("/api/trainingsubjects", trainingSubjectDTO);

        // Create a training module payload with random dates
        var trainingModuleDTO =
            TrainingModuleHelper.GenerateAddTrainingModuleDTORandomDates(createdTrainingSubjectDTO.Id);

        // Act : Post new module
        var createdTrainingModuleDTO =
            await PostAndDeserializeAsync<TrainingModuleDTO>("/api/trainingmodules", trainingModuleDTO);

        // Assert
        Assert.NotNull(createdTrainingModuleDTO);
        Assert.Equal(trainingModuleDTO.TrainingSubjectId, createdTrainingModuleDTO.TrainingSubjectId);
        Assert.True(trainingModuleDTO.Periods.SequenceEqual(createdTrainingModuleDTO.Periods));
    }

    [Fact]
    public async Task AddCollaboratorToModule_Return201Created()
    {
        // Arrange
        // Create a random training subject payload
        var trainingSubjectDTO =
                    TrainingSubjectHelper.GenerateRandomAddTrainingSubjectDTO();

        // Post new subject and get Id
        var createdTrainingSubjectDTO =
                    await PostAndDeserializeAsync<TrainingSubjectDTO>("/api/trainingsubjects", trainingSubjectDTO);

        // Create a training module payload with random dates
        var trainingModuleDTO =
            TrainingModuleHelper.GenerateAddTrainingModuleDTORandomDates(createdTrainingSubjectDTO.Id);

        // Post new module
        var createdTrainingModuleDTO =
            await PostAndDeserializeAsync<TrainingModuleDTO>("/api/trainingmodules", trainingModuleDTO);


        // Create a new Collaborator to add to module
        var collaboratorDTO = CollaboratorHelper.GenerateRandomCollaboratorDto();

        // Post new collaborator to add 
        var createdCollaboratorDTO =
            await PostAndDeserializeAsync<CollaboratorDTO>("/api/collaborators", collaboratorDTO);

        var assocTMCDTO =
            AssociationTrainingModuleCollaboratorHelper.GenerateCreateAssociationTrainingModuleCollaboratorDTO(createdCollaboratorDTO.Id);

        // Act : Add new collaborator to module
        var createdAssocDTO =
            await PostAndDeserializeAsync<AssociationTrainingModuleCollaboratorDTO>($"/api/trainingmodules/{createdTrainingModuleDTO.Id}/collaborators", assocTMCDTO);

        // Assert
        Assert.NotNull(createdAssocDTO);
        Assert.Equal(createdTrainingModuleDTO.Id, createdAssocDTO.TrainingModuleId);
        Assert.Equal(createdCollaboratorDTO.Id, createdAssocDTO.CollaboratorId);
    }

    [Fact]
    public async Task GetActiveCollaboratorsWithNoTrainingDoneForSubject_Return200Ok()
    {
        // Arrange
        var iniCollabdIds =
            await GetAndDeserializeAsync<IEnumerable<Guid>>($"/api/collaborators");
        
        // Create a random training subject payload
        var trainingSubjectDTO =
                    TrainingSubjectHelper.GenerateRandomAddTrainingSubjectDTO();

        // Post new subject and get Id
        var createdTrainingSubjectDTO =
                    await PostAndDeserializeAsync<TrainingSubjectDTO>("/api/trainingsubjects", trainingSubjectDTO);

        // Create a training module payload with random dates
        var trainingModuleDTO =
            TrainingModuleHelper.GenerateAddTrainingModuleDTORandomDates(createdTrainingSubjectDTO.Id);

        // Post new module
        var createdTrainingModuleDTO =
            await PostAndDeserializeAsync<TrainingModuleDTO>("/api/trainingmodules", trainingModuleDTO);

        // Act
        var collabdIds =
            await GetAndDeserializeAsync<IEnumerable<Guid>>($"/api/trainingmodules/not-completed/subjects/{createdTrainingSubjectDTO.Id}/collaborators/active/");

        // Assert
        Assert.True(iniCollabdIds.SequenceEqual(collabdIds));
    }
} */