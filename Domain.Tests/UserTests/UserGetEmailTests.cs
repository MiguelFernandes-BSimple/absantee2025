using Domain.Models;
using Moq;

namespace Domain.Tests.UserTests;

public class UserGetEmailTests
{
    [Fact]
    public void WhenGettingEmail_ThenReturnsEmail()
    {
        //arrange
        var email = "john@email.com";
        var user = new User(1, "John", "Doe", email, It.IsAny<PeriodDateTime>());

        //act
        var userEmail = user.GetEmail();

        //assert
        Assert.Equal(email, userEmail);
    }
}