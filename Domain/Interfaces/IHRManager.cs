namespace Domain.Interfaces;

public interface IHRManager
{
    public long GetId();
    public long GetUserId();
    public IPeriodDateTime GetPeriodDateTime();


    public bool ContractContainsDates(IPeriodDateTime periodDateTime);

    public bool Equals(Object? obj);
}
