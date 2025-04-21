using System.Text.RegularExpressions;

public class FormationSubject : IFormationSubject
{
    private long _id;
    private string _title;
    private string _description;

    public FormationSubject(string title, string description)
    {
        Regex titleRegex = new Regex(@"^[A-Za-zÀ-ÖØ-öø-ÿ\s]{1,20}$");
        Regex descriptionRegex = new Regex(@"^[A-Za-zÀ-ÖØ-öø-ÿ\s]{1,100}$");

        if (!titleRegex.IsMatch(title))
        {
            throw new ArgumentException("Invalid title");
        }

        if (!descriptionRegex.IsMatch(description))
        {
            throw new ArgumentException("Invalid description");
        }

        _title = title;
        _description = description;

    }

    public FormationSubject(long id, string title, string description)
    {
        _id = id;
        _title = title;
        _description = description;
    }


    public long GetId()
    {
        return _id;
    }

    public string GetTitle()
    {
        return _title;
    }

    public string GetDescription()
    {
        return _description;
    }

}

