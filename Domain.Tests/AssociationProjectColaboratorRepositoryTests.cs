namespace Domain.Tests;

using Domain;
using Moq;

public class AssociationProjectCollaboratorRepositoryTest
{
    [Fact]
    public void WhenPassingCorrectValues_ThenObjectIsCreated()
    {
        //arrange
        Mock<IAssociationProjectCollaborator> assocMock = new Mock<IAssociationProjectCollaborator>();
        List<IAssociationProjectCollaborator> associationsProjectCollaborator = new List<IAssociationProjectCollaborator> { assocMock.Object };

        //act
        new AssociationProjectCollaboratorRepository(associationsProjectCollaborator);

        //assert
    }

    [Fact]
    public void WhenPassingProject_ThenReturnAllProjectCollaborators()
    {
        //arrange
        Mock<IProject> projectMock = new Mock<IProject>();

        Mock<IAssociationProjectCollaborator> assocMock1 = new Mock<IAssociationProjectCollaborator>();
        Mock<IAssociationProjectCollaborator> assocMock2 = new Mock<IAssociationProjectCollaborator>();
        List<IAssociationProjectCollaborator> associationsProjectCollaborator = new List<IAssociationProjectCollaborator> {
            assocMock1.Object,
            assocMock2.Object
        };

        Mock<ICollaborator> collab1 = new Mock<ICollaborator>();
        Mock<ICollaborator> collab2 = new Mock<ICollaborator>();

        assocMock1.Setup(a => a.HasProject(projectMock.Object)).Returns(false);
        assocMock1.Setup(a => a.GetCollaborator()).Returns(collab1.Object);

        assocMock2.Setup(a => a.HasProject(projectMock.Object)).Returns(true);
        assocMock2.Setup(a => a.GetCollaborator()).Returns(collab2.Object);

        List<ICollaborator> expected = new List<ICollaborator> { collab2.Object };

        var assoc = new AssociationProjectCollaboratorRepository(associationsProjectCollaborator);

        //act
        var result = assoc.FindAllProjectCollaborators(projectMock.Object);

        //assert
        Assert.Equal(expected.Count(), result.Count());
        Assert.True(expected.SequenceEqual(result));
    }

    [Fact]
    public void WhenProjectHasNoAssociation_ThenReturnEmptyList()
    {
        //arrange
        Mock<IProject> projectMock = new Mock<IProject>();

        Mock<IAssociationProjectCollaborator> assocMock1 = new Mock<IAssociationProjectCollaborator>();
        List<IAssociationProjectCollaborator> associationsProjectCollaborator = new List<IAssociationProjectCollaborator> {
            assocMock1.Object,
        };

        Mock<ICollaborator> collab1 = new Mock<ICollaborator>();

        assocMock1.Setup(a => a.HasProject(projectMock.Object)).Returns(false);
        assocMock1.Setup(a => a.AssociationIntersectDates(It.IsAny<DateOnly>(), It.IsAny<DateOnly>())).Returns(true);
        assocMock1.Setup(a => a.GetCollaborator()).Returns(collab1.Object);

        var assoc = new AssociationProjectCollaboratorRepository(associationsProjectCollaborator);

        //act
        var result = assoc.FindAllProjectCollaboratorsBetween(projectMock.Object, It.IsAny<DateOnly>(), It.IsAny<DateOnly>());

        //assert
        Assert.Empty(result);
    }

    [Fact]
    public void WhenDatesDontIntersect_ThenReturnEmptyList()
    {
        //arrange
        Mock<IProject> projectMock = new Mock<IProject>();

        Mock<IAssociationProjectCollaborator> assocMock1 = new Mock<IAssociationProjectCollaborator>();
        List<IAssociationProjectCollaborator> associationsProjectCollaborator = new List<IAssociationProjectCollaborator> {
            assocMock1.Object,
        };

        Mock<ICollaborator> collab1 = new Mock<ICollaborator>();

        assocMock1.Setup(a => a.HasProject(projectMock.Object)).Returns(true);
        assocMock1.Setup(a => a.AssociationIntersectDates(It.IsAny<DateOnly>(), It.IsAny<DateOnly>())).Returns(false);
        assocMock1.Setup(a => a.GetCollaborator()).Returns(collab1.Object);

        var assoc = new AssociationProjectCollaboratorRepository(associationsProjectCollaborator);

        //act
        var result = assoc.FindAllProjectCollaboratorsBetween(projectMock.Object, It.IsAny<DateOnly>(), It.IsAny<DateOnly>());

        //assert
        Assert.Empty(result);
    }

    [Fact]
    public void WhenProjectHasNoAssociation_AndDatesDontIntersect_ThenReturnEmptyList()
    {
        //arrange
        Mock<IProject> projectMock = new Mock<IProject>();

        Mock<IAssociationProjectCollaborator> assocMock1 = new Mock<IAssociationProjectCollaborator>();
        List<IAssociationProjectCollaborator> associationsProjectCollaborator = new List<IAssociationProjectCollaborator> {
            assocMock1.Object,
        };

        Mock<ICollaborator> collab1 = new Mock<ICollaborator>();

        assocMock1.Setup(a => a.HasProject(projectMock.Object)).Returns(false);
        assocMock1.Setup(a => a.AssociationIntersectDates(It.IsAny<DateOnly>(), It.IsAny<DateOnly>())).Returns(false);
        assocMock1.Setup(a => a.GetCollaborator()).Returns(collab1.Object);

        var assoc = new AssociationProjectCollaboratorRepository(associationsProjectCollaborator);

        //act
        var result = assoc.FindAllProjectCollaboratorsBetween(projectMock.Object, It.IsAny<DateOnly>(), It.IsAny<DateOnly>());

        //assert
        Assert.Empty(result);
    }


    [Fact]
    public void WhenProjectHasAssociationAndDatesIntersect_ThenReturnCollaborator()
    {
        //arrange
        Mock<IProject> projectMock = new Mock<IProject>();

        Mock<IAssociationProjectCollaborator> assocMock1 = new Mock<IAssociationProjectCollaborator>();
        List<IAssociationProjectCollaborator> associationsProjectCollaborator = new List<IAssociationProjectCollaborator> {
            assocMock1.Object,
        };

        Mock<ICollaborator> collab1 = new Mock<ICollaborator>();

        assocMock1.Setup(a => a.HasProject(projectMock.Object)).Returns(true);
        assocMock1.Setup(a => a.AssociationIntersectDates(It.IsAny<DateOnly>(), It.IsAny<DateOnly>())).Returns(true);
        assocMock1.Setup(a => a.GetCollaborator()).Returns(collab1.Object);

        List<ICollaborator> expected = new List<ICollaborator> { collab1.Object };

        var assoc = new AssociationProjectCollaboratorRepository(associationsProjectCollaborator);

        //act
        var result = assoc.FindAllProjectCollaboratorsBetween(projectMock.Object, It.IsAny<DateOnly>(), It.IsAny<DateOnly>());

        //assert
        Assert.Equal(expected.Count(), result.Count());
        Assert.True(expected.SequenceEqual(result));
    }
}