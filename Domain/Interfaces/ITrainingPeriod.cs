using Domain.Models;

namespace Domain.Interfaces;

public interface ITrainingPeriod
{
    public long GetId();
    public PeriodDate GetPeriodDate();
}
