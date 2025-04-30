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
using Infrastructure.Repositories;

namespace Application.Services
{
    public class ProjectService
    {
        private readonly IProjectRepository _repository;
        private readonly IProjectFactory _factory;
        private readonly IMapper _mapper;

        public ProjectService(IProjectRepository repository, IProjectFactory factory, IMapper mapper)
        {
            _repository = repository;
            _factory = factory;
            _mapper = mapper;
        }

        public async Task<IEnumerable<IProject>> GetAll()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<IProject?> GetProjectById(Guid id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<ProjectDTO> Add(CreateProjectDTO projectDTO)
        {
            Project proj;
            try
            {
                proj = await _factory.Create(projectDTO.Title, projectDTO.Acronym, projectDTO.PeriodDate);
                await _repository.AddAsync(proj);
            }
            catch (Exception)
            {
                return null;
            }


            return _mapper.Map<Project, ProjectDTO>(proj);
        }
    }
}
