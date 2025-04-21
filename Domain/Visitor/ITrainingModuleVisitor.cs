using Domain.Models;

namespace Domain.Visitor;

public interface ITrainingModuleVisitor {
    long Id {get;}
    long SubjectId {get;}
    List<PeriodDateTime> Periods {get;}
}
