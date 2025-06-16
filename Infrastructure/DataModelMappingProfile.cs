using AutoMapper;
using Domain.Models;
using Infrastructure.DataModel;
using Infrastructure.Resolvers;

namespace Infrastructure;

public class DataModelMappingProfile : Profile
{
    public DataModelMappingProfile()
    {
        CreateMap<TrainingPeriod, TrainingPeriodDataModel>();
        CreateMap<TrainingPeriodDataModel, TrainingPeriod>()
            .ConvertUsing<TrainingPeriodDataModelConverter>();
        CreateMap<TrainingSubject, TrainingSubjectDataModel>();
        CreateMap<TrainingSubjectDataModel, TrainingSubject>()
            .ConvertUsing<TrainingSubjectDataModelConverter>();
        CreateMap<TrainingModule, TrainingModuleDataModel>();
        CreateMap<TrainingModuleDataModel, TrainingModule>()
            .ConvertUsing<TrainingModuleDataModelConverter>();
    }

}