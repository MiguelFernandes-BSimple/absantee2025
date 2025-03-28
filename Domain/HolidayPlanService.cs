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
            if (initDate > endDate || holidayPlanRepository == null || associationProjectCollaboratorRepository == null)
            {
                return 0;
            }

            var collaborators = associationProjectCollaboratorRepository.FindAllProjectCollaborators(project);
            int totalHolidayDays = 0;

            foreach (var collaborator in collaborators)
            {
                var holidayPlans = holidayPlanRepository.GetHolidayPlansByCollaborator(collaborator);

                foreach (var holidayPlan in holidayPlans)
                {
                    foreach (var holidayPeriod in holidayPlan.GetHolidayPeriods())
                    {
                        var holidayStart = holidayPeriod.GetInitDate();
                        var holidayEnd = holidayPeriod.GetFinalDate();

                        if (holidayEnd < initDate || holidayStart > endDate)
                        {
                            continue;
                        }

                        var effectiveStart = holidayStart < initDate ? initDate : holidayStart;
                        var effectiveEnd = holidayEnd > endDate ? endDate : holidayEnd;

                        totalHolidayDays += effectiveEnd.DayNumber - effectiveStart.DayNumber + 1;
                    }
                }
            }
            
            return totalHolidayDays;
        }
    }
}   