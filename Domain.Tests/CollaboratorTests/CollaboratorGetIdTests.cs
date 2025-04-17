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
            var id = 1;
            var collaborator = new Collaborator(id, It.IsAny<long>(), It.IsAny<PeriodDateTime>());

            // Act
            var result = collaborator.GetId();

            // Assert
            Assert.Equal(id, result);
        }
    }
}
