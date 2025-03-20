using System.Text.RegularExpressions;

namespace Domain;
public class Project : IProject
{
    private string _title;
    private string _acronym;
    private DateOnly _initDate;
    private DateOnly _finalDate;

    public Project(string title, string acronym, DateOnly initDate, DateOnly finalDate)
    {
        if (CheckInputValues(title, acronym, initDate, finalDate))
        {
            this._title = title;
            this._acronym = acronym;
            this._initDate = initDate;
            this._finalDate = finalDate;
        }
        else
            throw new ArgumentException("Invalid Arguments");
    }

    private bool CheckInputValues(string title, string acronym, DateOnly initDate, DateOnly finalDate)
    {
        if (initDate > finalDate)
            return false;

        Regex tituloRegex = new Regex(@"^.{1,50}$");
        Regex siglaRegex = new Regex(@"^[A-Z0-9]{1,10}$");

        if (!tituloRegex.IsMatch(title) || !siglaRegex.IsMatch(acronym))
        {
            return false;
        }

        return true;
    }

    public bool IsInside(DateOnly dataInicio, DateOnly dataFim){
        return dataInicio >= this._initDate && dataFim <= this._finalDate;
    }

    public bool IsFinished(){
        return DateOnly.FromDateTime(DateTime.Today) > this._finalDate;
    }
}