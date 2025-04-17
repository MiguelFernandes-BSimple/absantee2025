using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.Models;
using Moq;

namespace Domain.Tests.HRManagerTests
{
    public class HRManagerContractContainsDatesTests
    {
        [Fact]
        public void WhenPeriodDateTimeInsideContract_ThenReturnsTrue()
        {
            //arrange
            PeriodDateTime periodDateTime = new PeriodDateTime(new DateTime(2024, 10, 10), new DateTime(2024, 10, 20));
            PeriodDateTime periodDateTime2 = new PeriodDateTime(new DateTime(2024, 10, 13), new DateTime(2024, 10, 17));

            var hrManager = new HRManager(It.IsAny<long>(), periodDateTime);

            //act
            var result = hrManager.ContractContainsDates(periodDateTime2);

            //assert
            Assert.True(result);
        }

        [Fact]
        public void WhenPeriodDateTimeNotInsideContract_ThenReturnsFalse()
        {
            //arrange
            PeriodDateTime periodDateTime = new PeriodDateTime(new DateTime(2024, 10, 10), new DateTime(2024, 10, 20));
            PeriodDateTime periodDateTime2 = new PeriodDateTime(new DateTime(2024, 10, 7), new DateTime(2024, 10, 9));

            var hrManager = new HRManager(It.IsAny<long>(), periodDateTime);
            //act
            var result = hrManager.ContractContainsDates(periodDateTime2);

            //assert
            Assert.False(result);
        }
    }
}
