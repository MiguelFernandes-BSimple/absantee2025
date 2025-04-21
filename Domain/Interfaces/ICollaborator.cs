using Domain.Models;

namespace Domain.Interfaces;

public interface ICollaborator
{
    public long GetId();
    public long GetUserId();
    public long _id { get; set; }
    public long _userId { get; set; }
    public PeriodDateTime _periodDateTime { get; set; }
    public bool ContractContainsDates(PeriodDateTime periodDateTime);
    public void AddTrainingModule(long id);
    public bool IsAlreadyOnTraining(long tmId);
}
