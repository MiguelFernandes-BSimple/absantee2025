using Domain;

namespace Domain.Tests;

public class HasSurnames
{
    [Theory]
    [InlineData("FirstSurname SecondSurname")]
    [InlineData("FirstSurname")]
    [InlineData("SecondSurname")]
    [InlineData("First")]
    [InlineData("Name")]
    [InlineData("Sec")]
    [InlineData("second")]
    public void WhenHasSurnamesGetsACorrectSurname_ReturnTrue(string surnameToSearch)
    {
        //Arrange
        string names = "FirstName SecondName";
        string surnames = "FirstSurname SecondSurname";
        string email = "email@domain.pt";
        DateTime deactivationDate = DateTime.Now.AddYears(1);

        //User instance
        User user = new User(names, surnames, email, deactivationDate);

        //Assert
        Assert.True(
            //Act
            user.HasSurnames(surnameToSearch)
        );
    }

    [Theory]
    [InlineData("")]
    [InlineData("FirstSurnames")]
    [InlineData("FirstSurname SecondSurnames")]
    public void WhenHasSurnamesGetsWrongName_ReturnFalse(string surnameToSearch)
    {
        //Arrange
        string names = "FirstName SecondName";
        string surnames = "FirstSurname SecondSurname";
        string email = "email@domain.pt";
        DateTime deactivationDate = DateTime.Now.AddYears(1);

        //User instance
        User user = new User(names, surnames, email, deactivationDate);

        //Assert
        Assert.False(
            //Act
            user.HasSurnames(surnameToSearch)
        );
    }
}