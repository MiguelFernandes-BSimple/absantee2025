using Domain;

public class ProjetoTest{
    public static IEnumerable<object[]> GetProjetoData_CamposValidos()
    {
        yield return new object[] { "Titulo 1", "T1", DateOnly.FromDateTime(DateTime.Now), DateOnly.FromDateTime(DateTime.Now.AddDays(1)) };
        yield return new object[] { "Projeto_Academia_2024", "PA2024", DateOnly.FromDateTime(DateTime.Now.AddYears(1)), DateOnly.FromDateTime(DateTime.Now.AddYears(3)) };
        yield return new object[] { "Projeto-Academia-2024(2)", "PA2024V2",DateOnly.FromDateTime(DateTime.Now.AddYears(-2)), DateOnly.FromDateTime(DateTime.Now.AddYears(1)) };
    }

    [Theory]
    [MemberData(nameof(GetProjetoData_CamposValidos))]
    public void CriarProjeto_CamposValidos(string Titulo, string Sigla, DateOnly dataInicio, DateOnly dataFim){
        //arrange

        //act
        new Project(Titulo, Sigla, dataInicio, dataFim);

        //assert
    }

    public static IEnumerable<object[]> GetProjetoData_TituloInvalido()
    {
        yield return new object[] { "", "T1", DateOnly.FromDateTime(DateTime.Now), DateOnly.FromDateTime(DateTime.Now.AddDays(1)) };
        yield return new object[] { new string('a', 51), "T1", DateOnly.FromDateTime(DateTime.Now.AddYears(1)), DateOnly.FromDateTime(DateTime.Now.AddYears(3)) };
    }

    [Theory]
    [MemberData(nameof(GetProjetoData_TituloInvalido))]
    public void CriarProjeto_TituloInvalido_Exception(string Titulo, string Sigla, DateOnly dataInicio, DateOnly dataFim){
        //arrange

        //assert
        ArgumentException exception = Assert.Throws<ArgumentException>(() =>
            //act
            new Project(Titulo, Sigla, dataInicio, dataFim));

        Assert.Equal("Invalid Arguments", exception.Message);
    }

    public static IEnumerable<object[]> GetProjetoData_SiglaInvalida()
    {
        yield return new object[] { "Titulo 1", "T_1", DateOnly.FromDateTime(DateTime.Now), DateOnly.FromDateTime(DateTime.Now.AddDays(1)) };
        yield return new object[] { "Projeto_Academia_2024", "", DateOnly.FromDateTime(DateTime.Now.AddYears(1)), DateOnly.FromDateTime(DateTime.Now.AddYears(3)) };
        yield return new object[] { "Projeto_Academia_2024_", new string('A', 11), DateOnly.FromDateTime(DateTime.Now.AddYears(1)), DateOnly.FromDateTime(DateTime.Now.AddYears(3)) };
        yield return new object[] { "Projeto_Academia_2024_", "pa2024", DateOnly.FromDateTime(DateTime.Now.AddYears(1)), DateOnly.FromDateTime(DateTime.Now.AddYears(3)) };
    }

    [Theory]
    [MemberData(nameof(GetProjetoData_SiglaInvalida))]
    public void CriarProjeto_SiglaInvalida_Exception(string Titulo, string Sigla, DateOnly dataInicio, DateOnly dataFim){
        //arrange

        //assert
        ArgumentException exception = Assert.Throws<ArgumentException>(() =>
            //act
            new Project(Titulo, Sigla, dataInicio, dataFim));

        Assert.Equal("Invalid Arguments", exception.Message);
    }


    public static IEnumerable<object[]> GetProjetoData_DatasInvalidas()
    {
        yield return new object[] { "Titulo 1", "T1", DateOnly.FromDateTime(DateTime.Now).AddDays(5), DateOnly.FromDateTime(DateTime.Now.AddDays(1)) };
        yield return new object[] { "Projeto_Academia_2024", "PA2024", DateOnly.FromDateTime(DateTime.Now).AddYears(-1), DateOnly.FromDateTime(DateTime.Now.AddYears(-3)) };
    }

    [Theory]
    [MemberData(nameof(GetProjetoData_DatasInvalidas))]
    public void CriarProjeto_DatasInvalidas_Exception(string Titulo, string Sigla, DateOnly dataInicio, DateOnly dataFim){
        //arrange

        //assert
        ArgumentException exception = Assert.Throws<ArgumentException>(() =>
            //act
            new Project(Titulo, Sigla, dataInicio, dataFim));

        Assert.Equal("Invalid Arguments", exception.Message);
    }

    public static IEnumerable<object[]> GetProjetoData_CamposInvalidos()
    {
        yield return new object[] { "", "", DateOnly.FromDateTime(DateTime.Now).AddDays(5), DateOnly.FromDateTime(DateTime.Now.AddDays(1)) };
    }

    [Theory]
    [MemberData(nameof(GetProjetoData_CamposInvalidos))]
    public void CriarProjeto_InputsInvalidos_Exception(string Titulo, string Sigla, DateOnly dataInicio, DateOnly dataFim){
        //arrange

        //assert
         ArgumentException exception = Assert.Throws<ArgumentException>(() =>
            //act
            new Project(Titulo, Sigla, dataInicio, dataFim));

        Assert.Equal("Invalid Arguments", exception.Message);
    }

    public static IEnumerable<object[]> GetProjetoData_CompareDataInicio()
    {
        yield return new object[] { DateOnly.FromDateTime(DateTime.Now), DateOnly.FromDateTime(DateTime.Now).AddDays(1), 1 };
        yield return new object[] { DateOnly.FromDateTime(DateTime.Now).AddYears(-1), DateOnly.FromDateTime(DateTime.Now).AddYears(-3), -1 };
        yield return new object[] { new DateOnly(2000, 1, 1),  new DateOnly(2000, 1, 1), 0};
    }

    // [Theory]
    // [MemberData(nameof(GetProjetoData_CompareDataInicio))]
    // public void CompareWithDataInicio_Sucesso(DateOnly dataInicio, DateOnly dateCompare, int expected){
    //     //arrange
    //     Project projeto = new Project("Titulo 1", "T1", dataInicio, DateOnly.MaxValue);

    //     //act
    //     int result = projeto.CompareWithDataInicio(dateCompare);

    //     //assert
    //     Assert.Equal(expected, result);
    // }

    // public static IEnumerable<object[]> GetProjetoData_CompareDataFim()
    // {
    //     yield return new object[] { DateOnly.FromDateTime(DateTime.Now), DateOnly.FromDateTime(DateTime.Now).AddDays(1), 1 };
    //     yield return new object[] { DateOnly.FromDateTime(DateTime.Now).AddYears(-1), DateOnly.FromDateTime(DateTime.Now).AddYears(-3), -1 };
    //     yield return new object[] { new DateOnly(2000, 1, 1),  new DateOnly(2000, 1, 1), 0};
    // }

    // [Theory]
    // [MemberData(nameof(GetProjetoData_CompareDataFim))]
    // public void CompareWithDataFim_Sucesso(DateOnly dataFim, DateOnly dateCompare, int expected){
    //     //arrange
    //     Project projeto = new Project("Titulo 1", "T1", DateOnly.MinValue, dataFim);

    //     //act
    //     int result = projeto.CompareWithDataFim(dateCompare);

    //     //assert
    //     Assert.Equal(expected, result);
    // }
}