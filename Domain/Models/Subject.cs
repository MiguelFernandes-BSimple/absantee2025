using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Domain.Interfaces;


namespace Domain.Models;

public class Subject : ISubject
{
    private long _id;
    private string _title;
    private string _description;

    public Subject(long id, string title, string description)
    {
        Regex titleRegex = new Regex(@"^[A-Za-z0-9\s]{1,20}$");
        Regex descRegex = new Regex(@"^[A-Za-z0-9\s]{1,100}$");

        if (!titleRegex.IsMatch(title))
            throw new ArgumentException("Invalid title.");
        if (!descRegex.IsMatch(description))
            throw new ArgumentException("Invalid description.");
        _id = id;
        _title = title;
        _description = description;
    }

    public long GetId()
    {
        return _id;
    }
    public void SetId(long id)
    {
        _id = id;
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
