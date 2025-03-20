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
        new HolidayPeriod(dataInicio, dataFim);

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
            new HolidayPeriod(dataInicio, dataFim));

        Assert.Equal("Invalid Arguments", exception.Message);
    }

    [Theory]
    [MemberData(nameof(GetPeriodoFeriasData_CamposValidos))]
    public void GetDatas_Sucesso(DateOnly dataInicio, DateOnly dataFim){
        //arrange

        //act
        HolidayPeriod periodoFerias = new HolidayPeriod(dataInicio, dataFim);
        DateOnly actualDataInicio = periodoFerias.GetInitDate();
        DateOnly actualDataFim = periodoFerias.GetFinalDate();

        //assert
        Assert.Equal(dataInicio, actualDataInicio);
        Assert.Equal(dataFim, actualDataFim);
    }

    public static IEnumerable<object[]> GetPeriodoFeriasOverlapData()
    {
        yield return new object[] { DateOnly.FromDateTime(new DateTime(2020, 1, 1)), DateOnly.FromDateTime(new DateTime(2021, 1, 1)), true };
        yield return new object[] { DateOnly.FromDateTime(new DateTime(2020, 1, 2)), DateOnly.FromDateTime(new DateTime(2020, 12, 31)), true };
        yield return new object[] { DateOnly.FromDateTime(new DateTime(2019, 12, 31)), DateOnly.FromDateTime(new DateTime(2021, 1, 1)), false };
        yield return new object[] { DateOnly.FromDateTime(new DateTime(2020, 1, 2)), DateOnly.FromDateTime(new DateTime(2021, 1, 2)), false };
        yield return new object[] { DateOnly.FromDateTime(new DateTime(2019, 12, 31)), DateOnly.FromDateTime(new DateTime(2021, 1, 2)), false };
    }

    [Theory]
    [MemberData(nameof(GetPeriodoFeriasOverlapData))]
    public void PeriodoFeriasOverlap_Sucesso(DateOnly dataInicioNew, DateOnly dataFimNew, bool expected){
        //arrange
        DateOnly dataInicio = DateOnly.FromDateTime(new DateTime(2020, 1, 1));
        DateOnly dataFim = DateOnly.FromDateTime(new DateTime(2021, 1, 1));
        HolidayPeriod periodoFerias = new HolidayPeriod(dataInicio, dataFim);
        HolidayPeriod periodoFeriasNew = new HolidayPeriod(dataInicioNew, dataFimNew);

        //act
        bool result = periodoFerias.HolidayPeriodOverlap(periodoFeriasNew);

        //assert
        Assert.Equal(expected, result);
    }
}