namespace Domain;
public class TrainingPeriod {

    private DateOnly _initDate;
    private DateOnly _finalDate;
    public TrainingPeriod(DateOnly dataInicio, DateOnly dataFim)
    {
        if(CheckInputValues(dataInicio, dataFim)){
            _initDate = dataInicio;
            _finalDate = dataFim;
        }
        else
            throw new ArgumentException("Invalid Arguments");
    }

    private bool CheckInputValues(DateOnly _initDate, DateOnly _finalDate)
    {
        if (_initDate > _finalDate)
            return false;

        if (_initDate < DateOnly.FromDateTime(DateTime.Now))
            return false;

        return true;
    }
}