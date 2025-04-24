using AutoMapper;
using Domain.Models;
using Infrastructure.DataModel;
using Infrastructure.Resolvers;

namespace Infrastructure;

public class DataModelMappingProfile : Profile
{
    public DataModelMappingProfile()
    {
        CreateMap<User, UserDataModel>();
        CreateMap<UserDataModel, User>()
            .ConvertUsing<UserDataModelConverter>();
        CreateMap<HRManager, HRManagerDataModel>();
        CreateMap<HRManagerDataModel, HRManager>()
            .ConvertUsing<HRManagerDataModelConverter>();
        CreateMap<ProjectManager, ProjectManagerDataModel>();
        CreateMap<ProjectManagerDataModel, ProjectManager>()
            .ConvertUsing<ProjectManagerDataModelConverter>();
        CreateMap<Collaborator, CollaboratorDataModel>();
        CreateMap<CollaboratorDataModel, Collaborator>()
            .ConvertUsing<CollaboratorDataModelConverter>();
        CreateMap<AssociationProjectCollaborator, AssociationProjectCollaboratorDataModel>();
        CreateMap<AssociationProjectCollaboratorDataModel, AssociationProjectCollaborator>()
            .ConvertUsing<AssociationProjectCollaboratorDataModelConverter>();
        CreateMap<Project, ProjectDataModel>();
        CreateMap<ProjectDataModel, Project>()
            .ConvertUsing<ProjectDataModelConverter>();
        CreateMap<AssociationTrainingModuleCollaborator, AssociationTrainingModuleCollaboratorDataModel>();
        CreateMap<AssociationTrainingModuleCollaboratorDataModel, AssociationTrainingModuleCollaborator>()
            .ConvertUsing<AssociationTrainingModuleCollaboratorDataModelConverter>();
        CreateMap<TrainingPeriod, TrainingPeriodDataModel>();
        CreateMap<TrainingPeriodDataModel, TrainingPeriod>()
            .ConvertUsing<TrainingPeriodDataModelConverter>();
        CreateMap<TrainingSubject, TrainingSubjectDataModel>();
        CreateMap<TrainingSubjectDataModel, TrainingSubject>()
            .ConvertUsing<TrainingSubjectDataModelConverter>();
        CreateMap<TrainingModule, TrainingModuleDataModel>();
        CreateMap<TrainingModuleDataModel, TrainingModule>()
            .ConvertUsing<TrainingModuleDataModelConverter>();
        CreateMap<HolidayPlan, HolidayPlanDataModel>();
        CreateMap<HolidayPlanDataModel, HolidayPlan>()
            .ConvertUsing<HolidayPlanDataModelConverter>();
        CreateMap<HolidayPeriod, HolidayPeriodDataModel>();
        CreateMap<HolidayPeriodDataModel, HolidayPeriod>()
            .ConvertUsing<HolidayPeriodDataModelConverter>();
    }

}