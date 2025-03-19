using Domain;
using Moq;

public class GestorRHTest{
    public static IEnumerable<object[]> GetGestorRHData_CamposValidos()
    {
        yield return new object[] { DateTime.Now, DateTime.Now.AddDays(1) };
        yield return new object[] { DateTime.Now.AddDays(10), DateTime.Now.AddYears(3) };
        yield return new object[] { DateTime.Now.AddDays(10), null! };
    }

    [Theory]
    [MemberData(nameof(GetGestorRHData_CamposValidos))]
    public void CriarGestorRH_CamposValidos(DateTime dataInicio, DateTime? dataFim){
        //arrange
        Mock<IUtilizador> utilizador = new Mock<IUtilizador>();
        utilizador.Setup(u => u.IsBiggerThenDataDesativacao(It.IsAny<DateTime>())).Returns(false);
        utilizador.Setup(u => u.IsDesativo()).Returns(false);

        //act
        new GestorRH(utilizador.Object, dataInicio, dataFim);

        //assert
    }

    public static IEnumerable<object[]> GetGestorRHData_DatasInvalidas()
    {
        yield return new object[] { DateTime.Now.AddDays(5), DateTime.Now.AddDays(1) };
        yield return new object[] { DateTime.Now.AddYears(-1), DateTime.Now.AddYears(-3) };
    }

    [Theory]
    [MemberData(nameof(GetGestorRHData_DatasInvalidas))]
    public void CriarGestorRH_DatasInvalidas_Exception(DateTime dataInicio, DateTime dataFim){
        //arrange
        Mock<IUtilizador> utilizador = new Mock<IUtilizador>();
        utilizador.Setup(u => u.IsBiggerThenDataDesativacao(dataFim)).Returns(false);
        utilizador.Setup(u => u.IsDesativo()).Returns(false);

        //assert
        ArgumentException exception = Assert.Throws<ArgumentException>(() =>
            //act
            new GestorRH(utilizador.Object, dataInicio, dataFim));

        Assert.Equal("Invalid Arguments", exception.Message);
    }

    [Theory]
    [MemberData(nameof(GetGestorRHData_CamposValidos))]
    public void CriarGestorRH_DataFimMaiorDataDesativacao_Exception(DateTime dataInicio, DateTime dataFim){
        //arrange
        Mock<IUtilizador> utilizador = new Mock<IUtilizador>();
        utilizador.Setup(u => u.IsBiggerThenDataDesativacao(dataFim)).Returns(true);
        utilizador.Setup(u => u.IsDesativo()).Returns(false);

        //assert
        ArgumentException exception = Assert.Throws<ArgumentException>(() =>
            //act
            new GestorRH(utilizador.Object, dataInicio, dataFim));

        Assert.Equal("Invalid Arguments", exception.Message);
    }

    [Theory]
    [MemberData(nameof(GetGestorRHData_CamposValidos))]
    public void CriarGestorRH_UtilizadorInativo_Exception(DateTime dataInicio, DateTime dataFim){
        //arrange
        Mock<IUtilizador> utilizador = new Mock<IUtilizador>();
        utilizador.Setup(u => u.IsBiggerThenDataDesativacao(dataFim)).Returns(false);
        utilizador.Setup(u => u.IsDesativo()).Returns(true);

        //assert
        ArgumentException exception = Assert.Throws<ArgumentException>(() =>
            //act
            new GestorRH(utilizador.Object, dataInicio, dataFim));

        Assert.Equal("Invalid Arguments", exception.Message);
    }

    [Theory]
    [MemberData(nameof(GetGestorRHData_DatasInvalidas))]
    public void CriarGestorRH_InputsInvalidos_Exception(DateTime dataInicio, DateTime dataFim){
        //arrange
        Mock<IUtilizador> utilizador = new Mock<IUtilizador>();
        utilizador.Setup(u => u.IsBiggerThenDataDesativacao(dataFim)).Returns(true);
        utilizador.Setup(u => u.IsDesativo()).Returns(true);

        //assert
        ArgumentException exception = Assert.Throws<ArgumentException>(() =>
            //act
            new GestorRH(utilizador.Object, dataInicio, dataFim));

        Assert.Equal("Invalid Arguments", exception.Message);
    }
}