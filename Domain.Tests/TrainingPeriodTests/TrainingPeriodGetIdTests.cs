using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.Models;
using Moq;

namespace Domain.Tests.TrainingPeriodTests
{
    public class TrainingPeriodGetIdTests
    {
        [Fact]
        public void WhenGettingId_ThenReturnsId()
        {
            //Arrange
            var id = 1;
            var trainingPeriod = new TrainingPeriod(id, It.IsAny<IPeriodDate>());

            //Act
            var result = trainingPeriod.GetId();

            //Assert
            Assert.Equal(id, result);
        }
    }
}
