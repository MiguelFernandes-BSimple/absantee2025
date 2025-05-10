/* using Application.Services;
using Domain.Models;
using Domain.Factory;
using Domain.Interfaces;
using Moq;
using Domain.IRepository;

namespace Application.Tests.AssociationProjectServiceTests;

public class AssociationProjectCollaboratorServiceAdd
{
    [Fact]
    public async Task WhenAddingValidAssociation_ThenItsAddedSuccessfully()
    {
        // Arrange
        var mockFactory = new Mock<IAssociationProjectCollaboratorFactory>();
        var mockRepository = new Mock<IAssociationProjectCollaboratorRepository>();

        long collabId = 1;
        long projectId = 2;

        mockFactory
            .Setup(f => f.Create(It.IsAny<PeriodDate>(), collabId, projectId))
            .ReturnsAsync(It.IsAny<AssociationProjectCollaborator>());

        var service = new AssociationProjectCollaboratorService(mockRepository.Object, mockFactory.Object);

        // Act
        await Task.Run(() => service.Add(It.IsAny<PeriodDate>(), collabId, projectId));

        // Assert
        mockRepository.Verify(r => r.AddAsync(It.IsAny<AssociationProjectCollaborator>()), Times.Once);
    }

} */