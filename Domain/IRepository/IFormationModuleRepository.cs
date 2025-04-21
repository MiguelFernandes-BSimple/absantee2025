using Domain.Interfaces;
using Domain.Visitor;

namespace Domain.IRepository;

public interface IFormationModuleRepository : IGenericRepository<IFormationModule, IFormationModuleVisitor>
{
    public Task<IFormationModule?> GetBySubjectId(long subjectId);

}