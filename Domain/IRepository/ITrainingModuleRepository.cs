using Domain.Interfaces;
using Domain.Models;
using Domain.Visitor;

namespace Domain.IRepository
{
    public interface ITrainingModuleRepository
    {
        Task<IEnumerable<ITrainingModule>> GetBySubjectIdAndFinished(long subjectId, DateTime period);
    }
}
