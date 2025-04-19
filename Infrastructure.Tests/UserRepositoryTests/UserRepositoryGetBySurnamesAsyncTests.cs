using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.Visitor;
using Infrastructure.DataModel;
using Infrastructure.Mapper;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

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
            // Arrange
            var options = new DbContextOptionsBuilder<AbsanteeContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using var context = new AbsanteeContext(options);

            var user1 = new UserDataModel
            {
                Id = 1,
                Names = "Morgan",
                Surnames = surname1,
                Email = "user1@gmail.com"
            };

            var user2 = new UserDataModel
            {
                Id = 2,
                Names = "Donald",
                Surnames = surname2,
                Email = "user2@gmail.com"
            };

            context.Users.AddRange(user1, user2);
            await context.SaveChangesAsync();

            var mapperDouble = new Mock<IMapper<IUser, IUserVisitor>>();
            mapperDouble.Setup(m => m.ToDomain(It.IsAny<IEnumerable<UserDataModel>>()))
                .Returns((IEnumerable<UserDataModel> dms) =>
                {
                    return dms.Select(dm =>
                    {
                        var user = new Mock<IUser>();
                        user.Setup(u => u.GetId()).Returns(dm.Id);
                        user.Setup(u => u.GetSurnames()).Returns(dm.Surnames);
                        return user.Object;
                    }).ToList();
                });

            var userRepository = new UserRepositoryEF(context, mapperDouble.Object);

            // Act
            var result = await userRepository.GetBySurnamesAsync("Silva");

            // Assert
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
