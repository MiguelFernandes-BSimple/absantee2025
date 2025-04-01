using Domain.Models;

namespace Domain.Tests.UserTests;

public class IsDeactivated
{

    public static IEnumerable<object[]> GetDeactivationDate()
    {
        yield return new object[] { DateTime.Now.AddHours(1) };
        yield return new object[] { DateTime.Now.AddYears(1) };
    }

    [Theory]
    [MemberData(nameof(GetDeactivationDate))]
    public void WhenCurrentDateIsBeforeDeactivationDate_ThenReturnFalse(DateTime deactivationDate)
    {
        // Arrange
        User user = new User("John", "Doe", "john@email.com", deactivationDate);

        // Act
        bool result = user.IsDeactivated();

        // Assert
        Assert.False(result);
    }
}