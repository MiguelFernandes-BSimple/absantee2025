using Domain.Models;

namespace Domain.Tests.UserTests;

public class DeactivateUser
{
    [Fact]
    public void WhenDeactivatingAnActiveUser_ThenReturnTrue()
    {
        // Arrange 
        User user = new User("John", "Doe", "john.doe@email.com", DateTime.MaxValue);

        // Act
        bool result = user.DeactivateUser();

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void WhenDeactivatingAnAlreadyDeactivatedUser_ThenReturnFalse()
    {
        // Arrange

        User user = new User("John", "Doe", "john.doe@email.com", DateTime.MaxValue);
        user.DeactivateUser();

        // Act
        bool result = user.DeactivateUser();

        // Assert
        Assert.False(result);

    }
}