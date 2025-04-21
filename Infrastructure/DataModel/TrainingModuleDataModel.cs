using Domain.Interfaces;
using Domain.Models;
using Domain.Visitor;

namespace Infrastructure.DataModel;

public class TrainingModuleDataModel : ITrainingModuleVisitor
{
    public long Id { get; set; }

    public long TrainingSubjectId { get; set; }

    public List<PeriodDateTime> Periods { get; set; }

    public TrainingModuleDataModel()
    {

    }
    public TrainingModuleDataModel(ITrainingModule tm)
    {
        Id = tm.Id;
        TrainingSubjectId = tm.TrainingSubjectId;
        Periods = tm.Periods;
    }
}