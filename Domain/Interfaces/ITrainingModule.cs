using Domain.Models;

namespace Domain.Interfaces;



public interface ITrainingModule
{
    long GetId();
    void SetId(long id);
    Subject GetAssunto();
    List<PeriodDateTime> GetPeriodos();
    void SetPeriodos(List<PeriodDateTime> periodos);
}