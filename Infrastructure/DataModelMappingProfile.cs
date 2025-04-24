using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Factory;
using Domain.Models;
using Infrastructure.DataModel;

namespace Infrastructure
{
    public class DataModelMappingProfile : Profile
    {
        public DataModelMappingProfile()
        {
            CreateMap<Collaborator, CollaboratorDataModel>();
            CreateMap<CollaboratorDataModel, Collaborator>();
            CreateMap<AssociationProjectCollaborator, AssociationProjectCollaboratorDataModel>();
            CreateMap<AssociationProjectCollaboratorDataModel, AssociationProjectCollaborator>();
            CreateMap<TrainingModule, TrainingModuleDataModel>();
            CreateMap<TrainingModuleDataModel, TrainingModule>();
            CreateMap<TrainingSubjectDataModel, TrainingSubject>();
            CreateMap<TrainingSubject, TrainingSubjectDataModel>();
            CreateMap<Project, ProjectDataModel>();
            CreateMap<ProjectDataModel, Project>()
            .ConstructUsing((src, context) =>
            {
                var factory = (IProjectFactory)context.ServiceCtor(typeof(IProjectFactory));
                return factory.Create(src);
            });
            CreateMap<TrainingModuleCollaboratorDataModel, TrainingModuleCollaborators>();
            CreateMap<TrainingModuleCollaborators, TrainingModuleCollaboratorDataModel>();
        }
        
    }
}
