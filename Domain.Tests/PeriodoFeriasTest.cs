using Domain;

public class PeriodoFeriasTest{
    public static IEnumerable<object[]> GetPeriodoFeriasData_CamposValidos()
    {
        yield return new object[] { DateOnly.FromDateTime(DateTime.Now), DateOnly.FromDateTime(DateTime.Now.AddDays(1)) };
        yield return new object[] { DateOnly.FromDateTime(DateTime.Now.AddYears(1)), DateOnly.FromDateTime(DateTime.Now.AddYears(2)) };
    }

    [Theory]
    [MemberData(nameof(GetPeriodoFeriasData_CamposValidos))]
    public void CriarPeriodoFerias_CamposValidos(DateOnly dataInicio, DateOnly dataFim){
        //arrange

        //act
        new PeriodoFerias(dataInicio, dataFim);

        //assert
    }

    public static IEnumerable<object[]> GetPeriodoFeriasData_DatasInvalidas()
    {
        yield return new object[] { DateOnly.FromDateTime(DateTime.Now).AddDays(5), DateOnly.FromDateTime(DateTime.Now.AddDays(1)) };
        yield return new object[] { DateOnly.FromDateTime(DateTime.Now).AddYears(-1), DateOnly.FromDateTime(DateTime.Now.AddYears(-3)) };
    }

    [Theory]
    [MemberData(nameof(GetPeriodoFeriasData_DatasInvalidas))]
    public void CriarPeriodoFerias_DatasInvalidas_Exception(DateOnly dataInicio, DateOnly dataFim){
        //arrange

        //assert
        ArgumentException exception = Assert.Throws<ArgumentException>(() =>
            //act
            new PeriodoFerias(dataInicio, dataFim));

        Assert.Equal("Invalid Arguments", exception.Message);
    }

    [Theory]
    [MemberData(nameof(GetPeriodoFeriasData_CamposValidos))]
    public void GetDatas_Sucesso(DateOnly dataInicio, DateOnly dataFim){
        //arrange

        //act
        PeriodoFerias periodoFerias = new PeriodoFerias(dataInicio, dataFim);
        DateOnly actualDataInicio = periodoFerias.GetDataInicio();
        DateOnly actualDataFim = periodoFerias.GetDataFim();

        //assert
        Assert.Equal(dataInicio, actualDataInicio);
        Assert.Equal(dataFim, actualDataFim);
    }
}