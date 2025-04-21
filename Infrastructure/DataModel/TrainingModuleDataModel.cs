using Domain.Models;
using Domain.Visitor;

namespace Infrastructure.DataModel;

public class TrainingModuleDataModel : ITrainingModuleVisitor {
    public long Id {get; set;}
    public long SubjectId {get; set;}
    public List<PeriodDateTime> Periods {get; set;}

    public TrainingModuleDataModel(TrainingModule module) {
        Id = module.GetId();
        SubjectId = module.GetSubjectId();
        Periods = module.GetPeriods();
    }
}
