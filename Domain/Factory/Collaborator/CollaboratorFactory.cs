using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.IRepository;
using Domain.Models;
using Domain.Visitor;

namespace Domain.Factory
{
    public class CollaboratorFactory : ICollaboratorFactory
    {
        private readonly ICollaboratorRepository _collabRepository;
        private readonly IUserRepository _userRepository;

        public CollaboratorFactory(ICollaboratorRepository collabRepository, IUserRepository userRepository)
        {
            _collabRepository = collabRepository;
            _userRepository = userRepository;
        }

        public Collaborator Create(long userId, IPeriodDateTime periodDateTime)
        {
            IUser? user = _userRepository.GetById((int)userId);

            if (user == null)
                throw new ArgumentException("User dont exists");

            if (user.DeactivationDateIsBefore(periodDateTime.GetFinalDate()))
                throw new ArgumentException("Invalid Arguments");

            if (user.IsDeactivated())
                throw new ArgumentException("Invalid Arguments");

            Collaborator collab = new Collaborator(userId, periodDateTime);

            if (_collabRepository.isRepeated(collab))
                throw new ArgumentException("Collaborator already exists");

            return collab;
        }

        public Collaborator Create(ICollaboratorVisitor visitor)
        {
            return new Collaborator(visitor.Id, visitor.UserID, visitor.PeriodDateTime);
        }
    }
}
