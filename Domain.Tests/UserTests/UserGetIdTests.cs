using Domain.Models;
using Moq;

namespace Domain.Tests.UserTests;

public class UserGetIdTests
{
    [Fact]
    public void WhenGettingId_ThenReturnsId()
    {
        //arrange
        var id = 1;
        var user = new User(id, "John", "Doe", "john@email.com", It.IsAny<PeriodDateTime>());

        //act
        var userId = user.GetId();

        //assert
        Assert.Equal(id, userId);
    }
}