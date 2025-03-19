using Domain;
using Moq;

public class ColaboradorTest{
    public static IEnumerable<object[]> GetColaboradorData_CamposValidos()
    {
        yield return new object[] { DateTime.Now, DateTime.Now.AddDays(1) };
        yield return new object[] { DateTime.Now.AddYears(-1), DateTime.Now.AddYears(2) };
        yield return new object[] { DateTime.Now.AddYears(-1), null! };
    }

    [Theory]
    [MemberData(nameof(GetColaboradorData_CamposValidos))]
    public void CriarColaborador_CamposValidos(DateTime dataInicio, DateTime? dataFim){
        //arrange
        Mock<IUtilizador> utilizador = new Mock<IUtilizador>();
        utilizador.Setup(u => u.IsBiggerThenDataDesativacao(It.IsAny<DateTime>())).Returns(false);
        utilizador.Setup(u => u.IsDesativo()).Returns(false);

        //act
        new Colaborador(utilizador.Object, dataInicio, dataFim);

        //assert
    }


    public static IEnumerable<object[]> GetColaboradorData_DatasInvalidas()
    {
        yield return new object[] { DateTime.Now.AddDays(5), DateTime.Now.AddDays(1) };
        yield return new object[] { DateTime.Now.AddYears(-1), DateTime.Now.AddYears(-3) };

    }

    [Theory]
    [MemberData(nameof(GetColaboradorData_DatasInvalidas))]
    public void CriarColaborador_DatasInvalidas_Exception(DateTime dataInicio, DateTime dataFim){
        //arrange
        Mock<IUtilizador> utilizador = new Mock<IUtilizador>();
        utilizador.Setup(u => u.IsBiggerThenDataDesativacao(dataFim)).Returns(false);
        utilizador.Setup(u => u.IsDesativo()).Returns(false);

        //assert
        ArgumentException exception = Assert.Throws<ArgumentException>(() =>
            //act
            new Colaborador(utilizador.Object, dataInicio, dataFim));

        Assert.Equal("Invalid Arguments", exception.Message);
    }

    [Theory]
    [MemberData(nameof(GetColaboradorData_CamposValidos))]
    public void CriarColaborador_DataFimMaiorDataDesativacao_Exception(DateTime dataInicio, DateTime dataFim){
        //arrange
        Mock<IUtilizador> utilizador = new Mock<IUtilizador>();
        utilizador.Setup(u => u.IsBiggerThenDataDesativacao(dataFim)).Returns(true);
        utilizador.Setup(u => u.IsDesativo()).Returns(false);

        //assert
        ArgumentException exception = Assert.Throws<ArgumentException>(() =>
            //act
            new Colaborador(utilizador.Object, dataInicio, dataFim));

        Assert.Equal("Invalid Arguments", exception.Message);
    }

    [Theory]
    [MemberData(nameof(GetColaboradorData_CamposValidos))]
    public void CriarColaborador_UtilizadorInativo_Exception(DateTime dataInicio, DateTime dataFim){
        //arrange
        Mock<IUtilizador> utilizador = new Mock<IUtilizador>();
        utilizador.Setup(u => u.IsBiggerThenDataDesativacao(dataFim)).Returns(false);
        utilizador.Setup(u => u.IsDesativo()).Returns(true);

        //assert
        ArgumentException exception = Assert.Throws<ArgumentException>(() =>
            //act
            new Colaborador(utilizador.Object, dataInicio, dataFim));

        Assert.Equal("Invalid Arguments", exception.Message);
    }

    [Theory]
    [MemberData(nameof(GetColaboradorData_DatasInvalidas))]
    public void CriarColaborador_InputsInvalidos_Exception(DateTime dataInicio, DateTime dataFim){
        //arrange
        Mock<IUtilizador> utilizador = new Mock<IUtilizador>();
        utilizador.Setup(u => u.IsBiggerThenDataDesativacao(dataFim)).Returns(true);
        utilizador.Setup(u => u.IsDesativo()).Returns(true);

        //assert
        ArgumentException exception = Assert.Throws<ArgumentException>(() =>
            //act
            new Colaborador(utilizador.Object, dataInicio, dataFim));

        Assert.Equal("Invalid Arguments", exception.Message);
    }
}