using Domain.Models;

namespace Domain.Interfaces;

public interface IProjectManager
{
    public long GetId();
    public void SetId(long id);
    public long GetUserId();
    public PeriodDateTime GetPeriodDateTime();
}
