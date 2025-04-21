using Domain.Interfaces;
using Domain.Models;
using Domain.Visitor;

namespace Infrastructure.DataModel;

public class FormationModuleDataModel : IFormationModuleVisitor
{
    public long Id { get; set; }
    public long FormationSubjectId { get; set; }
    public List<PeriodDateTime> FormationPeriods { get; set; }

    public List<PeriodDateTime> GetFormationPeriods()
    {
        return FormationPeriods;
    }

    public FormationModuleDataModel(IFormationModule formationModule)
    {
        Id = formationModule.GetId();
        FormationSubjectId = formationModule.GetFormationSubjectId();
        FormationPeriods = formationModule.GetFormationPeriods();
    }

    public FormationModuleDataModel() { }
}