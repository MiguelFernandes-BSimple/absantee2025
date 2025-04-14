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
    public class IsRepeated
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
            var collaboratorDM1 = new Mock<ICollaboratorVisitor>();
            var collaboratorDM2 = new Mock<ICollaboratorVisitor>();
            var users = new List<CollaboratorDataModel>
            {
                (CollaboratorDataModel)collaboratorDM1.Object,
                (CollaboratorDataModel)collaboratorDM2.Object
            }.AsQueryable();

            var mockSet = new Mock<DbSet<CollaboratorDataModel>>();
            mockSet.As<IQueryable<CollaboratorDataModel>>().Setup(m => m.Provider).Returns(users.Provider);
            mockSet.As<IQueryable<CollaboratorDataModel>>().Setup(m => m.Expression).Returns(users.Expression);
            mockSet.As<IQueryable<CollaboratorDataModel>>().Setup(m => m.ElementType).Returns(users.ElementType);
            mockSet.As<IQueryable<CollaboratorDataModel>>().Setup(m => m.GetEnumerator()).Returns(users.GetEnumerator());

            var absanteeMock = new Mock<IAbsanteeContext>();
            absanteeMock.Setup(a => a.Collaborators).Returns(mockSet.Object);

            collaboratorDM1.Setup(c => c.UserID).Returns(1);

            collaboratorDM2.Setup(c => c.UserID).Returns(2);
            var periodDateTime1 = new Mock<IPeriodDateTime>();
            periodDateTime1.Setup(a => a.GetInitDate()).Returns(init1);
            periodDateTime1.Setup(a => a.GetFinalDate()).Returns(final1);
            collaboratorDM2.Setup(c => c.PeriodDateTime).Returns((PeriodDateTime)periodDateTime1.Object);

            var collaborator = new Mock<ICollaborator>();
            collaborator.Setup(c => c.GetUserId()).Returns(2);
            var periodDateTime2 = new Mock<IPeriodDateTime>();
            periodDateTime2.Setup(a => a.GetInitDate()).Returns(new DateTime(2020, 1, 1));
            periodDateTime2.Setup(a => a.GetFinalDate()).Returns(new DateTime(2021, 1, 1));

            var collabMapper = new Mock<CollaboratorMapper>();

            var collaboratorRepository = new CollaboratorRepository((AbsanteeContext)absanteeMock.Object, collabMapper.Object);
            //Act 
            var result = await collaboratorRepository.IsRepeated(collaborator.Object);

            //Assert
            Assert.Equal(expected, result);
        }
    }
}
