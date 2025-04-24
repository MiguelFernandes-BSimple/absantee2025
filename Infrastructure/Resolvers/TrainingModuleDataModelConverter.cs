using AutoMapper;
using Domain.Factory;
using Domain.Models;
using Infrastructure.DataModel;

namespace Infrastructure.Resolvers;

public class TrainingModuleDataModelConverter : ITypeConverter<TrainingModuleDataModel, TrainingModule>
{
    private readonly ITrainingModuleFactory _factory;

    public TrainingModuleDataModelConverter(ITrainingModuleFactory factory)
    {
        _factory = factory;
    }

    public TrainingModule Convert(TrainingModuleDataModel source, TrainingModule destination, ResolutionContext context)
    {
        return _factory.Create(source);
    }
}
