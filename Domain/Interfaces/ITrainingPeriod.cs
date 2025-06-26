using Domain.Models;

namespace Domain.Interfaces;

public interface ITrainingPeriod
{
    Guid Id { get; }
    PeriodDate PeriodDate { get; }
}
