using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Factory;
using Domain.IRepository;

namespace Application.Services
{
    public class ProjectService
    {
        private IProjectRepository _projectRepository;
        private IUserRepository _userRepository;
        private IProjectFactory _projectFactory;

        public ProjectService(IProjectRepository projectRepository, IUserRepository userRepository, IProjectFactory projectFactory)
        {
            _projectRepository = projectRepository;
            _userRepository = userRepository;
            _projectFactory = projectFactory;
        }
    }
}