using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    public class UserRepositoryGetByNamesAsyncTests
{
    [Theory]
    [InlineData("John", "Johnny", 2)]
    [InlineData("Johnny", "Pedro", 1)]
    [InlineData("Morgan", "Pedro", 0)]
    public async Task WhenGettingByNamesAsync_ShouldReturnCorrectUsers(string name1, string name2, int expectedCount)
    {
        // Arrange
        var options = new DbContextOptionsBuilder<AbsanteeContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        using var context = new AbsanteeContext(options);

        var user1 = new UserDataModel
        {
            Id = 1,
            Names = name1,
            Surnames = "Doe",
            Email = "user1@gmail.com"
        };

        var user2 = new UserDataModel
        {
            Id = 2,
            Names = name2,
            Surnames = "Bravo",
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
                    user.Setup(u => u.GetNames()).Returns(dm.Names);
                    return user.Object;
                }).ToList();
            });

        var userRepository = new UserRepositoryEF(context, mapperDouble.Object);

        // Act
        var result = await userRepository.GetByNamesAsync("John");

        // Assert
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

