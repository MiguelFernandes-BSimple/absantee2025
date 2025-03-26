namespace Domain.Tests;

using Domain;
using Moq;

public class AssociationProjectColaboratorRepositoryTest
{
    [Fact]
    public void WhenPassingCorrectValues_ThenObjectIsCreated()
    {
        //arrange
        Mock<IAssociationProjectColaborator> assocMock = new Mock<IAssociationProjectColaborator>();
        List<IAssociationProjectColaborator> associationsProjectColaborator = new List<IAssociationProjectColaborator> { assocMock.Object };

        //act
        new AssociationProjectColaboratorRepository(associationsProjectColaborator);

        //assert
    }

    [Fact]
    public void WhenPassingProject_ThenReturnAllProjectCollaborators()
    {
        //arrange
        Mock<IProject> projectMock = new Mock<IProject>();

        Mock<IAssociationProjectColaborator> assocMock1 = new Mock<IAssociationProjectColaborator>();
        Mock<IAssociationProjectColaborator> assocMock2 = new Mock<IAssociationProjectColaborator>();
        List<IAssociationProjectColaborator> associationsProjectColaborator = new List<IAssociationProjectColaborator> {
            assocMock1.Object,
            assocMock2.Object
        };

        Mock<IColaborator> colab1 = new Mock<IColaborator>();
        Mock<IColaborator> colab2 = new Mock<IColaborator>();

        assocMock1.Setup(a => a.HasProject(projectMock.Object)).Returns(false);
        assocMock1.Setup(a => a.GetColaborator()).Returns(colab1.Object);

        assocMock2.Setup(a => a.HasProject(projectMock.Object)).Returns(true);
        assocMock2.Setup(a => a.GetColaborator()).Returns(colab2.Object);

        List<IColaborator> expected = new List<IColaborator> { colab2.Object };

        var assoc = new AssociationProjectColaboratorRepository(associationsProjectColaborator);

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

        Mock<IAssociationProjectColaborator> assocMock1 = new Mock<IAssociationProjectColaborator>();
        List<IAssociationProjectColaborator> associationsProjectColaborator = new List<IAssociationProjectColaborator> {
            assocMock1.Object,
        };

        Mock<IColaborator> colab1 = new Mock<IColaborator>();

        assocMock1.Setup(a => a.HasProject(projectMock.Object)).Returns(false);
        assocMock1.Setup(a => a.AssociationIntersectDates(It.IsAny<DateOnly>(), It.IsAny<DateOnly>())).Returns(true);
        assocMock1.Setup(a => a.GetColaborator()).Returns(colab1.Object);

        var assoc = new AssociationProjectColaboratorRepository(associationsProjectColaborator);

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

        Mock<IAssociationProjectColaborator> assocMock1 = new Mock<IAssociationProjectColaborator>();
        List<IAssociationProjectColaborator> associationsProjectColaborator = new List<IAssociationProjectColaborator> {
            assocMock1.Object,
        };

        Mock<IColaborator> colab1 = new Mock<IColaborator>();

        assocMock1.Setup(a => a.HasProject(projectMock.Object)).Returns(true);
        assocMock1.Setup(a => a.AssociationIntersectDates(It.IsAny<DateOnly>(), It.IsAny<DateOnly>())).Returns(false);
        assocMock1.Setup(a => a.GetColaborator()).Returns(colab1.Object);

        var assoc = new AssociationProjectColaboratorRepository(associationsProjectColaborator);

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

        Mock<IAssociationProjectColaborator> assocMock1 = new Mock<IAssociationProjectColaborator>();
        List<IAssociationProjectColaborator> associationsProjectColaborator = new List<IAssociationProjectColaborator> {
            assocMock1.Object,
        };

        Mock<IColaborator> colab1 = new Mock<IColaborator>();

        assocMock1.Setup(a => a.HasProject(projectMock.Object)).Returns(false);
        assocMock1.Setup(a => a.AssociationIntersectDates(It.IsAny<DateOnly>(), It.IsAny<DateOnly>())).Returns(false);
        assocMock1.Setup(a => a.GetColaborator()).Returns(colab1.Object);

        var assoc = new AssociationProjectColaboratorRepository(associationsProjectColaborator);

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

        Mock<IAssociationProjectColaborator> assocMock1 = new Mock<IAssociationProjectColaborator>();
        List<IAssociationProjectColaborator> associationsProjectColaborator = new List<IAssociationProjectColaborator> {
            assocMock1.Object,
        };

        Mock<IColaborator> colab1 = new Mock<IColaborator>();

        assocMock1.Setup(a => a.HasProject(projectMock.Object)).Returns(true);
        assocMock1.Setup(a => a.AssociationIntersectDates(It.IsAny<DateOnly>(), It.IsAny<DateOnly>())).Returns(true);
        assocMock1.Setup(a => a.GetColaborator()).Returns(colab1.Object);

        List<IColaborator> expected = new List<IColaborator> { colab1.Object };

        var assoc = new AssociationProjectColaboratorRepository(associationsProjectColaborator);

        //act
        var result = assoc.FindAllProjectCollaboratorsBetween(projectMock.Object, It.IsAny<DateOnly>(), It.IsAny<DateOnly>());

        //assert
        Assert.Equal(expected.Count(), result.Count());
        Assert.True(expected.SequenceEqual(result));
    }
}