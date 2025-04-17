using Domain.Models;
using Moq;

namespace Domain.Tests.CollaboratorTests
{
    public class CollaboratorConstructorTests
    {
        [Fact]
        public void WhenCreatingCollaboratorWithValidUserIdAndPeriod_ThenCollaboratorIsCreatedCorrectly()
        {
            // Arrange

            // Act
            Collaborator collab = new Collaborator(It.IsAny<long>(), It.IsAny<PeriodDateTime>());

            // Assert
            Assert.NotNull(collab);
        }

        [Fact]
        public void WhenCreatingCollaboratorWithValidIdAndUserIdAndPeriod_ThenCollaboratorIsCreatedCorrectly()
        {
            // Arrange

            // Act
            Collaborator collab = new Collaborator(It.IsAny<long>(), It.IsAny<long>(), It.IsAny<PeriodDateTime>());

            // Assert
            Assert.NotNull(collab);
        }
    }
}
