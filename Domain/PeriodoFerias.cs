
namespace Domain;
public class PeriodoFerias : IPeriodoFerias {
    private DateOnly DataInicio;
    private DateOnly DataFim;

    public PeriodoFerias(DateOnly dataInicio, DateOnly dataFim)
    {
        if (CheckInputValues(dataInicio, dataFim)){
            DataInicio = dataInicio;
            DataFim = dataFim;
        } else 
            throw new ArgumentException("Invalid Arguments");
    }

    public DateOnly GetDataInicio(){
        return DataInicio;
    }

    public DateOnly GetDataFim(){
        return DataFim;
    }

    private bool CheckInputValues(DateOnly dataInicio, DateOnly dataFim){
        if(dataInicio > dataFim)
            return false;
        
        return true;
    }

    public bool PeriodoFeriasOverlap(IPeriodoFerias periodoFerias){
        return DataInicio < periodoFerias.GetDataFim() 
            && periodoFerias.GetDataInicio() < DataFim;
    }
}