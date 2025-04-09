using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.Models;

namespace Domain.Factory
{
    public class TrustedCollaboratorFactory : ITrustedCollaboratorFactory
    {
        public Collaborator Create(long id, long userId, IUser user, IPeriodDateTime periodDateTime)
        {
            return new Collaborator(id, userId, user, periodDateTime);
        }
    }
}
