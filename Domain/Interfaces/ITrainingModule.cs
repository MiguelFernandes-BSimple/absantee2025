using Domain.Models;

namespace Domain.Interfaces;

public interface ITrainingModule
{
    public long Id { get; set; }
    public long TrainingSubjectId { get; set; }
    public List<PeriodDateTime> Periods { get; set; }
}