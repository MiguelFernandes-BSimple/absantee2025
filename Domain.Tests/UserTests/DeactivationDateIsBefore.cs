using Domain;

namespace Domain.Tests.UserTests;

public class DeactivationDateIsBefore
{
    public static IEnumerable<object[]> GetDeactivationDateAndCompare()
    {
        yield return new object[] { DateTime.Now.AddHours(1), DateTime.Now.AddHours(2) };
        yield return new object[] { DateTime.Now.AddYears(1), DateTime.Now.AddYears(2) };
    }

    [Theory]
    [MemberData(nameof(GetDeactivationDateAndCompare))]
    public void WhenGivenDateIsAfterDeactivationDate_ThenReturnTrue(DateTime deactivationDate, DateTime dateCompare)
    {
        // Arrange
        User user = new User("John", "Doe", "john@email.com", deactivationDate);

        // Act
        bool result = user.DeactivationDateIsBefore(dateCompare);

        // Assert
        Assert.True(result);
    }

    public static IEnumerable<object[]> GetDatesEqualAndBeforeDeactivatonDate()
    {
        yield return new object[] { DateTime.MaxValue };
        yield return new object[] { new DateTime(2045, 4, 1, 1, 1, 1) };
    }
    [Theory]
    [MemberData(nameof(GetDatesEqualAndBeforeDeactivatonDate))]
    public void WhenDeactivationDateIsEqualOrAfter_ThenReturnFalse(DateTime date)
    {
        //arrange
        User user = new User("John", "Doe", "john.doe@email.com", DateTime.MaxValue);

        //act
        bool result = user.DeactivationDateIsBefore(date);

        //assert
        Assert.False(result);
    }
}