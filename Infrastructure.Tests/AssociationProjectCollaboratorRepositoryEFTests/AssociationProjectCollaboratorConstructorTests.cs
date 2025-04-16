using Domain.Interfaces;
using Domain.Visitor;
using Infrastructure.Mapper;
using Moq;

namespace Infrastructure.Tests.AssociationProjectCollaboratorRepositoryEFTests;

public class AssociationProjectCollaboratorConstructorTests
{
    [Fact]
    public void WhenPassingCorrectParameters_ThenCreateAssociationRepository()
    {
        // Arrange
        Mock<AbsanteeContext> contextDouble = new Mock<AbsanteeContext>();
        Mock<IMapper<IAssociationProjectCollaborator, IAssociationProjectCollaboratorVisitor>> mapperMock =
             new Mock<IMapper<IAssociationProjectCollaborator, IAssociationProjectCollaboratorVisitor>>();

        // Act
        new AssociationProjectCollaboratorRepositoryEF(contextDouble.Object, mapperMock.Object);

        // Assert
    }
}