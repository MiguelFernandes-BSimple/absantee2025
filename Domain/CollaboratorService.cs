using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain
{
    public class CollaboratorService
    {
        //private IAssociationProjectCollaboratorRepository? _associationProjectCollaboratorRepository;
        private IHolidayPlanRepository _holidayPlanRepository;

        /*public CollaboratorService(IAssociationProjectCollaboratorRepository associationProjectCollaboratorRepository)
        {
            this._associationProjectCollaboratorRepository = associationProjectCollaboratorRepository;
        }
*/
        public CollaboratorService(IHolidayPlanRepository holidayPlanRepository)
        {
            _holidayPlanRepository = holidayPlanRepository;
        }
        /*
                public CollaboratorService(IAssociationProjectCollaboratorRepository? associationProjectCollaboratorRepository, IHolidayPlanRepository? holidayPlanRepository)
                {
                    this._associationProjectCollaboratorRepository = associationProjectCollaboratorRepository;
                    this._holidayPlanRepository = holidayPlanRepository;
                }
        */
        //UC15: Como gestor de RH, quero listar os colaboradores que já registaram períodos de férias superiores a x dias 
        public IEnumerable<ICollaborator> FindAllWithHolidayPeriodsLongerThan(int days)
        {
            return _holidayPlanRepository
                .FindAllWithHolidayPeriodsLongerThan(days)
                .Select(p => p.GetCollaborator())
                .Distinct();
        }
    }
}