using Domain;

public class ProjectTest{
    public static IEnumerable<object[]> ProjectData_ValidData()
    {
        yield return new object[] { "Titulo 1", "T1", DateOnly.FromDateTime(DateTime.Now), DateOnly.FromDateTime(DateTime.Now.AddDays(1)) };
        yield return new object[] { "Projeto_Academia_2024", "PA2024", DateOnly.FromDateTime(DateTime.Now.AddYears(1)), DateOnly.FromDateTime(DateTime.Now.AddYears(3)) };
        yield return new object[] { "Projeto-Academia-2024(2)", "PA2024V2",DateOnly.FromDateTime(DateTime.Now.AddYears(-2)), DateOnly.FromDateTime(DateTime.Now.AddYears(1)) };
        yield return new object[] { "Projeto-Academia-2024(3)", "PA2024V3",DateOnly.FromDateTime(DateTime.Now.AddYears(1)), DateOnly.FromDateTime(DateTime.Now.AddYears(1)) };
    }

    [Theory]
    [MemberData(nameof(ProjectData_ValidData))]
    public void WhenPassingValidData_ProjectIsCreated(string Title, string Acronym, DateOnly InitData, DateOnly FinalDate){
        //arrange

        //act
        new Project(Title, Acronym, InitData, FinalDate);

        //assert
    }

    public static IEnumerable<object[]> ProjectData_InvalidTitle()
    {
        yield return new object[] { "", "T1", DateOnly.FromDateTime(DateTime.Now), DateOnly.FromDateTime(DateTime.Now.AddDays(1)) };
        yield return new object[] { new string('a', 51), "T1", DateOnly.FromDateTime(DateTime.Now.AddYears(1)), DateOnly.FromDateTime(DateTime.Now.AddYears(3)) };
    }

    [Theory]
    [MemberData(nameof(ProjectData_InvalidTitle))]
    public void WhenPassingInvalidTitle_ThrowException(string Title, string Acronym, DateOnly InitDate, DateOnly FinalDate){
        //arrange

        //assert
        ArgumentException exception = Assert.Throws<ArgumentException>(() =>
            //act
            new Project(Title, Acronym, InitDate, FinalDate));

        Assert.Equal("Invalid Arguments", exception.Message);
    }

    public static IEnumerable<object[]> ProjectData_InvalidAcronym()
    {
        yield return new object[] { "Titulo 1", "T_1", DateOnly.FromDateTime(DateTime.Now), DateOnly.FromDateTime(DateTime.Now.AddDays(1)) };
        yield return new object[] { "Projeto_Academia_2024", "", DateOnly.FromDateTime(DateTime.Now.AddYears(1)), DateOnly.FromDateTime(DateTime.Now.AddYears(3)) };
        yield return new object[] { "Projeto_Academia_2024_", new string('A', 11), DateOnly.FromDateTime(DateTime.Now.AddYears(1)), DateOnly.FromDateTime(DateTime.Now.AddYears(3)) };
        yield return new object[] { "Projeto_Academia_2024_", "pa2024", DateOnly.FromDateTime(DateTime.Now.AddYears(1)), DateOnly.FromDateTime(DateTime.Now.AddYears(3)) };
    }

    [Theory]
    [MemberData(nameof(ProjectData_InvalidAcronym))]
    public void WhenPassingInvalidAcronym_ThrowException(string Title, string Acronym, DateOnly InitDate, DateOnly FinalDate){
        //arrange

        //assert
        ArgumentException exception = Assert.Throws<ArgumentException>(() =>
            //act
            new Project(Title, Acronym, InitDate, FinalDate));

        Assert.Equal("Invalid Arguments", exception.Message);
    }


    [Fact]
    public void WhenPassingInvalidProjectDates_ThrowException(){
        //arrange
        string Title = "Titulo 1";
        string Acronym = "T1";
        DateOnly InitDate = DateOnly.FromDateTime(DateTime.Today);
        DateOnly FinalDate = DateOnly.FromDateTime(DateTime.Today).AddDays(-1);

        //assert
        ArgumentException exception = Assert.Throws<ArgumentException>(() =>
            //act
            new Project(Title, Acronym, InitDate, FinalDate));

        Assert.Equal("Invalid Arguments", exception.Message);
    }

    public static IEnumerable<object[]> IsInside_ValidCompareData()
    {
        yield return new object[] { DateOnly.FromDateTime(DateTime.Now), DateOnly.FromDateTime(DateTime.Now).AddYears(1) };
        yield return new object[] { DateOnly.FromDateTime(DateTime.Now).AddDays(1), DateOnly.FromDateTime(DateTime.Now).AddDays(10) };
    }

    [Theory]
    [MemberData(nameof(IsInside_ValidCompareData))]
    public void WhenPassingValidData_IsInside_ReturnTrue(DateOnly InitDate, DateOnly FinalDate){
        //arrange
        DateOnly ProjectInitDate = DateOnly.FromDateTime(DateTime.Now);
        DateOnly ProjectFinalDate = DateOnly.FromDateTime(DateTime.Now).AddYears(1);
        Project projeto = new Project("Titulo 1", "T1", ProjectInitDate, ProjectFinalDate);

        //act
        bool result = projeto.IsInside(InitDate, FinalDate);

        //assert
        Assert.True(result);
    }

    public static IEnumerable<object[]> IsInside_InvalidCompareData()
    {
        yield return new object[] { DateOnly.FromDateTime(DateTime.Now.AddDays(-1)), DateOnly.FromDateTime(DateTime.Now.AddDays(10)) };
        yield return new object[] { DateOnly.FromDateTime(DateTime.Now.AddDays(1)), DateOnly.FromDateTime(DateTime.Now.AddYears(2)) };
    }

    [Theory]
    [MemberData(nameof(IsInside_InvalidCompareData))]
    public void WhenPassingInvalidData_IsInside_ReturnFalse(DateOnly InitDate, DateOnly FinalDate){
        //arrange
        DateOnly ProjectInitDate = DateOnly.FromDateTime(DateTime.Now);
        DateOnly ProjectFinalDate = DateOnly.FromDateTime(DateTime.Now.AddYears(1));
        Project project = new Project("Titulo 1", "T1", ProjectInitDate, ProjectFinalDate);

        //act
        bool result = project.IsInside(InitDate, FinalDate);

        //assert
        Assert.False(result);
    }


    [Fact]
    public void WhenProjectIsFinished_ReturnTrue(){
        //arrange
        DateOnly ProjectInitDate = DateOnly.FromDateTime(DateTime.Now).AddYears(-1);
        DateOnly ProjectFinalDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-1));
        Project project = new Project("Titulo 1", "T1", ProjectInitDate, ProjectFinalDate);

        //act
        bool result = project.IsFinished();

        //assert
        Assert.True(result);
    }

    public static IEnumerable<object[]> ProjectEndDate_NotFinished()
    {
        yield return new object[] { DateOnly.FromDateTime(DateTime.Now) };
        yield return new object[] { DateOnly.FromDateTime(DateTime.Now.AddDays(1)) };
    }
    [Theory]
    [MemberData(nameof(ProjectEndDate_NotFinished))]
    public void WhenProjectIsNotFinished_ReturnFalse(DateOnly ProjectFinalDate){
        //arrange
        DateOnly ProjectInitDate = DateOnly.FromDateTime(DateTime.Now).AddYears(-1);
        Project project = new Project("Titulo 1", "T1", ProjectInitDate, ProjectFinalDate);

        //act
        bool result = project.IsFinished();

        //assert
        Assert.False(result);
    }
}