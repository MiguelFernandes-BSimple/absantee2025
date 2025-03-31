using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain
{
    public class HolidayPlanService
    {
        // private IAssociationProjectCollaboratorRepository? _associationProjectCollaboratorRepository;
        private IHolidayPlanRepository _holidayPlanRepository;

        // public HolidayPlanService(IAssociationProjectCollaboratorRepository associationProjectCollaboratorRepository)
        // {
        //     this._associationProjectCollaboratorRepository = associationProjectCollaboratorRepository;
        // }

        public HolidayPlanService(IHolidayPlanRepository holidayPlanRepository)
        {
            this._holidayPlanRepository = holidayPlanRepository;
        }

        // public HolidayPlanService(IAssociationProjectCollaboratorRepository associationProjectCollaboratorRepository, IHolidayPlanRepository holidayPlanRepository)
        // {
        //     this._associationProjectCollaboratorRepository = associationProjectCollaboratorRepository;
        //     this._holidayPlanRepository = holidayPlanRepository;
        // }

        //UC16: Como gestor de projeto, quero listar quantos dias de férias um colaborador tem marcado durante um projeto
        public int GetHolidayDaysOfCollaboratorInProject(IAssociationProjectCollaborator association)
        {

            int numberOfHolidayDays = 0;

            var collaboratorHolidayPlan = _holidayPlanRepository.FindHolidayPlanByAssociationProjectCollaborator(association);

            if (collaboratorHolidayPlan == null)
                return numberOfHolidayDays;

            numberOfHolidayDays = collaboratorHolidayPlan.GetNumberOfHolidayDaysBetween(
                association.GetInitDate(),
                association.GetFinalDate()
            );

            return numberOfHolidayDays;
        }
    }
}