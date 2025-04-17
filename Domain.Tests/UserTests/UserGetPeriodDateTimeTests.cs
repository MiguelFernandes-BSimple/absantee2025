using Domain.Interfaces;
using Domain.Models;
using Moq;

namespace Domain.Tests.UserTests
{
    public class UserGetPeriodDateTimeTests
    {
        [Fact]
        public void WhenGettingPeriodDateTime_ThenReturnsPeriodDateTime()
        {
            //arrange
            PeriodDateTime periodDateTime = new PeriodDateTime(It.IsAny<DateTime>(), It.IsAny<DateTime>());
            var user = new User(It.IsAny<long>(), "John", "Doe", "john@email.com", periodDateTime);
            //act
            var userPeriodDateTime = user._periodDateTime;

            //assert
            Assert.Equal(periodDateTime, userPeriodDateTime);
        }
    }
}