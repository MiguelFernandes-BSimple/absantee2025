using Classes;

public interface IPeriodoFerias{
    public DateOnly getDataInicio();
    public DateOnly getDataFim();
    public bool PeriodoFeriasOverlap(IPeriodoFerias periodoFerias);
}