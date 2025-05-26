using Domain.Interfaces;
using Domain.IRepository;
using Domain.Models;
using Infrastructure.DataModel;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace Infrastructure.Repositories;

public class ProjectRepositoryEF : GenericRepositoryEF<Project, ProjectDataModel>, IProjectRepository
{
    private readonly IMapper _ProjectMapper;
    public ProjectRepositoryEF(AbsanteeContext context, IMapper mapper) : base(context, mapper)
    {
        _ProjectMapper = mapper;
    }

    public override Project? GetById(Guid id)
    {
        var projectDM = this._context.Set<ProjectDataModel>()
                            .FirstOrDefault(p => p.Id == id);

        if (projectDM == null)
            return null;

        var project = _ProjectMapper.Map<ProjectDataModel, Project>(projectDM);
        return project;
    }

    public override async Task<Project?> GetByIdAsync(Guid id)
    {
        var projectDM = await this._context.Set<ProjectDataModel>()
                            .FirstOrDefaultAsync(c => c.Id == id);

        if (projectDM == null)
            return null;

        var project = _ProjectMapper.Map<ProjectDataModel, Project>(projectDM);
        return project;
    }

    public async Task<bool> CheckIfAcronymIsUnique(string acronym)
    {
        var found = await this._context.Set<ProjectDataModel>()
                            .FirstOrDefaultAsync(c => c.Acronym == acronym);

        return found == null;
    }

    public async Task<IEnumerable<Project>> GetByIdAsync(IEnumerable<Guid> projectIds)
    {
        var projectDMs = await this._context.Set<ProjectDataModel>().Where(p => projectIds.Contains(p.Id)).ToListAsync();

        var projects = projectDMs.Select(_ProjectMapper.Map<ProjectDataModel, Project>);

        return projects;
    }
}
