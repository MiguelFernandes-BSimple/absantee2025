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
    public class TrainingPeriodGetPeriodDateTests
    {
        [Fact]
        public void WhenGettingPeriodDate_ThenReturnsPeriodDate()
        {
            //Arrange
            var periodDate = new Mock<IPeriodDate>();
            var trainingPeriod = new TrainingPeriod(It.IsAny<long>(), periodDate.Object);

            //Act
            var result = trainingPeriod.GetPeriodDate();

            //Assert
            Assert.Equal(periodDate.Object, result);
        }
    }
}
