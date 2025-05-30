using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Models;

namespace Application.DTO.Collaborators
{
    public class CollabCreateDataDTO
    {
        public CollabCreateDataDTO(string names, string surnames, string email, DateTime deactivationDate, PeriodDateTime periodDateTime)
        {
            Names = names;
            Surnames = surnames;
            Email = email;
            this.deactivationDate = deactivationDate;
            PeriodDateTime = periodDateTime;
        }

        public string Names { get; set; }
        public string Surnames { get; set; }
        public string Email { get; set; }
        public DateTime deactivationDate { get; set; }
        public PeriodDateTime PeriodDateTime { get; set; }
    }
}