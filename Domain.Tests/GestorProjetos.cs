using Domain;
using Moq;

public class GestorProjetosTest{
    public static IEnumerable<object[]> GetGestorProjetosData_CamposValidos()
    {
        yield return new object[] { DateTime.Now, DateTime.Now.AddDays(1) };
        yield return new object[] { DateTime.Now, DateTime.Now.AddYears(3) };
    }

    [Theory]
    [MemberData(nameof(GetGestorProjetosData_CamposValidos))]
    public void CriarGestorProjetos_CamposValidos(DateTime dataInicio, DateTime dataFim){
        //arrange
        Mock<IUtilizador> utilizador = new Mock<IUtilizador>();
        utilizador.Setup(u => u.IsBiggerThenDataDesativacao(dataFim)).Returns(false);
        utilizador.Setup(u => u.IsDesativo()).Returns(false);

        //act
        new GestorProjetos(dataInicio, dataFim, utilizador.Object);

        //assert
    }


    public static IEnumerable<object[]> GetGestorProjetosData_DatasInvalidas()
    {
        yield return new object[] { DateTime.Now.AddDays(5), DateTime.Now.AddDays(1) };
        yield return new object[] { DateTime.Now.AddYears(-1), DateTime.Now.AddYears(-3) };
    }

    [Theory]
    [MemberData(nameof(GetGestorProjetosData_DatasInvalidas))]
    public void CriarGestorProjetos_DatasInvalidas_Exception(DateTime dataInicio, DateTime dataFim){
        //arrange
        Mock<IUtilizador> utilizador = new Mock<IUtilizador>();
        utilizador.Setup(u => u.IsBiggerThenDataDesativacao(dataFim)).Returns(false);
        utilizador.Setup(u => u.IsDesativo()).Returns(false);

        //assert
        ArgumentException exception = Assert.Throws<ArgumentException>(() =>
            //act
            new GestorProjetos(dataInicio, dataFim, utilizador.Object));

        Assert.Equal("Invalid Arguments", exception.Message);
    }

    [Theory]
    [MemberData(nameof(GetGestorProjetosData_CamposValidos))]
    public void CriarGestorProjetos_DataFimMaiorDataDesativacao_Exception(DateTime dataInicio, DateTime dataFim){
        //arrange
        Mock<IUtilizador> utilizador = new Mock<IUtilizador>();
        utilizador.Setup(u => u.IsBiggerThenDataDesativacao(dataFim)).Returns(true);
        utilizador.Setup(u => u.IsDesativo()).Returns(false);

        //assert
        ArgumentException exception = Assert.Throws<ArgumentException>(() =>
            //act
            new GestorProjetos(dataInicio, dataFim, utilizador.Object));

        Assert.Equal("Invalid Arguments", exception.Message);
    }

    [Theory]
    [MemberData(nameof(GetGestorProjetosData_CamposValidos))]
    public void CriarGestorProjetos_UtilizadorInativo_Exception(DateTime dataInicio, DateTime dataFim){
        //arrange
        Mock<IUtilizador> utilizador = new Mock<IUtilizador>();
        utilizador.Setup(u => u.IsBiggerThenDataDesativacao(dataFim)).Returns(false);
        utilizador.Setup(u => u.IsDesativo()).Returns(true);

        //assert
        ArgumentException exception = Assert.Throws<ArgumentException>(() =>
            //act
            new GestorProjetos(dataInicio, dataFim, utilizador.Object));

        Assert.Equal("Invalid Arguments", exception.Message);
    }

    [Theory]
    [MemberData(nameof(GetGestorProjetosData_DatasInvalidas))]
    public void CriarGestorProjetos_InputsInvalidos_Exception(DateTime dataInicio, DateTime dataFim){
        //arrange
        Mock<IUtilizador> utilizador = new Mock<IUtilizador>();
        utilizador.Setup(u => u.IsBiggerThenDataDesativacao(dataFim)).Returns(true);
        utilizador.Setup(u => u.IsDesativo()).Returns(true);

        //assert
        ArgumentException exception = Assert.Throws<ArgumentException>(() =>
            //act
            new GestorProjetos(dataInicio, dataFim, utilizador.Object));

        Assert.Equal("Invalid Arguments", exception.Message);
    }
}