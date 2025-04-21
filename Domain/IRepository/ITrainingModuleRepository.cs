using Domain.Interfaces;
using Domain.Visitor;

namespace Domain.IRepository;

public interface ITrainingModuleRepository : IGenericRepository<ITrainingModule, ITrainingModuleVisitor> {
    
}
