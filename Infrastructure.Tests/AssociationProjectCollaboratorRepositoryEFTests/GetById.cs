using Domain.Interfaces;
using Domain.Visitor;
using Infrastructure;
using Infrastructure.DataModel;
using Infrastructure.Mapper;
using Microsoft.EntityFrameworkCore;
using Moq;

public class GetById
{
    [Fact]
    public void WhenPassingExistingId_ThenReturnCorrectAssociation()
    {
        // Arrange
        // Create mockSet
        var AssocDM1 = new Mock<IAssociationProjectCollaboratorVisitor>();
        var AssocDM2 = new Mock<IAssociationProjectCollaboratorVisitor>();
        var users = new List<AssociationProjectCollaboratorDataModel>
        {
            (AssociationProjectCollaboratorDataModel)AssocDM1.Object,
            (AssociationProjectCollaboratorDataModel)AssocDM2.Object
        }.AsQueryable();

        var mockSet = new Mock<DbSet<AssociationProjectCollaboratorDataModel>>();
        mockSet.As<IQueryable<AssociationProjectCollaboratorDataModel>>().Setup(m => m.Provider).Returns(users.Provider);
        mockSet.As<IQueryable<AssociationProjectCollaboratorDataModel>>().Setup(m => m.Expression).Returns(users.Expression);
        mockSet.As<IQueryable<AssociationProjectCollaboratorDataModel>>().Setup(m => m.ElementType).Returns(users.ElementType);
        mockSet.As<IQueryable<AssociationProjectCollaboratorDataModel>>().Setup(m => m.GetEnumerator()).Returns(users.GetEnumerator());

        // Setup assoc id to be searched
        long assocDM1Id = 1;
        long assocDM2Id = 2;
        AssocDM1.Setup(a => a.Id).Returns(assocDM1Id);
        AssocDM2.Setup(a => a.Id).Returns(assocDM2Id);

        // declare and set up context with associations
        var mockContext = new Mock<IAbsanteeContext>();
        mockContext.Setup(c => c.Associations).Returns(mockSet.Object);

        var mapper = new Mock<AssociationProjectCollaboratorMapper>();

        var assocRepo = new AssociationProjectCollaboratorRepositoryEF((AbsanteeContext)mockContext.Object, mapper.Object);

        // Act
        IAssociationProjectCollaborator? result = assocRepo.GetById(assocDM1Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(assocDM1Id, result.GetId());
    }
}