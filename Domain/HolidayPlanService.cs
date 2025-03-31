using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain
{
    public class HolidayPlanService
    {
        private IAssociationProjectCollaboratorRepository _associationProjectCollaboratorRepository;
        private IHolidayPlanRepository _holidayPlanRepository;

        // public HolidayPlanService(IAssociationProjectCollaboratorRepository associationProjectCollaboratorRepository)
        // {
        //     this._associationProjectCollaboratorRepository = associationProjectCollaboratorRepository;
        // }

        // public HolidayPlanService(IHolidayPlanRepository holidayPlanRepository)
        // {
        //     this._holidayPlanRepository = holidayPlanRepository;
        // }

        public HolidayPlanService(IAssociationProjectCollaboratorRepository associationProjectCollaboratorRepository, IHolidayPlanRepository holidayPlanRepository)
        {
            this._associationProjectCollaboratorRepository = associationProjectCollaboratorRepository;
            this._holidayPlanRepository = holidayPlanRepository;
        }

        //UC16: Como gestor de projeto, quero listar quantos dias de férias um colaborador tem marcado durante um projeto
        public int GetHolidayDaysOfCollaboratorInProject(IProject project, ICollaborator collaborator)
        {

            var association = _associationProjectCollaboratorRepository.FindByProjectAndCollaborator(project, collaborator);

            if (association == null)
                throw new Exception("A associação com os parâmetros fornecidos não existe.");

            int numberOfHolidayDays = 0;

            var collaboratorHolidayPlan = _holidayPlanRepository.FindHolidayPlanByCollaborator(collaborator);

            if (collaboratorHolidayPlan == null)
            {
                return numberOfHolidayDays;
            }

            numberOfHolidayDays = collaboratorHolidayPlan.GetNumberOfHolidayDaysBetween(
                association.GetInitDate(),
                association.GetFinalDate()
            );

            return numberOfHolidayDays;
        }
    }
}