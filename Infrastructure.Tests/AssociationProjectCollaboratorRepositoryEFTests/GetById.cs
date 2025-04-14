using Domain.Interfaces;
using Domain.Models;
using Domain.Visitor;
using Infrastructure.DataModel;
using Infrastructure.Mapper;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Infrastructure.Tests.AssociationProjectCollaboratorRepositoryEFTests;

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

        // Declare and set up context with associations
        var mockContext = new Mock<IAbsanteeContext>();
        mockContext.Setup(c => c.Associations).Returns(mockSet.Object);

        AssocDM1.Setup(a => a.Id).Returns(assocDM1Id);
        AssocDM2.Setup(a => a.Id).Returns(assocDM2Id);

        var mapper = new Mock<IMapper<AssociationProjectCollaborator, AssociationProjectCollaboratorDataModel>>();

        // Convert to domain
        Mock<IAssociationProjectCollaborator> expected = new Mock<IAssociationProjectCollaborator>();

        mapper.Setup(m => m.ToDomain((AssociationProjectCollaboratorDataModel)AssocDM2.Object)).Returns((AssociationProjectCollaborator)expected.Object);

        var assocRepo =
            new AssociationProjectCollaboratorRepositoryEF((AbsanteeContext)mockContext.Object, (AssociationProjectCollaboratorMapper)mapper.Object);

        // Act
        IAssociationProjectCollaborator? result = assocRepo.GetById(assocDM2Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expected.Object.GetId(), result.GetId());
    }

    [Fact]
    public void WhenPassingNewId_ThenReturnNull()
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
        long newId = 3;

        // Declare and set up context with associations
        var mockContext = new Mock<IAbsanteeContext>();
        mockContext.Setup(c => c.Associations).Returns(mockSet.Object);

        AssocDM1.Setup(a => a.Id).Returns(assocDM1Id);
        AssocDM2.Setup(a => a.Id).Returns(assocDM2Id);

        var mapper = new Mock<IMapper<AssociationProjectCollaborator, AssociationProjectCollaboratorDataModel>>();

        var assocRepo =
            new AssociationProjectCollaboratorRepositoryEF((AbsanteeContext)mockContext.Object, (AssociationProjectCollaboratorMapper)mapper.Object);

        // Act
        IAssociationProjectCollaborator? result = assocRepo.GetById(newId);

        // Assert
        Assert.Null(result);
    }
}
