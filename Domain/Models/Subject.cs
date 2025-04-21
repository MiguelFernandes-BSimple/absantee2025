using Domain.Interfaces;
using System.Text.RegularExpressions;

namespace Domain.Models;

public class Subject : ISubject
{
    private long _id;
    private string _title;
    private string _description;

    public Subject(string title, string description)
    {

        Regex titleRegex = new Regex(@"^[A-Za-zÀ-ÖØ-öø-ÿ\s]{1,20}$");

        if (!titleRegex.IsMatch(title) || title == null)
            throw new ArgumentException("Invalid title.");

        Regex descriptionRegex = new Regex(@"^[A-Za-zÀ-ÖØ-öø-ÿ\s]{1,50}$");

        if (!descriptionRegex.IsMatch(description) || description == null)
            throw new ArgumentException("Invalid description.");

        _title = title;
        _description = description;
    }

    public Subject(long id, string title, string description)
    {
        _id = id;
        _title = title;
        _description = description;
    }
}
