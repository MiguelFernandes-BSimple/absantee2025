using Domain.Factory;
using Domain.Interfaces;
using Domain.IRepository;
using Domain.Models;
using Domain.Visitor;
using Moq;

namespace Domain.Tests.AssociationTrainingModuleCollaboratorTests;

public class AssociationTrainingModuleCollaboratorFactoryTests
{
    [Fact]
    public async Task WhenPassingValidParameters_ThenInstatiateObject()
    {
        // Arrange
        Mock<IAssociationTrainingModuleCollaboratorRepository> assocRepo = new Mock<IAssociationTrainingModuleCollaboratorRepository>();
        Mock<ITrainingModuleRepository> tmRepo = new Mock<ITrainingModuleRepository>();
        Mock<ICollaboratorRepository> collabRepo = new Mock<ICollaboratorRepository>();

        Mock<ITrainingModule> tm = new Mock<ITrainingModule>();
        Mock<ICollaborator> collab = new Mock<ICollaborator>();
        long trainingModuleId = 1;
        long collabId = 1;

        tmRepo.Setup(tr => tr.GetByIdAsync(trainingModuleId)).ReturnsAsync(tm.Object);
        collabRepo.Setup(cr => cr.GetByIdAsync(collabId)).ReturnsAsync(collab.Object);

        // Unicity test
        assocRepo.Setup(ar => ar.FindByCollaborator(collabId)).ReturnsAsync((IAssociationTrainingModuleCollaborator)null!);

        AssociationTrainingModuleCollaboratorFactory factory =
            new AssociationTrainingModuleCollaboratorFactory(assocRepo.Object, tmRepo.Object, collabRepo.Object);

        // Act
        AssociationTrainingModuleCollaborator result =
            await factory.Create(trainingModuleId, collabId);

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public async Task WhenPassingInvalidTrainingModule_ThenThrowException()
    {
        // Arrange
        Mock<IAssociationTrainingModuleCollaboratorRepository> assocRepo = new Mock<IAssociationTrainingModuleCollaboratorRepository>();
        Mock<ITrainingModuleRepository> tmRepo = new Mock<ITrainingModuleRepository>();
        Mock<ICollaboratorRepository> collabRepo = new Mock<ICollaboratorRepository>();

        Mock<ITrainingModule> tm = new Mock<ITrainingModule>();
        Mock<ICollaborator> collab = new Mock<ICollaborator>();
        long trainingModuleId = 1;
        long collabId = 1;

        tmRepo.Setup(tr => tr.GetByIdAsync(trainingModuleId)).ReturnsAsync((ITrainingModule)null!);
        collabRepo.Setup(cr => cr.GetByIdAsync(collabId)).ReturnsAsync(collab.Object);

        // Unicity test
        assocRepo.Setup(ar => ar.FindByCollaborator(collabId)).ReturnsAsync((IAssociationTrainingModuleCollaborator)null!);

        AssociationTrainingModuleCollaborator expected =
            new AssociationTrainingModuleCollaborator(trainingModuleId, collabId);

        AssociationTrainingModuleCollaboratorFactory factory =
            new AssociationTrainingModuleCollaboratorFactory(assocRepo.Object, tmRepo.Object, collabRepo.Object);

        // Assert
        ArgumentException exception = await Assert.ThrowsAsync<ArgumentException>(() =>
            // Act
            factory.Create(trainingModuleId, collabId));

        Assert.Equal("Invalid inputs", exception.Message);
    }

    [Fact]
    public async Task WhenPassingInvalidCollaborator_ThenThrowException()
    {
        // Arrange
        Mock<IAssociationTrainingModuleCollaboratorRepository> assocRepo = new Mock<IAssociationTrainingModuleCollaboratorRepository>();
        Mock<ITrainingModuleRepository> tmRepo = new Mock<ITrainingModuleRepository>();
        Mock<ICollaboratorRepository> collabRepo = new Mock<ICollaboratorRepository>();

        Mock<ITrainingModule> tm = new Mock<ITrainingModule>();
        Mock<ICollaborator> collab = new Mock<ICollaborator>();
        long trainingModuleId = 1;
        long collabId = 1;

        tmRepo.Setup(tr => tr.GetByIdAsync(trainingModuleId)).ReturnsAsync(tm.Object);
        collabRepo.Setup(cr => cr.GetByIdAsync(collabId)).ReturnsAsync((ICollaborator)null!);

        // Unicity test
        assocRepo.Setup(ar => ar.FindByCollaborator(collabId)).ReturnsAsync((IAssociationTrainingModuleCollaborator)null!);

        AssociationTrainingModuleCollaborator expected =
            new AssociationTrainingModuleCollaborator(trainingModuleId, collabId);

        AssociationTrainingModuleCollaboratorFactory factory =
            new AssociationTrainingModuleCollaboratorFactory(assocRepo.Object, tmRepo.Object, collabRepo.Object);

        // Assert
        ArgumentException exception = await Assert.ThrowsAsync<ArgumentException>(() =>
            // Act
            factory.Create(trainingModuleId, collabId));

        Assert.Equal("Invalid inputs", exception.Message);
    }

    [Fact]
    public async Task WhenPassingRepeatedCollaboratorEntry_ThenThrowException()
    {
        // Arrange
        Mock<IAssociationTrainingModuleCollaboratorRepository> assocRepo = new Mock<IAssociationTrainingModuleCollaboratorRepository>();
        Mock<ITrainingModuleRepository> tmRepo = new Mock<ITrainingModuleRepository>();
        Mock<ICollaboratorRepository> collabRepo = new Mock<ICollaboratorRepository>();

        Mock<ITrainingModule> tm = new Mock<ITrainingModule>();
        Mock<ICollaborator> collab = new Mock<ICollaborator>();
        long trainingModuleId = 1;
        long collabId = 1;

        tmRepo.Setup(tr => tr.GetByIdAsync(trainingModuleId)).ReturnsAsync(tm.Object);
        collabRepo.Setup(cr => cr.GetByIdAsync(collabId)).ReturnsAsync(collab.Object);

        // Unicity test
        assocRepo.Setup(ar => ar.FindByCollaborator(collabId)).ReturnsAsync(It.IsAny<IAssociationTrainingModuleCollaborator>());

        AssociationTrainingModuleCollaborator expected =
            new AssociationTrainingModuleCollaborator(trainingModuleId, collabId);

        AssociationTrainingModuleCollaboratorFactory factory =
            new AssociationTrainingModuleCollaboratorFactory(assocRepo.Object, tmRepo.Object, collabRepo.Object);

        // Assert
        ArgumentException exception = await Assert.ThrowsAsync<ArgumentException>(() =>
            // Act
            factory.Create(trainingModuleId, collabId));

        Assert.Equal("Invalid inputs", exception.Message);
    }

    [Fact]
    public void WhenPassingValidVisitor_ThenInstatiateObject()
    {
        // Arrange
        var visitor = new Mock<IAssociationTrainingModuleCollaboratorVisitor>();
        visitor.Setup(v => v.Id).Returns(It.IsAny<long>());
        visitor.Setup(v => v.TrainingModuleId).Returns(It.IsAny<long>());
        visitor.Setup(v => v.CollaboratorId).Returns(It.IsAny<long>());

        Mock<IAssociationTrainingModuleCollaboratorRepository> assocRepo = new Mock<IAssociationTrainingModuleCollaboratorRepository>();
        Mock<ITrainingModuleRepository> tmRepo = new Mock<ITrainingModuleRepository>();
        Mock<ICollaboratorRepository> collabRepo = new Mock<ICollaboratorRepository>();

        AssociationTrainingModuleCollaboratorFactory factory =
            new AssociationTrainingModuleCollaboratorFactory(assocRepo.Object, tmRepo.Object, collabRepo.Object);

        // Act
        var result = factory.Create(visitor.Object);

        // Assert
        Assert.NotNull(result);
    }
}