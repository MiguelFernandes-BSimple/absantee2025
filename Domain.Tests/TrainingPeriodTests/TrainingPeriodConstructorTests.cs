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
            PeriodDate periodDate = new PeriodDate(new DateOnly(2025, 06, 05), new DateOnly(2025, 06, 10));

            //Act
            TrainingPeriod tperiod = new TrainingPeriod(periodDate);

            //Assert
            Assert.NotNull(tperiod);
        }

        [Fact]
        public void WhenPassingDatesInThePast_ThenThrowsArgumentException()
        {
            // Arrange
            PeriodDate periodDate = new PeriodDate(new DateOnly(2024, 06, 05), new DateOnly(2024, 06, 10));

            // Assert
            ArgumentException exception = Assert.Throws<ArgumentException>(
                () =>
                     //act
                     new TrainingPeriod(periodDate)

            );

            Assert.Equal("Period date cannot start in the past.", exception.Message);
        }

        [Fact]
        public void WhenPassingValidIdAndPeriod_ThenTrainingPeriodIsCreated()
        {
            //Arrange

            //Act
            TrainingPeriod tperiod = new TrainingPeriod(It.IsAny<long>(), It.IsAny<PeriodDate>());

            //Assert
            Assert.NotNull(tperiod);
        }
    }
}
