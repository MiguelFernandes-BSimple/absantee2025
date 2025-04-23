using Domain.Interfaces;
using Domain.IRepository;
using Domain.Models;
using Infrastructure.DataModel;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace Infrastructure.Repositories;

public class ProjectRepository : GenericRepository<IProject, ProjectDataModel>, IProjectRepository
{
    private readonly IMapper _ProjectMapper;
    public ProjectRepository(AbsanteeContext context, IMapper mapper) : base(context, mapper)
    {
        _ProjectMapper = mapper;
    }

    public override IProject? GetById(long id)
    {
        var projectDM = this._context.Set<ProjectDataModel>()
                            .FirstOrDefault(p => p.Id == id);

        if (projectDM == null)
            return null;

        var project = _ProjectMapper.Map<ProjectDataModel, Project>(projectDM);
        return project;
    }

    public override async Task<IProject?> GetByIdAsync(long id)
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
    public async Task<IEnumerable<IProject>> GetAllAsync()
    {
        var projectDM = await _context.Set<ProjectDataModel>().ToListAsync();
        var projects = projectDM.Select(d => _ProjectMapper.Map<ProjectDataModel, Project>(d));
        return projects;
    }

}
