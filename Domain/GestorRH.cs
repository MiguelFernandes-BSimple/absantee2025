namespace Domain;
public class GestorRH {
    private DateTime DataInicio;
    private DateTime DataFim;
    private IUtilizador Utilizador;

    public GestorRH(IUtilizador Utilizador, DateTime DataInicio, DateTime? DataFim = null)
    {
        if(!DataFim.HasValue)
            DataFim = DateTime.MaxValue;
        if(checkInputFields(DataInicio, (DateTime)DataFim, Utilizador)) {
            this.DataInicio = DataInicio;
            this.DataFim = (DateTime)DataFim;
            this.Utilizador = Utilizador;
        } else 
            throw new ArgumentException("Invalid Arguments");
    }

    private bool checkInputFields(DateTime DataInicio, DateTime DataFim, IUtilizador utilizador){
        if(DataInicio > DataFim)
            return false;

        if(utilizador.IsBiggerThenDataDesativacao(DataFim))
            return false;
        
        if(utilizador.IsDesativo())
            return false;
        
        return true;
    }
}