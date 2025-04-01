using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Moq;

namespace Application.Tests.CollaboratorServiceTests
{
    public class Constructor
    {
        [Fact]
        public void WhenCreatingWithValidParameters_ThenObjectIsCreated()
        {
            //arrange
            Mock<IHolidayPlanRepository> holidayPlanRepositoryDouble = new Mock<IHolidayPlanRepository>();
            Mock<IAssociationProjectCollaboratorRepository> assocRepoMock = new Mock<IAssociationProjectCollaboratorRepository>();

            //act & assert
            new CollaboratorService(assocRepoMock.Object, holidayPlanRepositoryDouble.Object);

        }
    }
}
