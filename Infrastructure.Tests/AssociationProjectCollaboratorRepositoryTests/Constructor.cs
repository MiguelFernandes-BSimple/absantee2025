using Moq;
using Domain.Interfaces;
using Infrastructure.Repositories;
using System;

namespace Infrastructure.Tests.AssociationProjectCollaboratorRepositoryTests;

public class Constructor
{
    [Fact]
    public void WhenPassingListWithOneElement_ThenObjectIsCreated()
    {
        //arrange
        Mock<IAssociationProjectCollaborator> assocMock = new Mock<IAssociationProjectCollaborator>();
        List<IAssociationProjectCollaborator> associationsProjectCollaborator = new List<IAssociationProjectCollaborator> { assocMock.Object };

        //act
        new AssociationProjectCollaboratorRepository(associationsProjectCollaborator);

        //assert
    }

    [Fact]
    public void WhenPassingListWithTwoDifferentElements_ThenObjectIsCreated()
    {
        //arrange
        Mock<IAssociationProjectCollaborator> assocMock1 = new Mock<IAssociationProjectCollaborator>();
        Mock<IAssociationProjectCollaborator> assocMock2 = new Mock<IAssociationProjectCollaborator>();
        List<IAssociationProjectCollaborator> associationsProjectCollaborator = new List<IAssociationProjectCollaborator> 
        { 
            assocMock1.Object,
            assocMock2.Object
        };

        assocMock2.Setup(a => a.Equals(assocMock1.Object)).Returns(false);

        //act
        new AssociationProjectCollaboratorRepository(associationsProjectCollaborator);

        //assert
    }

    [Fact]
    public void WhenPassingListWithTwoEqualElements_ThenThrowException()
    {
        //arrange
        Mock<IAssociationProjectCollaborator> assocMock1 = new Mock<IAssociationProjectCollaborator>();
        Mock<IAssociationProjectCollaborator> assocMock2 = new Mock<IAssociationProjectCollaborator>();
        Mock<IAssociationProjectCollaborator> assocMock3 = new Mock<IAssociationProjectCollaborator>();
        List<IAssociationProjectCollaborator> associationsProjectCollaborator = new List<IAssociationProjectCollaborator>
        {
            assocMock1.Object,
            assocMock2.Object,
            assocMock3.Object,
        };

        assocMock2.Setup(a => a.Equals(assocMock1.Object)).Returns(false);
        assocMock3.Setup(a => a.Equals(assocMock1.Object)).Returns(true);

        //assert
        var exception = Assert.Throws<ArgumentException>(() =>
            //act
            new AssociationProjectCollaboratorRepository(associationsProjectCollaborator)
        );

        Assert.Equal("Invalid Arguments", exception.Message);
    }
}
