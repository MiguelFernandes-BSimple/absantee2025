using Domain;
using Moq;

public class AssociacaoProjetoColaboradorTest{
    // public static IEnumerable<object[]> GetAssociacaoProjetoColaboradorData_CamposValidos()
    // {
    //     yield return new object[] { DateOnly.FromDateTime(DateTime.Now), DateOnly.FromDateTime(DateTime.Now.AddDays(1)) };
    //     yield return new object[] { DateOnly.FromDateTime(DateTime.Now.AddYears(1)), DateOnly.FromDateTime(DateTime.Now.AddYears(3)) };
    // }

    // [Theory]
    // [MemberData(nameof(GetAssociacaoProjetoColaboradorData_CamposValidos))]
    // public void CriarAssociacaoProjetoColaborador_CamposValidos(DateOnly dataInicio, DateOnly dataFim){
    //     //arrange
    //     Mock<IProject> ProjetoMock = new Mock<IProject>();
    //     ProjetoMock.Setup(p => p.CompareWithDataInicio(dataInicio)).Returns(1);
    //     ProjetoMock.Setup(p => p.CompareWithDataFim(dataFim)).Returns(-1);

    //     ProjetoMock.Setup(c => c.CompareWithDataFim(DateOnly.FromDateTime(DateTime.Today))).Returns(-1); 

    //     Mock<IColaborator> ColaboradorMock = new Mock<IColaborator>();
    //     ColaboradorMock.Setup(c => c.CompareWithDataInicio(dataInicio.ToDateTime(TimeOnly.MinValue))).Returns(1); 
    //     ColaboradorMock.Setup(c => c.CompareWithDataFim(dataFim.ToDateTime(TimeOnly.MinValue))).Returns(-1); 

    //     //act
    //     new AssociationProjectColaborator(dataInicio, dataFim, ColaboradorMock.Object, ProjetoMock.Object);

    //     //assert
    // }

    // public static IEnumerable<object[]> GetAssociacaoProjetoColaboradorData_DatasAssociacaoInvalidas()
    // {
    //     yield return new object[] { DateOnly.FromDateTime(DateTime.Now).AddDays(5), DateOnly.FromDateTime(DateTime.Now.AddDays(1)) };
    //     yield return new object[] { DateOnly.FromDateTime(DateTime.Now).AddYears(-1), DateOnly.FromDateTime(DateTime.Now.AddYears(-3)) };
    // }

    // [Theory]
    // [MemberData(nameof(GetAssociacaoProjetoColaboradorData_DatasAssociacaoInvalidas))]
    // public void CriarAssociacaoProjetoColaborador_DatasInvalidas_Exception(DateOnly dataInicio, DateOnly dataFim){
    //     //arrange
    //     Mock<IProject> ProjetoMock = new Mock<IProject>();
    //     ProjetoMock.Setup(p => p.CompareWithDataInicio(dataInicio)).Returns(1);
    //     ProjetoMock.Setup(p => p.CompareWithDataFim(dataFim)).Returns(-1);

    //     ProjetoMock.Setup(c => c.CompareWithDataFim(DateOnly.FromDateTime(DateTime.Today))).Returns(-1); 

    //     Mock<IColaborator> ColaboradorMock = new Mock<IColaborator>();
    //     ColaboradorMock.Setup(c => c.CompareWithDataInicio(dataInicio.ToDateTime(TimeOnly.MinValue))).Returns(1); 
    //     ColaboradorMock.Setup(c => c.CompareWithDataFim(dataFim.ToDateTime(TimeOnly.MinValue))).Returns(-1); 

    //     //assert
    //     ArgumentException exception = Assert.Throws<ArgumentException>(() =>
    //         //act
    //         new AssociationProjectColaborator(dataInicio, dataFim, ColaboradorMock.Object, ProjetoMock.Object));

    //     Assert.Equal("Invalid Arguments", exception.Message);
    // }

    // public static IEnumerable<object[]> GetAssociacaoProjetoColaboradorData_DataProjetoInvalidaAssociacao()
    // {
    //     yield return new object[] { 
    //         DateOnly.FromDateTime(DateTime.Now), DateOnly.FromDateTime(DateTime.Now.AddDays(1))};
    // }

    // [Theory]
    // [MemberData(nameof(GetAssociacaoProjetoColaboradorData_DataProjetoInvalidaAssociacao))]
    // public void CriarAssociacaoProjetoColaborador_DataProjetoInvalidaAssociacao_Exception(
    //                 DateOnly dataInicio, DateOnly dataFim){
    //     //arrange
    //     Mock<IProject> ProjetoMock = new Mock<IProject>();
    //     ProjetoMock.Setup(p => p.IsInside(It.IsAny<DateOnly>(), It.IsAny<DateOnly>())).Returns(false);

    //     ProjetoMock.Setup(c => c.IsFinished()).Returns(false); 

    //     Mock<IColaborator> ColaboradorMock = new Mock<IColaborator>();
    //     ColaboradorMock.Setup(c => c.IsInside(It.IsAny<DateTime>(), It.IsAny<DateTime>())).Returns(true);

    //     //assert
    //     ArgumentException exception = Assert.Throws<ArgumentException>(() =>
    //         //act
    //         new AssociationProjectColaborator(dataInicio, dataFim, ColaboradorMock.Object, ProjetoMock.Object));

    //     Assert.Equal("Invalid Arguments", exception.Message);
    // }

    // [Fact]
    // public void CriarAssociacaoProjetoColaborador_ProjetoJaTerminado_Exception(){
    //     //arrange
    //     DateOnly dataInicio = DateOnly.FromDateTime(DateTime.Now.AddYears(-2));
    //     DateOnly dataFim = DateOnly.FromDateTime(DateTime.Now.AddYears(-1));

    //     Mock<IProject> ProjetoMock = new Mock<IProject>();
    //     ProjetoMock.Setup(p => p.IsInside(It.IsAny<DateOnly>(), It.IsAny<DateOnly>())).Returns(true);

    //     ProjetoMock.Setup(c => c.IsFinished()).Returns(true); 

    //     Mock<IColaborator> ColaboradorMock = new Mock<IColaborator>();
    //     ColaboradorMock.Setup(c => c.IsInside(It.IsAny<DateTime>(), It.IsAny<DateTime>())).Returns(true);

    //     //assert
    //     ArgumentException exception = Assert.Throws<ArgumentException>(() =>
    //         //act
    //         new AssociationProjectColaborator(dataInicio, dataFim, ColaboradorMock.Object, ProjetoMock.Object));

    //     Assert.Equal("Invalid Arguments", exception.Message);
    // }

    // public static IEnumerable<object[]> GetAssociacaoProjetoColaboradorData_DataColaboradorInvalidaAssociacao()
    // {
    //     yield return new object[] { 
    //         DateOnly.FromDateTime(DateTime.Now), DateOnly.FromDateTime(DateTime.Now.AddDays(1))
    // }

    // [Theory]
    // [MemberData(nameof(GetAssociacaoProjetoColaboradorData_DataColaboradorInvalidaAssociacao))]
    // public void CriarAssociacaoProjetoColaborador_DataColaboradorInvalidaAssociacao_Exception(
    //                 DateOnly dataInicio, DateOnly dataFim,
    //                 int projetoCompareDataInicio,
    //                 int projetoCompareDataFim,
    //                 int projetoCompareDataHoje,
    //                 int colaboradorCompareDataInicio,
    //                 int colaboradorCompareDataFim){
    //     //arrange
    //     Mock<IProject> ProjetoMock = new Mock<IProject>();
    //     ProjetoMock.Setup(p => p.CompareWithDataInicio(dataInicio)).Returns(projetoCompareDataInicio);
    //     ProjetoMock.Setup(p => p.CompareWithDataFim(dataFim)).Returns(projetoCompareDataFim);

    //     ProjetoMock.Setup(c => c.CompareWithDataFim(DateOnly.FromDateTime(DateTime.Today))).Returns(projetoCompareDataHoje); 


    //     Mock<IColaborator> ColaboradorMock = new Mock<IColaborator>();
    //     ColaboradorMock.Setup(c => c.CompareWithDataInicio(dataInicio.ToDateTime(TimeOnly.MinValue))).Returns(colaboradorCompareDataInicio); 
    //     ColaboradorMock.Setup(c => c.CompareWithDataFim(dataFim.ToDateTime(TimeOnly.MinValue))).Returns(colaboradorCompareDataFim); 

    //     //assert
    //     ArgumentException exception = Assert.Throws<ArgumentException>(() =>
    //         //act
    //         new AssociationProjectColaborator(dataInicio, dataFim, ColaboradorMock.Object, ProjetoMock.Object));

    //     Assert.Equal("Invalid Arguments", exception.Message);
    // }
}