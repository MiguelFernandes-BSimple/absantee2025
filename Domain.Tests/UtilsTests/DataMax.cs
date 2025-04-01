using Domain;

namespace Domain.Tests.UtilsTests;

public class DataMax
{


    public static IEnumerable<object[]> DatesForMaxDate()
    {
        yield return new object[] { new DateOnly(2025, 04, 02), new DateOnly(2025, 04, 01), new DateOnly(2025, 04, 02) };
        yield return new object[] { new DateOnly(2025, 04, 02), new DateOnly(2025, 04, 02), new DateOnly(2025, 04, 02) };
        yield return new object[] { new DateOnly(2025, 04, 02), new DateOnly(2025, 04, 10), new DateOnly(2025, 04, 10) };
    }

    [Theory]
    [MemberData(nameof(DatesForMaxDate))]
    public void WhenPassing2ValidDates_ThenReturnDataMax(DateOnly iniDate, DateOnly endDate, DateOnly expected)
    {
        //arrange

        //act
        DateOnly result = Utils.DataMax(iniDate, endDate);

        //assert
        Assert.Equal(expected, result);
    }

}