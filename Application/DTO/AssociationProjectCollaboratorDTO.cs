using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Application.DTO
{
    public record AssociationProjectCollaboratorDTO
    {
        public Guid Id { get; set; }
        public Guid CollaboratorId { get; set; }
        public string? CollaboratorEmail { get; set; }
        public Guid ProjectId { get; set; }
        public string? ProjectAcronym { get; set; }
        public PeriodDate PeriodDate { get; set; }
    }
}
