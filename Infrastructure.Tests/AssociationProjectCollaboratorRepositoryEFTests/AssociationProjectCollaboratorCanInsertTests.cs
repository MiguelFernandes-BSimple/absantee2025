using Domain.Interfaces;
using Domain.Models;
using Domain.Visitor;
using Infrastructure.DataModel;
using Infrastructure.Mapper;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Infrastructure.Tests.AssociationProjectCollaboratorRepositoryEFTests;

public class AssociationProjectCollaboratorCanInsertTests
{
    [Fact]
    public async Task WhenPassingValidNewAssociationParameters_ThenReturnTrue()
    {
        // Arrange
        // Create mockSet
        var AssocDM1 = new Mock<IAssociationProjectCollaboratorVisitor>();
        var AssocDM2 = new Mock<IAssociationProjectCollaboratorVisitor>();
        var AssocDM3 = new Mock<IAssociationProjectCollaboratorVisitor>();
        var users = new List<AssociationProjectCollaboratorDataModel>
        {
            (AssociationProjectCollaboratorDataModel)AssocDM1.Object,
            (AssociationProjectCollaboratorDataModel)AssocDM2.Object,
            (AssociationProjectCollaboratorDataModel)AssocDM3.Object
        }.AsQueryable();

        var mockSet = new Mock<DbSet<AssociationProjectCollaboratorDataModel>>();
        mockSet.As<IQueryable<AssociationProjectCollaboratorDataModel>>().Setup(m => m.Provider).Returns(users.Provider);
        mockSet.As<IQueryable<AssociationProjectCollaboratorDataModel>>().Setup(m => m.Expression).Returns(users.Expression);
        mockSet.As<IQueryable<AssociationProjectCollaboratorDataModel>>().Setup(m => m.ElementType).Returns(users.ElementType);
        mockSet.As<IQueryable<AssociationProjectCollaboratorDataModel>>().Setup(m => m.GetEnumerator()).Returns(users.GetEnumerator());

        // Setup assoc id
        long assocDM1Id = 1;
        long assocDM2Id = 2;
        long assocDM3Id = 3;

        // Setup assoc project id
        long collab1Id = 1;
        long collab2Id = 2;

        // Setup assoc project id
        long project1Id = 1;
        long project2Id = 2;

        // Periods for each association
        Mock<IPeriodDate> periodDate1 = new Mock<IPeriodDate>();
        Mock<IPeriodDate> periodDate2 = new Mock<IPeriodDate>();

        // New association period - to search
        Mock<IPeriodDate> periodToSearch = new Mock<IPeriodDate>();

        // Declare and set up context with associations
        var mockContext = new Mock<IAbsanteeContext>();
        mockContext.Setup(c => c.Associations).Returns(mockSet.Object);

        // Association Ids
        AssocDM1.Setup(a => a.Id).Returns(assocDM1Id);
        AssocDM2.Setup(a => a.Id).Returns(assocDM2Id);
        AssocDM3.Setup(a => a.Id).Returns(assocDM3Id);

        // Collab Ids
        AssocDM2.Setup(a => a.CollaboratorId).Returns(collab1Id);
        AssocDM2.Setup(a => a.CollaboratorId).Returns(collab2Id);
        AssocDM3.Setup(a => a.CollaboratorId).Returns(collab2Id);

        // Project Ids
        AssocDM2.Setup(a => a.ProjectId).Returns(project1Id);
        AssocDM2.Setup(a => a.ProjectId).Returns(project2Id);
        AssocDM3.Setup(a => a.ProjectId).Returns(project2Id);

        // PeriodDates 
        AssocDM1.Setup(a => a.Period).Returns((PeriodDate)periodDate1.Object);
        AssocDM2.Setup(a => a.Period).Returns((PeriodDate)periodDate2.Object);
        AssocDM3.Setup(a => a.Period).Returns((PeriodDate)periodDate1.Object);

        // Filtered list - same collab id and same project id
        List<AssociationProjectCollaboratorDataModel> assocDmsList =
            new List<AssociationProjectCollaboratorDataModel> { (AssociationProjectCollaboratorDataModel)AssocDM2.Object, (AssociationProjectCollaboratorDataModel)AssocDM3.Object };

        var mapper = new Mock<IMapper<IAssociationProjectCollaborator, IAssociationProjectCollaboratorVisitor>>();

        // Instatiate repository
        var assocRepo =
            new AssociationProjectCollaboratorRepositoryEF((AbsanteeContext)mockContext.Object, mapper.Object);

        // Act
        bool result = await assocRepo.CanInsert(periodToSearch.Object, project2Id, collab2Id);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task WhenPassingInValidNewAssociationParameters_ThenReturnFalse()
    {
        // Arrange
        // Create mockSet
        var AssocDM1 = new Mock<IAssociationProjectCollaboratorVisitor>();
        var AssocDM2 = new Mock<IAssociationProjectCollaboratorVisitor>();
        var AssocDM3 = new Mock<IAssociationProjectCollaboratorVisitor>();
        var users = new List<AssociationProjectCollaboratorDataModel>
        {
            (AssociationProjectCollaboratorDataModel)AssocDM1.Object,
            (AssociationProjectCollaboratorDataModel)AssocDM2.Object,
            (AssociationProjectCollaboratorDataModel)AssocDM3.Object
        }.AsQueryable();

        var mockSet = new Mock<DbSet<AssociationProjectCollaboratorDataModel>>();
        mockSet.As<IQueryable<AssociationProjectCollaboratorDataModel>>().Setup(m => m.Provider).Returns(users.Provider);
        mockSet.As<IQueryable<AssociationProjectCollaboratorDataModel>>().Setup(m => m.Expression).Returns(users.Expression);
        mockSet.As<IQueryable<AssociationProjectCollaboratorDataModel>>().Setup(m => m.ElementType).Returns(users.ElementType);
        mockSet.As<IQueryable<AssociationProjectCollaboratorDataModel>>().Setup(m => m.GetEnumerator()).Returns(users.GetEnumerator());

        // Setup assoc id
        long assocDM1Id = 1;
        long assocDM2Id = 2;
        long assocDM3Id = 3;

        // Setup assoc project id
        long collab1Id = 1;
        long collab2Id = 2;

        // Setup assoc project id
        long project1Id = 1;
        long project2Id = 2;

        // Periods for each association
        Mock<IPeriodDate> periodDate1 = new Mock<IPeriodDate>();
        Mock<IPeriodDate> periodDate2 = new Mock<IPeriodDate>();

        // Declare and set up context with associations
        var mockContext = new Mock<IAbsanteeContext>();
        mockContext.Setup(c => c.Associations).Returns(mockSet.Object);

        // Association Ids
        AssocDM1.Setup(a => a.Id).Returns(assocDM1Id);
        AssocDM2.Setup(a => a.Id).Returns(assocDM2Id);
        AssocDM3.Setup(a => a.Id).Returns(assocDM3Id);

        // Collab Ids
        AssocDM2.Setup(a => a.CollaboratorId).Returns(collab1Id);
        AssocDM2.Setup(a => a.CollaboratorId).Returns(collab2Id);
        AssocDM3.Setup(a => a.CollaboratorId).Returns(collab2Id);

        // Project Ids
        AssocDM2.Setup(a => a.ProjectId).Returns(project1Id);
        AssocDM2.Setup(a => a.ProjectId).Returns(project2Id);
        AssocDM3.Setup(a => a.ProjectId).Returns(project2Id);

        // PeriodDates 
        AssocDM1.Setup(a => a.Period).Returns((PeriodDate)periodDate1.Object);
        AssocDM2.Setup(a => a.Period).Returns((PeriodDate)periodDate2.Object);
        AssocDM3.Setup(a => a.Period).Returns((PeriodDate)periodDate1.Object);

        // Filtered list - same collab id and same project id
        List<AssociationProjectCollaboratorDataModel> assocDmsList =
            new List<AssociationProjectCollaboratorDataModel> { (AssociationProjectCollaboratorDataModel)AssocDM2.Object, (AssociationProjectCollaboratorDataModel)AssocDM3.Object };

        var mapper = new Mock<IMapper<IAssociationProjectCollaborator, IAssociationProjectCollaboratorVisitor>>();

        // Instatiate repository
        var assocRepo =
            new AssociationProjectCollaboratorRepositoryEF((AbsanteeContext)mockContext.Object, mapper.Object);

        // Act
        // going to search with AssocDM2 parameters - invalid new association parameters
        bool result = await assocRepo.CanInsert(periodDate2.Object, project2Id, collab2Id);

        // Assert
        Assert.False(result);
    }
}