// using Domain.Models;
// using Domain.Interfaces;
// using Moq;

// namespace Domain.Tests.HolidayPlanTests;

// public class HolidayPlanGetNumberOfHolidayDaysBetweenTests
// {
//     public static IEnumerable<object[]> GetHolidayDaysBetweenData()
//     {
//         yield return new object[]
//         {
//             new List<int> { 3, 2 },
//             5,
//         };
//         yield return new object[]
//         {
//             new List<int> { 0 },
//             0,
//         };
//         yield return new object[]
//         {
//             new List<int> { 10, 0, 5 },
//             15,
//         };
//     }

//     [Theory]
//     [MemberData(nameof(GetHolidayDaysBetweenData))]
//     public void GetNumberOfHolidayDaysBetween_ShouldReturnCorrectSumValue(
//         List<int> daysByPeriod,
//         int expectedTotal
//     )
//     {
//         // Arrange
//         var holidayPeriods = new List<IHolidayPeriod>();

//         foreach (var days in daysByPeriod)
//         {
//             var holidayPeriodDouble = new Mock<IHolidayPeriod>();
//             holidayPeriodDouble.Setup(hp => hp._periodDate).Returns(It.IsAny<PeriodDate>());

//             holidayPeriodDouble
//                 .Setup(p =>
//                     p.GetNumberOfCommonUtilDaysBetweenPeriods(
//                         It.IsAny<PeriodDate>()
//                     )
//                 )
//                 .Returns(days);
//             holidayPeriods.Add(holidayPeriodDouble.Object);
//         }


//         var holidayPlan = new HolidayPlan(It.IsAny<long>(), holidayPeriods);

//         // Act
//         var result = holidayPlan.GetNumberOfHolidayDaysBetween(It.IsAny<PeriodDate>());

//         // Assert
//         Assert.Equal(expectedTotal, result);
//     }
// }