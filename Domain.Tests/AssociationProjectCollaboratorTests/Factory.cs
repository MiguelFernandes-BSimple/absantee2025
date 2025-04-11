namespace Domain.Tests.AssociationProjectCollaboratorTests;

using Moq;
using Domain.Interfaces;
using Domain.Models;
using Domain.Factory;
using Domain.IRepository;
using Domain.Visitor;

public class Factory
{
    [Fact]
    public void WhenPassingValidValueObjects_ThenAssociationProjectCollaboratorIsCreated()
    {
        // Arrange
        // Get repositories
        Mock<ICollaboratorRepository> collabRepo = new Mock<ICollaboratorRepository>();
        Mock<IProjectRepository> projectRepo = new Mock<IProjectRepository>();

        // Collaborator and Project stubs
        Mock<ICollaborator> collab = new Mock<ICollaborator>();
        Mock<IProject> project = new Mock<IProject>();
        Mock<IPeriodDate> periodDate = new Mock<IPeriodDate>();

        // Get collab and project from repos
        collabRepo.Setup(cr => cr.GetById((int)It.IsAny<long>())).Returns(collab.Object);
        projectRepo.Setup(pr => pr.GetById((int)It.IsAny<long>())).Returns(project.Object);

        // All validations
        project.Setup(p => p.ContainsDates(periodDate.Object)).Returns(true);
        project.Setup(p => p.IsFinished()).Returns(false);
        collab.Setup(c => c.ContractContainsDates(It.IsAny<IPeriodDateTime>())).Returns(true);

        // Instatiate Factory
        IAssociationProjectCollaboratorFactory factory = new AssociationProjectCollaboratorFactory(collabRepo.Object, projectRepo.Object);

        // Act
        AssociationProjectCollaborator result = factory.Create(periodDate.Object, collab.Object.GetId(), project.Object.GetId());

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public void WhenPassingInvalidCollaboratorId_ThenThrowException()
    {
        // Arrange
        // Get repositories
        Mock<ICollaboratorRepository> collabRepo = new Mock<ICollaboratorRepository>();
        Mock<IProjectRepository> projectRepo = new Mock<IProjectRepository>();

        // Collaborator and Project stubs
        Mock<ICollaborator> collab = new Mock<ICollaborator>();
        Mock<IProject> project = new Mock<IProject>();
        Mock<IPeriodDate> periodDate = new Mock<IPeriodDate>();

        // Get collab and project from repos
        collabRepo.Setup(cr => cr.GetById((int)It.IsAny<long>())).Returns((ICollaborator)null!);
        projectRepo.Setup(pr => pr.GetById((int)It.IsAny<long>())).Returns(project.Object);

        // All validations
        // Project doesnt contain the dates
        project.Setup(p => p.ContainsDates(periodDate.Object)).Returns(false);
        project.Setup(p => p.IsFinished()).Returns(false);
        collab.Setup(c => c.ContractContainsDates(It.IsAny<IPeriodDateTime>())).Returns(true);

        // Instatiate Factory
        IAssociationProjectCollaboratorFactory factory = new AssociationProjectCollaboratorFactory(collabRepo.Object, projectRepo.Object);

        // Assert
        ArgumentException exception = Assert.Throws<ArgumentException>(() =>
            // Act
            factory.Create(periodDate.Object, collab.Object.GetId(), project.Object.GetId()));

        Assert.Equal("Invalid Arguments", exception.Message);
    }

    [Fact]
    public void WhenPassingInvalidProjectId_ThenThrowException()
    {
        // Arrange
        // Get repositories
        Mock<ICollaboratorRepository> collabRepo = new Mock<ICollaboratorRepository>();
        Mock<IProjectRepository> projectRepo = new Mock<IProjectRepository>();

        // Collaborator and Project stubs
        Mock<ICollaborator> collab = new Mock<ICollaborator>();
        Mock<IProject> project = new Mock<IProject>();
        Mock<IPeriodDate> periodDate = new Mock<IPeriodDate>();

        // Get collab and project from repos
        collabRepo.Setup(cr => cr.GetById((int)It.IsAny<long>())).Returns(collab.Object);
        projectRepo.Setup(pr => pr.GetById((int)It.IsAny<long>())).Returns((IProject)null!);

        // All validations
        // Project doesnt contain the dates
        project.Setup(p => p.ContainsDates(periodDate.Object)).Returns(false);
        project.Setup(p => p.IsFinished()).Returns(false);
        collab.Setup(c => c.ContractContainsDates(It.IsAny<IPeriodDateTime>())).Returns(true);

        // Instatiate Factory
        IAssociationProjectCollaboratorFactory factory = new AssociationProjectCollaboratorFactory(collabRepo.Object, projectRepo.Object);

        // Assert
        ArgumentException exception = Assert.Throws<ArgumentException>(() =>
            // Act
            factory.Create(periodDate.Object, collab.Object.GetId(), project.Object.GetId()));

        Assert.Equal("Invalid Arguments", exception.Message);
    }

    [Fact]
    public void WhenAssociationDatesAreNotInProjectDates_ThenThrowException()
    {
        // Arrange
        // Get repositories
        Mock<ICollaboratorRepository> collabRepo = new Mock<ICollaboratorRepository>();
        Mock<IProjectRepository> projectRepo = new Mock<IProjectRepository>();

        // Collaborator and Project stubs
        Mock<ICollaborator> collab = new Mock<ICollaborator>();
        Mock<IProject> project = new Mock<IProject>();
        Mock<IPeriodDate> periodDate = new Mock<IPeriodDate>();

        // Get collab and project from repos
        collabRepo.Setup(cr => cr.GetById((int)It.IsAny<long>())).Returns(collab.Object);
        projectRepo.Setup(pr => pr.GetById((int)It.IsAny<long>())).Returns(project.Object);

        // All validations
        // Project doesnt contain the dates
        project.Setup(p => p.ContainsDates(periodDate.Object)).Returns(false);
        project.Setup(p => p.IsFinished()).Returns(false);
        collab.Setup(c => c.ContractContainsDates(It.IsAny<IPeriodDateTime>())).Returns(true);

        // Instatiate Factory
        IAssociationProjectCollaboratorFactory factory = new AssociationProjectCollaboratorFactory(collabRepo.Object, projectRepo.Object);

        // Assert
        ArgumentException exception = Assert.Throws<ArgumentException>(() =>
            // Act
            factory.Create(periodDate.Object, collab.Object.GetId(), project.Object.GetId()));

        Assert.Equal("Invalid Arguments", exception.Message);
    }


    [Fact]
    public void WhenProjectIsFinished_ThenThrowException()
    {
        // Arrange
        // Get repositories
        Mock<ICollaboratorRepository> collabRepo = new Mock<ICollaboratorRepository>();
        Mock<IProjectRepository> projectRepo = new Mock<IProjectRepository>();

        // Collaborator and Project stubs
        Mock<ICollaborator> collab = new Mock<ICollaborator>();
        Mock<IProject> project = new Mock<IProject>();
        Mock<IPeriodDate> periodDate = new Mock<IPeriodDate>();

        // Get collab and project from repos
        collabRepo.Setup(cr => cr.GetById((int)It.IsAny<long>())).Returns(collab.Object);
        projectRepo.Setup(pr => pr.GetById((int)It.IsAny<long>())).Returns(project.Object);

        // All validations
        project.Setup(p => p.ContainsDates(periodDate.Object)).Returns(false);
        // Contract is finished
        project.Setup(p => p.IsFinished()).Returns(true);
        collab.Setup(c => c.ContractContainsDates(It.IsAny<IPeriodDateTime>())).Returns(false);

        // Instatiate Factory
        IAssociationProjectCollaboratorFactory factory = new AssociationProjectCollaboratorFactory(collabRepo.Object, projectRepo.Object);

        // Assert
        ArgumentException exception = Assert.Throws<ArgumentException>(() =>
            // Act
            factory.Create(periodDate.Object, collab.Object.GetId(), project.Object.GetId()));


        Assert.Equal("Invalid Arguments", exception.Message);
    }

    [Fact]
    public void WhenProjectDatesOutsideAssociation_ThenThrowException()
    {
        // Arrange
        // Get repositories
        Mock<ICollaboratorRepository> collabRepo = new Mock<ICollaboratorRepository>();
        Mock<IProjectRepository> projectRepo = new Mock<IProjectRepository>();

        // Collaborator and Project stubs
        Mock<ICollaborator> collab = new Mock<ICollaborator>();
        Mock<IProject> project = new Mock<IProject>();
        Mock<IPeriodDate> periodDate = new Mock<IPeriodDate>();

        // Get collab and project from repos
        collabRepo.Setup(cr => cr.GetById((int)It.IsAny<long>())).Returns(collab.Object);
        projectRepo.Setup(pr => pr.GetById((int)It.IsAny<long>())).Returns(project.Object);

        // All validations
        project.Setup(p => p.ContainsDates(periodDate.Object)).Returns(false);
        project.Setup(p => p.IsFinished()).Returns(false);
        // Contract doesn't contain the dates
        collab.Setup(c => c.ContractContainsDates(It.IsAny<IPeriodDateTime>())).Returns(false);

        // Instatiate Factory
        IAssociationProjectCollaboratorFactory factory = new AssociationProjectCollaboratorFactory(collabRepo.Object, projectRepo.Object);

        // Assert
        ArgumentException exception = Assert.Throws<ArgumentException>(() =>
            // Act
            factory.Create(periodDate.Object, collab.Object.GetId(), project.Object.GetId()));


        Assert.Equal("Invalid Arguments", exception.Message);
    }

    [Fact]
    public void WhenPassingVisitor_ThenAssociationProjectCollaboratorIsCreated()
    {
        // Arrange
        Mock<IAssociationProjectCollaboratorVisitor> visitor = new Mock<IAssociationProjectCollaboratorVisitor>();

        Mock<ICollaboratorRepository> collabRepo = new Mock<ICollaboratorRepository>();
        Mock<IProjectRepository> projectRepo = new Mock<IProjectRepository>();

        IAssociationProjectCollaboratorFactory factory = new AssociationProjectCollaboratorFactory(collabRepo.Object, projectRepo.Object);

        // Act
        factory.Create(visitor.Object);

        //Assert
    }
}
