using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Models;

namespace Application.DTO.Collaborator
{
    public record CollaboratorEditedDTO
    {
        public string Names { get; set; }
        public string Surnames { get; set; }
        public string Email { get; set; }
        public PeriodDateTime UserPeriod { get; set; }
        public PeriodDateTime CollaboratorPeriod { get; set; }

        public CollaboratorEditedDTO(string names, string surnames, string email, PeriodDateTime userPeriod, PeriodDateTime collaboratorPeriod)
        {
            Names = names;
            Surnames = surnames;
            Email = email;
            UserPeriod = userPeriod;
            CollaboratorPeriod = collaboratorPeriod;
        }

    }
}