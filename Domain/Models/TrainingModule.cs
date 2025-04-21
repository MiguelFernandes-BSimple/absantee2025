using Domain.Interfaces;

namespace Domain.Models;

public class TrainingModule : ITrainingModule
{
    public long Id { get; set; }
    public long SubjectId { get; set; }
    public List<PeriodDateTime> Periods { get; set; }

    public TrainingModule(long subjectId, List<PeriodDateTime> periods)
    {

        SubjectId = subjectId;
        Periods = periods;
    }

    public TrainingModule(long id, long subjectId, List<PeriodDateTime> periods)
    {
        Id = id;
        SubjectId = subjectId;
        Periods = periods;
    }
}
