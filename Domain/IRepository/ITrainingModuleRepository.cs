
using Domain.Models;
using Domain.Visitor;

namespace Domain.IRepository
{
    public interface ITrainingModuleRepository : IGenericRepository<TrainingModule, ITrainingModuleVisitor>
    {
        Task<IEnumerable<TrainingModule>> GetBySubjectIdAndFinished(Guid subjectId, DateTime period);
        public Task<bool> HasOverlappingPeriodsAsync(Guid trainingSubjectId, List<PeriodDateTime> newPeriods);

        Task<IEnumerable<TrainingModule>> GetBySubjectAndAfterDateFinished(Guid  subjectId, DateTime date);
    }
}
