using Domain.Interfaces;
namespace Domain.Models;

public class TrainingPeriod : ITrainingPeriod
{
    private IPeriodDate _periodDate;

    public TrainingPeriod(IPeriodDate periodDate)
    {
        if (CheckInputValues(periodDate))
        {
            _periodDate = periodDate;
        }
        else
            throw new ArgumentException("Invalid Arguments");
    }

    private bool CheckInputValues(IPeriodDate periodDate)
    {

        if (periodDate.GetInitDate() < DateOnly.FromDateTime(DateTime.Now))
            return false;

        return true;
    }
}
