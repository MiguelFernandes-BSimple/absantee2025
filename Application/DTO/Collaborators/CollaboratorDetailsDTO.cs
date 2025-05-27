using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Application.DTO.Collaborators
{
    public record CollaboratorDetailsDTO
    {
        public string Id {  get; set; }
        public string Names { get; set; }
        public string Surnames { get; set; }
        public string Email { get; set; }
        public PeriodDate PeriodDate { get; set; }
    }
}
