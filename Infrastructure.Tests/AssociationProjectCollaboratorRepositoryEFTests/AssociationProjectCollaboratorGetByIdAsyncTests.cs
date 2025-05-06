//using Domain.Interfaces;
//using Domain.Models;
//using Domain.Visitor;
//using Infrastructure.DataModel;
//using AutoMapper;
//using Microsoft.EntityFrameworkCore;
//using Moq;

//namespace Infrastructure.Tests.AssociationProjectCollaboratorRepositoryEFTests;

//public class AssociationProjectCollaboratorGetByIdAsyncTests
//{
//    private readonly IMapper _mapper;

//    public AssociationProjectCollaboratorGetByIdAsyncTests()
//    {
//        var config = new MapperConfiguration(cfg =>
//        {
//            // Add both profiles for testing both mappings
//            cfg.AddProfile<DataModelMappingProfile>();
//        });

//        _mapper = config.CreateMapper();
//    }

//    [Fact]
//    public async Task WhenPassingExistingId_ThenReturnCorrectAssociation()
//    {
//        // Arrange
//        var options = new DbContextOptionsBuilder<AbsanteeContext>()
//            .UseInMemoryDatabase(Guid.NewGuid().ToString())
//            .Options;

//        using var context = new AbsanteeContext(options);

//        long assocDM1Id = 1;
//        long assocDM2Id = 2;

//        var assoc1 = new AssociationProjectCollaboratorDataModel { Id = assocDM1Id };
//        var assoc2 = new AssociationProjectCollaboratorDataModel { Id = assocDM2Id };

//        context.Associations.AddRange(assoc1, assoc2);
//        await context.SaveChangesAsync();

//        var expected = new Mock<IAssociationProjectCollaborator>();
//        expected.Setup(e => e.Id).Returns(assocDM2Id);

//        var assocRepo = new AssociationProjectCollaboratorRepositoryEF(context, _mapper);

//        // Act
//        var result = await assocRepo.GetByIdAsync(assocDM2Id);

//        // Assert
//        Assert.Equal(expected.Object, result);
//    }

    
//    [Fact]
//    public async Task WhenPassingNewId_ThenReturnNull()
//    {
//        // Arrange
//        var options = new DbContextOptionsBuilder<AbsanteeContext>()
//            .UseInMemoryDatabase(Guid.NewGuid().ToString())
//            .Options;

//        using var context = new AbsanteeContext(options);

//        long assocDM1Id = 1;
//        long assocDM2Id = 2;
//        long assocDM3Id = 3;

//        var assoc1 = new AssociationProjectCollaboratorDataModel { Id = assocDM1Id };
//        var assoc2 = new AssociationProjectCollaboratorDataModel { Id = assocDM2Id };

//        context.Associations.AddRange(assoc1, assoc2);
//        await context.SaveChangesAsync();

//        var assocRepo = new AssociationProjectCollaboratorRepositoryEF(context, _mapper);

//        // Act
//        var result = await assocRepo.GetByIdAsync(assocDM3Id);

//        // Assert
//        Assert.Null(result);
//    }
//}