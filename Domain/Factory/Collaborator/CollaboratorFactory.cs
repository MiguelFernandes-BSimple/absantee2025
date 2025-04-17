﻿using System.Threading.Tasks;
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

        public async Task<Collaborator> Create(long userId, PeriodDateTime periodDateTime)
        {
            IUser? user = _userRepository.GetById((int)userId);

            if (user == null)
                throw new ArgumentException("User dont exists");

            if (user.DeactivationDateIsBefore(periodDateTime.GetFinalDate()))
                throw new ArgumentException("User deactivation date is before collaborator contract end date.");

            if (user.IsDeactivated())
                throw new ArgumentException("User is deactivated.");

            Collaborator collab = new Collaborator(userId, periodDateTime);

            if (await _collabRepository.IsRepeated(collab))
                throw new ArgumentException("Collaborator already exists");

            return collab;
        }

        public Collaborator Create(ICollaboratorVisitor visitor)
        {
            return new Collaborator(visitor.Id, visitor.UserID, visitor.PeriodDateTime);
        }
    }
}
