using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.Models;

namespace Domain.Tests.PeriodDateTests
{
    public class PeriodDateDurationTests
    {
        [Fact]
        public void WhenDurationIsCalled_ThenReturnExpectedDurationInDays()
        {
            //arrange
            DateOnly initDate = new DateOnly(2020, 1, 1);
            DateOnly finalDate = new DateOnly(2020, 1, 10);

            PeriodDate periodDate = new PeriodDate(initDate, finalDate);

            //act
            var result = periodDate.Duration();

            //assert
            Assert.Equal(10, result);
        }
    }
}
