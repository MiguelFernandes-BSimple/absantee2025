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
            CreateMap<Collaborator, CollaboratorDataModel>();
            CreateMap<CollaboratorDataModel, Collaborator>()
                .ConvertUsing<CollaboratorDataModelToCollaboratorConverter>();
            CreateMap<AssociationProjectCollaborator, AssociationProjectCollaboratorDataModel>();
            CreateMap<AssociationProjectCollaboratorDataModel, AssociationProjectCollaborator>();
            CreateMap<TrainingModule, TrainingModuleDataModel>();
            CreateMap<TrainingModuleDataModel, TrainingModule>()
                .ConvertUsing<TrainingModuleDataModelToTrainingModuleConverter>();
            CreateMap<TrainingSubjectDataModel, TrainingSubject>()
                .ConvertUsing<TrainingSubjectDataModelToTrainingSubjectConverter>();
            CreateMap<TrainingSubject, TrainingSubjectDataModel>();
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
