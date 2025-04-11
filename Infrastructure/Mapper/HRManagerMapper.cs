
using Domain.Models;
using Infrastructure.DataModel;
using Domain.Factory;

namespace Infrastructure.Mapper;

public class HRManagerMapper : IMapper<HRManager, HRManagerDataModel>
{
    private IHRManagerFactory _checkHRManagerFactory;

    public HRManagerMapper(IHRManagerFactory hRManagerFactory)
    {
        _checkHRManagerFactory = hRManagerFactory;
    }

    public HRManager ToDomain(HRManagerDataModel hRManagerDataModel)
    {
        var HRManagerDomain = _checkHRManagerFactory.Create(hRManagerDataModel);
        return HRManagerDomain;
    }

    public HRManagerDataModel ToDataModel(HRManager hRManager)
    {
        return new HRManagerDataModel(hRManager);
    }

    public IEnumerable<HRManager> ToDomain(IEnumerable<HRManagerDataModel> hRManagerDataModels)
    {
        return hRManagerDataModels.Select(ToDomain);
    }
    public IEnumerable<HRManagerDataModel> ToDataModel(IEnumerable<HRManager> hRManagers)
    {
        return hRManagers.Select(ToDataModel);
    }
}