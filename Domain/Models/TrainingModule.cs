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
        for (int p = 0; p < periods.Count(); p++)
        {
            PeriodDateTime currPeriod = periods[p];
            if (currPeriod._finalDate < DateTime.Now)
                throw new ArgumentException("Invalid input");

            bool intersects = periods.Skip(p + 1).Any(currPeriod.Intersects);

            if (intersects)
                throw new ArgumentException("Invalid inputs");
        }

        Periods = periods;
    }
}