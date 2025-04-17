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
    public class HRManagerGetUserIdTests
    {
        [Fact]
        public void WhenGettingUserId_ThenReturnsUserId()
        {
            //arrange
            var userId = 1;
            var hrManager = new HRManager(It.IsAny<long>(), userId, It.IsAny<PeriodDateTime>());
            //act
            var hrUserId = hrManager.GetUserId();

            //assert
            Assert.Equal(userId, hrUserId);
        }
    }
}
