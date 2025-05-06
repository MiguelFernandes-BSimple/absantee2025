//using Domain.Interfaces;
//using Domain.Models;
//using Domain.Visitor;
//using Infrastructure.DataModel;
//using AutoMapper;
//using Microsoft.EntityFrameworkCore;
//using Moq;

//namespace Infrastructure.Tests.AssociationProjectCollaboratorRepositoryEFTests;

//public class AssociationProjectCollaboratorFindByProjectAndCollaboratorAsyncTests
//{
//    private readonly IMapper _mapper;

//    public AssociationProjectCollaboratorFindByProjectAndCollaboratorAsyncTests()
//    {
//        var config = new MapperConfiguration(cfg =>
//        {
//            // Add both profiles for testing both mappings
//            cfg.AddProfile<DataModelMappingProfile>();
//        });

//        _mapper = config.CreateMapper();
//    }

//    [Fact]
//    public async Task WhenPassingExistingProjectAndCollaboratorIdCombo_ThenReturnAssociation()
//    {
//        // Arrange
//        // ------------ Setup test in-memory database ------------
//        var options = new DbContextOptionsBuilder<AbsanteeContext>()
//            .UseInMemoryDatabase(Guid.NewGuid().ToString())
//            .Options;

//        using var context = new AbsanteeContext(options);

//        PeriodDate period =
//            new PeriodDate(DateOnly.FromDateTime(DateTime.Now), DateOnly.FromDateTime(DateTime.Now.AddMonths(2)));

//        var assoc1 = new Mock<IAssociationProjectCollaborator>();
//        assoc1.Setup(a => a.ProjectId).Returns(1);
//        assoc1.Setup(a => a.CollaboratorId).Returns(1);
//        assoc1.Setup(a => a.PeriodDate).Returns(period);
//        var assocDM1 = new AssociationProjectCollaboratorDataModel(assoc1.Object);
//        context.AssociationsProjectCollaborator.Add(assocDM1);

//        var assoc2 = new Mock<IAssociationProjectCollaborator>();
//        assoc2.Setup(a => a.ProjectId).Returns(2);
//        assoc2.Setup(a => a.CollaboratorId).Returns(2);
//        assoc2.Setup(a => a.PeriodDate).Returns(period);
//        var assocDM2 = new AssociationProjectCollaboratorDataModel(assoc2.Object);
//        context.AssociationsProjectCollaborator.Add(assocDM2);

//        var assoc3 = new Mock<IAssociationProjectCollaborator>();
//        assoc3.Setup(a => a.ProjectId).Returns(3);
//        assoc3.Setup(a => a.CollaboratorId).Returns(3);
//        assoc3.Setup(a => a.PeriodDate).Returns(period);
//        var assocDM3 = new AssociationProjectCollaboratorDataModel(assoc3.Object);
//        context.AssociationsProjectCollaborator.Add(assocDM3);

//        await context.SaveChangesAsync();

//        // -----------------------------------------

//        long collabId = 3;
//        long projectId = 3;

//        var expected = new Mock<IAssociationProjectCollaborator>().Object;

//        var repo = new AssociationProjectCollaboratorRepositoryEF(context, _mapper);

//        // Act
//        var result = await repo.FindByProjectAndCollaboratorAsync(projectId, collabId);

//        // Assert
//        Assert.Equal(expected, result);
//    }

//    [Fact]
//    public async Task WhenPassingNonExistingProjectAndCollaboratorIdCombo_ThenReturnNull()
//    {
//        // Arrange
//        // ------------ Setup test in-memory database ------------
//        var options = new DbContextOptionsBuilder<AbsanteeContext>()
//            .UseInMemoryDatabase(Guid.NewGuid().ToString())
//            .Options;

//        using var context = new AbsanteeContext(options);

//        PeriodDate period =
//            new PeriodDate(DateOnly.FromDateTime(DateTime.Now), DateOnly.FromDateTime(DateTime.Now.AddMonths(2)));

//        var assoc1 = new Mock<IAssociationProjectCollaborator>();
//        assoc1.Setup(a => a.ProjectId).Returns(1);
//        assoc1.Setup(a => a.CollaboratorId).Returns(1);
//        assoc1.Setup(a => a.PeriodDate).Returns(period);
//        var assocDM1 = new AssociationProjectCollaboratorDataModel(assoc1.Object);
//        context.Associations.Add(assocDM1);

//        var assoc2 = new Mock<IAssociationProjectCollaborator>();
//        assoc2.Setup(a => a.ProjectId).Returns(2);
//        assoc2.Setup(a => a.CollaboratorId).Returns(2);
//        assoc2.Setup(a => a.PeriodDate).Returns(period);
//        var assocDM2 = new AssociationProjectCollaboratorDataModel(assoc2.Object);
//        context.Associations.Add(assocDM2);

//        var assoc3 = new Mock<IAssociationProjectCollaborator>();
//        assoc3.Setup(a => a.ProjectId).Returns(3);
//        assoc3.Setup(a => a.CollaboratorId).Returns(3);
//        assoc3.Setup(a => a.PeriodDate).Returns(period);
//        var assocDM3 = new AssociationProjectCollaboratorDataModel(assoc3.Object);
//        context.Associations.Add(assocDM3);

//        await context.SaveChangesAsync();

//        // -----------------------------------------

//        long collabId = 2;
//        long projectId = 3;

//        var repo = new AssociationProjectCollaboratorRepositoryEF(context, _mapper);

//        // Act
//        var result = await repo.FindByProjectAndCollaboratorAsync(projectId, collabId);

//        // Assert
//        Assert.Null(result);
//    }

//    [Fact]
//    public async Task WhenPassingNonExistingProjectId_ThenReturnNull()
//    {
//        // Arrange
//        // ------------ Setup test in-memory database ------------
//        var options = new DbContextOptionsBuilder<AbsanteeContext>()
//            .UseInMemoryDatabase(Guid.NewGuid().ToString())
//            .Options;

//        using var context = new AbsanteeContext(options);

//        PeriodDate period =
//            new PeriodDate(DateOnly.FromDateTime(DateTime.Now), DateOnly.FromDateTime(DateTime.Now.AddMonths(2)));

//        var assoc1 = new Mock<IAssociationProjectCollaborator>();
//        assoc1.Setup(a => a.ProjectId).Returns(1);
//        assoc1.Setup(a => a.CollaboratorId).Returns(1);
//        assoc1.Setup(a => a.PeriodDate).Returns(period);
//        var assocDM1 = new AssociationProjectCollaboratorDataModel(assoc1.Object);
//        context.Associations.Add(assocDM1);

//        var assoc2 = new Mock<IAssociationProjectCollaborator>();
//        assoc2.Setup(a => a.ProjectId).Returns(2);
//        assoc2.Setup(a => a.CollaboratorId).Returns(2);
//        assoc2.Setup(a => a.PeriodDate).Returns(period);
//        var assocDM2 = new AssociationProjectCollaboratorDataModel(assoc2.Object);
//        context.Associations.Add(assocDM2);

//        var assoc3 = new Mock<IAssociationProjectCollaborator>();
//        assoc3.Setup(a => a.ProjectId).Returns(3);
//        assoc3.Setup(a => a.CollaboratorId).Returns(3);
//        assoc3.Setup(a => a.PeriodDate).Returns(period);
//        var assocDM3 = new AssociationProjectCollaboratorDataModel(assoc3.Object);
//        context.Associations.Add(assocDM3);

//        await context.SaveChangesAsync();

//        // -----------------------------------------

//        long collabId = 2;
//        long projectId = 4;

//        var repo = new AssociationProjectCollaboratorRepositoryEF(context, _mapper);

//        // Act
//        var result = await repo.FindByProjectAndCollaboratorAsync(projectId, collabId);

//        // Assert
//        Assert.Null(result);
//    }

//    [Fact]
//    public async Task WhenPassingNonExistingCollaboratorId_ThenReturnNull()
//    {
//        // Arrange
//        // ------------ Setup test in-memory database ------------
//        var options = new DbContextOptionsBuilder<AbsanteeContext>()
//            .UseInMemoryDatabase(Guid.NewGuid().ToString())
//            .Options;

//        using var context = new AbsanteeContext(options);

//        PeriodDate period =
//            new PeriodDate(DateOnly.FromDateTime(DateTime.Now), DateOnly.FromDateTime(DateTime.Now.AddMonths(2)));

//        var assoc1 = new Mock<IAssociationProjectCollaborator>();
//        assoc1.Setup(a => a.ProjectId).Returns(1);
//        assoc1.Setup(a => a.CollaboratorId).Returns(1);
//        assoc1.Setup(a => a.PeriodDate).Returns(period);
//        var assocDM1 = new AssociationProjectCollaboratorDataModel(assoc1.Object);
//        context.Associations.Add(assocDM1);

//        var assoc2 = new Mock<IAssociationProjectCollaborator>();
//        assoc2.Setup(a => a.ProjectId).Returns(2);
//        assoc2.Setup(a => a.CollaboratorId).Returns(2);
//        assoc2.Setup(a => a.PeriodDate).Returns(period);
//        var assocDM2 = new AssociationProjectCollaboratorDataModel(assoc2.Object);
//        context.Associations.Add(assocDM2);

//        var assoc3 = new Mock<IAssociationProjectCollaborator>();
//        assoc3.Setup(a => a.ProjectId).Returns(3);
//        assoc3.Setup(a => a.CollaboratorId).Returns(3);
//        assoc3.Setup(a => a.PeriodDate).Returns(period);
//        var assocDM3 = new AssociationProjectCollaboratorDataModel(assoc3.Object);
//        context.Associations.Add(assocDM3);

//        await context.SaveChangesAsync();

//        // -----------------------------------------

//        long collabId = 4;
//        long projectId = 3;

//        var repo = new AssociationProjectCollaboratorRepositoryEF(context, _mapper);

//        // Act
//        var result = await repo.FindByProjectAndCollaboratorAsync(projectId, collabId);

//        // Assert
//        Assert.Null(result);
//    }
//}