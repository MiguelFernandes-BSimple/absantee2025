namespace Domain.Interfaces;

public interface IPeriodDateTime
{
    public DateTime GetInitDate();
    public DateTime GetFinalDate();
    public void SetFinalDate(DateTime finalDate);
    public bool IsFinalDateUndefined();
    public bool Contains(IPeriodDateTime periodDateTime);
    public bool Intersects(IPeriodDateTime periodDateTime);

}