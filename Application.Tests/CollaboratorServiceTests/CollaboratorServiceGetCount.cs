using Moq;

namespace Application.Tests.CollaboratorServiceTests
{
    public class CollaboratorServiceGetCount : CollaboratorServiceTestBase
    {
        [Fact]
        public async Task GetCount_ReturnsCount()
        {
            // arrange
            var expected = 5;
            CollaboratorRepositoryDouble.Setup(repo => repo.GetCount()).ReturnsAsync(expected);

            // act
            var result = await CollaboratorService.GetCount();

            // assert
            Assert.Equal(expected, result);
        }
    }
}