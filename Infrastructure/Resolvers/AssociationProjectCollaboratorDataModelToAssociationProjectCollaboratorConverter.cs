using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Factory;
using Domain.Interfaces;
using Domain.Models;
using Infrastructure.DataModel;

namespace Infrastructure.Resolvers
{
    public class AssociationProjectCollaboratorDataModelToAssociationProjectCollaboratorConverter : ITypeConverter<AssociationProjectCollaboratorDataModel, AssociationProjectCollaborator>
    {
        private readonly IAssociationProjectCollaboratorFactory _factory;

        public AssociationProjectCollaboratorDataModelToAssociationProjectCollaboratorConverter(IAssociationProjectCollaboratorFactory factory)
        {
            _factory = factory;
        }

        public AssociationProjectCollaborator Convert(AssociationProjectCollaboratorDataModel source, AssociationProjectCollaborator destination, ResolutionContext context)
        {
            return _factory.Create(source);
        }
    }
}
