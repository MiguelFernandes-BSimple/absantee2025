using Infrastructure.Mapper;
using Moq;

namespace Infrastructure.Repositories.AssociationProjectCollaboratorRepositoryEFTests;

public class Constructor
{
    [Fact]
    public void WhenPassingCorrectParameters_ThenCreateAssociationRepository()
    {
        // Arrange
        Mock<AbsanteeContext> contextDouble = new Mock<AbsanteeContext>();
        Mock<AssociationProjectCollaboratorMapper> mapperMock = new Mock<AssociationProjectCollaboratorMapper>();

        // Act
        new AssociationProjectCollaboratorRepositoryEF(contextDouble.Object, mapperMock.Object);

        // Assert
    }
}