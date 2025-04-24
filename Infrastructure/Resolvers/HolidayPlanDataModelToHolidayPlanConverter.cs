using AutoMapper;
using Domain.Factory;
using Domain.Models;
using Infrastructure.DataModel;

namespace Infrastructure.Resolvers;

public class HolidayPlanDataModelToHolidayPlanConverter : ITypeConverter<HolidayPlanDataModel, HolidayPlan>
{
    private readonly IHolidayPlanFactory _factory;

    public HolidayPlanDataModelToHolidayPlanConverter(IHolidayPlanFactory factory)
    {
        _factory = factory;
    }

    public HolidayPlan Convert(HolidayPlanDataModel source, HolidayPlan destination, ResolutionContext context)
    {
        return _factory.Create(source);
    }
}
