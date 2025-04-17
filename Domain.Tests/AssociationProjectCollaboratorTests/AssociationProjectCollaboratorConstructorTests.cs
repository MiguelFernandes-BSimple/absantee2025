using Domain.Interfaces;
using Domain.Models;
using Moq;

namespace Domain.Tests.AssociationProjectCollaboratorTests;

public class AssociationProjectCollaboratorConstructorTests
{
    [Fact]
    public void WhenPassingArguments_ThenClassIsInstatiated()
    {
        // Arrange
        Mock<PeriodDate> periodDate = new Mock<PeriodDate>();

        // Act
        var result = new AssociationProjectCollaborator(It.IsAny<long>(), It.IsAny<long>(), periodDate.Object);

        // Assert
        Assert.NotNull(result);
    }
}