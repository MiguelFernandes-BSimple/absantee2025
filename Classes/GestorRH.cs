namespace Classes;
public class GestorRH {
    private DateTime DataInicio;
    private DateTime DataFim;
    private IUtilizador Utilizador;

    public GestorRH(DateTime DataInicio, DateTime DataFim, IUtilizador Utilizador){
        if(checkInputFields(DataInicio, DataFim, Utilizador)) {
            this.DataInicio = DataInicio;
            this.DataFim = DataFim;
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