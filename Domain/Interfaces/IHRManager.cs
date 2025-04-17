using Domain.Models;

namespace Domain.Interfaces;

public interface IHRManager
{
    public long GetId();
    public long GetUserId();
    public PeriodDateTime _periodDateTime { get; set; }

    public bool ContractContainsDates(PeriodDateTime periodDateTime);

    public bool Equals(Object? obj);
}
