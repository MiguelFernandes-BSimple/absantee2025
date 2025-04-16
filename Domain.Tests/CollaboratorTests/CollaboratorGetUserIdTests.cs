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
    public class CollaboratorGetUserIdTests
    {
        [Fact]
        public void WhenGettingId_ThenReturnsId()
        {
            // Arrange
            var userId = 1;
            var collaborator = new Collaborator(It.IsAny<long>(), userId, It.IsAny<IPeriodDateTime>());

            // Act
            var result = collaborator.GetUserId();

            // Assert
            Assert.Equal(userId, result);
        }
    }
}
