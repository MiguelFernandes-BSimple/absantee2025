using Domain.Interfaces;
namespace Domain.Models;

public class TrainingPeriod : ITrainingPeriod
{
    private DateOnly _initDate;
    private DateOnly _finalDate;

    public TrainingPeriod(DateOnly initDate, DateOnly finalDate)
    {
        if (CheckInputValues(initDate, finalDate))
        {
            _initDate = initDate;
            _finalDate = finalDate;
        }
        else
            throw new ArgumentException("Invalid Arguments");
    }

    private bool CheckInputValues(DateOnly initDate, DateOnly finalDate)
    {
        if (initDate > finalDate)
            return false;

        if (initDate < DateOnly.FromDateTime(DateTime.Now))
            return false;

        return true;
    }
}
