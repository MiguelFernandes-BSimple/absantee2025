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
    public void WhenPassingProject_ThenReturnAllAssociationProjectCollaborator()
    {
        //arrange
        Mock<IProject> projectMock = new Mock<IProject>();

        Mock<IAssociationProjectCollaborator> assocMock1 = new Mock<IAssociationProjectCollaborator>();
        Mock<IAssociationProjectCollaborator> assocMock2 = new Mock<IAssociationProjectCollaborator>();
        List<IAssociationProjectCollaborator> associationsProjectCollaborator = new List<IAssociationProjectCollaborator> {
            assocMock1.Object,
            assocMock2.Object
        };

        assocMock1.Setup(a => a.HasProject(projectMock.Object)).Returns(false);

        assocMock2.Setup(a => a.HasProject(projectMock.Object)).Returns(true);

        List<IAssociationProjectCollaborator> expected = new List<IAssociationProjectCollaborator> {assocMock2.Object};

        var assoc = new AssociationProjectCollaboratorRepository(associationsProjectCollaborator);

        //act
        var result = assoc.FindAllByProject(projectMock.Object);

        //assert
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

        assocMock1.Setup(a => a.HasProject(projectMock.Object)).Returns(false);
        assocMock1.Setup(a => a.AssociationIntersectDates(It.IsAny<DateOnly>(), It.IsAny<DateOnly>())).Returns(true);

        var assoc = new AssociationProjectCollaboratorRepository(associationsProjectCollaborator);

        //act
        var result = assoc.FindAllByProjectAndBetweenPeriod(projectMock.Object, It.IsAny<DateOnly>(), It.IsAny<DateOnly>());

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

        assocMock1.Setup(a => a.HasProject(projectMock.Object)).Returns(true);
        assocMock1.Setup(a => a.AssociationIntersectDates(It.IsAny<DateOnly>(), It.IsAny<DateOnly>())).Returns(false);

        var assoc = new AssociationProjectCollaboratorRepository(associationsProjectCollaborator);

        //act
        var result = assoc.FindAllByProjectAndBetweenPeriod(projectMock.Object, It.IsAny<DateOnly>(), It.IsAny<DateOnly>());

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

        assocMock1.Setup(a => a.HasProject(projectMock.Object)).Returns(false);
        assocMock1.Setup(a => a.AssociationIntersectDates(It.IsAny<DateOnly>(), It.IsAny<DateOnly>())).Returns(false);

        var assoc = new AssociationProjectCollaboratorRepository(associationsProjectCollaborator);

        //act
        var result = assoc.FindAllByProjectAndBetweenPeriod(projectMock.Object, It.IsAny<DateOnly>(), It.IsAny<DateOnly>());

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

        assocMock1.Setup(a => a.HasProject(projectMock.Object)).Returns(true);
        assocMock1.Setup(a => a.AssociationIntersectDates(It.IsAny<DateOnly>(), It.IsAny<DateOnly>())).Returns(true);

        var assoc = new AssociationProjectCollaboratorRepository(associationsProjectCollaborator);

        List<IAssociationProjectCollaborator> expected = associationsProjectCollaborator;

        //act
        var result = assoc.FindAllByProjectAndBetweenPeriod(projectMock.Object, It.IsAny<DateOnly>(), It.IsAny<DateOnly>());

        //assert
        Assert.True(expected.SequenceEqual(result));
    }
}