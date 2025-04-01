using Domain;
namespace Domain.Tests.ProjectTests;

public class ContainsDates
{
    public static IEnumerable<object[]> ContainsDates_ValidCompareData()
    {
        yield return new object[] { DateOnly.FromDateTime(DateTime.Now), DateOnly.FromDateTime(DateTime.Now).AddYears(1) };
        yield return new object[] { DateOnly.FromDateTime(DateTime.Now).AddDays(1), DateOnly.FromDateTime(DateTime.Now).AddDays(10) };
    }

    [Theory]
    [MemberData(nameof(ContainsDates_ValidCompareData))]
    public void WhenPassingValidData_ThenContainsDatesReturnTrue(DateOnly InitDate, DateOnly FinalDate)
    {
        //arrange
        DateOnly ProjectInitDate = DateOnly.FromDateTime(DateTime.Now);
        DateOnly ProjectFinalDate = DateOnly.FromDateTime(DateTime.Now).AddYears(1);
        Project projeto = new Project("Titulo 1", "T1", ProjectInitDate, ProjectFinalDate);

        //act
        bool result = projeto.ContainsDates(InitDate, FinalDate);

        //assert
        Assert.True(result);
    }
    public static IEnumerable<object[]> ContainsDates_InvalidCompareData()
    {
        yield return new object[] { DateOnly.FromDateTime(DateTime.Now.AddDays(-1)), DateOnly.FromDateTime(DateTime.Now.AddDays(10)) };
        yield return new object[] { DateOnly.FromDateTime(DateTime.Now.AddDays(1)), DateOnly.FromDateTime(DateTime.Now.AddYears(2)) };
    }

    [Theory]
    [MemberData(nameof(ContainsDates_InvalidCompareData))]
    public void WhenPassingInvalidData_ThenContainsDatesReturnFalse(DateOnly InitDate, DateOnly FinalDate)
    {
        //arrange
        DateOnly ProjectInitDate = DateOnly.FromDateTime(DateTime.Now);
        DateOnly ProjectFinalDate = DateOnly.FromDateTime(DateTime.Now.AddYears(1));
        Project project = new Project("Titulo 1", "T1", ProjectInitDate, ProjectFinalDate);

        //act
        bool result = project.ContainsDates(InitDate, FinalDate);

        //assert
        Assert.False(result);
    }
}
