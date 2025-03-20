using Domain;
using Moq;

public class GestorProjetosTest{
    public static IEnumerable<object[]> GetGestorProjetosData_CamposValidos()
    {
        yield return new object[] { DateTime.Now, DateTime.Now.AddDays(1) };
        yield return new object[] { DateTime.Now, DateTime.Now.AddYears(3) };
        yield return new object[] { DateTime.Now, null! };
    }

    [Theory]
    [MemberData(nameof(GetGestorProjetosData_CamposValidos))]
    public void CriarGestorProjetos_CamposValidos(DateTime dataInicio, DateTime? dataFim){
        //arrange
        Mock<IUser> utilizador = new Mock<IUser>();
        utilizador.Setup(u => u.DeactivationDateIsBeforeThen(It.IsAny<DateTime>())).Returns(false);
        utilizador.Setup(u => u.IsDeactivated()).Returns(false);

        //act
        new ProjectManager(utilizador.Object, dataInicio, dataFim);

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
        Mock<IUser> utilizador = new Mock<IUser>();
        utilizador.Setup(u => u.DeactivationDateIsBeforeThen(dataFim)).Returns(false);
        utilizador.Setup(u => u.IsDeactivated()).Returns(false);

        //assert
        ArgumentException exception = Assert.Throws<ArgumentException>(() =>
            //act
            new ProjectManager(utilizador.Object, dataInicio, dataFim));

        Assert.Equal("Invalid Arguments", exception.Message);
    }

    [Theory]
    [MemberData(nameof(GetGestorProjetosData_CamposValidos))]
    public void CriarGestorProjetos_DataFimMaiorDataDesativacao_Exception(DateTime dataInicio, DateTime dataFim){
        //arrange
        Mock<IUser> utilizador = new Mock<IUser>();
        utilizador.Setup(u => u.DeactivationDateIsBeforeThen(dataFim)).Returns(true);
        utilizador.Setup(u => u.IsDeactivated()).Returns(false);

        //assert
        ArgumentException exception = Assert.Throws<ArgumentException>(() =>
            //act
            new ProjectManager(utilizador.Object, dataInicio, dataFim));

        Assert.Equal("Invalid Arguments", exception.Message);
    }

    [Theory]
    [MemberData(nameof(GetGestorProjetosData_CamposValidos))]
    public void CriarGestorProjetos_UtilizadorInativo_Exception(DateTime dataInicio, DateTime dataFim){
        //arrange
        Mock<IUser> utilizador = new Mock<IUser>();
        utilizador.Setup(u => u.DeactivationDateIsBeforeThen(dataFim)).Returns(false);
        utilizador.Setup(u => u.IsDeactivated()).Returns(true);

        //assert
        ArgumentException exception = Assert.Throws<ArgumentException>(() =>
            //act
            new ProjectManager(utilizador.Object, dataInicio, dataFim));

        Assert.Equal("Invalid Arguments", exception.Message);
    }

    [Theory]
    [MemberData(nameof(GetGestorProjetosData_DatasInvalidas))]
    public void CriarGestorProjetos_InputsInvalidos_Exception(DateTime dataInicio, DateTime dataFim){
        //arrange
        Mock<IUser> utilizador = new Mock<IUser>();
        utilizador.Setup(u => u.DeactivationDateIsBeforeThen(dataFim)).Returns(true);
        utilizador.Setup(u => u.IsDeactivated()).Returns(true);

        //assert
        ArgumentException exception = Assert.Throws<ArgumentException>(() =>
            //act
            new ProjectManager(utilizador.Object, dataInicio, dataFim));

        Assert.Equal("Invalid Arguments", exception.Message);
    }
}