using Domain.Interfaces;
using Domain.Models;
using Domain.Visitor;
using Infrastructure.DataModel;
using Infrastructure.Mapper;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Infrastructure.Tests.AssociationProjectCollaboratorRepositoryEFTests;

public class AssociationProjectCollaboratorFindAllByProjectAndBetweenPeriodAsyncTests
{
    [Fact]
    public async Task WhenPassingExistingProjectIdAndPeriodContains_ThenReturnRelatedAssociations()
    {
        // Arrange
        // ------------ Setup test in-memory database ------------
        var options = new DbContextOptionsBuilder<AbsanteeContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        using var context = new AbsanteeContext(options);

        PeriodDate period =
            new PeriodDate(DateOnly.FromDateTime(DateTime.Now), DateOnly.FromDateTime(DateTime.Now.AddMonths(2)));

        var assoc1 = new Mock<IAssociationProjectCollaborator>();
        assoc1.Setup(a => a.ProjectId).Returns(1);
        assoc1.Setup(a => a.CollaboratorId).Returns(1);
        assoc1.Setup(a => a.PeriodDate).Returns(period);
        var assocDM1 = new AssociationProjectCollaboratorDataModel(assoc1.Object);
        context.Associations.Add(assocDM1);

        var assoc2 = new Mock<IAssociationProjectCollaborator>();
        assoc2.Setup(a => a.ProjectId).Returns(2);
        assoc2.Setup(a => a.CollaboratorId).Returns(2);
        assoc2.Setup(a => a.PeriodDate).Returns(period);
        var assocDM2 = new AssociationProjectCollaboratorDataModel(assoc2.Object);
        context.Associations.Add(assocDM2);

        var assoc3 = new Mock<IAssociationProjectCollaborator>();
        assoc3.Setup(a => a.ProjectId).Returns(2);
        assoc3.Setup(a => a.CollaboratorId).Returns(3);
        assoc3.Setup(a => a.PeriodDate).Returns(period);
        var assocDM3 = new AssociationProjectCollaboratorDataModel(assoc3.Object);
        context.Associations.Add(assocDM3);

        await context.SaveChangesAsync();

        // -----------------------------------------

        long projectIdToSearch = 2;
        PeriodDate periodToSearch =
            new PeriodDate(DateOnly.FromDateTime(DateTime.Now.AddMonths(-1)), DateOnly.FromDateTime(DateTime.Now.AddMonths(3)));

        List<AssociationProjectCollaboratorDataModel> assocDMList =
            new List<AssociationProjectCollaboratorDataModel> { assocDM2, assocDM3 };

        List<IAssociationProjectCollaborator> expected =
            new List<IAssociationProjectCollaborator>();

        var mapper = new Mock<IMapper<IAssociationProjectCollaborator, IAssociationProjectCollaboratorVisitor>>();

        // Convert to domain
        mapper.Setup(m => m.ToDomain(assocDMList)).Returns(expected);

        // Instatiate repository
        var assocRepo =
            new AssociationProjectCollaboratorRepositoryEF(context, mapper.Object);

        // Act
        IEnumerable<IAssociationProjectCollaborator> result = await assocRepo.FindAllByProjectAndBetweenPeriodAsync(projectIdToSearch, periodToSearch);

        // Assert
        Assert.True(expected.SequenceEqual(result));
    }

    [Fact]
    public async Task WhenPassingNotExistingProjectId_ThenReturnEmpty()
    {
        // Arrange
        // ------------ Setup test in-memory database ------------
        var options = new DbContextOptionsBuilder<AbsanteeContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        using var context = new AbsanteeContext(options);

        PeriodDate period =
            new PeriodDate(DateOnly.FromDateTime(DateTime.Now), DateOnly.FromDateTime(DateTime.Now.AddMonths(2)));

        var assoc1 = new Mock<IAssociationProjectCollaborator>();
        assoc1.Setup(a => a.ProjectId).Returns(1);
        assoc1.Setup(a => a.CollaboratorId).Returns(1);
        assoc1.Setup(a => a.PeriodDate).Returns(period);
        var assocDM1 = new AssociationProjectCollaboratorDataModel(assoc1.Object);
        context.Associations.Add(assocDM1);

        var assoc2 = new Mock<IAssociationProjectCollaborator>();
        assoc2.Setup(a => a.ProjectId).Returns(2);
        assoc2.Setup(a => a.CollaboratorId).Returns(2);
        assoc2.Setup(a => a.PeriodDate).Returns(period);
        var assocDM2 = new AssociationProjectCollaboratorDataModel(assoc2.Object);
        context.Associations.Add(assocDM2);

        var assoc3 = new Mock<IAssociationProjectCollaborator>();
        assoc3.Setup(a => a.ProjectId).Returns(2);
        assoc3.Setup(a => a.CollaboratorId).Returns(3);
        assoc3.Setup(a => a.PeriodDate).Returns(period);
        var assocDM3 = new AssociationProjectCollaboratorDataModel(assoc3.Object);
        context.Associations.Add(assocDM3);

        await context.SaveChangesAsync();

        // -----------------------------------------

        long projectIdToSearch = 4;

        PeriodDate periodToSearch =
           new PeriodDate(DateOnly.FromDateTime(DateTime.Now.AddMonths(-1)), DateOnly.FromDateTime(DateTime.Now.AddMonths(3)));

        Mock<IMapper<IAssociationProjectCollaborator, IAssociationProjectCollaboratorVisitor>> mapper =
            new Mock<IMapper<IAssociationProjectCollaborator, IAssociationProjectCollaboratorVisitor>>();

        // Instatiate repository
        AssociationProjectCollaboratorRepositoryEF assocRepo =
            new AssociationProjectCollaboratorRepositoryEF(context, mapper.Object);

        // Act
        IEnumerable<IAssociationProjectCollaborator> result = await assocRepo.FindAllByProjectAndBetweenPeriodAsync(projectIdToSearch, periodToSearch);

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public async Task WhenPassingExistingProjectIdAndPeriodNotContains_ThenReturnEmpty()
    {
        // Arrange
        // ------------ Setup test in-memory database ------------
        var options = new DbContextOptionsBuilder<AbsanteeContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        using var context = new AbsanteeContext(options);

        PeriodDate period =
            new PeriodDate(DateOnly.FromDateTime(DateTime.Now), DateOnly.FromDateTime(DateTime.Now.AddMonths(2)));

        var assoc1 = new Mock<IAssociationProjectCollaborator>();
        assoc1.Setup(a => a.ProjectId).Returns(1);
        assoc1.Setup(a => a.CollaboratorId).Returns(1);
        assoc1.Setup(a => a.PeriodDate).Returns(period);
        var assocDM1 = new AssociationProjectCollaboratorDataModel(assoc1.Object);
        context.Associations.Add(assocDM1);

        var assoc2 = new Mock<IAssociationProjectCollaborator>();
        assoc2.Setup(a => a.ProjectId).Returns(2);
        assoc2.Setup(a => a.CollaboratorId).Returns(2);
        assoc2.Setup(a => a.PeriodDate).Returns(period);
        var assocDM2 = new AssociationProjectCollaboratorDataModel(assoc2.Object);
        context.Associations.Add(assocDM2);

        var assoc3 = new Mock<IAssociationProjectCollaborator>();
        assoc3.Setup(a => a.ProjectId).Returns(3);
        assoc3.Setup(a => a.CollaboratorId).Returns(3);
        assoc3.Setup(a => a.PeriodDate).Returns(period);
        var assocDM3 = new AssociationProjectCollaboratorDataModel(assoc3.Object);
        context.Associations.Add(assocDM3);

        await context.SaveChangesAsync();

        // -----------------------------------------

        long projectIdToSearch = 3;

        PeriodDate periodToSearch =
           new PeriodDate(DateOnly.FromDateTime(DateTime.Now.AddMonths(1)), DateOnly.FromDateTime(DateTime.Now.AddMonths(2)));

        Mock<IMapper<IAssociationProjectCollaborator, IAssociationProjectCollaboratorVisitor>> mapper =
            new Mock<IMapper<IAssociationProjectCollaborator, IAssociationProjectCollaboratorVisitor>>();

        // Instatiate repository
        AssociationProjectCollaboratorRepositoryEF assocRepo =
            new AssociationProjectCollaboratorRepositoryEF(context, mapper.Object);

        // Act
        IEnumerable<IAssociationProjectCollaborator> result = await assocRepo.FindAllByProjectAndBetweenPeriodAsync(projectIdToSearch, periodToSearch);

        // Assert
        Assert.Empty(result);
    }
}