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

       public int GetHolidayDaysForProjectCollaboratorBetweenDates(IProject project, DateOnly initDate, DateOnly endDate)
        {
            if (holidayPlanRepository == null || associationProjectCollaboratorRepository == null || initDate > endDate)
            {
                return 0;
            }

            var associations = associationProjectCollaboratorRepository.FindAllByProject(project);

            int totalHolidayDays = 0;

            foreach (var association in associations)
            {
                var holidayPlans = holidayPlanRepository.GetHolidayPlansByAssociations(association);

                foreach (var holidayPlan in holidayPlans)
                {
                    var holidayPeriods = holidayPlan.GetHolidayPeriods()
                        .Where(hp => hp.GetInitDate() <= endDate && hp.GetFinalDate() >= initDate);

                    foreach (var period in holidayPeriods)
                    {
                        totalHolidayDays += period.GetDurationInDays(initDate, endDate);
                    }
                }
            }

            return totalHolidayDays;
        }
    }
}   