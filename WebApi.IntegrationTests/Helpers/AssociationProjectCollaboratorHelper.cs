using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTO;
using Domain.Models;

namespace WebApi.IntegrationTests.Helpers
{
    public static class AssociationProjectCollaboratorHelper
    {
        public static CreateAssociationProjectCollaboratorDTO GenerateCreateAssociationProjectCollaboratorDto(Guid collaboratorId, PeriodDate periodDate)
        {
            return new CreateAssociationProjectCollaboratorDTO
            {
                CollaboratorId = collaboratorId,
                PeriodDate = periodDate
            };
        }
    }
}
