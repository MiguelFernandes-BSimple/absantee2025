using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Interfaces;
using Infrastructure.Repositories;
using Moq;

namespace Infrastructure.Tests.HolidayPlanRepositoryTests
{
    public class FindAllHolidayPeriodsForAllCollaboratorsBetweenDates
    {
        [Fact]
        public void WhenPassingValidData_ThenReturnsAllHolidayPeriodsForAllCollaboratorsBetweenDates()
        {
            // arrange
            var collabDouble = new Mock<ICollaborator>();
            var collabList = new List<ICollaborator> { collabDouble.Object };

            var holidayPeriodDouble = new Mock<IHolidayPeriod>();
            var expectedPeriods = new List<IHolidayPeriod> { holidayPeriodDouble.Object };

            var holidayPlanDouble = new Mock<IHolidayPlan>();
            holidayPlanDouble.Setup(hpd => hpd.GetCollaborator()).Returns(collabDouble.Object);
            holidayPlanDouble.Setup(hpd => hpd.GetHolidayPeriodsBetweenPeriod(It.IsAny<IPeriodDate>())).Returns(expectedPeriods);

            var hprepo = new HolidayPlanRepository(new List<IHolidayPlan> { holidayPlanDouble.Object });

            // act
            var result = hprepo.FindAllHolidayPeriodsForAllCollaboratorsBetweenDates(collabList, It.IsAny<IPeriodDate>());

            // assert
            Assert.Equal(result, expectedPeriods);
        }
    }
}