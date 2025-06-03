using Domain.Interfaces;
using Domain.IRepository;
using Domain.Models;
using Infrastructure.DataModel;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace Infrastructure.Repositories;

public class ProjectRepositoryEF : GenericRepositoryEF<IProject, Project, ProjectDataModel>, IProjectRepository
{
    private readonly IMapper _projectMapper;
    public ProjectRepositoryEF(AbsanteeContext context, IMapper mapper) : base(context, mapper)
    {
        _projectMapper = mapper;
    }

    public override IProject? GetById(Guid id)
    {
        var projectDM = this._context.Set<ProjectDataModel>()
                            .FirstOrDefault(p => p.Id == id);

        if (projectDM == null)
            return null;

        var project = _projectMapper.Map<ProjectDataModel, Project>(projectDM);
        return project;
    }

    public override async Task<IProject?> GetByIdAsync(Guid id)
    {
        var projectDM = await this._context.Set<ProjectDataModel>()
                            .FirstOrDefaultAsync(c => c.Id == id);

        if (projectDM == null)
            return null;

        var project = _projectMapper.Map<ProjectDataModel, Project>(projectDM);
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

        var projects = projectDMs.Select(_projectMapper.Map<ProjectDataModel, Project>);

        return projects;
    }

    public async Task<Project?> UpdateProject(IProject project)
    {
        var projectDM = await _context.Set<ProjectDataModel>()
                            .FirstOrDefaultAsync(p => p.Id == project.Id);

        if (projectDM == null) return null;

        projectDM.Title = project.Title;
        projectDM.Acronym = project.Acronym;
        projectDM.PeriodDate = project.PeriodDate;

        _context.Set<ProjectDataModel>().Update(projectDM);
        await _context.SaveChangesAsync();

        return _projectMapper.Map<ProjectDataModel, Project>(projectDM);

    }
}
