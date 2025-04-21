using Domain.Models;

namespace Domain.Interfaces;

public interface IFormationPeriod
{
    public long GetId();
    public PeriodDate _periodDate { get; set; }

}