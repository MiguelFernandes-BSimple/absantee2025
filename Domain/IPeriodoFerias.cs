using Domain;

public interface IPeriodoFerias{
    public DateOnly GetDataInicio();
    public DateOnly GetDataFim();
    public bool PeriodoFeriasOverlap(IPeriodoFerias periodoFerias);
}