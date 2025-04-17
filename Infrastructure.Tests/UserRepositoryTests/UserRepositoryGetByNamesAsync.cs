using Domain.Interfaces;
using Domain.Visitor;
using Infrastructure.DataModel;
using Infrastructure.Mapper;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Infrastructure.Tests.UserRepositoryTests
{
    public class UserRepositoryGetByNamesAsync
    {
        [Theory]
        [InlineData("John", "Johnny", 2)]
        [InlineData("Johnny", "Pedro", 1)]
        [InlineData("Morgan", "Pedro", 0)]
        public async Task WhenGettingByNamesAsync_ShouldReturnCorrectUsers(string name1, string name2, int expectedCount)
        {

            //Arrange
            var periodDateTimeDouble1 = new Mock<IPeriodDateTime>();
            periodDateTimeDouble1.Setup(p => p.GetInitDate()).Returns(DateTime.Now);
            periodDateTimeDouble1.Setup(p => p.GetFinalDate()).Returns(DateTime.MaxValue);

            var userDM1 = new Mock<IUserVisitor>();
            userDM1.Setup(u => u.Id).Returns(1);
            userDM1.Setup(u => u.Names).Returns(name1);
            userDM1.Setup(u => u.Surnames).Returns("Doe");
            userDM1.Setup(u => u.Email).Returns("user1@gmail.com");

            var userDM2 = new Mock<IUserVisitor>();
            userDM2.Setup(u => u.Id).Returns(2);
            userDM2.Setup(u => u.Names).Returns(name2);
            userDM2.Setup(u => u.Surnames).Returns("Bravo");
            userDM2.Setup(u => u.Email).Returns("user2@gmail.com");

            var users = new List<UserDataModel>
            {
                (UserDataModel)userDM1.Object,
                (UserDataModel)userDM2.Object
            }.AsQueryable();

            var mockSet = new Mock<DbSet<UserDataModel>>();
            mockSet.As<IQueryable<UserDataModel>>().Setup(m => m.Provider).Returns(users.Provider);
            mockSet.As<IQueryable<UserDataModel>>().Setup(m => m.Expression).Returns(users.Expression);
            mockSet.As<IQueryable<UserDataModel>>().Setup(m => m.ElementType).Returns(users.ElementType);
            mockSet.As<IQueryable<UserDataModel>>().Setup(m => m.GetEnumerator()).Returns(users.GetEnumerator());

            var absanteeMock = new Mock<IAbsanteeContext>();
            absanteeMock.Setup(a => a.Users).Returns(mockSet.Object);

            var mapperDouble = new Mock<IMapper<IUser, UserDataModel>>();
            mapperDouble.Setup(m => m.ToDomain(It.IsAny<IEnumerable<UserDataModel>>())).Returns((IEnumerable<UserDataModel> dms) =>
            {
                return dms.Select(dm =>
                {
                    var user = new Mock<IUser>();
                    user.Setup(u => u.GetId()).Returns(dm.Id);
                    user.Setup(u => u.GetNames()).Returns(dm.Names);
                    return user.Object;
                }).ToList();
            });

            var userRepository = new UserRepositoryEF((AbsanteeContext)absanteeMock.Object, (UserMapper)mapperDouble.Object);

            //Act
            var result = await userRepository.GetByNamesAsync("John");

            //Assert
            Assert.Equal(expectedCount, result.Count());
            if (expectedCount > 0)
            {
                Assert.All(result, u => Assert.Contains("John", u.GetNames(), StringComparison.OrdinalIgnoreCase));
            }
            else
            {
                Assert.Empty(result);
            }
        }
    }
}

