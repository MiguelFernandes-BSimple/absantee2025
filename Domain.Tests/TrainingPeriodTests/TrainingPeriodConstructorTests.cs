using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Factory.TrainingPeriodFactory;
using Domain.Interfaces;
using Domain.Models;
using Moq;

namespace Domain.Tests.TrainingPeriodTests
{
    public class TrainingPeriodConstructorTests
    {
        [Fact]
        public void WhenPassingValidDates_ThenTrainingPeriodIsCreated()
        {
            //Arrange
            Mock<PeriodDate> periodDate = new Mock<PeriodDate>();

            periodDate.Setup(pd => pd.IsInitDateSmallerThan(DateOnly.FromDateTime(DateTime.Now))).Returns(false);

            //Act
            new TrainingPeriod(periodDate.Object);

            //Assert
        }

        [Fact]
        public void WhenPassingDatesInThePast_ThenThrowsArgumentException()
        {
            // Arrange
            Mock<PeriodDate> periodDate = new Mock<PeriodDate>();

            periodDate.Setup(pd => pd.IsInitDateSmallerThan(DateOnly.FromDateTime(DateTime.Now))).Returns(true);


            // Assert
            ArgumentException exception = Assert.Throws<ArgumentException>(
                () =>
                    //act
                    new TrainingPeriod(periodDate.Object)

            );

            Assert.Equal("Period date cannot start in the past.", exception.Message);
        }

        [Fact]
        public void WhenPassingValidIdAndPeriod_ThenTrainingPeriodIsCreated()
        {
            //Arrange

            //Act
            new TrainingPeriod(It.IsAny<long>(), It.IsAny<PeriodDate>());

            //Assert
        }
    }
}
