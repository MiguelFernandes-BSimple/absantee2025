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

        public ProjectManager Create(long userId, IPeriodDateTime periodDateTime)
        {
            var user = _userRepository.GetById(userId);

            if (user == null)
                throw new ArgumentException("User does not exist");

            else if (user.DeactivationDateIsBefore(periodDateTime.GetInitDate()))
                throw new ArgumentException("Deactivation date is before init date");

            else if (user.IsDeactivated())
                throw new ArgumentException("User is deactivated");

            else return new ProjectManager(userId, periodDateTime);
        }

        public ProjectManager Create(long userId, DateTime initDate)
        {
            var user = _userRepository.GetById(userId);

            if (user == null)
                throw new ArgumentException("User does not exist");

            else if (user.DeactivationDateIsBefore(initDate))
                throw new ArgumentException("Deactivation date is before init date");

            else if (user.IsDeactivated())
                throw new ArgumentException("User is deactivated");

            else return new ProjectManager(userId, initDate);
        }

        public ProjectManager Create(IProjectManagerVisitor pmv)
        {
            return new ProjectManager(pmv.UserId, pmv.PeriodDateTime);
        }
    }
}