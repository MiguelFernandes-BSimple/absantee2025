using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Factory;
using Domain.Interfaces;
using Domain.IRepository;
using Domain.Models;
using Moq;

namespace Domain.Tests.CollaboratorTests
{
    public class CollaboratorConstructorTests
    {
        [Fact]
        public void WhenCreatingCollaboratorWithValidUserIdAndPeriod_ThenCollaboratorIsCreatedCorrectly()
        {
            //arrange

            //act
            new Collaborator(It.IsAny<long>(), It.IsAny<PeriodDateTime>());

            //assert
        }

        [Fact]
        public void WhenCreatingCollaboratorWithValidIdAndUserIdAndPeriod_ThenCollaboratorIsCreatedCorrectly()
        {
            //arrange

            //act
            new Collaborator(It.IsAny<long>(), It.IsAny<long>(), It.IsAny<PeriodDateTime>());

            //assert
        }
    }
}
