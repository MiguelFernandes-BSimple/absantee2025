using Domain.Interfaces;
using Domain.Models;
using Domain.Visitor;
using Infrastructure.DataModel;
using Infrastructure.Mapper;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Infrastructure.Tests.AssociationProjectCollaboratorRepositoryEFTests;

public class AssociationProjectCollaboratorFindAllByProjectAndCollaboratorAsyncTests
{
    [Fact]
    public async Task WhenPassingExistingProjectAndCollaboratorIdCombo_ThenReturnAssociation()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<AbsanteeContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        using var context = new AbsanteeContext(options);

        var mockDomain1 = new Mock<IAssociationProjectCollaborator>();
        mockDomain1.Setup(x => x.GetId()).Returns(1);
        mockDomain1.Setup(x => x.GetProjectId()).Returns(1);
        mockDomain1.Setup(x => x.GetCollaboratorId()).Returns(1);

        var periodDate1 = new PeriodDate
        (
            new DateOnly(2023, 1, 1),
            new DateOnly(2023, 12, 31)
        );
        mockDomain1.Setup(x => x._periodDate).Returns(periodDate1); 

        var assoc1 = new AssociationProjectCollaboratorDataModel(mockDomain1.Object);

        var mockDomain2 = new Mock<IAssociationProjectCollaborator>();
        mockDomain2.Setup(x => x.GetId()).Returns(2);
        mockDomain2.Setup(x => x.GetProjectId()).Returns(2);
        mockDomain2.Setup(x => x.GetCollaboratorId()).Returns(2);

        var periodDate2 = new PeriodDate
        (
            new DateOnly(2022, 1, 1),
            new DateOnly(2023, 12, 31)
        );
        mockDomain2.Setup(x => x._periodDate).Returns(periodDate2); 
    

        var assoc2 = new AssociationProjectCollaboratorDataModel(mockDomain2.Object);

        var mockDomain3 = new Mock<IAssociationProjectCollaborator>();
        mockDomain3.Setup(x => x.GetId()).Returns(3);
        mockDomain3.Setup(x => x.GetProjectId()).Returns(3);
        mockDomain3.Setup(x => x.GetCollaboratorId()).Returns(3);

        var mockPeriodDate3 = new Mock<PeriodDate>();
        var periodDate3 = new PeriodDate
        (
            new DateOnly(2021, 1, 1),
            new DateOnly(2022, 12, 31)
        );
        mockDomain3.Setup(x => x._periodDate).Returns(periodDate3); 
        

        var assoc3 = new AssociationProjectCollaboratorDataModel(mockDomain3.Object);

        context.Associations.AddRange(assoc1, assoc2, assoc3);
        context.SaveChanges();

        // Mock retorno do mapper
        var domainAssoc2 = new Mock<IAssociationProjectCollaborator>();
        var domainAssoc3 = new Mock<IAssociationProjectCollaborator>();
        var expected = new List<IAssociationProjectCollaborator> { domainAssoc2.Object, domainAssoc3.Object };

        var mapper = new Mock<IMapper<IAssociationProjectCollaborator, IAssociationProjectCollaboratorVisitor>>();
        mapper.Setup(m => m.ToDomain(It.Is<List<AssociationProjectCollaboratorDataModel>>(list =>
            list.Count == 2 && list.Any(x => x.Id == 2) && list.Any(x => x.Id == 3))))
            .Returns(expected);

        var repo = new AssociationProjectCollaboratorRepositoryEF(context, mapper.Object);

        // Act
        var result = await repo.FindAllByProjectAndCollaboratorAsync(2, 2);

        // Assert
        Assert.Equal(expected, result);
    }
}