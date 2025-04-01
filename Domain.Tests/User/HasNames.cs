using Domain;

namespace Domain.Tests;

public class HasNames
{
    [Theory]
    [InlineData("FirstName SecondName")]
    [InlineData("FirstName")]
    [InlineData("SecondName")]
    [InlineData("First")]
    [InlineData("name")]
    [InlineData("Sec")]
    [InlineData("second")]
    public void WhenHasNamesGetsCorrectName_ReturnTrue(string nameToSearch)
    {
        // Arrange
        string names = "FirstName SecondName";
        string surnames = "Surnames";
        string email = "email@domain.pt";
        DateTime deactivationDate = DateTime.Now.AddYears(1);

        // Create User instance
        User user = new User(names, surnames, email, deactivationDate);

        // Assert
        Assert.True(
            // Act
            user.HasNames(nameToSearch)
        );
    }

    [Theory]
    [InlineData("")]
    [InlineData("FirstNames")]
    [InlineData("FirstName SecondNames")]
    public void WhenHasNamesGetsWrongName_ReturnFalse(string nameToSearch)
    {
        //Arrange
        string names = "FirstName SecondName";
        string surnames = "Surnames";
        string email = "email@domain.pt";
        DateTime deactivationDate = DateTime.Now.AddYears(1);

        //User instance
        User user = new User(names, surnames, email, deactivationDate);

        //Assert
        Assert.False(
            //Act
            user.HasNames(nameToSearch)
        );
    }
}