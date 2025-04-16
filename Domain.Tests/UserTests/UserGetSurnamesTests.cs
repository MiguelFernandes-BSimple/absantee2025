using Domain.Interfaces;
using Domain.Models;
using Moq;

public class UserGetSurnamesTests
{
    [Fact]
    public void WhenGettingSurnames_ThenReturnsSurnames()
    {
        //arrange
        var surname = "Doe";
        var user = new User(1, "John", surname, "john@email.com", It.IsAny<IPeriodDateTime>());

        //act
        var userSurname = user.GetSurnames();

        //assert
        Assert.Equal(surname, userSurname);
    }
}