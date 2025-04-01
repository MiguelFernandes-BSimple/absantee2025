using Domain;

namespace Domain.Tests;

public class DataMin
{
    public static IEnumerable<object[]> DatesForMinDate()
    {
        yield return new object[] { new DateOnly(2025, 04, 02), new DateOnly(2025, 04, 01), new DateOnly(2025, 04, 01) };
        yield return new object[] { new DateOnly(2025, 04, 02), new DateOnly(2025, 04, 02), new DateOnly(2025, 04, 02) };
        yield return new object[] { new DateOnly(2025, 04, 02), new DateOnly(2025, 04, 10), new DateOnly(2025, 04, 02) };
    }

    [Theory]
    [MemberData(nameof(DatesForMinDate))]
    public void WhenPassing2ValidDates_ThenReturnDataMin(DateOnly iniDate, DateOnly endDate, DateOnly expected)
    {
        //arrange

        //act
        DateOnly result = Utils.DataMin(iniDate, endDate);

        //assert
        Assert.Equal(expected, result);
    }
}