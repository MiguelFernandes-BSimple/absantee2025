using Domain.Interfaces;
using Domain.Visitor;

namespace Domain.IRepository
{
    public interface ITrainingModuleRepository : IGenericRepository<ITrainingModule, ITrainingModuleVisitor>
    {
        Task<IEnumerable<ITrainingModule>> GetBySubjectIdAndFinished(long subjectId);
    }
}
