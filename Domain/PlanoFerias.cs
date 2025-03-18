using System.Security.Cryptography;
using Domain;

public class PlanoFerias {
    private List<IPeriodoFerias> periodoFeriasList;
    private IColaborador colaborador;

    public PlanoFerias(List<IPeriodoFerias> periodoFerias, IColaborador colaborador)
    {
        if(CheckInputValues(periodoFerias, colaborador)){
            this.periodoFeriasList = periodoFerias;
            this.colaborador = colaborador;
        } else 
            throw new ArgumentException("Invalid Arguments");      
    }

    private bool CheckInputValues(List<IPeriodoFerias> periodoFerias, IColaborador colaborador){
        for (int i = 0; i < periodoFerias.Count; i++)
        {
            IPeriodoFerias currentPeriodoFerias = periodoFerias[i];
            if(colaborador.CompareWithDataInicio(currentPeriodoFerias.getDataInicio()
                                                    .ToDateTime(TimeOnly.MinValue)) < 0 
            || colaborador.CompareWithDataFim(currentPeriodoFerias.getDataFim()
                                                    .ToDateTime(TimeOnly.MinValue)) > 0)
                return false;
            for (int j = i + 1; j < periodoFerias.Count; j++)
            {
                if (currentPeriodoFerias.PeriodoFeriasOverlap(periodoFerias[j])){
                    return false;
                }
            }
        }
        return true;
    }
}