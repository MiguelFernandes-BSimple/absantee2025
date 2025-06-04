using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO
{
    public record CreateAssociationCollaboratorProjectDTO
    {
        public Guid ProjectId { get; set; }
        public PeriodDate PeriodDate { get; set; }
    }
}
