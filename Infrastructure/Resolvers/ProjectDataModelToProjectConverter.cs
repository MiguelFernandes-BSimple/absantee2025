using AutoMapper;
using Domain.Factory;
using Domain.Models;
using Infrastructure.DataModel;

namespace Infrastructure.Resolvers
{
    public class ProjectDataModelToProjectConverter : ITypeConverter<ProjectDataModel, Project>
    {
        private readonly IProjectFactory _factory;

        public ProjectDataModelToProjectConverter(IProjectFactory factory)
        {
            _factory = factory;
        }

        public Project Convert(ProjectDataModel source, Project destination, ResolutionContext context)
        {
            return _factory.Create(source);
        }
    }
}
