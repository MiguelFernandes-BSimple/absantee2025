using Domain.Models;

namespace Domain.Interfaces;

public interface IHRManager
{
    public Guid Id { get;} 
    public Guid UserId { get;} 
    public PeriodDateTime PeriodDateTime { get;}


    public bool ContractContainsDates(PeriodDateTime periodDateTime);

    public bool Equals(Object? obj);
}
