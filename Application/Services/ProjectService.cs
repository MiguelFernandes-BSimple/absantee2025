using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Factory;
using Domain.Interfaces;
using Domain.IRepository;
using Domain.Models;
using Domain.Visitor;

namespace Application.Services
{
    public class ProjectService
    {
        private IProjectRepository _projectRepository;
        private IProjectFactory _projectFactory;

        public ProjectService(IProjectRepository projectRepository, IProjectFactory projectFactory)
        {
            _projectRepository = projectRepository;
            _projectFactory = projectFactory;
        }

        public async Task<bool> Add(Project project)
        {
            project = await _projectFactory.Create(project.Title, project.Acronym, project.PeriodDate);
            await _projectRepository.AddAsync(project);
            await _projectRepository.SaveChangesAsync();
            return true;
        }
        public async Task<IEnumerable<IProject>> GetAll()
        {
            var Project = await _projectRepository.GetAllAsync();
            return Project;
        }

    }
}