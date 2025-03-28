using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain
{
    public class HolidayPlanService
    {
        private IAssociationProjectCollaboratorRepository? associationProjectCollaboratorRepository;
        private IHolidayPlanRepository? holidayPlanRepository;

        public HolidayPlanService(IAssociationProjectCollaboratorRepository associationProjectCollaboratorRepository)
        {
            this.associationProjectCollaboratorRepository = associationProjectCollaboratorRepository;
        }

        public HolidayPlanService(IHolidayPlanRepository holidayPlanRepository)
        {
            this.holidayPlanRepository = holidayPlanRepository;
        }

        public HolidayPlanService(IAssociationProjectCollaboratorRepository associationProjectCollaboratorRepository, IHolidayPlanRepository holidayPlanRepository)
        {
            this.associationProjectCollaboratorRepository = associationProjectCollaboratorRepository;
            this.holidayPlanRepository = holidayPlanRepository;
        }
        //uc21
        public IEnumerable<IHolidayPeriod> FindAllHolidayPeriodsForAllProjectCollaboratorsBetweenDates(
            IProject project,
            DateOnly initDate,
            DateOnly endDate
        )
        {
            var emptyList = Enumerable.Empty<IHolidayPeriod>();

            if (associationProjectCollaboratorRepository == null)
            {
                throw new Exception();
            }

            var validCollaborators = associationProjectCollaboratorRepository.FindAllProjectCollaboratorsBetween(
                project,
                initDate,
                endDate
            );
            if (validCollaborators == null || initDate > endDate)
            {
                return emptyList;
            }
            else
            {
                if (holidayPlanRepository == null)
                {
                    return emptyList;
                }
                var _holidayPlans = holidayPlanRepository.FindAll();
                return _holidayPlans
                    .Where(hp => validCollaborators.Contains(hp.GetCollaborator()))
                    .SelectMany(hp =>
                        hp.GetHolidayPeriods()
                            .Where(hp => hp.GetInitDate() <= endDate && hp.GetFinalDate() >= initDate)
                    );
            }
        }
    }
}