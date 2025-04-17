using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;
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
            var periodDateTime = new Mock<PeriodDateTime>();
            var collaborator = new Collaborator(It.IsAny<long>(), It.IsAny<long>(), periodDateTime.Object);

            // Act
            var result = collaborator._periodDateTime;

            // Assert
            Assert.Equal(periodDateTime.Object, result);
        }
    }
}
