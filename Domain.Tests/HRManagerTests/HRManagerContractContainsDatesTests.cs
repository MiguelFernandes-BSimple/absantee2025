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
            Mock<PeriodDateTime> periodDateTime = new Mock<PeriodDateTime>();
            periodDateTime.Setup(p => p.Contains(It.IsAny<PeriodDateTime>())).Returns(true);

            var hrManager = new HRManager(It.IsAny<long>(), periodDateTime.Object); 
            //act
            var result = hrManager.ContractContainsDates(It.IsAny<PeriodDateTime>());

            //assert
            Assert.True(result);
        }

        [Fact]
        public void WhenPeriodDateTimeNotInsideContract_ThenReturnsFalse()
        {
            //arrange
            Mock<PeriodDateTime> periodDateTime = new Mock<PeriodDateTime>();
            periodDateTime.Setup(p => p.Contains(It.IsAny<PeriodDateTime>())).Returns(false);

            var hrManager = new HRManager(It.IsAny<long>(), periodDateTime.Object);
            //act
            var result = hrManager.ContractContainsDates(It.IsAny<PeriodDateTime>());

            //assert
            Assert.False(result);
        }
    }
}
