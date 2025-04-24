using AutoMapper;
using Domain.Factory;
using Domain.Models;
using Infrastructure.DataModel;

namespace Infrastructure.Resolvers;

public class HolidayPeriodDataModelConverter : ITypeConverter<HolidayPeriodDataModel, HolidayPeriod>
{
    private readonly IHolidayPeriodFactory _factory;

    public HolidayPeriodDataModelConverter(IHolidayPeriodFactory factory)
    {
        _factory = factory;
    }

    public HolidayPeriod Convert(HolidayPeriodDataModel source, HolidayPeriod destination, ResolutionContext context)
    {
        return _factory.Create(source);
    }
}
