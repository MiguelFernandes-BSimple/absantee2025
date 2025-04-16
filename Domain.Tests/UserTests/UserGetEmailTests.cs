using Domain.Interfaces;
using Domain.Models;
using Moq;

public class UserGetEmailTests
{
    [Fact]
    public void WhenGettingEmail_ThenReturnsEmail()
    {
        //arrange
        var email = "john@email.com";
        var user = new User(1, "John", "Doe", email, It.IsAny<IPeriodDateTime>());

        //act
        var userEmail = user.GetEmail();

        //assert
        Assert.Equal(email, userEmail);
    }
}