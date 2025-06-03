using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTO;
using AutoMapper;
using Domain.Factory;
using Domain.Interfaces;
using Domain.IRepository;
using Domain.Models;
using Infrastructure;
using Infrastructure.Repositories;

namespace Application.Services
{
    public class ProjectService
    {
        private readonly IProjectRepository _repository;
        private readonly IProjectFactory _factory;
        private readonly IMapper _mapper;
        private readonly AbsanteeContext _context;

        public ProjectService(AbsanteeContext context, IProjectRepository repository, IProjectFactory factory, IMapper mapper)
        {
            _context = context;
            _repository = repository;
            _factory = factory;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<ProjectDTO>>> GetAll()
        {
            var projects = await _repository.GetAllAsync();
            var result = projects.Select(_mapper.Map<ProjectDTO>);
            return Result<IEnumerable<ProjectDTO>>.Success(result);
        }

        public async Task<Result<ProjectDTO>> EditProject(ProjectDTO projectDTO)
        {
            var project = await _repository.GetByIdAsync(projectDTO.Id);
            if (project == null)
                return Result<ProjectDTO>.Failure(Error.NotFound("Project not found"));

            project.UpdateTitle(projectDTO.Title);
            project.UpdateAcronym(projectDTO.Acronym);
            project.UpdatePeriodDate(projectDTO.PeriodDate);

            var updatedProject = await _repository.UpdateProject(project);

            if (updatedProject == null)
                return Result<ProjectDTO>.Failure(Error.InternalServerError("Failed to update project"));

            await _context.SaveChangesAsync();

            var result = _mapper.Map<ProjectDTO>(updatedProject);

            return Result<ProjectDTO>.Success(result);
        }

        public async Task<Result<ProjectDTO>> GetProjectById(Guid id)
        {
            var project = await _repository.GetByIdAsync(id);
            if (project == null)
                return Result<ProjectDTO>.Failure(Error.NotFound("Project not found"));
            var result = _mapper.Map<ProjectDTO>(project);
            return Result<ProjectDTO>.Success(result);
        }

        public async Task<Result<ProjectDTO>> Add(CreateProjectDTO projectDTO)
        {
            Project proj;
            try
            {
                proj = await _factory.Create(projectDTO.Title, projectDTO.Acronym, projectDTO.PeriodDate);
                await _repository.AddAsync(proj);
            }
            catch (ArgumentException a)
            {
                return Result<ProjectDTO>.Failure(Error.BadRequest(a.Message));
            }
            catch (Exception ex)
            {
                return Result<ProjectDTO>.Failure(Error.InternalServerError(ex.Message));
            }

            var result = _mapper.Map<Project, ProjectDTO>(proj);
            return Result<ProjectDTO>.Success(result);
        }
    }
}
