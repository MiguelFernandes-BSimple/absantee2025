using Application.Services;
using Domain.Models;
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
        Mock<IAssociationProjectCollaboratorFactory> apcFactory = new Mock<IAssociationProjectCollaboratorFactory>();
        apcFactory.Setup(apcFactory => apcFactory.Create(It.IsAny<IPeriodDate>(), It.IsAny<long>(), It.IsAny<long>())).ReturnsAsync(It.IsAny<AssociationProjectCollaborator>());

        AssociationProjectCollaboratorService service =
            new AssociationProjectCollaboratorService(It.IsAny<AssociationProjectCollaboratorRepositoryEF>(), apcFactory.Object);

        // Act
        service.Add(It.IsAny<IPeriodDate>(), It.IsAny<long>(), It.IsAny<long>());

        // Assert
    }

}