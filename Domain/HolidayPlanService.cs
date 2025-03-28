using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain
{
    public class HolidayPlanService
    {
        private IAssociationProjectCollaboratorRepository associationProjectCollaboratorRepository;
        private IHolidayPlanRepository holidayPlanRepository;

        public HolidayPlanService(IAssociationProjectCollaboratorRepository associationProjectCollaboratorRepository, IHolidayPlanRepository holidayPlanRepository)
        {
            this.associationProjectCollaboratorRepository = associationProjectCollaboratorRepository;
            this.holidayPlanRepository = holidayPlanRepository;
        }

        //UC21: Como gestor de projeto, quero listar os períodos de férias dos colaboradores dum projeto, num período
        public IEnumerable<IHolidayPeriod> FindAllHolidayPeriodsForAllProjectCollaboratorsBetweenDates(
            IProject project,
            DateOnly initDate,
            DateOnly endDate
        )
        {
            var validCollaborators = associationProjectCollaboratorRepository.FindAllByProjectAndBetweenPeriod(
                project,
                initDate,
                endDate
            ).Select(a => a.GetCollaborator());

            if (initDate > endDate)
            {
                return Enumerable.Empty<IHolidayPeriod>();
            }
            return holidayPlanRepository.FindAllHolidayPeriodsForAllCollaboratorsBetweenDates(validCollaborators.ToList(), initDate, endDate);

        }
        //uc22
        public int GetHolidayDaysForProjectCollaboratorBetweenDates(
            IProject project,
            ICollaborator collaborator,
            DateOnly initDate,
            DateOnly endDate
        )
        {
            if (initDate > endDate)
            {
                return 0;
            }
            var association = associationProjectCollaboratorRepository.FindByProjectandCollaborator(project, collaborator);
            if (association == null)
            {
                throw new Exception("");
            }


            int totalHolidayDays = 0;
            var holidayPeriods = holidayPlanRepository.FindHolidayPeriodsByCollaborator(collaborator);

            foreach (var holidayColabPeriod in holidayPeriods)
            {
                DateOnly holidayStart = holidayColabPeriod.GetInitDate();
                DateOnly holidayEnd = holidayColabPeriod.GetFinalDate();

                totalHolidayDays += holidayColabPeriod.GetNumberOfCommonUtilDaysBetweenPeriods(
                    holidayStart,
                    holidayEnd
                );
            }
            return totalHolidayDays;

        }


    //     public int GetHolidayDaysForProjectAllCollaboratorBetweenDates(IProject project, DateOnly initDate, DateOnly endDate)
    //     {
    //         if (initDate > endDate || holidayPlanRepository == null || associationProjectCollaboratorRepository == null)
    //         {
    //             return 0;
    //         }

    //         var collaborators = associationProjectCollaboratorRepository.FindAllByProjectAndBetweenPeriod(
    //             project,
    //             initDate,
    //             endDate
    //         ).Select(a => a.GetCollaborator());

    //         int totalHolidayDays = 0;

    //         foreach (var collaborator in collaborators)
    //         {
    //             var holidayPlans = holidayPlanRepository.GetHolidayPlansByCollaborator(collaborator);

    //             foreach (var holidayPlan in holidayPlans)
    //             {
    //                 foreach (var holidayPeriod in holidayPlan.GetHolidayPeriods())
    //                 {
    //                     var holidayStart = holidayPeriod.GetInitDate();
    //                     var holidayEnd = holidayPeriod.GetFinalDate();

    //                     if (holidayEnd < initDate || holidayStart > endDate)
    //                     {
    //                         continue;
    //                     }

    //                     var effectiveStart = holidayStart < initDate ? initDate : holidayStart;
    //                     var effectiveEnd = holidayEnd > endDate ? endDate : holidayEnd;

    //                     totalHolidayDays += effectiveEnd.DayNumber - effectiveStart.DayNumber + 1;
    //                 }
    //             }
    //         }
            
    //         return totalHolidayDays;
    //     }
    // }
    }
}   