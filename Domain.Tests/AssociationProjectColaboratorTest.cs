using Domain;
using Moq;

public class AssociationProjectColaboratorTest{

    public static IEnumerable<object[]> ValidDates()
    {
        yield return new object[] { DateOnly.FromDateTime(DateTime.Now), DateOnly.FromDateTime(DateTime.Now).AddYears(1) };
        yield return new object[] { DateOnly.FromDateTime(DateTime.Today), DateOnly.FromDateTime(DateTime.Today) };
    }

    [Theory]
    [MemberData(nameof(ValidDates))]
    public void WhenPassingValidData_ThenAssociationProjectColaboratorIsCreated(DateOnly initDate, DateOnly finalDate){
        //arrange
        Mock<IProject> ProjetoMock = new Mock<IProject>();
        ProjetoMock.Setup(p => p.IsInside(It.IsAny<DateOnly>(), It.IsAny<DateOnly>())).Returns(true);

        ProjetoMock.Setup(c => c.IsFinished()).Returns(false);

        Mock<IColaborator> ColaboradorMock = new Mock<IColaborator>();
        ColaboradorMock.Setup(c => c.IsInside(It.IsAny<DateTime>(), It.IsAny<DateTime>())).Returns(true);

        //act
        new AssociationProjectColaborator(initDate, finalDate, ColaboradorMock.Object, ProjetoMock.Object);

        //assert
    }


    [Fact]
    public void WhenAssociationDatesAreInvalid_ThenThrowException(){
        //arrange
        DateOnly initDate = DateOnly.FromDateTime(DateTime.Now).AddYears(2);
        DateOnly finalDate = DateOnly.FromDateTime(DateTime.Now).AddYears(1);
        Mock<IProject> ProjetoMock = new Mock<IProject>();

        Mock<IColaborator> ColaboradorMock = new Mock<IColaborator>();

        //assert
        ArgumentException exception = Assert.Throws<ArgumentException>(() =>
            //act
            new AssociationProjectColaborator(initDate, finalDate, ColaboradorMock.Object, ProjetoMock.Object));

        Assert.Equal("Invalid Arguments", exception.Message);
    }


    [Fact]
    public void WhenProjectDatesOutsideAssociation_ThenThrowException(){
        //arrange
        DateOnly initDate = DateOnly.FromDateTime(DateTime.Now);
        DateOnly finalDate = DateOnly.FromDateTime(DateTime.Now).AddYears(1);

        Mock<IProject> ProjetoMock = new Mock<IProject>();
        ProjetoMock.Setup(p => p.IsInside(It.IsAny<DateOnly>(), It.IsAny<DateOnly>())).Returns(false);

        ProjetoMock.Setup(c => c.IsFinished()).Returns(false); 

        Mock<IColaborator> ColaboradorMock = new Mock<IColaborator>();
        ColaboradorMock.Setup(c => c.IsInside(It.IsAny<DateTime>(), It.IsAny<DateTime>())).Returns(true);

        //assert
        ArgumentException exception = Assert.Throws<ArgumentException>(() =>
            //act
            new AssociationProjectColaborator(initDate, finalDate, ColaboradorMock.Object, ProjetoMock.Object));

        Assert.Equal("Invalid Arguments", exception.Message);
    }

    [Fact]
    public void WhenProjectIsFinished_ThenThrowException(){
        //arrange
        DateOnly dataInicio = DateOnly.FromDateTime(DateTime.Now);
        DateOnly dataFim = DateOnly.FromDateTime(DateTime.Now.AddYears(1));

        Mock<IProject> ProjetoMock = new Mock<IProject>();
        ProjetoMock.Setup(p => p.IsInside(It.IsAny<DateOnly>(), It.IsAny<DateOnly>())).Returns(true);

        ProjetoMock.Setup(c => c.IsFinished()).Returns(true); 

        Mock<IColaborator> ColaboradorMock = new Mock<IColaborator>();
        ColaboradorMock.Setup(c => c.IsInside(It.IsAny<DateTime>(), It.IsAny<DateTime>())).Returns(true);

        //assert
        ArgumentException exception = Assert.Throws<ArgumentException>(() =>
            //act
            new AssociationProjectColaborator(dataInicio, dataFim, ColaboradorMock.Object, ProjetoMock.Object));

        Assert.Equal("Invalid Arguments", exception.Message);
    }

    [Fact]
    public void WhenColaboradorDatesOutsideAssociationDates_ThenThrowException(){
        //arrange
        DateOnly dataInicio = DateOnly.FromDateTime(DateTime.Now);
        DateOnly dataFim = DateOnly.FromDateTime(DateTime.Now.AddYears(1));

        Mock<IProject> ProjetoMock = new Mock<IProject>();
        ProjetoMock.Setup(p => p.IsInside(It.IsAny<DateOnly>(), It.IsAny<DateOnly>())).Returns(true);

        ProjetoMock.Setup(c => c.IsFinished()).Returns(false); 

        Mock<IColaborator> ColaboradorMock = new Mock<IColaborator>();
        ColaboradorMock.Setup(c => c.IsInside(It.IsAny<DateTime>(), It.IsAny<DateTime>())).Returns(false);


        //assert
        ArgumentException exception = Assert.Throws<ArgumentException>(() =>
            //act
            new AssociationProjectColaborator(dataInicio, dataFim, ColaboradorMock.Object, ProjetoMock.Object));

        Assert.Equal("Invalid Arguments", exception.Message);
    }
}