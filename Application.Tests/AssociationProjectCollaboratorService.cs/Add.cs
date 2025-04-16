using Application.Services;
using Domain.Models;
using Domain.Factory;
using Domain.Interfaces;
using Moq;
using Domain.IRepository;

namespace Application.Tests.AssociationProjectServiceTests;

public class Add
{
    [Fact]
    public async Task WhenAddingValidAssociation_ThenItsAddedSuccessfully()
    {
        // Arrange
        var mockFactory = new Mock<IAssociationProjectCollaboratorFactory>();
        var mockRepository = new Mock<IAssociationProjectCollaboratorRepository>();

        var periodDate = new Mock<IPeriodDate>();
        long collabId = 1;
        long projectId = 2;

        mockFactory
            .Setup(f => f.Create(periodDate.Object, collabId, projectId))
            .ReturnsAsync(It.IsAny<AssociationProjectCollaborator>());

        var service = new AssociationProjectCollaboratorService(mockRepository.Object, mockFactory.Object);

        // Act
        await Task.Run(() => service.Add(periodDate.Object, collabId, projectId));

        // Assert
        mockRepository.Verify(r => r.AddAsync(It.IsAny<AssociationProjectCollaborator>()), Times.Once);
    }

}