using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.IRepository;
using Domain.Models;
using Domain.Visitor;

namespace Domain.Factory
{
    public class ProjectManagerFactory : IProjectManagerFactory
    {
        private IUserRepository _userRepository;

        public ProjectManagerFactory(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ProjectManager> Create(Guid userId, PeriodDateTime periodDateTime)
        {
            IUser? user = await _userRepository.GetByIdAsync(userId);

            if (user == null)
                throw new ArgumentException("User does not exist");

            else if (user.DeactivationDateIsBefore(periodDateTime._initDate))
                throw new ArgumentException("Deactivation date is before init date");

            else if (user.IsDeactivated())
                throw new ArgumentException("User is deactivated");
            var ProjectManager = new ProjectManager(userId, periodDateTime);
            return ProjectManager;
        }

        public async Task<ProjectManager> Create(Guid userId, DateTime initDate)
        {
            var periodDateTime = new PeriodDateTime(initDate, DateTime.MaxValue);
            return await Create(userId, periodDateTime);
        }

        public ProjectManager Create(IProjectManagerVisitor pmv)
        {
            return new ProjectManager(pmv.UserId, pmv.PeriodDateTime);
        }
    }
}