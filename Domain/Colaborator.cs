
namespace Domain;


public class Colaborator : IColaborator {
    private DateTime _initDate;
    private DateTime _finalDate;
    private IUser _user;

    public Colaborator(IUser user, DateTime initDate, DateTime? finalDate = null)
    {
        if(!finalDate.HasValue)
            finalDate = DateTime.MaxValue;
        if (checkInputFields(initDate, (DateTime)finalDate, user)){
            this._initDate = initDate;
            this._finalDate = (DateTime)finalDate;
            this._user = user;
        } else
            throw new ArgumentException("Invalid Arguments");
    }

    private bool checkInputFields(DateTime DataInicio, DateTime DataFim, IUser utilizador){
        if(DataInicio > DataFim)
            return false;

        if(utilizador.DeactivationDateIsBeforeThen(DataFim))
            return false;
        
        if(utilizador.IsDeactivated())
            return false;
        
        return true;
    }
    
    public bool IsInside(DateTime dataInicio, DateTime dataFim){
        return dataInicio >= this._initDate && dataFim <= this._finalDate;
    }

}