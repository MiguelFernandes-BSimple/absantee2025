using Domain.Interfaces;

namespace Domain.Models;

public class TrainingModule : ITrainingModule
{
    public long Id { get; set; }
    public long TrainingSubjectId { get; set; }
    public List<PeriodDateTime> Periods { get; set; }

    public TrainingModule(long trainingSubjectId)
    {
        TrainingSubjectId = trainingSubjectId;
        Periods = new List<PeriodDateTime>();
    }

    public TrainingModule(long trainingSubjectId, List<PeriodDateTime> periods) : this(trainingSubjectId)
    {
        bool isNotInFuture =
            periods.Any(p => p._initDate < DateTime.Now);

        if (isNotInFuture)
            throw new ArgumentException("Invalid inputs");

        for (int p = 0; p < periods.Count(); p++)
        {
            PeriodDateTime currPeriod = periods[p];
            bool intersects = periods.Any(p2 => currPeriod.Intersects(p2));

            if (intersects)
                throw new ArgumentException("Invalid inputs");
        }

        Periods = periods;
    }
}