using Domain.Factory;
using Domain.Models;
using Infrastructure.DataModel;
using Domain.Interfaces;

namespace Infrastructure.Mapper;

public class FormationModuleMapper : IMapper<IFormationModule, FormationModuleDataModel>
{
    private IFormationModuleFactory _formationModuleFactory;
    private readonly FormationPeriodMapper _formationPeriodMapper;

    public FormationModuleMapper(IFormationModuleFactory formationModuleFactory, FormationPeriodMapper formationPeriodMapper)
    {
        _formationModuleFactory = formationModuleFactory;
        _formationPeriodMapper = formationPeriodMapper;
    }

    public IFormationModule ToDomain(FormationModuleDataModel formationModuleDataModel)
    {
        var formationModuleDomain = _formationModuleFactory.Create(formationModuleDataModel);
        return formationModuleDomain;
    }

    public IEnumerable<IFormationModule> ToDomain(IEnumerable<FormationModuleDataModel> formationModuleDataModels)
    {
        return formationModuleDataModels.Select(ToDomain);
    }

    public FormationModuleDataModel ToDataModel(IFormationModule formationModule)
    {
        return new FormationModuleDataModel(formationModule, _formationPeriodMapper);
    }

    public IEnumerable<FormationModuleDataModel> ToDataModel(IEnumerable<IFormationModule> formationModules)
    {
        return formationModules.Select(ToDataModel);
    }
}