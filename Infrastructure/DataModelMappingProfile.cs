using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Factory;
using Domain.Models;
using Infrastructure.DataModel;
using Infrastructure.Resolvers;

namespace Infrastructure
{
    public class DataModelMappingProfile : Profile
    {
        public DataModelMappingProfile()
        {
            CreateMap<User, UserDataModel>();
            CreateMap<UserDataModel, User>()
                .ConvertUsing<UserDataModelToUserConverter>();
            CreateMap<HRManager, HRManagerDataModel>();
            CreateMap<HRManagerDataModel, HRManager>()
                .ConvertUsing<HRManagerDataModelConverter>();
            CreateMap<ProjectManager, ProjectManagerDataModel>();
            CreateMap<ProjectManagerDataModel, ProjectManager>()
                .ConvertUsing<ProjectManagerDataModelConverter>();
            CreateMap<Collaborator, CollaboratorDataModel>();
            CreateMap<CollaboratorDataModel, Collaborator>()
                .ConvertUsing<CollaboratorDataModelToCollaboratorConverter>();
            CreateMap<AssociationProjectCollaborator, AssociationProjectCollaboratorDataModel>();
            CreateMap<AssociationProjectCollaboratorDataModel, AssociationProjectCollaborator>();
            CreateMap<TrainingModule, TrainingModuleDataModel>();
            CreateMap<TrainingModuleDataModel, TrainingModule>()
                .ConvertUsing<TrainingModuleDataModelToTrainingModuleConverter>();
            CreateMap<TrainingPeriod, TrainingPeriodDataModel>();
            CreateMap<TrainingPeriodDataModel, TrainingPeriod>()
                .ConvertUsing<TrainingPeriodDataModelConverter>();
            CreateMap<TrainingSubject, TrainingSubjectDataModel>();
            CreateMap<TrainingSubjectDataModel, TrainingSubject>()
                .ConvertUsing<TrainingSubjectDataModelToTrainingSubjectConverter>();
            CreateMap<Project, ProjectDataModel>();
            CreateMap<ProjectDataModel, Project>()
                .ConvertUsing<ProjectDataModelToProjectConverter>();
            CreateMap<AssociationTrainingModuleCollaboratorDataModel, AssociationTrainingModuleCollaborator>()
                .ConvertUsing<AssociationTrainingModuleCollaboratorDataModelConverter>();
            CreateMap<AssociationTrainingModuleCollaborator, AssociationTrainingModuleCollaboratorDataModel>();
            CreateMap<HolidayPeriod, HolidayPeriodDataModel>();
            CreateMap<HolidayPeriodDataModel, HolidayPeriod>()
                .ConvertUsing<HolidayPeriodDataModelToHolidayPeriodConverter>();
            CreateMap<HolidayPlan, HolidayPlanDataModel>();
            CreateMap<HolidayPlanDataModel, HolidayPlan>()
                .ConvertUsing<HolidayPlanDataModelToHolidayPlanConverter>();
        }

    }
}
