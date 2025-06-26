using AutoMapper;
using Domain.Factory.TrainingPeriodFactory;
using Domain.Models;
using Infrastructure.DataModel;

namespace Infrastructure.Resolvers;

public class TrainingPeriodDataModelConverter : ITypeConverter<TrainingPeriodDataModel, TrainingPeriod>
{
    private readonly ITrainingPeriodFactory _factory;

    public TrainingPeriodDataModelConverter(ITrainingPeriodFactory factory)
    {
        _factory = factory;
    }

    public TrainingPeriod Convert(TrainingPeriodDataModel source, TrainingPeriod destination, ResolutionContext context)
    {
        return _factory.Create(source);
    }
}
