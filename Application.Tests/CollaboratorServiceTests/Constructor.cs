using Infrastructure.Interfaces;
using Application.Services;
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
