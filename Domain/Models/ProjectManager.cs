using Domain.Interfaces;

namespace Domain.Models;

public class ProjectManager : IProjectManager
{
    public Guid Id {get;}
    public Guid UserId {get;}
    public PeriodDateTime PeriodDateTime {get;}

    public ProjectManager(Guid userId, PeriodDateTime periodDateTime)
    {
        UserId = userId;
        PeriodDateTime = periodDateTime;
    }

    public ProjectManager(Guid id, Guid userId, PeriodDateTime periodDateTime)
        : this(userId, periodDateTime)
    {
        Id = id;
    }
}