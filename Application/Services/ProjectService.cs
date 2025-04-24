using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTO;
using Domain.Factory;
using Domain.Interfaces;
using Domain.IRepository;
using Domain.Models;
using Infrastructure.Repositories;

namespace Application.Services
{
    public class ProjectService
    {
        private readonly IProjectRepository _repository;
        private readonly IProjectFactory _factory;

        public ProjectService(IProjectRepository repository, IProjectFactory factory)
        {
            _repository = repository;
            _factory = factory;
        }

        public async Task<IEnumerable<IProject>> GetAll()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<IProject?> GetProjectById(Guid id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<bool> Add(ProjectDTO projectDTO)
        {
            Project proj;
            try
            {
                proj = await _factory.Create(projectDTO.Title, projectDTO.Acronym, projectDTO.PeriodDate);
                await _repository.AddAsync(proj);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
    }
}
