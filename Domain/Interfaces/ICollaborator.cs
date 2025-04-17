using Domain.Models;

namespace Domain.Interfaces;

public interface ICollaborator
{
    public long GetId();
    public long GetUserId();
    public IPeriodDateTime GetPeriodDateTime();
    public long _id { get; set; }
    public long _userId { get; set; }
    public IPeriodDateTime _periodDateTime { get; set; }


    public bool ContractContainsDates(IPeriodDateTime periodDateTime);

    public bool Equals(Object? obj);

}
