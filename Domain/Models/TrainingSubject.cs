using System.Text.RegularExpressions;
using Domain.Interfaces;

namespace Domain.Models;

public class TrainingSubject : ITrainingSubject
{
    public long Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }


    public TrainingSubject(string title, string description)
    {
        if (string.IsNullOrWhiteSpace(title) ||
            title.Count() < 20 ||
            Regex.IsMatch(title, @"^[a-zA-Z0-9]+$"))
            throw new ArgumentException("Invalid input");

        if (string.IsNullOrWhiteSpace(description) ||
            description.Count() < 100 ||
            Regex.IsMatch(title, @"^[a-zA-Z0-9]+$"))
            throw new ArgumentException("Invalid input");

        Title = title;
        Description = description;
    }
}