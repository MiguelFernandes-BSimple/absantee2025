using Domain.Interfaces;
using Domain.Models;
using Domain.Visitor;
using Infrastructure.Mapper;

namespace Infrastructure.DataModel;

public class FormationModuleDataModel : IFormationModuleVisitor
{
    private IMapper<IFormationPeriod, FormationPeriodDataModel> _mapper;
    public long Id { get; set; }
    public long FormationSubjectId { get; set; }
    public List<FormationPeriodDataModel> FormationPeriodsDM { get; set; }

    public List<IFormationPeriod> GetFormationPeriods()
    {
        return _mapper.ToDomain(FormationPeriodsDM).ToList();
    }

    public FormationModuleDataModel(IFormationModule formationModule, IMapper<IFormationPeriod, FormationPeriodDataModel> mapper)
    {
        _mapper = mapper;
        Id = formationModule.GetId();
        FormationSubjectId = formationModule.GetFormationSubjectId();
        FormationPeriodsDM = mapper.ToDataModel(formationModule.GetFormationPeriods()).ToList();
    }

    public FormationModuleDataModel() { }
}