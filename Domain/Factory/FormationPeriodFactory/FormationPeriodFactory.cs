using Domain.Factory.FormationPeriodFactory;
using Domain.Models;
using Domain.Visitor;

namespace Domain.Factory.TrainingPeriodFactory;

public class FormationPeriodFactory : IFormationPeriodFactory
{
    public FormationPeriodFactory()
    {
    }

    public FormationPeriod Create(PeriodDate periodDate)
    {
        return new FormationPeriod(periodDate);
    }

    public FormationPeriod Create(IFormationPeriodVisitor formationPeriodVisitor)
    {
        return new FormationPeriod(formationPeriodVisitor.Id, formationPeriodVisitor.PeriodDate);
    }
}