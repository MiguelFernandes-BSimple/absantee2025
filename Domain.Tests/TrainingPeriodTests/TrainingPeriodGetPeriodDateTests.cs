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
            PeriodDate periodDate = new PeriodDate(It.IsAny<DateOnly>(), It.IsAny<DateOnly>());
            var trainingPeriod = new TrainingPeriod(It.IsAny<long>(), periodDate);

            //Act
            var result = trainingPeriod.GetPeriodDate();

            //Assert
            Assert.Equal(periodDate, result);
        }
    }
}
