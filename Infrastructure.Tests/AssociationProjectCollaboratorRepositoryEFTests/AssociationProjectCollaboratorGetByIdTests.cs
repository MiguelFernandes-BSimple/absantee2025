using Domain.Interfaces;
using Domain.Models;
using Domain.Visitor;
using Infrastructure.DataModel;
using Infrastructure.Mapper;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Infrastructure.Tests.AssociationProjectCollaboratorRepositoryEFTests;

public class AssociationProjectCollaboratorGetByIdTests
{
    [Fact]
    public void WhenPassingExistingId_ThenReturnCorrectAssociation()
    {
        // arrange
        var options = new DbContextOptionsBuilder<AbsanteeContext>()
        .UseInMemoryDatabase(Guid.NewGuid().ToString())
        .Options;

        using var context = new AbsanteeContext(options);

        PeriodDate period = new PeriodDate(DateOnly.FromDateTime(DateTime.Now), DateOnly.FromDateTime(DateTime.Now.AddMonths(1)));

        var assoc1 = new Mock<IAssociationProjectCollaborator>();
        assoc1.Setup(a => a.Id).Returns(1);
        var assocDM1 = new AssociationProjectCollaboratorDataModel(assoc1.Object);

        var assoc2 = new Mock<IAssociationProjectCollaborator>();
        assoc2.Setup(a => a.Id).Returns(2);
        var assocDM2 = new AssociationProjectCollaboratorDataModel(assoc2.Object);

        var assoc3 = new Mock<IAssociationProjectCollaborator>();
        assoc3.Setup(a => a.Id).Returns(3);
        var assocDM3 = new AssociationProjectCollaboratorDataModel(assoc3.Object);

        context.Associations.Add(assocDM1);
        context.Associations.Add(assocDM2);
        context.Associations.Add(assocDM3);

        context.SaveChangesAsync();

        var expected = new Mock<IAssociationProjectCollaborator>().Object;

        Mock<IMapper<IAssociationProjectCollaborator, IAssociationProjectCollaboratorVisitor>> mapper =
            new Mock<IMapper<IAssociationProjectCollaborator, IAssociationProjectCollaboratorVisitor>>();
        mapper.Setup(m => m.ToDomain(assocDM2)).Returns(expected);

        var associationRepo = new AssociationProjectCollaboratorRepositoryEF(context, mapper.Object);

        // act
        var result = associationRepo.GetById(2);

        // assert
        Assert.Equal(result, expected);
    }




    [Fact]
    public void WhenPassingNewId_ThenReturnNull()
    {
        // arrange
        var options = new DbContextOptionsBuilder<AbsanteeContext>()
        .UseInMemoryDatabase(Guid.NewGuid().ToString())
        .Options;

        using var context = new AbsanteeContext(options);

        PeriodDate period = new PeriodDate(DateOnly.FromDateTime(DateTime.Now), DateOnly.FromDateTime(DateTime.Now.AddMonths(1)));

        var assoc1 = new Mock<IAssociationProjectCollaborator>();
        assoc1.Setup(a => a.Id).Returns(1);
        var assocDM1 = new AssociationProjectCollaboratorDataModel(assoc1.Object);

        var assoc2 = new Mock<IAssociationProjectCollaborator>();
        assoc2.Setup(a => a.Id).Returns(2);
        var assocDM2 = new AssociationProjectCollaboratorDataModel(assoc2.Object);

        var assoc3 = new Mock<IAssociationProjectCollaborator>();
        assoc3.Setup(a => a.Id).Returns(3);
        var assocDM3 = new AssociationProjectCollaboratorDataModel(assoc3.Object);

        context.Associations.Add(assocDM1);
        context.Associations.Add(assocDM2);
        context.Associations.Add(assocDM3);

        context.SaveChangesAsync();

        Mock<IMapper<IAssociationProjectCollaborator, IAssociationProjectCollaboratorVisitor>> mapper =
            new Mock<IMapper<IAssociationProjectCollaborator, IAssociationProjectCollaboratorVisitor>>();

        var associationRepo = new AssociationProjectCollaboratorRepositoryEF(context, mapper.Object);

        // act
        var result = associationRepo.GetById(6);

        // assert
        Assert.Null(result);
    }
}
