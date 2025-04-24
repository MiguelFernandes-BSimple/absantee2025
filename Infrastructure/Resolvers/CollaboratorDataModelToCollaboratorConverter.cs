using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Factory;
using Domain.Models;
using Infrastructure.DataModel;

namespace Infrastructure.Resolvers
{
    public class CollaboratorDataModelToCollaboratorConverter : ITypeConverter<CollaboratorDataModel, Collaborator>
    {
        private readonly ICollaboratorFactory _factory;

        public CollaboratorDataModelToCollaboratorConverter(ICollaboratorFactory factory)
        {
            _factory = factory;
        }

        public Collaborator Convert(CollaboratorDataModel source, Collaborator destination, ResolutionContext context)
        {
            return _factory.Create(source);
        }
    }
}