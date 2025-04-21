using System.ComponentModel.DataAnnotations.Schema;
using Domain.Models;
using Domain.Visitor;
using Domain.Interfaces;

namespace Infrastructure.DataModel;

[Table("FormationPeriod")]
public class FormationPeriodDataModel : IFormationPeriodVisitor
{
    public long Id { get; set; }
    public PeriodDate PeriodDate { get; set; }

    public FormationPeriodDataModel(IFormationPeriod formationPeriod)
    {
        Id = formationPeriod.GetId();
        PeriodDate = formationPeriod._periodDate;
    }

    public FormationPeriodDataModel()
    {
    }
}
