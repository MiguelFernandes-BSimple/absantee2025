using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Factory;
using Domain.Models;
using Infrastructure.DataModel;

namespace Infrastructure.Resolvers;

public class TrainingModuleDataModelToTrainingModuleConverter : ITypeConverter<TrainingModuleDataModel, TrainingModule>
{
    private readonly ITrainingModuleFactory _factory;

    public TrainingModuleDataModelToTrainingModuleConverter(ITrainingModuleFactory factory)
    {
        _factory = factory;
    }

    public TrainingModule Convert(TrainingModuleDataModel source, TrainingModule destination, ResolutionContext context)
    {
        return _factory.Create(source);
    }
}
