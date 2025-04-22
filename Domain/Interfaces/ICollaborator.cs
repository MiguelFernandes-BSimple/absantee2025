using Domain.Models;

namespace Domain.Interfaces;

public interface ICollaborator
{
    public long GetId();
    public long GetUserId();
    public long Id { get; set; }
    public long UserId { get; set; }
    public PeriodDateTime PeriodDateTime { get; set; }
    public bool ContractContainsDates(PeriodDateTime periodDateTime);
}
