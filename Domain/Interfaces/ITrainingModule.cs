using Domain.Models;

namespace Domain.Interfaces
{
    public interface ITrainingModule
    {
        long Id { get; }
        long TrainingSubjectId { get; }
        List<PeriodDateTime> Periods { get; }
    }
}
