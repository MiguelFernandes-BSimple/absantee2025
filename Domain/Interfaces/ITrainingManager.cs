using Domain.Models;

namespace Domain.Interfaces;

public interface ITrainingManager
{
    public long GetId();
    public void SetId(long id);
    public long GetUserId();
    public PeriodDateTime GetPeriodDateTime();
}