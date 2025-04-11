namespace Domain.Tests.AssociationProjectCollaboratorTests;

using Moq;
using Domain.Interfaces;
using Domain.Models;
using Domain.IRepository;
using Domain.Factory;

public class AssociationIntersectDates
{

    [Fact]
    public void WhenAssociationIntersectDatesReceivesValidaData_ReturnTrue()
    {
        // Arrange
        // Get repositories
        Mock<ICollaboratorRepository> collabRepo = new Mock<ICollaboratorRepository>();
        Mock<IProjectRepository> projectRepo = new Mock<IProjectRepository>();

        // Collaborator and Project stubs
        Mock<ICollaborator> collab = new Mock<ICollaborator>();
        Mock<IProject> project = new Mock<IProject>();
        Mock<IPeriodDate> periodDate = new Mock<IPeriodDate>();
        Mock<IPeriodDate> periodDateToIntersect = new Mock<IPeriodDate>();

        // Get collab and project from repos
        collabRepo.Setup(cr => cr.GetById((int)It.IsAny<long>())).Returns(collab.Object);
        projectRepo.Setup(pr => pr.GetById((int)It.IsAny<long>())).Returns(project.Object);

        // All validations
        project.Setup(p => p.ContainsDates(periodDate.Object)).Returns(true);
        project.Setup(p => p.IsFinished()).Returns(false);
        collab.Setup(c => c.ContractContainsDates(It.IsAny<IPeriodDateTime>())).Returns(true);

        // Instatiate Factory
        IAssociationProjectCollaboratorFactory factory = new AssociationProjectCollaboratorFactory(collabRepo.Object, projectRepo.Object);

        AssociationProjectCollaborator assoc = factory.Create(periodDate.Object, collab.Object.GetId(), project.Object.GetId());

        periodDate.Setup(pd => pd.Intersects(periodDateToIntersect.Object)).Returns(true);

        //act
        bool result = assoc.AssociationIntersectPeriod(periodDateToIntersect.Object);
        //assert
        Assert.True(result);
    }

    [Fact]
    public void WhenAssociationIntersectDatesReceivesValidaData_ReturnFalse()
    {
        // Arrange
        // Get repositories
        Mock<ICollaboratorRepository> collabRepo = new Mock<ICollaboratorRepository>();
        Mock<IProjectRepository> projectRepo = new Mock<IProjectRepository>();

        // Collaborator and Project stubs
        Mock<ICollaborator> collab = new Mock<ICollaborator>();
        Mock<IProject> project = new Mock<IProject>();
        Mock<IPeriodDate> periodDate = new Mock<IPeriodDate>();
        Mock<IPeriodDate> periodDateToIntersect = new Mock<IPeriodDate>();

        // Get collab and project from repos
        collabRepo.Setup(cr => cr.GetById((int)It.IsAny<long>())).Returns(collab.Object);
        projectRepo.Setup(pr => pr.GetById((int)It.IsAny<long>())).Returns(project.Object);

        // All validations
        project.Setup(p => p.ContainsDates(periodDate.Object)).Returns(true);
        project.Setup(p => p.IsFinished()).Returns(false);
        collab.Setup(c => c.ContractContainsDates(It.IsAny<IPeriodDateTime>())).Returns(true);

        // Instatiate Factory
        IAssociationProjectCollaboratorFactory factory = new AssociationProjectCollaboratorFactory(collabRepo.Object, projectRepo.Object);

        AssociationProjectCollaborator assoc = factory.Create(periodDate.Object, collab.Object.GetId(), project.Object.GetId());

        periodDate.Setup(pd => pd.Intersects(periodDateToIntersect.Object)).Returns(false);

        //act
        bool result = assoc.AssociationIntersectPeriod(periodDateToIntersect.Object);
        //assert
        Assert.False(result);
    }
}
