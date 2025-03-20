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
        Mock<IUser> utilizador = new Mock<IUser>();
        utilizador.Setup(u => u.DeactivationDateIsBeforeThen(It.IsAny<DateTime>())).Returns(false);
        utilizador.Setup(u => u.IsDeactivated()).Returns(false);

        //act
        new RHManager(utilizador.Object, dataInicio, dataFim);

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
        Mock<IUser> utilizador = new Mock<IUser>();
        utilizador.Setup(u => u.DeactivationDateIsBeforeThen(dataFim)).Returns(false);
        utilizador.Setup(u => u.IsDeactivated()).Returns(false);

        //assert
        ArgumentException exception = Assert.Throws<ArgumentException>(() =>
            //act
            new RHManager(utilizador.Object, dataInicio, dataFim));

        Assert.Equal("Invalid Arguments", exception.Message);
    }

    [Theory]
    [MemberData(nameof(GetGestorRHData_CamposValidos))]
    public void CriarGestorRH_DataFimMaiorDataDesativacao_Exception(DateTime dataInicio, DateTime dataFim){
        //arrange
        Mock<IUser> utilizador = new Mock<IUser>();
        utilizador.Setup(u => u.DeactivationDateIsBeforeThen(dataFim)).Returns(true);
        utilizador.Setup(u => u.IsDeactivated()).Returns(false);

        //assert
        ArgumentException exception = Assert.Throws<ArgumentException>(() =>
            //act
            new RHManager(utilizador.Object, dataInicio, dataFim));

        Assert.Equal("Invalid Arguments", exception.Message);
    }

    [Theory]
    [MemberData(nameof(GetGestorRHData_CamposValidos))]
    public void CriarGestorRH_UtilizadorInativo_Exception(DateTime dataInicio, DateTime dataFim){
        //arrange
        Mock<IUser> utilizador = new Mock<IUser>();
        utilizador.Setup(u => u.DeactivationDateIsBeforeThen(dataFim)).Returns(false);
        utilizador.Setup(u => u.IsDeactivated()).Returns(true);

        //assert
        ArgumentException exception = Assert.Throws<ArgumentException>(() =>
            //act
            new RHManager(utilizador.Object, dataInicio, dataFim));

        Assert.Equal("Invalid Arguments", exception.Message);
    }

    [Theory]
    [MemberData(nameof(GetGestorRHData_DatasInvalidas))]
    public void CriarGestorRH_InputsInvalidos_Exception(DateTime dataInicio, DateTime dataFim){
        //arrange
        Mock<IUser> utilizador = new Mock<IUser>();
        utilizador.Setup(u => u.DeactivationDateIsBeforeThen(dataFim)).Returns(true);
        utilizador.Setup(u => u.IsDeactivated()).Returns(true);

        //assert
        ArgumentException exception = Assert.Throws<ArgumentException>(() =>
            //act
            new RHManager(utilizador.Object, dataInicio, dataFim));

        Assert.Equal("Invalid Arguments", exception.Message);
    }
}