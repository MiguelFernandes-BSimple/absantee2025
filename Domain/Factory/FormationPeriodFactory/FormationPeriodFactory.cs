using Domain.Factory.FormationPeriodFactory;
using Domain.IRepository;
using Domain.Models;
using Domain.Visitor;

namespace Domain.Factory.TrainingPeriodFactory;

public class FormationPeriodFactory : IFormationPeriodFactory
{

    private readonly IFormationModuleRepository _formationModuleRepository;
    private readonly ICollaboratorRepository _collaboratorRepository;

    public FormationPeriodFactory(IFormationModuleRepository formationModuleRepository, ICollaboratorRepository collaboratorRepository)
    {
        _formationModuleRepository = formationModuleRepository;
        _collaboratorRepository = collaboratorRepository;
    }

    public FormationPeriod Create(long formationModuleId, PeriodDate periodDate)
    {
        FormationPeriod formationPeriod = new FormationPeriod(periodDate);

        if (!_formationModuleRepository.CanInsertHolidayPeriod(formationModuleId, formationPeriod))
            throw new ArgumentException("Formation period already exists for this formation module.");

        var formationModule = _formationModuleRepository.GetById(formationModuleId);
        if (formationModule == null)
            throw new ArgumentException("Formation Module doesn't exist.");

        return new FormationPeriod(periodDate);
    }

    public FormationPeriod Create(IFormationPeriodVisitor formationPeriodVisitor)
    {
        return new FormationPeriod(formationPeriodVisitor.Id, formationPeriodVisitor.PeriodDate);
    }
}