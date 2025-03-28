using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain
{
    public class CollaboratorService
    {
        private IAssociationProjectCollaboratorRepository? associationProjectCollaboratorRepository;
        private IHolidayPlanRepository? holidayPlanRepository;

        public CollaboratorService(IAssociationProjectCollaboratorRepository associationProjectCollaboratorRepository)
        {
            this.associationProjectCollaboratorRepository = associationProjectCollaboratorRepository;
        }

        public CollaboratorService(IHolidayPlanRepository holidayPlanRepository)
        {
            this.holidayPlanRepository = holidayPlanRepository;
        }

        public CollaboratorService(IAssociationProjectCollaboratorRepository associationProjectCollaboratorRepository, IHolidayPlanRepository holidayPlanRepository)
        {
            this.associationProjectCollaboratorRepository = associationProjectCollaboratorRepository;
            this.holidayPlanRepository = holidayPlanRepository;
        }
    }
}