using Domain.Factory;
using Domain.Interfaces;
using Domain.IRepository;
using Domain.Visitor;
using Moq;

namespace Domain.Tests.TrainingSubjectTests;

public class TrainingSubjectFactoryTests
{
    // Happy path for Create(title, description)
    [Fact]
    public async Task WhenPassingValidParameters_ThenInstantiateObject()
    {
        // Arrange
        var _repoMock = new Mock<ITrainingSubjectRepository>();

        var title = "UniqueTitle";
        var description = "Valid description";
        _repoMock.Setup(r => r.FindByTitle(title)).ReturnsAsync((ITrainingSubject?)null);

        var _factory = new TrainingSubjectFactory(_repoMock.Object);

        // Act
        var result = await _factory.Create(title, description);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(title, result.Title);
        Assert.Equal(description, result.Description);
    }

    // Duplicate title — should throw ArgumentException
    [Fact]
    public async Task WhenPassingRepeatedTitle_ThenThrowException()
    {
        // Arrange
        var _repoMock = new Mock<ITrainingSubjectRepository>();

        var title = "DuplicateTitle";
        var description = "Valid description";
        var existingSubject = new Mock<ITrainingSubject>();
        _repoMock.Setup(r => r.FindByTitle(title)).ReturnsAsync(existingSubject.Object);
        var _factory = new TrainingSubjectFactory(_repoMock.Object);

        // Assert
        ArgumentException exception = await Assert.ThrowsAsync<ArgumentException>(() =>
            // Act
            _factory.Create(title, description));

        Assert.Equal("Invalid inputs", exception.Message);
    }

    // Visitor-based creation
    [Fact]
    public void WhenPassingValidVisitor_ThenInstantiateObject()
    {
        // Arrange
        var _repoMock = new Mock<ITrainingSubjectRepository>();

        var mockVisitor = new Mock<ITrainingSubjectVisitor>();
        mockVisitor.Setup(v => v.Title).Returns("VisitorTitle");
        mockVisitor.Setup(v => v.Description).Returns("Visitor description");

        var _factory = new TrainingSubjectFactory(_repoMock.Object);

        // Act
        var result = _factory.Create(mockVisitor.Object);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("VisitorTitle", result.Title);
        Assert.Equal("Visitor description", result.Description);
    }
}