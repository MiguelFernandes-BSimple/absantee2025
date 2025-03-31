using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain
{
    public class CollaboratorService
    {
        private IAssociationProjectCollaboratorRepository associationProjectCollaboratorRepository;
        private IHolidayPlanRepository holidayPlanRepository;

        public CollaboratorService(IAssociationProjectCollaboratorRepository associationProjectCollaboratorRepository, IHolidayPlanRepository holidayPlanRepository)
        {
            this.associationProjectCollaboratorRepository = associationProjectCollaboratorRepository;
            this.holidayPlanRepository = holidayPlanRepository;
        }

        // US14 - Como gestor de RH, quero listar os collaboradores que têm de férias num período
        public IEnumerable<ICollaborator> FindAllCollaboratorsWithHolidayPeriodsBetweenDates(DateOnly initDate, DateOnly endDate)
        {
            // estas verificações podem ser feitas dentro de uma classe period
            if (initDate > endDate)
            {
                return Enumerable.Empty<ICollaborator>();
            }
            else
            {

                return holidayPlanRepository.GetHolidayPlansWithHolidayPeriodValid(initDate, endDate)
                                                .Select(h => h.GetCollaborator())
                                                .Distinct();


            }
        }

        public IEnumerable<ICollaborator> FindAllByProject(IProject project){
            return this.associationProjectCollaboratorRepository.FindAllByProject(project).Select(a => a.GetCollaborator());
        }

        public IEnumerable<ICollaborator> FindAllByProjectAndBetweenPeriod(IProject project, DateOnly initDate, DateOnly finalDate){
            return this.associationProjectCollaboratorRepository.FindAllByProjectAndBetweenPeriod(project, initDate, finalDate).Select(a => a.GetCollaborator());
        }

    }
}