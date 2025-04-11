using Domain.Interfaces;
using Domain.Models;
using Infrastructure.DataModel;
using Infrastructure.Mapper;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Infrastructure.Tests.AssociationProjectCollaboratorRepositoryEFTests;

public class FindAllByProjectAsync
{
    // [Fact]
    // public async Task WhenPassingExistingProjectId_ThenReturnAssociationList()
    // {
    //     // Arrange
    //     Mock<DbContext> context = new Mock<DbContext>();
    //     Mock<AssociationProjectCollaboratorMapper> mapper = new Mock<AssociationProjectCollaboratorMapper>(null, null);

    //     Mock<IAssociationProjectCollaborator> assoc = new Mock<IAssociationProjectCollaborator>();
    //     Mock<AssociationProjectCollaboratorDataModel> dataModel = new Mock<AssociationProjectCollaboratorDataModel>();

    //     var mockDbSet = new Mock<DbSet<AssociationProjectCollaboratorDataModel>>();
    //     context.Setup(c => c.Set<AssociationProjectCollaboratorDataModel>()).Returns(mockDbSet.Object);



    //     var assocsDMList = new List<AssociationProjectCollaboratorDataModel> { dataModel.Object };
    //     var assocsList = new List<AssociationProjectCollaborator> { assoc.Object };

    //     mapper.Setup(m => m.ToDomain(assocsDMList)).Returns(assocsList);

    //     var repository = new AssociationProjectCollaboratorRepositoryEF(context.Object, mapper.Object);

    //     // Act
    //     var result = await repository.FindAllByProjectAsync();

    //     // Assert
    //     Assert.True(result);
    //     mockDbSet.Verify(m => m.Add(It.IsAny<AssociationProjectCollaboratorDataModel>()), Times.Once);
    //     context.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    // }
}