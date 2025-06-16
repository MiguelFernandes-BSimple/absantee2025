using Domain.Factory;
using Domain.IRepository;
using Domain.Models;
using Moq;

namespace Domain.Tests.TrainingModuleCollaboratorsTests;
public class TrainingModuleCollaboratorsFactoryTests
{
    [Fact]
    public void WhenPassingValidIds_ThenTrainingModuleCollaboratorIsCreated()
    {
        //Arrange
        var collabId = Guid.NewGuid();
        var collab = new Mock<Collaborator>();

        var trainingModuleId = Guid.NewGuid();
        var trainingSubject = new Mock<TrainingModule>();

        var collaboratorRepo = new Mock<ICollaboratorRepository>();
        collaboratorRepo.Setup(c => c.GetByIdAsync(collabId)).ReturnsAsync(collab.Object);

        var trainingSubjectRepo = new Mock<ITrainingModuleRepository>();
        trainingSubjectRepo.Setup(t => t.GetByIdAsync(trainingModuleId)).ReturnsAsync(trainingSubject.Object);

        var factory = new AssociationTrainingModuleCollaboratorFactory(collaboratorRepo.Object, trainingSubjectRepo.Object);

        //Act
        var result = factory.Create(trainingModuleId, collabId);

        //Assert
        Assert.NotNull(result);
    }

    [Fact]
    public async Task WhenCollaboratorDontExists_ThenThrowException()
    {
        //Arrange
        var collabId = Guid.NewGuid();

        var trainingModuleId = Guid.NewGuid();
        var trainingSubject = new Mock<TrainingModule>();

        var collaboratorRepo = new Mock<ICollaboratorRepository>();
        collaboratorRepo.Setup(c => c.GetByIdAsync(collabId)).ReturnsAsync((Collaborator?)null);

        var trainingSubjectRepo = new Mock<ITrainingModuleRepository>();
        trainingSubjectRepo.Setup(t => t.GetByIdAsync(trainingModuleId)).ReturnsAsync(trainingSubject.Object);

        var factory = new AssociationTrainingModuleCollaboratorFactory(collaboratorRepo.Object, trainingSubjectRepo.Object);

        //Assert
        var exception = await Assert.ThrowsAsync<ArgumentException>(() =>
            //Act
            factory.Create(trainingModuleId, collabId)
        ); 

        Assert.Equal("Collaborator must exists", exception.Message);
    }

    [Fact]
    public async Task WhenTrainingModuleDontExists_ThenThrowException()
    {
        //Arrange
        var collabId = Guid.NewGuid();
        var collab = new Mock<Collaborator>();

        var trainingModuleId = Guid.NewGuid();

        var collaboratorRepo = new Mock<ICollaboratorRepository>();
        collaboratorRepo.Setup(c => c.GetByIdAsync(collabId)).ReturnsAsync(collab.Object);

        var trainingSubjectRepo = new Mock<ITrainingModuleRepository>();
        trainingSubjectRepo.Setup(t => t.GetByIdAsync(trainingModuleId)).ReturnsAsync((TrainingModule?)null);

        var factory = new AssociationTrainingModuleCollaboratorFactory(collaboratorRepo.Object, trainingSubjectRepo.Object);

        //Assert
        var exception = await Assert.ThrowsAsync<ArgumentException>(() =>
            //Act
            factory.Create(trainingModuleId, collabId)
        );

        Assert.Equal("Training Module must exists", exception.Message);
    }
}
