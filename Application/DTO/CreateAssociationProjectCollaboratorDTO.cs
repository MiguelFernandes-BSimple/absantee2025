using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Application.DTO
{
    public record CreateAssociationProjectCollaboratorDTO
    {
        public Guid CollaboratorId { get; set; }
        public PeriodDate PeriodDate { get; set; }
    }
}
