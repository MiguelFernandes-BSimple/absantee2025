using Domain.Interfaces;

namespace Domain.Models;

public class HRManager : IHRManager
{
    public Guid Id { get; }
    public Guid UserId { get; }
    public PeriodDateTime PeriodDateTime { get; }

    public HRManager(Guid userId, PeriodDateTime periodDateTime)
    {
        PeriodDateTime = periodDateTime;
        UserId = userId;
    }

    public HRManager(Guid id, Guid userId, PeriodDateTime periodDateTime)
        : this(userId, periodDateTime)
    {
        Id = id;
    }

}