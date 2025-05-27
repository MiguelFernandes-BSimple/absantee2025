using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Domain.Models;

namespace Application.DTO.Collaborators
{
    public class CreateCollaboratorDto
    {
        // user attributes
        [Required(ErrorMessage = "Error. Name required.")]
        public string Names { get; set; }

        [Required(ErrorMessage = "Error. Surname required")]
        public string Surnames { get; set; }

        [Required(ErrorMessage = "Error. Email required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Error. DeactivationDate required")]
        public DateTime deactivationDate { get; set; }

        // collaborator attributes
        [Required(ErrorMessage = "Error. PeriodDateTime required")]
        public PeriodDateTime PeriodDateTime { get; set; }

        public CreateCollaboratorDto(string names, string surnames, string email, DateTime deactivationDate, PeriodDateTime periodDateTime)
        {
            Names = names;
            Surnames = surnames;
            Email = email;
            this.deactivationDate = deactivationDate;
            PeriodDateTime = periodDateTime;
        }

        public CreateCollaboratorDto()
        {

        }
    }
}