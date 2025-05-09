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
            Collaborator collab = new Collaborator(It.IsAny<Guid>(), It.IsAny<PeriodDateTime>());

            // Assert
            Assert.NotNull(collab);
        }

        [Fact]
        public void WhenCreatingCollaboratorWithValidIdAndUserIdAndPeriod_ThenCollaboratorIsCreatedCorrectly()
        {
            // Arrange

            // Act
            Collaborator collab = new Collaborator(It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<PeriodDateTime>());

            // Assert
            Assert.NotNull(collab);
        }
    }
}
