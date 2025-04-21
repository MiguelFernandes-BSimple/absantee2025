using Moq;
using Domain.Interfaces;
using Domain.Models;
using Domain.Factory;
using Domain.IRepository;
using Domain.Visitor;

namespace Domain.Tests.AssociationTrainingModuleCollaboratorTests;

public class AssociationTrainingModuleCollaboratorFactoryTests
{
    [Fact]
    public async Task WhenPassingValidData_ThenIsCreated()
    {
        // Arrange
        // Get repositories
        var collabRepo = new Mock<ICollaboratorRepository>();
        var trainingModuleRepo = new Mock<ITrainingModuleRepository>();
        var assocRepo = new Mock<IAssociationTrainingModuleCollaboratorRepository>();

        // Collaborator and Project stubs
        var collab = new Mock<ICollaborator>();
        var trainingModule = new Mock<ITrainingModule>();
        var periodDateTime = new PeriodDateTime(It.IsAny<DateTime>(), It.IsAny<DateTime>());

        // Collab and Project ids
        long collabId = 1;
        long trainingModuleId = 1;

        // Get collab and project from repos
        collabRepo.Setup(cr => cr.GetById(collabId)).Returns(collab.Object);
        trainingModuleRepo.Setup(pr => pr.GetById(trainingModuleId)).Returns(trainingModule.Object);

        // Class validations
        collab.Setup(c => c.ContractContainsDates(It.IsAny<PeriodDateTime>())).Returns(true);

        // Unicity validaiton
        assocRepo.Setup(ar => ar.CanInsert(collabId, trainingModuleId, periodDateTime)).ReturnsAsync(true);

        // Instatiate Factory
        IAssociationTrainingModuleCollaboratorFactory factory =
            new AssociationTrainingModuleCollaboratorFactory(collabRepo.Object, trainingModuleRepo.Object, assocRepo.Object);

        // Act
        AssociationTrainingModuleCollaborator? result =
            await factory.Create(collabId, trainingModuleId, periodDateTime);

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public async Task WhenPassingInvalidCollabId_ThenThrowsException()
    {
        // Arrange
        // Get repositories
        var collabRepo = new Mock<ICollaboratorRepository>();
        var trainingModuleRepo = new Mock<ITrainingModuleRepository>();
        var assocRepo = new Mock<IAssociationTrainingModuleCollaboratorRepository>();

        // Collaborator and Project stubs
        var collab = new Mock<ICollaborator>();
        var trainingModule = new Mock<ITrainingModule>();
        var periodDateTime = new PeriodDateTime(It.IsAny<DateTime>(), It.IsAny<DateTime>());

        // Collab and Project ids
        long collabId = 1;
        long trainingModuleId = 1;

        // Get collab and project from repos
        collabRepo.Setup(cr => cr.GetById(collabId)).Returns((ICollaborator)null!);
        trainingModuleRepo.Setup(pr => pr.GetById(trainingModuleId)).Returns(trainingModule.Object);

        // Class validations
        collab.Setup(c => c.ContractContainsDates(It.IsAny<PeriodDateTime>())).Returns(true);

        // Unicity validaiton
        assocRepo.Setup(ar => ar.CanInsert(collabId, trainingModuleId, periodDateTime)).ReturnsAsync(true);

        // Instatiate Factory
        IAssociationTrainingModuleCollaboratorFactory factory =
            new AssociationTrainingModuleCollaboratorFactory(collabRepo.Object, trainingModuleRepo.Object, assocRepo.Object);

        // Assert
        ArgumentException exception = await Assert.ThrowsAsync<ArgumentException>(() =>
            // Act
            factory.Create(collabId, trainingModuleId, periodDateTime));

        Assert.Equal("Invalid arguments", exception.Message);
    }

    [Fact]
    public async Task WhenPassingInvalidTrainingModuleId_ThenThrowsException()
    {
        // Arrange
        // Get repositories
        var collabRepo = new Mock<ICollaboratorRepository>();
        var trainingModuleRepo = new Mock<ITrainingModuleRepository>();
        var assocRepo = new Mock<IAssociationTrainingModuleCollaboratorRepository>();

        // Collaborator and Project stubs
        var collab = new Mock<ICollaborator>();
        var trainingModule = new Mock<ITrainingModule>();
        var periodDateTime = new PeriodDateTime(It.IsAny<DateTime>(), It.IsAny<DateTime>());

        // Collab and Project ids
        long collabId = 1;
        long trainingModuleId = 1;

        // Get collab and project from repos
        collabRepo.Setup(cr => cr.GetById(collabId)).Returns(collab.Object);
        trainingModuleRepo.Setup(pr => pr.GetById(trainingModuleId)).Returns((ITrainingModule)null!);

        // Class validations
        collab.Setup(c => c.ContractContainsDates(It.IsAny<PeriodDateTime>())).Returns(true);

        // Unicity validaiton
        assocRepo.Setup(ar => ar.CanInsert(collabId, trainingModuleId, periodDateTime)).ReturnsAsync(true);

        // Instatiate Factory
        IAssociationTrainingModuleCollaboratorFactory factory =
            new AssociationTrainingModuleCollaboratorFactory(collabRepo.Object, trainingModuleRepo.Object, assocRepo.Object);

        // Assert
        ArgumentException exception = await Assert.ThrowsAsync<ArgumentException>(() =>
            // Act
            factory.Create(collabId, trainingModuleId, periodDateTime));

        Assert.Equal("Invalid arguments", exception.Message);
    }

    [Fact]
    public async Task WhenCollaboratorDatesAreOutsideAssociation_ThenThrowsException()
    {
        // Arrange
        // Get repositories
        var collabRepo = new Mock<ICollaboratorRepository>();
        var trainingModuleRepo = new Mock<ITrainingModuleRepository>();
        var assocRepo = new Mock<IAssociationTrainingModuleCollaboratorRepository>();

        // Collaborator and Project stubs
        var collab = new Mock<ICollaborator>();
        var trainingModule = new Mock<ITrainingModule>();
        var periodDateTime = new PeriodDateTime(It.IsAny<DateTime>(), It.IsAny<DateTime>());

        // Collab and Project ids
        long collabId = 1;
        long trainingModuleId = 1;

        // Get collab and project from repos
        collabRepo.Setup(cr => cr.GetById(collabId)).Returns(collab.Object);
        trainingModuleRepo.Setup(pr => pr.GetById(trainingModuleId)).Returns(trainingModule.Object);

        // Class validations
        collab.Setup(c => c.ContractContainsDates(It.IsAny<PeriodDateTime>())).Returns(false);

        // Unicity validaiton
        assocRepo.Setup(ar => ar.CanInsert(collabId, trainingModuleId, periodDateTime)).ReturnsAsync(true);

        // Instatiate Factory
        IAssociationTrainingModuleCollaboratorFactory factory =
            new AssociationTrainingModuleCollaboratorFactory(collabRepo.Object, trainingModuleRepo.Object, assocRepo.Object);

        // Assert
        ArgumentException exception = await Assert.ThrowsAsync<ArgumentException>(() =>
            // Act
            factory.Create(collabId, trainingModuleId, periodDateTime));

        Assert.Equal("Invalid arguments", exception.Message);
    }

    [Fact]
    public async Task WhenAlreadyExistsAssociationInThatPeriod_ThenThrowsException()
    {
        // Arrange
        // Get repositories
        var collabRepo = new Mock<ICollaboratorRepository>();
        var trainingModuleRepo = new Mock<ITrainingModuleRepository>();
        var assocRepo = new Mock<IAssociationTrainingModuleCollaboratorRepository>();

        // Collaborator and Project stubs
        var collab = new Mock<ICollaborator>();
        var trainingModule = new Mock<ITrainingModule>();
        var periodDateTime = new PeriodDateTime(It.IsAny<DateTime>(), It.IsAny<DateTime>());

        // Collab and Project ids
        long collabId = 1;
        long trainingModuleId = 1;

        // Get collab and project from repos
        collabRepo.Setup(cr => cr.GetById(collabId)).Returns(collab.Object);
        trainingModuleRepo.Setup(pr => pr.GetById(trainingModuleId)).Returns(trainingModule.Object);

        // Class validations
        collab.Setup(c => c.ContractContainsDates(It.IsAny<PeriodDateTime>())).Returns(true);

        // Unicity validaiton
        assocRepo.Setup(ar => ar.CanInsert(collabId, trainingModuleId, periodDateTime)).ReturnsAsync(false);

        // Instatiate Factory
        IAssociationTrainingModuleCollaboratorFactory factory =
            new AssociationTrainingModuleCollaboratorFactory(collabRepo.Object, trainingModuleRepo.Object, assocRepo.Object);

        // Assert
        ArgumentException exception = await Assert.ThrowsAsync<ArgumentException>(() =>
            // Act
            factory.Create(collabId, trainingModuleId, periodDateTime));

        Assert.Equal("Invalid arguments", exception.Message);
    }


    [Fact]
    public void WhenPassingVisitor_ThenAssociationProjectCollaboratorIsCreated()
    {
        // Arrange
        var assocVisitor = new Mock<IAssociationTrainingModuleCollaboratorVisitor>();
        var assocRepo = new Mock<IAssociationTrainingModuleCollaboratorRepository>();

        var collabRepo = new Mock<ICollaboratorRepository>();
        var trainingModuleRepo = new Mock<ITrainingModuleRepository>();

        IAssociationTrainingModuleCollaboratorFactory factory =
            new AssociationTrainingModuleCollaboratorFactory(collabRepo.Object, trainingModuleRepo.Object, assocRepo.Object);

        // Act
        var result = factory.Create(assocVisitor.Object);

        //Assert
        Assert.NotNull(result);
    }
}
