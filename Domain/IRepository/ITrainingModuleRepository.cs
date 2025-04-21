using Domain.Interfaces;
using Domain.Models;
using Domain.Visitor;

namespace Domain.IRepository;
public interface ITrainingModuleRepository : IGenericRepository<ITrainingModule, ITrainingModuleVisitor>
{
    public Task<IEnumerable<ITrainingModule>> FindAllBySubject(long trainingSubjectId);
    public Task<IEnumerable<ITrainingModule>> FindAllBySubjectAndAfterPeriod(long trainingSubjectId, DateTime period);
}