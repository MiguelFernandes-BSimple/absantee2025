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
            var periodDateTime = new Mock<PeriodDateTime>();
            var user = new User(It.IsAny<long>(), "John", "Doe", "john@email.com", periodDateTime.Object);
            //act
            var userPeriodDateTime = user.GetPeriodDateTime();

            //assert
            Assert.Equal(periodDateTime.Object, userPeriodDateTime);
        }
    }
}