using AutoMapper;
using Domain.Factory;
using Domain.Models;
using Infrastructure.DataModel;

namespace Infrastructure.Resolvers;

public class ProjectDataModelConverter : ITypeConverter<ProjectDataModel, Project>
{
    private readonly IProjectFactory _factory;

    public ProjectDataModelConverter(IProjectFactory factory)
    {
        _factory = factory;
    }

    public Project Convert(ProjectDataModel source, Project destination, ResolutionContext context)
    {
        return _factory.Create(source);
    }
}
