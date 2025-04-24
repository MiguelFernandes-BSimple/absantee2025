using AutoMapper;
using Domain.Factory;
using Domain.Models;
using Infrastructure.DataModel;

namespace Infrastructure.Resolvers;
public class HRManagerDataModelConverter : ITypeConverter<HRManagerDataModel, HRManager>
{
    private readonly IHRManagerFactory _hRManagerFactory;

    public HRManagerDataModelConverter(IHRManagerFactory HRManagerFactory)
    {
        _hRManagerFactory = HRManagerFactory;
    }

    public HRManager Convert(HRManagerDataModel source, HRManager destination, ResolutionContext context)
    {
        return _hRManagerFactory.Create(source);
    }
}