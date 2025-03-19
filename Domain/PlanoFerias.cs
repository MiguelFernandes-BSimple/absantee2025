namespace Domain;

public class PlanoFerias
{
    private List<IPeriodoFerias> periodoFeriasList;
    private IColaborador colaborador;

    public PlanoFerias(IPeriodoFerias periodoFerias, IColaborador colaborador) :
        this(new List<IPeriodoFerias>() {periodoFerias}, colaborador)
    {
    }

    public PlanoFerias(List<IPeriodoFerias> periodoFeriasList, IColaborador colaborador)
    {
        if (CheckInputValues(periodoFeriasList, colaborador))
        {
            this.periodoFeriasList = new List<IPeriodoFerias>(periodoFeriasList);
            this.colaborador = colaborador;
        }
        else
            throw new ArgumentException("Invalid Arguments");
    }

    public bool AddPeriodoFerias(IPeriodoFerias periodoFerias)
    {
        if(CanInsertPeriodoFerias(periodoFerias, this.periodoFeriasList, this.colaborador)){
            periodoFeriasList.Add(periodoFerias);
            return true;
        } else
            return false;
    }

    private bool CheckInputValues(List<IPeriodoFerias> periodoFerias, IColaborador colaborador)
    {
        for (int i = 0; i < periodoFerias.Count; i++)
        {
            if (!CanInsertPeriodoFerias(periodoFerias[i], periodoFerias.Skip(i + 1).ToList(), colaborador)){
                return false;
            }
        }
        return true;
    }

    private bool CanInsertPeriodoFerias(IPeriodoFerias periodoFerias, List<IPeriodoFerias> periodoFeriasList, IColaborador colaborador)
    {
        if (colaborador.CompareWithDataInicio(periodoFerias.getDataInicio()
                                                    .ToDateTime(TimeOnly.MinValue)) < 0
            || colaborador.CompareWithDataFim(periodoFerias.getDataFim()
                                                    .ToDateTime(TimeOnly.MinValue)) > 0)
            return false;
        foreach (IPeriodoFerias pf in periodoFeriasList)
        {
            if (periodoFerias.PeriodoFeriasOverlap(pf))
            {
                return false;
            }
        }
        return true;
    }

    public bool IsSizeList(int size){
        return size == this.periodoFeriasList.Count();
    }
}