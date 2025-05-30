using Application.DTO.Collaborators;
using Domain.Models;
using Moq;

namespace Application.Tests.CollaboratorServiceTests;

public class CollaboratorServiceCreate : CollaboratorServiceTestBase
{
    [Fact]
    public async Task Create_ShouldReturnSuccessResult_WhenValidDataProvided()
    {
        // Arrange
        var createDto =
         new CollabCreateDataDTO("João", "Silva", "joao.silva@email.com", DateTime.UtcNow, new PeriodDateTime(DateTime.UtcNow, DateTime.UtcNow));

        var user = new User(Guid.NewGuid(), createDto.Names, createDto.Surnames, createDto.Email, createDto.PeriodDateTime);

        var collaborator = new Collaborator(Guid.NewGuid(), user.Id, createDto.PeriodDateTime);

        var holidayPlan = new HolidayPlan(collaborator.Id, new List<HolidayPeriod>());

        UserFactoryDouble.Setup(x => x.Create(
            createDto.Names, createDto.Surnames, createDto.Email, createDto.deactivationDate)).ReturnsAsync(user);

        UserRepositoryDouble.Setup(x => x.Add(user)).Returns(user);

        CollaboratorFactoryDouble.Setup(x => x.Create(user, createDto.PeriodDateTime)).ReturnsAsync(collaborator);

        CollaboratorRepositoryDouble.Setup(x => x.Add(collaborator)).Returns(collaborator);

        HolidayPlanFactoryDouble.Setup(x => x.Create(collaborator, new List<PeriodDate>()))
            .ReturnsAsync(holidayPlan);

        HolidayPlanRepositoryDouble.Setup(x => x.AddAsync(holidayPlan));

        _context.SaveChanges();

        // Act
        var result = await CollaboratorService.Create(createDto);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Equal(collaborator.Id, result.Value.CollabId);
        Assert.Equal(user.Id, result.Value.UserId);
        Assert.Equal(createDto.Email, result.Value.Email);
    }

    [Fact]
    public async Task Create_ShouldReturnFailureResult_WhenArgumentExceptionThrown()
    {
        // Arrange
        var createDto =
         new CollabCreateDataDTO("João", "Silva", "joao.silva@email.com", DateTime.UtcNow, new PeriodDateTime(DateTime.UtcNow, DateTime.UtcNow));

        UserFactoryDouble.Setup(x => x.Create(
            createDto.Names, createDto.Surnames, createDto.Email, createDto.deactivationDate))
            .ThrowsAsync(new ArgumentException("Invalid user data"));

        // Act
        var result = await CollaboratorService.Create(createDto);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal("Invalid user data", result.Error!.Message);
    }

    [Fact]
    public async Task Create_ShouldReturnFailureResult_WhenUnexpectedExceptionThrown()
    {
        // Arrange
        var createDto =
         new CollabCreateDataDTO("João", "Silva", "joao.silva@email.com", DateTime.UtcNow, new PeriodDateTime(DateTime.UtcNow, DateTime.UtcNow));

        UserFactoryDouble.Setup(x => x.Create(
            It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>()))
            .ThrowsAsync(new Exception("Unexpected failure"));

        // Act
        var result = await CollaboratorService.Create(createDto);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal("Unexpected failure", result.Error!.Message);
    }
}