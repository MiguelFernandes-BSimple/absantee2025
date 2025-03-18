using Classes;

public class PeriodoFormacaoTest{
    public static IEnumerable<object[]> GetPeriodoFormacaoData_CamposValidos()
    {
        yield return new object[] { DateOnly.FromDateTime(DateTime.Now), DateOnly.FromDateTime(DateTime.Now.AddDays(1)) };
        yield return new object[] { DateOnly.FromDateTime(DateTime.Now.AddYears(1)), DateOnly.FromDateTime(DateTime.Now.AddYears(3)) };
    }

    [Theory]
    [MemberData(nameof(GetPeriodoFormacaoData_CamposValidos))]
    public void CriarPeriodoFormacao_CamposValidos(DateOnly dataInicio, DateOnly dataFim){
        //arrange

        //act
        new PeriodoFormacao(dataInicio, dataFim);

        //assert
    }

    public static IEnumerable<object[]> GetPeriodoFormacaoData_DatasInvalidas()
    {
        yield return new object[] { DateOnly.FromDateTime(DateTime.Now).AddDays(5), DateOnly.FromDateTime(DateTime.Now.AddDays(1)) };
        yield return new object[] { DateOnly.FromDateTime(DateTime.Now).AddYears(4), DateOnly.FromDateTime(DateTime.Now.AddYears(2)) };
    }

    [Theory]
    [MemberData(nameof(GetPeriodoFormacaoData_DatasInvalidas))]
    public void CriarPeriodoFormacao_DatasInvalidas_Exception(DateOnly dataInicio, DateOnly dataFim){
        //arrange

        //assert
        ArgumentException exception = Assert.Throws<ArgumentException>(() =>
            //act
            new PeriodoFormacao(dataInicio, dataFim));

        Assert.Equal("Invalid Arguments", exception.Message);
    }

    public static IEnumerable<object[]> GetPeriodoFormacaoData_DataInicioInvalida()
    {
        yield return new object[] { DateOnly.FromDateTime(DateTime.Now).AddDays(-5), DateOnly.FromDateTime(DateTime.Now.AddDays(1)) };
        yield return new object[] { DateOnly.FromDateTime(DateTime.Now).AddYears(-3), DateOnly.FromDateTime(DateTime.Now.AddYears(-1)) };
    }

    [Theory]
    [MemberData(nameof(GetPeriodoFormacaoData_DataInicioInvalida))]
    public void CriarPeriodoFormacao_DataInicioInvalida_Exception(DateOnly dataInicio, DateOnly dataFim){
        //arrange

        //assert
        ArgumentException exception = Assert.Throws<ArgumentException>(() =>
            //act
            new PeriodoFormacao(dataInicio, dataFim));

        Assert.Equal("Invalid Arguments", exception.Message);
    }
}