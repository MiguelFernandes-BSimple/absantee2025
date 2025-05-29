using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Models;

namespace Application.DTO.Collaborators
{
    public class CollabData
    {
        // serve para receber os dados a partir do controller e converter para o serviço
        // adicionado depois de falar com o prof
        public Guid CollabId { get; set; }
        public Guid UserId { get; set; }
        public string Names { get; set; }
        public string Surnames { get; set; }
        public string Email { get; set; }
        public PeriodDateTime UserPeriod { get; set; }
        public PeriodDateTime CollaboratorPeriod { get; set; }
        
        public CollabData(Guid collabId, Guid userId, string names, string surnames, string email, PeriodDateTime userPeriod, PeriodDateTime collaboratorPeriod)
        {
            CollabId = collabId;
            UserId = userId;
            Names = names;
            Surnames = surnames;
            Email = email;
            UserPeriod = userPeriod;
            CollaboratorPeriod = collaboratorPeriod;
        }


        
    }
}