namespace Domain.Interfaces;

public interface IPeriodDateTime
{
    public DateTime GetInitDate();
    public DateTime GetFinalDate();
    public void SetInitDate(DateTime initDate);
    public void SetFinalDate(DateTime endDate);
    public bool IsFinalDateUndefined();
    public bool Contains(IPeriodDateTime periodDateTime);

}