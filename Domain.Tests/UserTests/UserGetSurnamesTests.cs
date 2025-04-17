using Domain.Models;
using Moq;

namespace Domain.Tests.UserTests;

public class UserGetSurnamesTests
{
    [Fact]
    public void WhenGettingSurnames_ThenReturnsSurnames()
    {
        //arrange
        var surname = "Doe";
        var user = new User(1, "John", surname, "john@email.com", It.IsAny<PeriodDateTime>());

        //act
        var userSurname = user.GetSurnames();

        //assert
        Assert.Equal(surname, userSurname);
    }
}