using Domain.Models;

namespace Domain.Interfaces;

public interface ICollaborator
{
    public long GetId();
    public long GetUserId();
    public IPeriodDateTime GetPeriodDateTime();


    public bool ContractContainsDates(IPeriodDateTime periodDateTime);

    public bool Equals(Object? obj);

}
