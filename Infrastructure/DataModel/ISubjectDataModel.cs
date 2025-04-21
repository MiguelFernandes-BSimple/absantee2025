using System.ComponentModel.DataAnnotations.Schema;
using Domain.Interfaces;
using Domain.Models;

namespace Infrastructure.DataModel;

[Table("Subject")]
public class SubjectDataModel
{
    public long Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }


    public SubjectDataModel(Subject subject)
    {
        Id = subject.GetId();
        Title = subject.GetTitle();
        Description = subject.GetDescription();
    }
}