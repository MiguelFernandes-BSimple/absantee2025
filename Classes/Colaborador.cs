
namespace Classes;


public class Colaborador : IColaborador {
    public Colaborador(DateTime DataInicio, DateTime DataFim, IUtilizador Utilizador)
    {
        if (checkInputFields(DataInicio, DataFim, Utilizador)){
            this.DataInicio = DataInicio;
            this.DataFim = DataFim;
            this.Utilizador = Utilizador;
        } else
            throw new ArgumentException("Invalid Arguments");
    }

    private DateTime DataInicio;
    private DateTime DataFim;
    private IUtilizador Utilizador;

    private bool checkInputFields(DateTime DataInicio, DateTime DataFim, IUtilizador utilizador){
        if(DataInicio > DataFim)
            return false;

        if(utilizador.IsBiggerThenDataDesativacao(DataFim))
            return false;
        
        if(utilizador.IsDesativo())
            return false;
        
        return true;
    }

    public int CompareWithDataInicio(DateTime dataInicio)
    {
        return dataInicio.CompareTo(this.DataInicio);
    }

    public int CompareWithDataFim(DateTime dataFim)
    {
        return dataFim.CompareTo(this.DataFim);
    }
}