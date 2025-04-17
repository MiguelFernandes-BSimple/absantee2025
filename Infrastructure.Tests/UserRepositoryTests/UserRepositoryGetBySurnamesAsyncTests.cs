using Domain.Interfaces;
using Domain.Models;
using Domain.Visitor;
using Infrastructure.DataModel;
using Infrastructure.Mapper;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Infrastructure.Tests.UserRepositoryTests
{
    public class UserRepositoryGetBySurnamesAsyncTests
    {

        [Theory]
        [InlineData("Silva", "Silvana", 2)]
        [InlineData("Silva", "Gomes", 1)]
        [InlineData("Gomes", "Pereira", 0)]
        public async Task WhenGettingBySurnamesAsync_ShouldReturnCorrectUsers(string surname1, string surname2, int expectedCount)
        {

            //Arrange
            var periodDateTimeDouble1 = new PeriodDateTime(DateTime.Now, DateTime.MaxValue);

            var userDM1 = new Mock<IUserVisitor>();
            userDM1.Setup(u => u.Id).Returns(1);
            userDM1.Setup(u => u.Names).Returns("Morgan");
            userDM1.Setup(u => u.Surnames).Returns(surname1);
            userDM1.Setup(u => u.Email).Returns("user1@gmail.com");

            var userDM2 = new Mock<IUserVisitor>();
            userDM2.Setup(u => u.Id).Returns(2);
            userDM2.Setup(u => u.Names).Returns("Donald");
            userDM2.Setup(u => u.Surnames).Returns(surname2);
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
                    user.Setup(u => u.GetSurnames()).Returns(dm.Surnames);
                    return user.Object;
                }).ToList();
            });

            var userRepository = new UserRepositoryEF((AbsanteeContext)absanteeMock.Object, (UserMapper)mapperDouble.Object);

            //Act
            var result = await userRepository.GetBySurnamesAsync("Silva");

            //Assert
            Assert.Equal(expectedCount, result.Count());
            if (expectedCount > 0)
            {
                Assert.All(result, u => Assert.Contains("Silva", u.GetSurnames(), StringComparison.OrdinalIgnoreCase));
            }
            else
            {
                Assert.Empty(result);
            }
        }
    }
}