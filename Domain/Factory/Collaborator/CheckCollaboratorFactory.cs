using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.IRepository;
using Domain.Models;

namespace Domain.Factory
{
    public class CheckCollaboratorFactory : ICheckCollaboratorFactory
    {
        private readonly ICollaboratorRepository _repository;

        public CheckCollaboratorFactory(ICollaboratorRepository repository)
        {
            _repository = repository;
        }

        public ICollaborator Create(IUser user, IPeriodDateTime periodDateTime)
        {
            ICollaborator collab = new Collaborator(user, periodDateTime);

            if (_repository.isRepeated(collab))
                throw new ArgumentException("Collaborator already exists");

            return collab;
        }
    }
}
