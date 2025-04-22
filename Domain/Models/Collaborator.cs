using Domain.Interfaces;

namespace Domain.Models;

public class Collaborator : ICollaborator
{
    public long Id { get; set; }
    public long UserId { get; set; }
    public PeriodDateTime PeriodDateTime { get; set; }

    public Collaborator(long userId, PeriodDateTime periodDateTime)
    {
        PeriodDateTime = periodDateTime;
        UserId = userId;
    }

    public Collaborator(long id, long userId, PeriodDateTime periodDateTime)
    {
        Id = id;
        UserId = userId;
        PeriodDateTime = periodDateTime;
    }
    public long GetId()
    {
        return Id;
    }
    public long GetUserId()
    {
        return UserId;
    }

    public bool ContractContainsDates(PeriodDateTime periodDateTime)
    {
        return PeriodDateTime.Contains(periodDateTime);
    }
}
