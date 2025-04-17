using Domain.Interfaces;
using Domain.Models;
using Moq;

namespace Domain.Tests.UserTests
{
    public class UserSetIdTests
    {
        [Fact]
        public void WhenPassingCorrectValue_ThenSetsId()
        {
            // arrange
            var id = 5;
            var user = new User(1, "John", "Doe", "john@email.com ", It.IsAny<PeriodDateTime>());

            // act
            user.SetId(id);
            var result = user.GetId();

            // assert
            Assert.Equal(result, id);
        }
    }
}