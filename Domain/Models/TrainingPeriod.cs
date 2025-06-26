using Domain.Interfaces;
namespace Domain.Models;

public class TrainingPeriod : ITrainingPeriod
{
    public Guid Id { get; }
    public PeriodDate PeriodDate { get; }

    public TrainingPeriod(PeriodDate periodDate)
    {
        if (periodDate.IsInitDateSmallerThan(DateOnly.FromDateTime(DateTime.Now)))
            throw new ArgumentException("Period date cannot start in the past.");

        Id = Guid.NewGuid();
        PeriodDate = periodDate;
    }

    public TrainingPeriod(Guid id, PeriodDate periodDate)
    {
        Id = id;
        PeriodDate = periodDate;
    }

}
