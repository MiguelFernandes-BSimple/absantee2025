using Domain.Models;

namespace Domain.Interfaces;

public class ITrainingModule
{
    public long Id { get; set; }
    public long TrainingSubjectId { get; set; }
    public List<PeriodDateTime> Periods { get; }
}