using Domain.Factory;
using Domain.Models;
using Infrastructure.DataModel;

namespace Infrastructure.Mapper;

public class FormationModuleMapper : IMapper<FormationModule, FormationModuleDataModel>
{
    private IFormationModuleFactory _formationModuleFactory;

    public FormationModuleMapper(IFormationModuleFactory formationModuleFactory)
    {
        _formationModuleFactory = formationModuleFactory;
    }

    public FormationModule ToDomain(FormationModuleDataModel formationModuleDataModel)
    {
        var formationModuleDomain = _formationModuleFactory.Create(formationModuleDataModel);
        return formationModuleDomain;
    }

    public IEnumerable<FormationModule> ToDomain(IEnumerable<FormationModuleDataModel> formationModuleDataModels)
    {
        return formationModuleDataModels.Select(ToDomain);
    }

    public FormationModuleDataModel ToDataModel(FormationModule formationModule)
    {
        return new FormationModuleDataModel(formationModule);
    }

    public IEnumerable<FormationModuleDataModel> ToDataModel(IEnumerable<FormationModule> formationModules)
    {
        return formationModules.Select(ToDataModel);
    }
}