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
            var userId = 1;
            var collaborator = new Collaborator(It.IsAny<long>(), userId, It.IsAny<PeriodDateTime>());

            // Act
            var result = collaborator.GetUserId();

            // Assert
            Assert.Equal(userId, result);
        }
    }
}
