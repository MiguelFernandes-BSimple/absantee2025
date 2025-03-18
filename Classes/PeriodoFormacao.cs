namespace Classes;
public class PeriodoFormacao {
    public PeriodoFormacao(DateOnly dataInicio, DateOnly dataFim)
    {
        if(CheckInputValues(dataInicio, dataFim)){
            DataInicio = dataInicio;
            DataFim = dataFim;
        }
        else
            throw new ArgumentException("Invalid Arguments");
    }

    private DateOnly DataInicio;
    private DateOnly DataFim;

    private bool CheckInputValues(DateOnly dataInicio, DateOnly dataFim)
    {
        if (dataInicio > dataFim)
            return false;

        if (dataInicio < DateOnly.FromDateTime(DateTime.Now))
            return false;

        return true;
    }
}