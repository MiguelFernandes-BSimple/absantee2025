namespace Domain.Tests.AssociationProjectCollaboratorTests;

using Moq;
using Domain.Interfaces;
using Domain.Models;
using Domain.Factory;
using Domain.IRepository;
using Domain.Visitor;
using System.Threading.Tasks;

public class Factory
{
    [Fact]
    public async Task WhenPassingValidValueObjects_ThenAssociationProjectCollaboratorIsCreated()
    {
        // Arrange
        // Get repositories
        Mock<ICollaboratorRepository> collabRepo = new Mock<ICollaboratorRepository>();
        Mock<IProjectRepository> projectRepo = new Mock<IProjectRepository>();
        Mock<IAssociationProjectCollaboratorRepository> assocRepo = new Mock<IAssociationProjectCollaboratorRepository>();

        // Collaborator and Project stubs
        Mock<ICollaborator> collab = new Mock<ICollaborator>();
        Mock<IProject> project = new Mock<IProject>();
        Mock<IPeriodDate> periodDate = new Mock<IPeriodDate>();

        // Collab and Project ids
        long collabId = 1;
        long projectId = 1;

        // Get collab and project from repos
        collabRepo.Setup(cr => cr.GetById(collabId)).Returns(collab.Object);
        projectRepo.Setup(pr => pr.GetById(projectId)).Returns(project.Object);

        // Class validations
        project.Setup(p => p.ContainsDates(periodDate.Object)).Returns(true);
        project.Setup(p => p.IsFinished()).Returns(false);
        collab.Setup(c => c.ContractContainsDates(It.IsAny<IPeriodDateTime>())).Returns(true);

        // Unicity validaiton
        assocRepo.Setup(ar => ar.FindByProjectAndCollaboratorAsync(projectId, collabId)).ReturnsAsync((IAssociationProjectCollaborator)null!);

        // Instatiate Factory
        IAssociationProjectCollaboratorFactory factory =
            new AssociationProjectCollaboratorFactory(collabRepo.Object, projectRepo.Object, assocRepo.Object);

        // Act
        AssociationProjectCollaborator? result =
            await factory.Create(periodDate.Object, collabId, projectId);

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public async Task WhenPassingInvalidCollaboratorId_ThenThrowException()
    {
        // Arrange
        // Get repositories
        Mock<ICollaboratorRepository> collabRepo = new Mock<ICollaboratorRepository>();
        Mock<IProjectRepository> projectRepo = new Mock<IProjectRepository>();
        Mock<IAssociationProjectCollaboratorRepository> assocRepo = new Mock<IAssociationProjectCollaboratorRepository>();

        // Collaborator and Project stubs
        Mock<ICollaborator> collab = new Mock<ICollaborator>();
        Mock<IProject> project = new Mock<IProject>();
        Mock<IPeriodDate> periodDate = new Mock<IPeriodDate>();

        // Collab and Project ids
        long collabId = 1;
        long projectId = 1;

        // Get collab and project from repos
        collabRepo.Setup(cr => cr.GetById(collabId)).Returns((ICollaborator)null!);
        projectRepo.Setup(pr => pr.GetById(projectId)).Returns(project.Object);

        // All validations
        // Project doesnt contain the dates
        project.Setup(p => p.ContainsDates(periodDate.Object)).Returns(false);
        project.Setup(p => p.IsFinished()).Returns(false);
        collab.Setup(c => c.ContractContainsDates(It.IsAny<IPeriodDateTime>())).Returns(true);

        // Unicity validaiton
        assocRepo.Setup(ar => ar.FindByProjectAndCollaboratorAsync(projectId, collabId)).ReturnsAsync((IAssociationProjectCollaborator)null!);

        // Instatiate Factory
        IAssociationProjectCollaboratorFactory factory =
            new AssociationProjectCollaboratorFactory(collabRepo.Object, projectRepo.Object, assocRepo.Object);

        // Assert
        ArgumentException exception = await Assert.ThrowsAsync<ArgumentException>(() =>
            // Act
            factory.Create(periodDate.Object, collabId, projectId));

        Assert.Equal("Invalid arguments", exception.Message);
    }

    [Fact]
    public async Task WhenPassingInvalidProjectId_ThenThrowException()
    {
        // Arrange
        // Get repositories
        Mock<ICollaboratorRepository> collabRepo = new Mock<ICollaboratorRepository>();
        Mock<IProjectRepository> projectRepo = new Mock<IProjectRepository>();
        Mock<IAssociationProjectCollaboratorRepository> assocRepo = new Mock<IAssociationProjectCollaboratorRepository>();

        // Collaborator and Project stubs
        Mock<ICollaborator> collab = new Mock<ICollaborator>();
        Mock<IProject> project = new Mock<IProject>();
        Mock<IPeriodDate> periodDate = new Mock<IPeriodDate>();

        // Collab and Project ids
        long collabId = 1;
        long projectId = 1;

        // Get collab and project from repos
        collabRepo.Setup(cr => cr.GetById(collabId)).Returns(collab.Object);
        projectRepo.Setup(pr => pr.GetById(projectId)).Returns((IProject)null!);

        // All validations
        // Project doesnt contain the dates
        project.Setup(p => p.ContainsDates(periodDate.Object)).Returns(false);
        project.Setup(p => p.IsFinished()).Returns(false);
        collab.Setup(c => c.ContractContainsDates(It.IsAny<IPeriodDateTime>())).Returns(true);

        // Unicity validaiton
        assocRepo.Setup(ar => ar.FindByProjectAndCollaboratorAsync(projectId, collabId)).ReturnsAsync((IAssociationProjectCollaborator)null!);

        // Instatiate Factory
        IAssociationProjectCollaboratorFactory factory =
            new AssociationProjectCollaboratorFactory(collabRepo.Object, projectRepo.Object, assocRepo.Object);

        // Assert
        ArgumentException exception = await Assert.ThrowsAsync<ArgumentException>(() =>
            // Act
            factory.Create(periodDate.Object, collabId, projectId));

        Assert.Equal("Invalid arguments", exception.Message);
    }

    [Fact]
    public async Task WhenAssociationDatesAreNotInProjectDates_ThenThrowException()
    {
        // Arrange
        // Get repositories
        Mock<ICollaboratorRepository> collabRepo = new Mock<ICollaboratorRepository>();
        Mock<IProjectRepository> projectRepo = new Mock<IProjectRepository>();
        Mock<IAssociationProjectCollaboratorRepository> assocRepo = new Mock<IAssociationProjectCollaboratorRepository>();

        // Collaborator and Project stubs
        Mock<ICollaborator> collab = new Mock<ICollaborator>();
        Mock<IProject> project = new Mock<IProject>();
        Mock<IPeriodDate> periodDate = new Mock<IPeriodDate>();

        // Collab and Project ids
        long collabId = 1;
        long projectId = 1;

        // Get collab and project from repos
        collabRepo.Setup(cr => cr.GetById(collabId)).Returns(collab.Object);
        projectRepo.Setup(pr => pr.GetById(projectId)).Returns(project.Object);

        // All validations
        // Project doesnt contain the dates
        project.Setup(p => p.ContainsDates(periodDate.Object)).Returns(false);
        project.Setup(p => p.IsFinished()).Returns(false);
        collab.Setup(c => c.ContractContainsDates(It.IsAny<IPeriodDateTime>())).Returns(true);

        // Unicity validaiton
        assocRepo.Setup(ar => ar.FindByProjectAndCollaboratorAsync(projectId, collabId)).ReturnsAsync((IAssociationProjectCollaborator)null!);

        // Instatiate Factory
        IAssociationProjectCollaboratorFactory factory =
            new AssociationProjectCollaboratorFactory(collabRepo.Object, projectRepo.Object, assocRepo.Object);

        // Assert
        ArgumentException exception = await Assert.ThrowsAsync<ArgumentException>(() =>
            // Act
            factory.Create(periodDate.Object, collabId, projectId));

        Assert.Equal("Invalid arguments", exception.Message);
    }


    [Fact]
    public async Task WhenProjectIsFinished_ThenThrowException()
    {
        // Arrange
        // Get repositories
        Mock<ICollaboratorRepository> collabRepo = new Mock<ICollaboratorRepository>();
        Mock<IProjectRepository> projectRepo = new Mock<IProjectRepository>();
        Mock<IAssociationProjectCollaboratorRepository> assocRepo = new Mock<IAssociationProjectCollaboratorRepository>();

        // Collaborator and Project stubs
        Mock<ICollaborator> collab = new Mock<ICollaborator>();
        Mock<IProject> project = new Mock<IProject>();
        Mock<IPeriodDate> periodDate = new Mock<IPeriodDate>();

        // Collab and Project ids
        long collabId = 1;
        long projectId = 1;

        // Get collab and project from repos
        collabRepo.Setup(cr => cr.GetById(collabId)).Returns(collab.Object);
        projectRepo.Setup(pr => pr.GetById(projectId)).Returns(project.Object);

        // All validations
        project.Setup(p => p.ContainsDates(periodDate.Object)).Returns(false);
        // Contract is finished
        project.Setup(p => p.IsFinished()).Returns(true);
        collab.Setup(c => c.ContractContainsDates(It.IsAny<IPeriodDateTime>())).Returns(false);

        // Unicity validaiton
        assocRepo.Setup(ar => ar.FindByProjectAndCollaboratorAsync(projectId, collabId)).ReturnsAsync((IAssociationProjectCollaborator)null!);

        // Instatiate Factory
        IAssociationProjectCollaboratorFactory factory =
        new AssociationProjectCollaboratorFactory(collabRepo.Object, projectRepo.Object, assocRepo.Object);

        // Assert
        ArgumentException exception = await Assert.ThrowsAsync<ArgumentException>(() =>
            // Act
            factory.Create(periodDate.Object, collabId, projectId));


        Assert.Equal("Invalid arguments", exception.Message);
    }

    [Fact]
    public async Task WhenProjectDatesOutsideAssociation_ThenThrowException()
    {
        // Arrange
        // Get repositories
        Mock<ICollaboratorRepository> collabRepo = new Mock<ICollaboratorRepository>();
        Mock<IProjectRepository> projectRepo = new Mock<IProjectRepository>();
        Mock<IAssociationProjectCollaboratorRepository> assocRepo = new Mock<IAssociationProjectCollaboratorRepository>();

        // Collaborator and Project stubs
        Mock<ICollaborator> collab = new Mock<ICollaborator>();
        Mock<IProject> project = new Mock<IProject>();
        Mock<IPeriodDate> periodDate = new Mock<IPeriodDate>();

        // Collab and Project ids
        long collabId = 1;
        long projectId = 1;

        // Get collab and project from repos
        collabRepo.Setup(cr => cr.GetById(collabId)).Returns(collab.Object);
        projectRepo.Setup(pr => pr.GetById(projectId)).Returns(project.Object);

        // All validations
        project.Setup(p => p.ContainsDates(periodDate.Object)).Returns(false);
        project.Setup(p => p.IsFinished()).Returns(false);
        // Contract doesn't contain the dates
        collab.Setup(c => c.ContractContainsDates(It.IsAny<IPeriodDateTime>())).Returns(false);

        // Unicity validaiton
        assocRepo.Setup(ar => ar.FindByProjectAndCollaboratorAsync(projectId, collabId)).ReturnsAsync((IAssociationProjectCollaborator)null!);

        // Instatiate Factory
        IAssociationProjectCollaboratorFactory factory =
            new AssociationProjectCollaboratorFactory(collabRepo.Object, projectRepo.Object, assocRepo.Object);

        // Assert
        ArgumentException exception = await Assert.ThrowsAsync<ArgumentException>(() =>
            // Act
            factory.Create(periodDate.Object, collabId, projectId));


        Assert.Equal("Invalid arguments", exception.Message);
    }

    [Fact]
    public async Task WhenPassingInputsMatchingExistingAssociationAndPeriodIsValid_TheAssociationrojectCollaboratorIsCreated()
    {
        // Arrange
        // Get repositories
        Mock<ICollaboratorRepository> collabRepo = new Mock<ICollaboratorRepository>();
        Mock<IProjectRepository> projectRepo = new Mock<IProjectRepository>();
        Mock<IAssociationProjectCollaboratorRepository> assocRepo = new Mock<IAssociationProjectCollaboratorRepository>();

        // Collaborator and Project stubs
        Mock<ICollaborator> collab = new Mock<ICollaborator>();
        Mock<IProject> project = new Mock<IProject>();
        Mock<IPeriodDate> periodDate = new Mock<IPeriodDate>();

        // Collab and Project ids
        long collabId = 1;
        long projectId = 1;

        // Get collab and project from repos
        collabRepo.Setup(cr => cr.GetById(collabId)).Returns(collab.Object);
        projectRepo.Setup(pr => pr.GetById(projectId)).Returns(project.Object);

        // Class validations
        project.Setup(p => p.ContainsDates(periodDate.Object)).Returns(true);
        project.Setup(p => p.IsFinished()).Returns(false);
        collab.Setup(c => c.ContractContainsDates(It.IsAny<IPeriodDateTime>())).Returns(true);

        // Unicity validaiton
        Mock<IAssociationProjectCollaborator> assoc = new Mock<IAssociationProjectCollaborator>();
        assocRepo.Setup(ar => ar.FindByProjectAndCollaboratorAsync(projectId, collabId)).ReturnsAsync(assoc.Object);

        Mock<IPeriodDate> assocPeriod = new Mock<IPeriodDate>();
        assoc.Setup(a => a.GetPeriodDate()).Returns(assocPeriod.Object);

        // The dates don't intersect
        periodDate.Setup(pd => pd.Intersects(assocPeriod.Object)).Returns(false);

        // Instatiate Factory
        IAssociationProjectCollaboratorFactory factory =
            new AssociationProjectCollaboratorFactory(collabRepo.Object, projectRepo.Object, assocRepo.Object);

        // Act
        AssociationProjectCollaborator? result =
            await factory.Create(periodDate.Object, collabId, projectId);

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public async Task WhenPassingInputsMatchingExistingAssociationAndPeriodIsInValid_TheThrowException()
    {
        // Arrange
        // Get repositories
        Mock<ICollaboratorRepository> collabRepo = new Mock<ICollaboratorRepository>();
        Mock<IProjectRepository> projectRepo = new Mock<IProjectRepository>();
        Mock<IAssociationProjectCollaboratorRepository> assocRepo = new Mock<IAssociationProjectCollaboratorRepository>();

        // Collaborator and Project stubs
        Mock<ICollaborator> collab = new Mock<ICollaborator>();
        Mock<IProject> project = new Mock<IProject>();
        Mock<IPeriodDate> periodDate = new Mock<IPeriodDate>();

        // Collab and Project ids
        long collabId = 1;
        long projectId = 1;

        // Get collab and project from repos
        collabRepo.Setup(cr => cr.GetById(collabId)).Returns(collab.Object);
        projectRepo.Setup(pr => pr.GetById(projectId)).Returns(project.Object);

        // All validations
        project.Setup(p => p.ContainsDates(periodDate.Object)).Returns(false);
        project.Setup(p => p.IsFinished()).Returns(false);
        // Contract doesn't contain the dates
        collab.Setup(c => c.ContractContainsDates(It.IsAny<IPeriodDateTime>())).Returns(false);

        // Unicity validaiton
        Mock<IAssociationProjectCollaborator> assoc = new Mock<IAssociationProjectCollaborator>();
        assocRepo.Setup(ar => ar.FindByProjectAndCollaboratorAsync(projectId, collabId)).ReturnsAsync(assoc.Object);

        Mock<IPeriodDate> assocPeriod = new Mock<IPeriodDate>();
        assoc.Setup(a => a.GetPeriodDate()).Returns(assocPeriod.Object);

        // Periods Intersect
        periodDate.Setup(pd => pd.Intersects(assocPeriod.Object)).Returns(true);

        // Instatiate Factory
        IAssociationProjectCollaboratorFactory factory =
            new AssociationProjectCollaboratorFactory(collabRepo.Object, projectRepo.Object, assocRepo.Object);

        // Assert
        ArgumentException exception = await Assert.ThrowsAsync<ArgumentException>(() =>
            // Act
            factory.Create(periodDate.Object, collabId, projectId));


        Assert.Equal("Invalid arguments", exception.Message);
    }

    [Fact]
    public void WhenPassingVisitor_ThenAssociationProjectCollaboratorIsCreated()
    {
        // Arrange
        Mock<IAssociationProjectCollaboratorVisitor> visitor = new Mock<IAssociationProjectCollaboratorVisitor>();
        Mock<IAssociationProjectCollaboratorRepository> assocRepo = new Mock<IAssociationProjectCollaboratorRepository>();

        Mock<ICollaboratorRepository> collabRepo = new Mock<ICollaboratorRepository>();
        Mock<IProjectRepository> projectRepo = new Mock<IProjectRepository>();

        IAssociationProjectCollaboratorFactory factory =
            new AssociationProjectCollaboratorFactory(collabRepo.Object, projectRepo.Object, assocRepo.Object);

        // Act
        factory.Create(visitor.Object);

        //Assert
    }
}
