using Domain.IRepository;
using Application.Services;
using Moq;
using Domain.IRepository;

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
            Mock<ICollaboratorRepository> collabRepository = new Mock<ICollaboratorRepository>();

            //act & assert
            new CollaboratorService(assocRepoMock.Object, holidayPlanRepositoryDouble.Object, collabRepository.Object);

        }
    }
}
