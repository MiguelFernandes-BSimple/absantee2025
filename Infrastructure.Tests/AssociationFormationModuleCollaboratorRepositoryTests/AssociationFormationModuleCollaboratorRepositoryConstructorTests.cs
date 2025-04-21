using Domain.Interfaces;
using Domain.Visitor;
using Infrastructure.Mapper;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Infrastructure.Tests.AssociationProjectCollaboratorRepositoryEFTests;

public class AssociationFormationModuleCollaboratorConstructorTests
{
    [Fact]
    public void WhenPassingCorrectParameters_ThenCreateAssociationRepository()
    {
        // Arrange
        DbContextOptions<AbsanteeContext> options = new DbContextOptions<AbsanteeContext>();

        Mock<AbsanteeContext> contextDouble = new Mock<AbsanteeContext>(options);
        Mock<IMapper<IAssociationFormationModuleCollaborator, IAssociationFormationModuleCollaboratorVisitor>> mapperMock =
             new Mock<IMapper<IAssociationFormationModuleCollaborator, IAssociationFormationModuleCollaboratorVisitor>>();

        // Act
        AssociationFormationModuleCollaboratorRepository result =
            new AssociationFormationModuleCollaboratorRepository(contextDouble.Object, mapperMock.Object);

        // Assert
        Assert.NotNull(result);
    }
}