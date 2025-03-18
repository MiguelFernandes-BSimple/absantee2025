using Domain;
using Moq;

public class PlanoFeriasTest{

    [Fact]
    public void CriarPlanoFerias_CamposValidos(){
        //arrange
        Mock<IPeriodoFerias> periodoFerias1 = new Mock<IPeriodoFerias>();
        Mock<IPeriodoFerias> periodoFerias2 = new Mock<IPeriodoFerias>();

        var periodoFeriasList = new List<IPeriodoFerias> { periodoFerias1.Object, periodoFerias2.Object };

        periodoFerias1.Setup(pf => pf.PeriodoFeriasOverlap(It.IsAny<IPeriodoFerias>())).Returns(false);

        Mock<IColaborador> ColaboradorMock = new Mock<IColaborador>();
        ColaboradorMock.Setup(c => c.CompareWithDataInicio(It.IsAny<DateTime>())).Returns(1); 
        ColaboradorMock.Setup(c => c.CompareWithDataFim(It.IsAny<DateTime>())).Returns(-1); 

        //act
        new PlanoFerias(periodoFeriasList, ColaboradorMock.Object);

        //assert
    }

    [Fact]
    public void CriarPlanoFerias_PeriodoFeriasOverlap(){
        //arrange
        Mock<IPeriodoFerias> periodoFerias1 = new Mock<IPeriodoFerias>();
        Mock<IPeriodoFerias> periodoFerias2 = new Mock<IPeriodoFerias>();

        var periodoFeriasList = new List<IPeriodoFerias> { periodoFerias1.Object, periodoFerias2.Object };

        periodoFerias1.Setup(pf => pf.PeriodoFeriasOverlap(It.IsAny<IPeriodoFerias>())).Returns(true);

        Mock<IColaborador> ColaboradorMock = new Mock<IColaborador>();
        ColaboradorMock.Setup(c => c.CompareWithDataInicio(It.IsAny<DateTime>())).Returns(1); 
        ColaboradorMock.Setup(c => c.CompareWithDataFim(It.IsAny<DateTime>())).Returns(-1); 

        //assert
        ArgumentException exception = Assert.Throws<ArgumentException>(() =>
            //act
            new PlanoFerias(periodoFeriasList, ColaboradorMock.Object));
        
        Assert.Equal("Invalid Arguments", exception.Message);
    }

    
    public static IEnumerable<object[]> GetPlanoFeriasData_DataColaboradorInvalidaPeriodoFerias()
    {
        yield return new object[] { -1, -1 }; //Data Inicio Associacao > Data Inicio Colaborador
        yield return new object[] { 1, 1 }; //Data Fim Associacao > Data Fim Colaborador
        yield return new object[] { -1, 1 }; //Data Inicio Associacao > Data Inicio Colaborador && Data Fim Associacao > Data Fim Colaborador
    }
    [Theory]
    [MemberData(nameof(GetPlanoFeriasData_DataColaboradorInvalidaPeriodoFerias))]

    public void CriarPlanoFerias_DataColaboradorInvalidaPeriodoFerias_Exception(
        int colaboradorCompareDataInicio,
        int colaboradorCompareDataFim
    ){
        //arrange
        Mock<IPeriodoFerias> periodoFerias1 = new Mock<IPeriodoFerias>();
        Mock<IPeriodoFerias> periodoFerias2 = new Mock<IPeriodoFerias>();

        var periodoFeriasList = new List<IPeriodoFerias> { periodoFerias1.Object, periodoFerias2.Object };

        periodoFerias1.Setup(pf => pf.PeriodoFeriasOverlap(It.IsAny<IPeriodoFerias>())).Returns(false);

        Mock<IColaborador> ColaboradorMock = new Mock<IColaborador>();
        ColaboradorMock.Setup(c => c.CompareWithDataInicio(It.IsAny<DateTime>())).Returns(colaboradorCompareDataInicio); 
        ColaboradorMock.Setup(c => c.CompareWithDataFim(It.IsAny<DateTime>())).Returns(colaboradorCompareDataFim); 

        //assert
        ArgumentException exception = Assert.Throws<ArgumentException>(() =>
            //act
            new PlanoFerias(periodoFeriasList, ColaboradorMock.Object));
        
        Assert.Equal("Invalid Arguments", exception.Message);
    }
}