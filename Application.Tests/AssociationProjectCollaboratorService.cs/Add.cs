using Application.Services;
using Domain.Factory;
using Domain.Interfaces;
using Moq;

namespace Application.Tests.AssociationProjectServiceTests;

public class Add
{
    [Fact]
    public void WhenAddingValidAssociation_ThenItsAddedSuccessfully()
    {
        // Arrange
        AssociationProjectCollaboratorService service =
            new AssociationProjectCollaboratorService(It.IsAny<AssociationProjectCollaboratorRepositoryEF>(), It.IsAny<AssociationProjectCollaboratorFactory>());

        // Act
        service.Add(It.IsAny<IPeriodDate>(), It.IsAny<long>(), It.IsAny<long>());

        // Assert
    }

}