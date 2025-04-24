using Domain.Interfaces;

namespace Domain.Models;

public class Collaborator : ICollaborator
{
    public Guid Id { get; }
    public Guid UserId { get; }
    public PeriodDateTime PeriodDateTime { get; set; }

    public Collaborator(Guid userId, PeriodDateTime periodDateTime)
    {
        PeriodDateTime = periodDateTime;
        UserId = userId;
        Id = new Guid();
    }

    public Collaborator(Guid id, Guid userId, PeriodDateTime periodDateTime)
    {
        Id = id;
        UserId = userId;
        PeriodDateTime = periodDateTime;
    }

    public bool ContractContainsDates(PeriodDateTime periodDateTime)
    {
        return PeriodDateTime.Contains(periodDateTime);
    }
}
