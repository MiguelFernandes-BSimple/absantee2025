using Domain.Interfaces;
using Domain.Models;
using Domain.Visitor;

namespace Domain.IRepository
{
    public interface ITrainingModuleRepository : IGenericRepository<ITrainingModule, ITrainingModuleVisitor>
    {
        Task<IEnumerable<ITrainingModule>> GetBySubjectIdAndFinished(Guid subjectId, DateTime period);
    }
}
