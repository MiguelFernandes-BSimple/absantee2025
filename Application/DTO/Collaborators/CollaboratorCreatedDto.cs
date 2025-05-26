using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Models;

namespace Application.DTO.Collaborators
{
    public class CollaboratorCreatedDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public PeriodDateTime PeriodDateTime { get; set; }

        public CollaboratorCreatedDto(Guid id, Guid userId, PeriodDateTime periodDateTime)
        {
            Id = id;
            UserId = userId;
            PeriodDateTime = periodDateTime;
        }

        public CollaboratorCreatedDto()
        {

        }

    }
}