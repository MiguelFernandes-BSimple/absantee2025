using Domain.Models;
using Moq;

namespace Domain.Tests.CollaboratorTests
{
    public class CollaboratorGetUserIdTests
    {
        [Fact]
        public void WhenGettingId_ThenReturnsId()
        {
            // Arrange
            Guid userId = new Guid();
            var collaborator = new Collaborator(It.IsAny<Guid>(), userId, It.IsAny<PeriodDateTime>());

            // Act
            var result = collaborator.Id;

            // Assert
            Assert.Equal(userId, result);
        }
    }
}
