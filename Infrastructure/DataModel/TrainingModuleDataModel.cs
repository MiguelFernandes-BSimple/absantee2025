using System.ComponentModel.DataAnnotations.Schema;
using Domain.Interfaces;
using Domain.Models;
using Domain.Visitor;

namespace Infrastructure.DataModel;

[Table("TrainingModule")]
public class TrainingModuleDataModel : ITrainingModuleVisitor
{
    public long Id { get; set; }
    public long SubjectId { get; set; }
    public List<PeriodDateTime> Periods { get; set; }

    public TrainingModuleDataModel() { }

    public TrainingModuleDataModel(ITrainingModule tm)
    {
        Id = tm.Id;
        SubjectId = tm.SubjectId;
        Periods = tm.Periods;
    }
}