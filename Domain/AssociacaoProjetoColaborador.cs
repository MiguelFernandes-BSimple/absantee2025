namespace Domain;

public class AssociacaoProjetoColaborador {
    private DateOnly DataInicio;
    private DateOnly DataFim;
    private IColaborador Colaborador;
    private IProjeto Projeto;

    public AssociacaoProjetoColaborador(DateOnly dataInicio, DateOnly dataFim, IColaborador colaborador, IProjeto projeto)
    {
        if(CheckInputValues(dataInicio, dataFim, colaborador, projeto)){
           DataInicio = dataInicio;
            DataFim = dataFim;
            Colaborador = colaborador;
            Projeto = projeto; 
        } else  	
            throw new ArgumentException("Invalid Arguments");        
    }

    private bool CheckInputValues(DateOnly dataInicio, DateOnly dataFim, IColaborador colaborador, IProjeto projeto){
        if(dataInicio > dataFim)
            return false;
        
        if(projeto.CompareWithDataInicio(dataInicio) < 0 
            || projeto.CompareWithDataFim(dataFim) > 0)
            return false;

        if(projeto.CompareWithDataFim(DateOnly.FromDateTime(DateTime.Today)) < 0)
            return false;

        if(colaborador.CompareWithDataInicio(dataInicio.ToDateTime(TimeOnly.MinValue)) < 0 
            || colaborador.CompareWithDataFim(dataFim.ToDateTime(TimeOnly.MinValue)) > 0)
            return false;

        return true;
    }

}