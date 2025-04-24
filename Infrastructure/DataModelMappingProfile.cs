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
            CreateMap<TrainingModuleCollaboratorDataModel, TrainingModuleCollaborators>();
            CreateMap<TrainingModuleCollaborators, TrainingModuleCollaboratorDataModel>();
            CreateMap<HolidayPeriod, HolidayPeriodDataModel>();
            CreateMap<HolidayPeriodDataModel, HolidayPeriod>()
                .ConvertUsing<HolidayPeriodDataModelToHolidayPeriodConverter>();
        }

    }
}
