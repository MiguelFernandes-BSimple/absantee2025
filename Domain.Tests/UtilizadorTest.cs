using Domain;

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
        //arrange

        //act
        new Utilizador(nomes, apelidos, email, dataDesativacao);

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
        //arrange

        //assert
        ArgumentException exception = Assert.Throws<ArgumentException>(() => 
            // act
            new Utilizador(nomes, apelidos, email, dataDesativacao));

        Assert.Equal("Invalid Arguments", exception.Message);
    }

    public static IEnumerable<object[]> GetUserData_ApelidosInvalidos()
    {
        yield return new object[] { "Jonh", new string('a', 51), "jonh@email.com", DateTime.Now.AddDays(1) };
        yield return new object[] { "Jonh", "Doe 13", "jonh@email.com", null! };
    }

    [Theory]
    [MemberData(nameof(GetUserData_ApelidosInvalidos))]
    public void CriarUtilizador_ApelidosInvalidos_Exception(string nomes, string apelidos, string email, DateTime? dataDesativacao){
        //arrange

        //assert
        ArgumentException exception = Assert.Throws<ArgumentException>(() => 
            // act
            new Utilizador(nomes, apelidos, email, dataDesativacao));

        Assert.Equal("Invalid Arguments", exception.Message);
    }

    public static IEnumerable<object[]> GetUserData_DataDesativacaoInvalida()
    {
        yield return new object[] { "Jonh", "Doe", "jonh@email.com", DateTime.Now.AddDays(-1) };
    }

    [Theory]
    [MemberData(nameof(GetUserData_DataDesativacaoInvalida))]
    public void CriarUtilizador_DatasDesativacao_Exception(string nomes, string apelidos, string email, DateTime? dataDesativacao){
        //arrange

        //assert
        ArgumentException exception = Assert.Throws<ArgumentException>(() => 
            // act
            new Utilizador(nomes, apelidos, email, dataDesativacao));

        Assert.Equal("Invalid Arguments", exception.Message);
    }

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
        //arrange

        //assert
        ArgumentException exception = Assert.Throws<ArgumentException>(() => 
            // act
            new Utilizador(nomes, apelidos, email, dataDesativacao));

        Assert.Equal("Invalid Arguments", exception.Message);
    }


    public static IEnumerable<object[]> GetDataDesativacao()
    {
        yield return new object[] { DateTime.Now.AddHours(1) };
        yield return new object[] { DateTime.Now.AddYears(1) };
    }

    [Theory]
    [MemberData(nameof(GetDataDesativacao))]
    public void IsDesativo_ReturnFalse(DateTime dataDesativacao){
        //arrange
        Utilizador utilizador = new Utilizador("Jonh", "Doe", "jonh@email.com", dataDesativacao);
        
        //act
        bool result = utilizador.IsDesativo();
            
        //assert
        Assert.False(result);
    }


    public static IEnumerable<object[]> GetDataDesativacaoAndDate()
    {
        yield return new object[] { DateTime.Now.AddHours(1), DateTime.Now.AddHours(2) };
        yield return new object[] { DateTime.Now.AddYears(1), DateTime.Now.AddYears(2) };
    }

    [Theory]
    [MemberData(nameof(GetDataDesativacaoAndDate))]
    public void IsBiggerThenDataDesativacao(DateTime dataDesativacao, DateTime dateCompare){
        //arrange
        Utilizador utilizador = new Utilizador("Jonh", "Doe", "jonh@email.com", dataDesativacao);
        
        //act
        bool result = utilizador.IsBiggerThenDataDesativacao(dateCompare);
            
        //assert
        Assert.True(result);
    }
}