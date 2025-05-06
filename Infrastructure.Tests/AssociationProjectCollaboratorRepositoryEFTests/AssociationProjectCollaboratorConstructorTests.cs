//using Domain.Interfaces;
//using Domain.Visitor;
//using AutoMapper;
//using Microsoft.EntityFrameworkCore;
//using Moq;

//namespace Infrastructure.Tests.AssociationProjectCollaboratorRepositoryEFTests;

//public class AssociationProjectCollaboratorConstructorTests
//{
//    [Fact]
//    public void WhenPassingCorrectParameters_ThenCreateAssociationRepository()
//    {
//        // Arrange
//        DbContextOptions<AbsanteeContext> options = new DbContextOptions<AbsanteeContext>();

//        Mock<AbsanteeContext> contextDouble = new Mock<AbsanteeContext>(options);
//        Mock<IMapper> mapperMock =
//             new Mock<IMapper>();

//        // Act
//        AssociationProjectCollaboratorRepositoryEF result =
//            new AssociationProjectCollaboratorRepositoryEF(contextDouble.Object, mapperMock.Object);

//        // Assert
//        Assert.NotNull(result);
//    }
//}