using Domain;

<<<<<<< Updated upstream
public class UserTest{
    public static IEnumerable<object[]> GetUserData_ValidFields()
    {
        yield return new object[] { "John", "Doe", "john@email.com", DateTime.Now.AddDays(1) };
        yield return new object[] { "John", "Doe", "john.doe@email.com", null! };
        yield return new object[] { "John Peter", "Doe", "john.doe.13@email.com",  DateTime.Now.AddYears(1) };
        yield return new object[] { "John", "Wallace Doe", "john.doe@company.com.pt",  DateTime.Now.AddYears(2) };
    }

    [Theory]
    [MemberData(nameof(GetUserData_ValidFields))]
    public void WhenCreatingUserWithValidFields_ThenNoExceptionIsThrown(string firstName, string lastName, string email, DateTime? deactivationDate){
        // Arrange & Act
        new User(firstName, lastName, email, deactivationDate);

        // Assert - No exception should be thrown
    }

    public static IEnumerable<object[]> GetUserData_InvalidFirstNames()
    {
        yield return new object[] { new string('a', 51), "Doe", "john@email.com", DateTime.Now.AddDays(1) };
        yield return new object[] { "", "Doe", "john@email.com", DateTime.Now.AddDays(1) };
        yield return new object[] { "John 13", "Doe", "john@email.com", null! };
    }

    [Theory]
    [MemberData(nameof(GetUserData_InvalidFirstNames))]
    public void WhenCreatingUserWithInvalidFirstName_ThenThrowsArgumentException(string firstName, string lastName, string email, DateTime? deactivationDate){
        // Assert
        ArgumentException exception = Assert.Throws<ArgumentException>(() => 
            // Act
            new User(firstName, lastName, email, deactivationDate));
=======
public class UtilizadorTest{
    public static IEnumerable<object[]> GetUserData_CamposValidos()
    {
        yield return new object[] { "Jonh", "Doe", "jonh@email.com", DateTime.Now.AddDays(1) };
        yield return new object[] { "Jonh", "Doe", "jonh.doe@email.com", null! };
        yield return new object[] { "Jonh Peter", "Doe", "jonh.doe.13@email.com",  DateTime.Now.AddYears(1) };
        yield return new object[] { "Jonh", "Wallace Doe", "jonh.doe@company.com.pt",  DateTime.Now.AddYears(2) };
    }

    [Theory]
    [MemberData(nameof(GetUserData_CamposValidos))]
    public void CriarUtilizador_CamposValidos(string nomes, string apelidos, string email, DateTime? dataDesativacao){
        

        //act
        new User(nomes, apelidos, email, dataDesativacao);

        //assert
    }

    public static IEnumerable<object[]> GetUserData_NomesInvalidos()
    {
        yield return new object[] { new string('a', 51), "Doe", "jonh@email.com", DateTime.Now.AddDays(1) };
        yield return new object[] { "", "Doe", "jonh@email.com", DateTime.Now.AddDays(1) };
        yield return new object[] { "Jonh 13", "Doe", "jonh@email.com", null! };
    }

    [Theory]
    [MemberData(nameof(GetUserData_NomesInvalidos))]
    public void CriarUtilizador_NomesInvalidos_Exception(string nomes, string apelidos, string email, DateTime? dataDesativacao){
        

        //assert
        ArgumentException exception = Assert.Throws<ArgumentException>(() => 
            // act
            new User(nomes, apelidos, email, dataDesativacao));
>>>>>>> Stashed changes

        Assert.Equal("Invalid Arguments", exception.Message);
    }

<<<<<<< Updated upstream
    public static IEnumerable<object[]> GetUserData_InvalidLastNames()
    {
        yield return new object[] { "John", new string('a', 51), "john@email.com", DateTime.Now.AddDays(1) };
        yield return new object[] { "John", "Doe 13", "john@email.com", null! };
    }

    [Theory]
    [MemberData(nameof(GetUserData_InvalidLastNames))]
    public void WhenCreatingUserWithInvalidLastName_ThenThrowsArgumentException(string firstName, string lastName, string email, DateTime? deactivationDate){
        // Assert
        ArgumentException exception = Assert.Throws<ArgumentException>(() => 
            // Act
            new User(firstName, lastName, email, deactivationDate));
=======
    public static IEnumerable<object[]> GetUserData_ApelidosInvalidos()
    {
        yield return new object[] { "Jonh", new string('a', 51), "jonh@email.com", DateTime.Now.AddDays(1) };
        yield return new object[] { "Jonh", "Doe 13", "jonh@email.com", null! };
    }

    [Theory]
    [MemberData(nameof(GetUserData_ApelidosInvalidos))]
    public void CriarUtilizador_ApelidosInvalidos_Exception(string nomes, string apelidos, string email, DateTime? dataDesativacao){
        

        //assert
        ArgumentException exception = Assert.Throws<ArgumentException>(() => 
            // act
            new User(nomes, apelidos, email, dataDesativacao));
>>>>>>> Stashed changes

        Assert.Equal("Invalid Arguments", exception.Message);
    }

<<<<<<< Updated upstream
    public static IEnumerable<object[]> GetUserData_InvalidDeactivationDate()
    {
        yield return new object[] { "John", "Doe", "john@email.com", DateTime.Now.AddDays(-1) };
    }

    [Theory]
    [MemberData(nameof(GetUserData_InvalidDeactivationDate))]
    public void WhenCreatingUserWithPastDeactivationDate_ThenThrowsArgumentException(string firstName, string lastName, string email, DateTime? deactivationDate){
        // Assert
        ArgumentException exception = Assert.Throws<ArgumentException>(() => 
            // Act
            new User(firstName, lastName, email, deactivationDate));
=======
    public static IEnumerable<object[]> GetUserData_DataDesativacaoInvalida()
    {
        yield return new object[] { "Jonh", "Doe", "jonh@email.com", DateTime.Now.AddDays(-1) };
    }

    [Theory]
    [MemberData(nameof(GetUserData_DataDesativacaoInvalida))]
    public void CriarUtilizador_DatasDesativacao_Exception(string nomes, string apelidos, string email, DateTime? dataDesativacao){
       

        //assert
        ArgumentException exception = Assert.Throws<ArgumentException>(() => 
            // act
            new User(nomes, apelidos, email, dataDesativacao));
>>>>>>> Stashed changes

        Assert.Equal("Invalid Arguments", exception.Message);
    }

<<<<<<< Updated upstream
    public static IEnumerable<object[]> GetUserData_InvalidFields()
    {
        yield return new object[] { "John 13", "Doe 13", "john@email.com", DateTime.Now.AddDays(1) };
        yield return new object[] { "", "Doe", "", DateTime.Now.AddDays(1) };
        yield return new object[] { "John", "", "john@email.com.", DateTime.Now.AddDays(1) };
        yield return new object[] { "", "Doe", "john@email.com", DateTime.Now.AddDays(-1) };
    }

    [Theory]
    [MemberData(nameof(GetUserData_InvalidFields))]
    public void WhenCreatingUserWithInvalidFields_ThenThrowsArgumentException(string firstName, string lastName, string email, DateTime? deactivationDate){
        // Assert
        ArgumentException exception = Assert.Throws<ArgumentException>(() => 
            // Act
            new User(firstName, lastName, email, deactivationDate));
=======
    public static IEnumerable<object[]> GetUserData_CamposInvalidos()
    {
        yield return new object[] { "Jonh 13", "Doe 13", "jonh@email.com", DateTime.Now.AddDays(1) };
        yield return new object[] { "", "Doe", "", DateTime.Now.AddDays(1) };
        yield return new object[] { "Jonh", "", "jonh@email.com.", DateTime.Now.AddDays(1) };
        yield return new object[] { "", "Doe", "jonh@email.com", DateTime.Now.AddDays(-1) };
    }

    [Theory]
    [MemberData(nameof(GetUserData_CamposInvalidos))]
    public void CriarUtilizador_CamposInvalidos_Exception(string nomes, string apelidos, string email, DateTime? dataDesativacao){
        

        //assert
        ArgumentException exception = Assert.Throws<ArgumentException>(() => 
            // act
            new User(nomes, apelidos, email, dataDesativacao));
>>>>>>> Stashed changes

        Assert.Equal("Invalid Arguments", exception.Message);
    }

<<<<<<< Updated upstream
    public static IEnumerable<object[]> GetDeactivationDate()
=======

    public static IEnumerable<object[]> GetDataDesativacao()
>>>>>>> Stashed changes
    {
        yield return new object[] { DateTime.Now.AddHours(1) };
        yield return new object[] { DateTime.Now.AddYears(1) };
    }

    [Theory]
<<<<<<< Updated upstream
    [MemberData(nameof(GetDeactivationDate))]
    public void WhenCurrentDateIsBeforeDeactivationDate_ThenReturnFalse(DateTime deactivationDate){
        // Arrange
        User user = new User("John", "Doe", "john@email.com", deactivationDate);
        
        // Act
        bool result = user.IsDeactivated();
            
        // Assert
        Assert.False(result);
    }

    public static IEnumerable<object[]> GetDeactivationDateAndCompare()
=======
    [MemberData(nameof(GetDataDesativacao))]
    public void IsDesativo_ReturnFalse(DateTime dataDesativacao){
        //arrange
        User utilizador = new User("Jonh", "Doe", "jonh@email.com", dataDesativacao);
        
        //act
        bool result = utilizador.IsDeactivated();
            
        //assert
        Assert.False(result);
    }


    public static IEnumerable<object[]> GetDataDesativacaoAndDate()
>>>>>>> Stashed changes
    {
        yield return new object[] { DateTime.Now.AddHours(1), DateTime.Now.AddHours(2) };
        yield return new object[] { DateTime.Now.AddYears(1), DateTime.Now.AddYears(2) };
    }

    [Theory]
<<<<<<< Updated upstream
    [MemberData(nameof(GetDeactivationDateAndCompare))]
    public void WhenGivenDateIsAfterDeactivationDate_ThenReturnTrue(DateTime deactivationDate, DateTime dateCompare){
        // Arrange
        User user = new User("John", "Doe", "john@email.com", deactivationDate);
        
        // Act
        bool result = user.DeactivationDateIsBeforeThen(dateCompare);
            
        // Assert
        Assert.True(result);
    }
=======
    [MemberData(nameof(GetDataDesativacaoAndDate))]
    public void IsBiggerThenDataDesativacao(DateTime dataDesativacao, DateTime dateCompare){
        //arrange
        User utilizador = new User("Jonh", "Doe", "jonh@email.com", dataDesativacao);
        
        //act
        bool result = utilizador.DeactivationDateIsBeforeThen(dateCompare);
            
        //assert
        Assert.True(result);
    }

   
>>>>>>> Stashed changes
}