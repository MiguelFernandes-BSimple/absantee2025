namespace Domain.Interfaces;

public interface ITrainingPeriod
{
    public long GetId();
    public void SetId(long id);
    public IPeriodDate GetPeriodDate();
}
