using AutoMapper;
using Domain.Factory;
using Domain.Models;
using Infrastructure.DataModel;

namespace Infrastructure.Resolvers;

public class ProjectManagerDataModelConverter : ITypeConverter<ProjectManagerDataModel, ProjectManager>
{
    private readonly IProjectManagerFactory _factory;

    public ProjectManagerDataModelConverter(IProjectManagerFactory factory)
    {
        _factory = factory;
    }

    public ProjectManager Convert(ProjectManagerDataModel source, ProjectManager destination, ResolutionContext context)
    {
        return _factory.Create(source);
    }
}