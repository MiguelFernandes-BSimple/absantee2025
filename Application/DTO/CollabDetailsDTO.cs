using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Models;

namespace Application.DTO
{
    public record CollabDetailsDTO
    {
        public Guid CollabId { get; set; }
        public Guid UserId { get; set; }
        public string Names { get; set; }
        public string Surnames { get; set; }
        public string Email { get; set; }
        public PeriodDateTime UserPeriod { get; set; }
        public PeriodDateTime CollaboratorPeriod { get; set; }
    }
}