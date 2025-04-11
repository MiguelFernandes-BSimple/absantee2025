using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.IRepository;
using Domain.Models;
using Infrastructure.DataModel;
using Infrastructure.Mapper;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ProjectRepository : GenericRepository<IProject, ProjectDataModel>, IProjectRepository
{
    private readonly ProjectMapper _mapper;
    public ProjectRepository(AbsanteeContext context, ProjectMapper mapper) : base(context, (IMapper<IProject, ProjectDataModel>)mapper)
    {
        _mapper = mapper;
    }

    public override IProject? GetById(long id)
    {
        var projectDM = this._context.Set<ProjectDataModel>()
                            .FirstOrDefault(p => p.Id == id);

        if (projectDM == null)
            return null;

        var project = _mapper.ToDomain(projectDM);
        return project;
    }

    public override async Task<IProject?> GetByIdAsync(long id)
    {
        var projectDM = await this._context.Set<ProjectDataModel>()
                            .FirstOrDefaultAsync(c => c.Id == id);

        if (projectDM == null)
            return null;

        var project = _mapper.ToDomain(projectDM);
        return project;
    }
}
