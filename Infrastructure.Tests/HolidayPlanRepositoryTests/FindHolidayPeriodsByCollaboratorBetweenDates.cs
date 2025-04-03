using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Interfaces;
using Infrastructure.Repositories;
using Moq;

namespace Infrastructure.Tests.HolidayPlanRepositoryTests
{
    public class FindHolidayPeriodsByCollaboratorBetweenDates
    {
        [Fact]
        public void WhenPassingCorrectData_ThenReturnsPeriodsByCollaboratorBetweenDates()
        {
            // arrange
            var collabDouble = new Mock<ICollaborator>();

            var holidayPeriodDouble = new Mock<IHolidayPeriod>();
            holidayPeriodDouble.Setup(hpd => hpd.Intersects(It.IsAny<IPeriodDate>())).Returns(true);

            var expectedPeriod = new List<IHolidayPeriod> { holidayPeriodDouble.Object };


            var holidayPlanDouble = new Mock<IHolidayPlan>();
            holidayPlanDouble.Setup(hpd => hpd.HasCollaborator(collabDouble.Object)).Returns(true);
            holidayPlanDouble.Setup(hpd => hpd.GetHolidayPeriodsBetweenPeriod(It.IsAny<IPeriodDate>())).Returns(expectedPeriod);


            var hprepo = new HolidayPlanRepository(new List<IHolidayPlan> { holidayPlanDouble.Object });

            // act
            var result = hprepo.FindHolidayPeriodsByCollaboratorBetweenDates(collabDouble.Object, It.IsAny<IPeriodDate>());

            // assert
            Assert.Equal(result, expectedPeriod);
        }

        [Fact]
        public void WhenPassingCorrectData_ThenReturnsEmptyList()
        {
            // arrange
            var collabDouble = new Mock<ICollaborator>();

            var holidayPeriodDouble = new Mock<IHolidayPeriod>();
            holidayPeriodDouble.Setup(hpd => hpd.Intersects(It.IsAny<IPeriodDate>())).Returns(true);

            var expectedPeriod = new List<IHolidayPeriod>();

            var holidayPlanDouble = new Mock<IHolidayPlan>();
            holidayPlanDouble.Setup(hpd => hpd.HasCollaborator(collabDouble.Object)).Returns(false);


            var hprepo = new HolidayPlanRepository(new List<IHolidayPlan> { holidayPlanDouble.Object });

            // act
            var result = hprepo.FindHolidayPeriodsByCollaboratorBetweenDates(collabDouble.Object, It.IsAny<IPeriodDate>());

            // assert
            Assert.Equal(result, expectedPeriod);
        }
    }
}