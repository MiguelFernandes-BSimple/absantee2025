using Domain;
using Moq;

public class PlanoFeriasTest{

    // [Fact]
    // public void CriarPlanoFerias_ListaPeriodoFerias_CamposValidos(){
    //     //arrange
    //     Mock<IHolidayPeriod> periodoFerias1 = new Mock<IHolidayPeriod>();
    //     Mock<IHolidayPeriod> periodoFerias2 = new Mock<IHolidayPeriod>();

    //     var periodoFeriasList = new List<IHolidayPeriod> { periodoFerias1.Object, periodoFerias2.Object };

    //     periodoFerias1.Setup(pf => pf.HolidayPeriodOverlap(It.IsAny<IHolidayPeriod>())).Returns(false);

    //     Mock<IColaborator> ColaboradorMock = new Mock<IColaborator>();
    //     ColaboradorMock.Setup(c => c.CompareWithDataInicio(It.IsAny<DateTime>())).Returns(1); 
    //     ColaboradorMock.Setup(c => c.CompareWithDataFim(It.IsAny<DateTime>())).Returns(-1); 

    //     //act
    //     new HolidaysPlan(periodoFeriasList, ColaboradorMock.Object);

    //     //assert
    // }

    // [Fact]
    // public void CriarPlanoFerias_UnicoPeriodoFerias_CamposValidos(){
    //     //arrange
    //     Mock<IHolidayPeriod> periodoFerias1 = new Mock<IHolidayPeriod>();

    //     periodoFerias1.Setup(pf => pf.HolidayPeriodOverlap(It.IsAny<IHolidayPeriod>())).Returns(false);

    //     Mock<IColaborator> ColaboradorMock = new Mock<IColaborator>();
    //     ColaboradorMock.Setup(c => c.CompareWithDataInicio(It.IsAny<DateTime>())).Returns(1); 
    //     ColaboradorMock.Setup(c => c.CompareWithDataFim(It.IsAny<DateTime>())).Returns(-1); 

    //     //act
    //     new HolidaysPlan(periodoFerias1.Object, ColaboradorMock.Object);

    //     //assert
    // }

    // [Fact]
    // public void CriarPlanoFerias_PeriodoFeriasOverlap(){
    //     //arrange
    //     Mock<IHolidayPeriod> periodoFerias1 = new Mock<IHolidayPeriod>();
    //     Mock<IHolidayPeriod> periodoFerias2 = new Mock<IHolidayPeriod>();

    //     var periodoFeriasList = new List<IHolidayPeriod> { periodoFerias1.Object, periodoFerias2.Object };

    //     periodoFerias1.Setup(pf => pf.HolidayPeriodOverlap(It.IsAny<IHolidayPeriod>())).Returns(true);

    //     Mock<IColaborator> ColaboradorMock = new Mock<IColaborator>();
    //     ColaboradorMock.Setup(c => c.CompareWithDataInicio(It.IsAny<DateTime>())).Returns(1); 
    //     ColaboradorMock.Setup(c => c.CompareWithDataFim(It.IsAny<DateTime>())).Returns(-1); 

    //     //assert
    //     ArgumentException exception = Assert.Throws<ArgumentException>(() =>
    //         //act
    //         new HolidaysPlan(periodoFeriasList, ColaboradorMock.Object));
        
    //     Assert.Equal("Invalid Arguments", exception.Message);
    // }

    
    // public static IEnumerable<object[]> GetPlanoFeriasData_DataColaboradorInvalidaPeriodoFerias()
    // {
    //     yield return new object[] { -1, -1 }; //Data Inicio Associacao > Data Inicio Colaborador
    //     yield return new object[] { 1, 1 }; //Data Fim Associacao > Data Fim Colaborador
    //     yield return new object[] { -1, 1 }; //Data Inicio Associacao > Data Inicio Colaborador && Data Fim Associacao > Data Fim Colaborador
    // }
    // [Theory]
    // [MemberData(nameof(GetPlanoFeriasData_DataColaboradorInvalidaPeriodoFerias))]

    // public void CriarPlanoFerias_DataColaboradorInvalidaPeriodoFerias_Exception(
    //     int colaboradorCompareDataInicio,
    //     int colaboradorCompareDataFim
    // ){
    //     //arrange
    //     Mock<IHolidayPeriod> periodoFerias1 = new Mock<IHolidayPeriod>();
    //     Mock<IHolidayPeriod> periodoFerias2 = new Mock<IHolidayPeriod>();

    //     var periodoFeriasList = new List<IHolidayPeriod> { periodoFerias1.Object, periodoFerias2.Object };

    //     periodoFerias1.Setup(pf => pf.HolidayPeriodOverlap(It.IsAny<IHolidayPeriod>())).Returns(false);

    //     Mock<IColaborator> ColaboradorMock = new Mock<IColaborator>();
    //     ColaboradorMock.Setup(c => c.CompareWithDataInicio(It.IsAny<DateTime>())).Returns(colaboradorCompareDataInicio); 
    //     ColaboradorMock.Setup(c => c.CompareWithDataFim(It.IsAny<DateTime>())).Returns(colaboradorCompareDataFim); 

    //     //assert
    //     ArgumentException exception = Assert.Throws<ArgumentException>(() =>
    //         //act
    //         new HolidaysPlan(periodoFeriasList, ColaboradorMock.Object));
        
    //     Assert.Equal("Invalid Arguments", exception.Message);
    // }

    // [Fact]
    // public void AdicionarPlanoFerias_Sucesso(){
    //     //arrange
    //     Mock<IHolidayPeriod> periodoFerias1 = new Mock<IHolidayPeriod>();
    //     Mock<IHolidayPeriod> periodoFerias2 = new Mock<IHolidayPeriod>();

    //     Mock<IHolidayPeriod> periodoFeriasAdicionar = new Mock<IHolidayPeriod>();

    //     var periodoFeriasList = new List<IHolidayPeriod> { periodoFerias1.Object, periodoFerias2.Object };

    //     periodoFeriasAdicionar.Setup(pf => pf.HolidayPeriodOverlap(It.IsAny<IHolidayPeriod>())).Returns(false);

    //     Mock<IColaborator> ColaboradorMock = new Mock<IColaborator>();
    //     ColaboradorMock.Setup(c => c.CompareWithDataInicio(It.IsAny<DateTime>())).Returns(1); 
    //     ColaboradorMock.Setup(c => c.CompareWithDataFim(It.IsAny<DateTime>())).Returns(-1); 

    //     HolidaysPlan planoFerias = new HolidaysPlan(periodoFeriasList, ColaboradorMock.Object);
    //     //act
    //     bool result = planoFerias.AddHolidayPeriod(periodoFeriasAdicionar.Object);       

    //     //assert
    //     Assert.True(result);
    //     Assert.True(planoFerias.IsSizeList(3));
    // }

    // [Fact]
    // public void AdicionarPlanoFerias_PeriodoFeriasOverlap_ReturnFalse(){
    //     //arrange
    //     Mock<IHolidayPeriod> periodoFerias1 = new Mock<IHolidayPeriod>();
    //     Mock<IHolidayPeriod> periodoFerias2 = new Mock<IHolidayPeriod>();

    //     Mock<IHolidayPeriod> periodoFeriasAdicionar = new Mock<IHolidayPeriod>();

    //     var periodoFeriasList = new List<IHolidayPeriod> { periodoFerias1.Object, periodoFerias2.Object };

    //     periodoFerias1.Setup(pf => pf.HolidayPeriodOverlap(It.IsAny<IHolidayPeriod>())).Returns(false);
    //     periodoFeriasAdicionar.Setup(pf => pf.HolidayPeriodOverlap(periodoFerias1.Object)).Returns(true);

    //     Mock<IColaborator> ColaboradorMock = new Mock<IColaborator>();
    //     ColaboradorMock.Setup(c => c.CompareWithDataInicio(It.IsAny<DateTime>())).Returns(1); 
    //     ColaboradorMock.Setup(c => c.CompareWithDataFim(It.IsAny<DateTime>())).Returns(-1); 

    //     HolidaysPlan planoFerias = new HolidaysPlan(periodoFeriasList, ColaboradorMock.Object);
    //     //act
    //     bool result = planoFerias.AddHolidayPeriod(periodoFeriasAdicionar.Object);       

    //     //assert
    //     Assert.False(result);
    //     Assert.True(planoFerias.IsSizeList(2));
    // }

    // [Theory]
    // [MemberData(nameof(GetPlanoFeriasData_DataColaboradorInvalidaPeriodoFerias))]
    // public void AdicionarPlanoFerias_DataColaboradorInvalidaPeriodoFerias_ReturnFalse(
    //     int colaboradorCompareDataInicio,
    //     int colaboradorCompareDataFim
    // ){
    //     //arrange
    //     Mock<IHolidayPeriod> periodoFerias1 = new Mock<IHolidayPeriod>();

    //     Mock<IHolidayPeriod> periodoFeriasAdicionar = new Mock<IHolidayPeriod>();

    //     var periodoFeriasList = new List<IHolidayPeriod> { periodoFerias1.Object };

    //     periodoFeriasAdicionar.Setup(pf => pf.HolidayPeriodOverlap(It.IsAny<IHolidayPeriod>())).Returns(false);

    //     Mock<IColaborator> ColaboradorMock = new Mock<IColaborator>();
    //     ColaboradorMock.SetupSequence(c => c.CompareWithDataInicio(
    //                                     It.IsAny<DateTime>()))
    //                                     .Returns(1) //Returns true in the constructor
    //                                     .Returns(colaboradorCompareDataInicio);
    //     ColaboradorMock.SetupSequence(c => c.CompareWithDataFim(
    //                                     It.IsAny<DateTime>()))
    //                                     .Returns(-1) //Returns true in the constructor
    //                                     .Returns(colaboradorCompareDataFim);

    //     HolidaysPlan planoFerias = new HolidaysPlan(periodoFeriasList, ColaboradorMock.Object);
    //     //act
    //     bool result = planoFerias.AddHolidayPeriod(periodoFeriasAdicionar.Object);       

    //     //assert
    //     Assert.False(result);
    //     Assert.True(planoFerias.IsSizeList(1));
    // }
}