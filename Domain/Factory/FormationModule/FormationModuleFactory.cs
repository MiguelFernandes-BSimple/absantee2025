using Domain.Interfaces;
using Domain.IRepository;
using Domain.Models;
using Domain.Visitor;

namespace Domain.Factory;

public class FormationModuleFactory : IFormationModuleFactory
{
    private readonly IFormationSubjectRepository _formationSubjectRepository;

    public FormationModuleFactory(IFormationSubjectRepository formationSubjectRepository)
    {
        _formationSubjectRepository = formationSubjectRepository;
    }
    public FormationModule Create(long formationSubjectId, List<PeriodDateTime> formationPeriods)
    {
        if (_formationSubjectRepository.GetById(formationSubjectId) == null)
            throw new ArgumentException("Formation subject does not exist.");

        return new FormationModule(formationSubjectId, formationPeriods);
    }

    public FormationModule Create(IFormationModuleVisitor visitor)
    {
        return new FormationModule(visitor.Id, visitor.FormationSubjectId, visitor.GetFormationPeriods());
    }
}