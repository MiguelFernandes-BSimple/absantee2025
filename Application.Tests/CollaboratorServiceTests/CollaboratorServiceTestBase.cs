using Moq;
using Domain.IRepository;
using Application.Services;
using Domain.Factory;
using Infrastructure;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Application.Tests.CollaboratorServiceTests
{
    public abstract class CollaboratorServiceTestBase
    {
        protected Mock<IAssociationProjectCollaboratorRepository> AssociationProjectCollaboratorRepositoryDouble;
        protected Mock<IHolidayPlanRepository> HolidayPlanRepositoryDouble;
        protected Mock<ICollaboratorRepository> CollaboratorRepositoryDouble;
        protected Mock<IUserRepository> UserRepositoryDouble;
        protected Mock<ICollaboratorFactory> CollaboratorFactoryDouble;
        protected Mock<IUserFactory> UserFactoryDouble;
        protected Mock<IAssociationTrainingModuleCollaboratorsRepository> AssociationTrainingModuleCollaboratorsRepositoryDouble;
        protected Mock<ITrainingModuleRepository> TrainingModuleRepositoryDouble;
        protected AbsanteeContext _context;
        protected Mock<IMapper> MapperDouble;

        protected CollaboratorService CollaboratorService;

        private static readonly Random _random = new();

        protected CollaboratorServiceTestBase()
        {
            var options = new DbContextOptionsBuilder<AbsanteeContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _context = new AbsanteeContext(options);

            AssociationProjectCollaboratorRepositoryDouble = new Mock<IAssociationProjectCollaboratorRepository>();
            HolidayPlanRepositoryDouble = new Mock<IHolidayPlanRepository>();
            CollaboratorRepositoryDouble = new Mock<ICollaboratorRepository>();
            UserRepositoryDouble = new Mock<IUserRepository>();
            CollaboratorFactoryDouble = new Mock<ICollaboratorFactory>();
            UserFactoryDouble = new Mock<IUserFactory>();
            AssociationTrainingModuleCollaboratorsRepositoryDouble = new Mock<IAssociationTrainingModuleCollaboratorsRepository>();
            TrainingModuleRepositoryDouble = new Mock<ITrainingModuleRepository>();
            MapperDouble = new Mock<IMapper>();

            CollaboratorService = new CollaboratorService(
                AssociationProjectCollaboratorRepositoryDouble.Object,
                HolidayPlanRepositoryDouble.Object,
                CollaboratorRepositoryDouble.Object,
                UserRepositoryDouble.Object,
                CollaboratorFactoryDouble.Object,
                UserFactoryDouble.Object,
                AssociationTrainingModuleCollaboratorsRepositoryDouble.Object,
                TrainingModuleRepositoryDouble.Object,
                _context,
                MapperDouble.Object
            );
        }
    }
}