using Domain.Interfaces;
using Infrastructure.DataModel;
using Infrastructure.Mapper;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Infrastructure.Tests.AssociationProjectCollaboratorRepositoryEFTests;

public class AddAsync
{
    [Fact]
    public async Task WhenAddingDifferentAssociation_ThenReturnTrue()
    {
        // Arrange
        var mockContext = new Mock<DbContext>();
        var mockMapper = new Mock<AssociationProjectCollaboratorMapper>(null, null);

        var assoc = new Mock<IAssociationProjectCollaborator>();
        var dataModel = new Mock<AssociationProjectCollaboratorDataModel>();

        mockMapper.Setup(m => m.ToDataModel(assoc.Object)).Returns(dataModel.Object);

        var mockDbSet = new Mock<DbSet<AssociationProjectCollaboratorDataModel>>();
        mockContext.Setup(c => c.Set<AssociationProjectCollaboratorDataModel>()).Returns(mockDbSet.Object);

        // Set up the SaveChangesAsync method to return a completed task
        mockContext.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        var repository = new AssociationProjectCollaboratorRepositoryEF(mockContext.Object, mockMapper.Object);

        // Act
        var result = await repository.AddAsync(assoc.Object);

        // Assert
        Assert.True(result);
        mockDbSet.Verify(m => m.Add(It.IsAny<AssociationProjectCollaboratorDataModel>()), Times.Once);
        mockContext.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task WhenAddingExistingAssociation_ThenThrowArgumentException()
    {
        // Arrange
        var mockContext = new Mock<DbContext>();
        var mockMapper = new Mock<AssociationProjectCollaboratorMapper>(null, null);
        var collaborator = new Mock<IAssociationProjectCollaborator>();

        var repository = new AssociationProjectCollaboratorRepositoryEF(mockContext.Object, mockMapper.Object);

        // Set up SaveChangesAsync to throw an exception
        mockContext.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>())).ThrowsAsync(new System.Exception("DB error"));

        // Act
        var result = await repository.AddAsync(collaborator.Object);

        // Assert
        Assert.False(result);
    }
}