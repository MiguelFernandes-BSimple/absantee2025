using Domain.Models;
using Moq;

namespace Domain.Tests.HRManagerTests
{
    public class HRManagerGetPeriodDateTimeTests
    {
        [Fact]
        public void WhenGettingPeriodDateTime_ThenReturnsPeriodDateTime()
        {
            //arrange
            PeriodDateTime periodDateTime = new PeriodDateTime(It.IsAny<DateTime>(), It.IsAny<DateTime>());
            var hrManager = new HRManager(It.IsAny<long>(), It.IsAny<long>(), periodDateTime);
            //act
            var hrPeriodDateTime = hrManager._periodDateTime;

            //assert
            Assert.Equal(periodDateTime, hrPeriodDateTime);
        }
    }
}
