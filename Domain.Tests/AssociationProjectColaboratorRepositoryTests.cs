namespace Domain.Tests;

using System.Security.Cryptography;
using Domain;
using Microsoft.VisualBasic;
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

        List<IAssociationProjectCollaborator> expected = new List<IAssociationProjectCollaborator> { assocMock2.Object };

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

    [Fact]
    public void WhenProjectAndCollaboratorHasNoAssociation_ThenReturnNull()
    {
        //arrange
        Mock<IProject> projectMock = new Mock<IProject>();
        Mock<ICollaborator> collabMock = new Mock<ICollaborator>();

        Mock<IAssociationProjectCollaborator> assocMock1 = new Mock<IAssociationProjectCollaborator>();
        List<IAssociationProjectCollaborator> associationsProjectCollaborator = new List<IAssociationProjectCollaborator> {
            assocMock1.Object,
        };

        assocMock1.Setup(a => a.HasProject(projectMock.Object)).Returns(false);
        assocMock1.Setup(a => a.HasCollaborator(collabMock.Object)).Returns(false);

        var assoc = new AssociationProjectCollaboratorRepository(associationsProjectCollaborator);

        //act
        var result = assoc.FindByProjectAndCollaborator(projectMock.Object, collabMock.Object);

        //assert
        Assert.Null(result);
    }


    [Fact]
    public void WhenProjectAndCollaboratorHasNoAssociation_ThenReturnAssociation()
    {
        //arrange
        Mock<IProject> projectMock = new Mock<IProject>();
        Mock<ICollaborator> collabMock = new Mock<ICollaborator>();

        Mock<IAssociationProjectCollaborator> assocMock1 = new Mock<IAssociationProjectCollaborator>();
        List<IAssociationProjectCollaborator> associationsProjectCollaborator = new List<IAssociationProjectCollaborator> {
            assocMock1.Object,
        };

        assocMock1.Setup(a => a.HasProject(projectMock.Object)).Returns(true);
        assocMock1.Setup(a => a.HasCollaborator(collabMock.Object)).Returns(true);

        var assoc = new AssociationProjectCollaboratorRepository(associationsProjectCollaborator);

        //act
        var result = assoc.FindByProjectAndCollaborator(projectMock.Object, collabMock.Object);

        //assert
        Assert.Equal(assocMock1.Object, result);
    }

    [Fact]
    public void WhenProjectDoesNotMatchButCollaboratorMatches_ThenReturnNull()
    {
        // Arrange
        Mock<IProject> projectMock = new Mock<IProject>();
        Mock<ICollaborator> collabMock = new Mock<ICollaborator>();

        Mock<IAssociationProjectCollaborator> assocMock1 = new Mock<IAssociationProjectCollaborator>();
        List<IAssociationProjectCollaborator> associationsProjectCollaborator = new List<IAssociationProjectCollaborator> {
            assocMock1.Object,
        };

        assocMock1.Setup(a => a.HasProject(projectMock.Object)).Returns(false); 
        assocMock1.Setup(a => a.HasCollaborator(collabMock.Object)).Returns(true); 

        var assoc = new AssociationProjectCollaboratorRepository(associationsProjectCollaborator);

        // Act
        var result = assoc.FindByProjectAndCollaborator(projectMock.Object, collabMock.Object);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void WhenProjectMatchesButCollaboratorDoesNotMatch_ThenReturnNull()
    {
        // Arrange
        Mock<IProject> projectMock = new Mock<IProject>();
        Mock<ICollaborator> collabMock = new Mock<ICollaborator>();

        Mock<IAssociationProjectCollaborator> assocMock1 = new Mock<IAssociationProjectCollaborator>();
        List<IAssociationProjectCollaborator> associationsProjectCollaborator = new List<IAssociationProjectCollaborator> {
            assocMock1.Object,
        };

        assocMock1.Setup(a => a.HasProject(projectMock.Object)).Returns(true); 
        assocMock1.Setup(a => a.HasCollaborator(collabMock.Object)).Returns(false);

        var assoc = new AssociationProjectCollaboratorRepository(associationsProjectCollaborator);

        // Act
        var result = assoc.FindByProjectAndCollaborator(projectMock.Object, collabMock.Object);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void WhenFindingByProjectAndCollaboratorWithExistingParameters_ThenReturnsAssociation()
    {

        //arrange
        var projectDouble = new Mock<IProject>();
        var collaboratorDouble = new Mock<ICollaborator>();
        var associationDouble = new Mock<IAssociationProjectCollaborator>();

        associationDouble.Setup(a => a.HasProject(projectDouble.Object)).Returns(true);
        associationDouble.Setup(a => a.HasCollaborator(collaboratorDouble.Object)).Returns(true);

        var repo = new AssociationProjectCollaboratorRepository(new List<IAssociationProjectCollaborator> { associationDouble.Object });

        //act
        var result = repo.FindByProjectAndCollaborator(projectDouble.Object, collaboratorDouble.Object);

        //assert
        Assert.Equal(associationDouble.Object, result);

    }

    [Theory]
    [InlineData(true, false)]
    [InlineData(false, true)]
    [InlineData(false, false)]
    public void WhenFindingByProjectAndCollaboratorWithNoAssociation_ThenThrowsException(bool first, bool second)
    {
        //arrange
        var projectDouble = new Mock<IProject>();
        var collaboratorDouble = new Mock<ICollaborator>();
        var associationDouble = new Mock<IAssociationProjectCollaborator>();

        associationDouble.Setup(a => a.HasProject(projectDouble.Object)).Returns(first);
        associationDouble.Setup(a => a.HasCollaborator(collaboratorDouble.Object)).Returns(second);

        var repo = new AssociationProjectCollaboratorRepository(new List<IAssociationProjectCollaborator> { associationDouble.Object });

        //act
        var result = repo.FindByProjectAndCollaborator(projectDouble.Object, collaboratorDouble.Object);

        //assert
        Assert.Null(result);
    }

    [Fact]
    public void WhenFindingByProjectAndCollaboratorWithExistingParameters_ThenReturnsAssociation()
    {

        //arrange
        var projectDouble = new Mock<IProject>();
        var collaboratorDouble = new Mock<ICollaborator>();
        var associationDouble = new Mock<IAssociationProjectCollaborator>();

        associationDouble.Setup(a => a.HasProject(projectDouble.Object)).Returns(true);
        associationDouble.Setup(a => a.HasCollaborator(collaboratorDouble.Object)).Returns(true);

        var repo = new AssociationProjectCollaboratorRepository(new List<IAssociationProjectCollaborator> { associationDouble.Object });

        //act
        var result = repo.FindByProjectAndCollaborator(projectDouble.Object, collaboratorDouble.Object);

        //assert
        Assert.Equal(associationDouble.Object, result);

    }

    [Theory]
    [InlineData(true, false)]
    [InlineData(false, true)]
    [InlineData(false, false)]
    public void WhenFindingByProjectAndCollaboratorWithNoAssociation_ThenThrowsException(bool first, bool second)
    {
        //arrange
        var projectDouble = new Mock<IProject>();
        var collaboratorDouble = new Mock<ICollaborator>();
        var associationDouble = new Mock<IAssociationProjectCollaborator>();

        associationDouble.Setup(a => a.HasProject(projectDouble.Object)).Returns(first);
        associationDouble.Setup(a => a.HasCollaborator(collaboratorDouble.Object)).Returns(second);

        var repo = new AssociationProjectCollaboratorRepository(new List<IAssociationProjectCollaborator> { associationDouble.Object });

        //act
        var result = repo.FindByProjectAndCollaborator(projectDouble.Object, collaboratorDouble.Object);

        //assert
        Assert.Null(result);
    }
}