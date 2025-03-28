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

        private bool IsHolidayPeriodValid(IHolidayPeriod period, DateOnly initDate, DateOnly endDate)
        {
            return period.GetInitDate() <= endDate && period.GetFinalDate() >= initDate;
        }

        // US14 - Como gestor de RH, quero listar os collaboradores que têm de férias num período
        public IEnumerable<ICollaborator> FindAllCollaboratorsWithHolidayPeriodsBetweenDates(DateOnly initDate, DateOnly endDate)
        {
            if (initDate > endDate)
            {
                return Enumerable.Empty<ICollaborator>();
            }
            else
            {
                if (holidayPlanRepository == null)
                {
                    return Enumerable.Empty<ICollaborator>();
                }

                var holidayPlans = holidayPlanRepository.FindAll();
                return holidayPlans.Where(h => h.GetHolidayPeriods().Any(p => IsHolidayPeriodValid(p, initDate, endDate)))
                                .Select(h => h.GetCollaborator())
                                .Distinct();


            }
        }
    }
}