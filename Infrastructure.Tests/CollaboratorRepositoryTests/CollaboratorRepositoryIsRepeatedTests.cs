using Domain.Interfaces;
using Infrastructure.DataModel;
using Moq;
using Domain.Models;
using Infrastructure.Repositories;

namespace Infrastructure.Tests.CollaboratorRepositoryTests
{
    public class CollaboratorRepositoryIsRepeatedTests : RepositoryTestBase
    {
        [Theory]
        [InlineData("2019-12-01", "2020-01-01", true)]   // overlap at start
        [InlineData("2020-01-01", "2020-01-10", true)]   // inside range
        [InlineData("2019-12-15", "2021-01-10", true)]   // fully overlaps
        [InlineData("2020-12-15", "2021-01-01", true)]   // overlaps at end
        [InlineData("2019-12-01", "2019-12-31", false)]  // ends before
        [InlineData("2021-01-02", "2021-02-01", false)]  // starts after
        public async Task WhenCollaboratorReceivedIsRepeated_ThenReturnsExpected(string init1Str, string final1Str, bool expected)
        {
            // Arrange
            var init1 = DateTime.Parse(init1Str);
            var final1 = DateTime.Parse(final1Str);

            var collaborator1 = new Mock<ICollaborator>();
            var userguid1 = Guid.NewGuid();
            collaborator1.Setup(c => c.UserId).Returns(userguid1);
            var periodDateTimeColab = new PeriodDateTime(init1, final1);
            collaborator1.Setup(c => c.PeriodDateTime).Returns(periodDateTimeColab);
            var collaboratorDM1 = new CollaboratorDataModel(collaborator1.Object);
            context.Collaborators.Add(collaboratorDM1);

            await context.SaveChangesAsync();

            var periodDateTime = new PeriodDateTime(new DateTime(2020, 1, 1), new DateTime(2021, 1, 1));

            var collaborator = new Mock<ICollaborator>();
            collaborator.Setup(c => c.UserId).Returns(userguid1);
            collaborator.Setup(c => c.PeriodDateTime).Returns(periodDateTime);

            var repo = new CollaboratorRepositoryEF(context, _mapper.Object);

            // Act
            var result = await repo.IsRepeated(collaborator.Object);

            // Assert
            Assert.Equal(expected, result);
        }
    }
}
