using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.IRepository;
using Infrastructure.DataModel;
using Microsoft.EntityFrameworkCore;
using Moq;
using Domain.Visitor;
using Domain.Models;
using Infrastructure.Mapper;
using Infrastructure.Repositories;

namespace Infrastructure.Tests.CollaboratorRepositoryTests
{
    public class CollaboratorRepositoryIsRepeated
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

            var options = new DbContextOptionsBuilder<AbsanteeContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()) // ensure isolation per test
                .Options;

            using var context = new AbsanteeContext(options);

            var collaborator1 = new Mock<ICollaborator>();
            collaborator1.Setup(c => c._userId).Returns(2);
            //var periodDateTimeColab = new Mock<IPeriodDateTime>();
            //periodDateTimeColab.Setup(pd => pd.GetInitDate()).Returns(init1);
            //periodDateTimeColab.Setup(pd => pd.GetFinalDate()).Returns(final1);
            var periodDateTimeColab = new PeriodDateTime(init1, final1);
            collaborator1.Setup(c => c._periodDateTime).Returns(periodDateTimeColab);
            context.Collaborators.Add(new CollaboratorDataModel(collaborator1.Object));

            var collaborator2 = new Mock<ICollaborator>();
            collaborator2.Setup(c => c._userId).Returns(2);
            //periodDateTimeColab.Setup(pd => pd.GetInitDate()).Returns(init1);
            //periodDateTimeColab.Setup(pd => pd.GetFinalDate()).Returns(final1);
            collaborator2.Setup(c => c._periodDateTime).Returns(periodDateTimeColab);
            context.Collaborators.Add(new CollaboratorDataModel(collaborator2.Object));

            await context.SaveChangesAsync();

            var periodDateTime = new Mock<IPeriodDateTime>();
            periodDateTime.Setup(p => p._initDate).Returns(new DateTime(2020, 1, 1));
            periodDateTime.Setup(p => p._finalDate).Returns(new DateTime(2021, 1, 1));

            var collaborator = new Mock<ICollaborator>();
            collaborator.Setup(c => c._userId).Returns(2);
            collaborator.Setup(c => c._periodDateTime).Returns(periodDateTime.Object);

            var mapper = new Mock<IMapper<ICollaborator, ICollaboratorVisitor>>();

            var repo = new CollaboratorRepository(context, mapper.Object);

            // Act
            var result = await repo.IsRepeated(collaborator.Object);

            // Assert
            Assert.Equal(expected, result);
        }
    }
}
