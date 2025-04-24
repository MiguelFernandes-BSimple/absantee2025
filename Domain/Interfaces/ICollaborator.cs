using Domain.Models;

namespace Domain.Interfaces;

public interface ICollaborator
{
    public Guid Id { get; }
    public Guid UserId { get; }
    public PeriodDateTime PeriodDateTime { get; }
    public bool ContractContainsDates(PeriodDateTime periodDateTime);
}
