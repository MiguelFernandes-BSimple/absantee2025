using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Application.DTO
{
    public class CreateAssociationProjectCollaboratorDTO
    {
        public Guid CollaboratorId { get; }
        public PeriodDate PeriodDate { get; }
    }
}
