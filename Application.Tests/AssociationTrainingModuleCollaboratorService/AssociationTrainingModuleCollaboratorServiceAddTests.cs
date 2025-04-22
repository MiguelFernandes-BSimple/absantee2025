using Application.Services;
using Domain.Factory;
using Domain.Interfaces;
using Domain.IRepository;
using Domain.Models;
using Moq;

namespace Application.Tests.AssociationTrainingModuleServiceTests;

public class AssociationTrainingModuleCollaboratorServiceAddTests
{
    [Fact]
    public async Task WhenPassingValidParameters_ThenObjectIsAdded()
    {
        // Arrange
        Mock<IAssociationTrainingModuleCollaboratorRepository> assocRepo =
            new Mock<IAssociationTrainingModuleCollaboratorRepository>();
        Mock<ITrainingModuleRepository> tmRepo = new Mock<ITrainingModuleRepository>();
        Mock<ICollaboratorRepository> collabRepo =
            new Mock<ICollaboratorRepository>();
        Mock<IAssociationTrainingModuleCollaboratorFactory> assocTCFactory =
            new Mock<IAssociationTrainingModuleCollaboratorFactory>();

        long trainingModuleId = 1;
        long collaboratorId = 1;

        Mock<ITrainingModule> tm = new Mock<ITrainingModule>();
        Mock<ICollaborator> collab = new Mock<ICollaborator>();

        tmRepo.Setup(tmr => tmr.GetByIdAsync(trainingModuleId)).ReturnsAsync(tm.Object);
        collabRepo.Setup(cr => cr.GetByIdAsync(collaboratorId)).ReturnsAsync(collab.Object);

        var service =
            new AssociationTrainingModuleCollaboratorService(assocRepo.Object, tmRepo.Object, collabRepo.Object, assocTCFactory.Object);

        // Act
        await service.Add(trainingModuleId, collaboratorId);

        // Assert
    }

    [Fact]
    public async Task WhenPassingInvalidTrainingModuleId_ThenThrowException()
    {
        // Arrange
        Mock<IAssociationTrainingModuleCollaboratorRepository> assocRepo =
            new Mock<IAssociationTrainingModuleCollaboratorRepository>();
        Mock<ITrainingModuleRepository> tmRepo = new Mock<ITrainingModuleRepository>();
        Mock<ICollaboratorRepository> collabRepo =
            new Mock<ICollaboratorRepository>();
        Mock<IAssociationTrainingModuleCollaboratorFactory> assocTCFactory =
            new Mock<IAssociationTrainingModuleCollaboratorFactory>();

        long collaboratorId = 1;

        Mock<ITrainingModule> tm = new Mock<ITrainingModule>();
        Mock<ICollaborator> collab = new Mock<ICollaborator>();

        tmRepo.Setup(tmr => tmr.GetByIdAsync(It.IsAny<long>())).ReturnsAsync((TrainingModule)null!);
        collabRepo.Setup(cr => cr.GetByIdAsync(collaboratorId)).ReturnsAsync(collab.Object);

        var service =
            new AssociationTrainingModuleCollaboratorService(assocRepo.Object, tmRepo.Object, collabRepo.Object, assocTCFactory.Object);

        // Assert
        ArgumentException exception = await Assert.ThrowsAsync<ArgumentException>(() =>
            // Act
            service.Add(It.IsAny<long>(), collaboratorId)
        );

        Assert.Equal("Invalid arguments", exception.Message);
    }

    [Fact]
    public async Task WhenPassingInvalidCollaboratorId_ThenThrowException()
    {
        // Arrange
        Mock<IAssociationTrainingModuleCollaboratorRepository> assocRepo =
            new Mock<IAssociationTrainingModuleCollaboratorRepository>();
        Mock<ITrainingModuleRepository> tmRepo = new Mock<ITrainingModuleRepository>();
        Mock<ICollaboratorRepository> collabRepo =
            new Mock<ICollaboratorRepository>();
        Mock<IAssociationTrainingModuleCollaboratorFactory> assocTCFactory =
            new Mock<IAssociationTrainingModuleCollaboratorFactory>();

        long trainingModuleId = 1;

        Mock<ITrainingModule> tm = new Mock<ITrainingModule>();
        Mock<ICollaborator> collab = new Mock<ICollaborator>();

        tmRepo.Setup(tmr => tmr.GetByIdAsync(trainingModuleId)).ReturnsAsync(tm.Object);
        collabRepo.Setup(cr => cr.GetByIdAsync(It.IsAny<long>())).ReturnsAsync((ICollaborator)null!);

        var service =
            new AssociationTrainingModuleCollaboratorService(assocRepo.Object, tmRepo.Object, collabRepo.Object, assocTCFactory.Object);

        // Assert
        ArgumentException exception = await Assert.ThrowsAsync<ArgumentException>(() =>
            // Act
            service.Add(trainingModuleId, It.IsAny<long>())
        );

        Assert.Equal("Invalid arguments", exception.Message);
    }
}