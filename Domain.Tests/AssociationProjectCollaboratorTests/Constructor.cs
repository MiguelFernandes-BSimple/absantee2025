using Domain.Interfaces;
using Domain.Models;
using Moq;

namespace Domain.Tests.AssociationProjectCollaboratorTests;

public class Constructor
{
    [Fact]
    public void WhenPassingArguments_ThenClassIsInstatiated()
    {
        // Arrange
        Mock<IPeriodDate> periodDate = new Mock<IPeriodDate>();

        // Act
        new AssociationProjectCollaborator(It.IsAny<long>(), It.IsAny<long>(), periodDate.Object);

        // Assert
    }
}