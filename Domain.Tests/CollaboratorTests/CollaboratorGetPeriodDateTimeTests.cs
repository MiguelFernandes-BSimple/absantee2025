using Domain.Models;
using Moq;

namespace Domain.Tests.CollaboratorTests
{
    public class CollaboratorGetPeriodDateTimeTests
    {
        [Fact]
        public void WhenGettingId_ThenReturnsId()
        {
            // Arrange
            PeriodDateTime periodDateTime = new PeriodDateTime(It.IsAny<DateTime>(), It.IsAny<DateTime>());
            Collaborator collaborator = new Collaborator(It.IsAny<long>(), It.IsAny<long>(), periodDateTime);

            // Act
            var result = collaborator._periodDateTime;

            // Assert
            Assert.Equal(periodDateTime, result);
        }
    }
}
