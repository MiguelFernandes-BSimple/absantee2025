using Domain.Models;
using Moq;

namespace Domain.Tests.CollaboratorTests
{
    public class CollaboratorGetIdTests
    {
        [Fact]
        public void WhenGettingId_ThenReturnsId()
        {
            // Arrange
            Guid id = new Guid();
            var collaborator = new Collaborator(id, It.IsAny<Guid>(), It.IsAny<PeriodDateTime>());

            // Act
            var result = collaborator.Id;

            // Assert
            Assert.Equal(id, result);
        }
    }
}
