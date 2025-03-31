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
            var validCollaborators = associationProjectCollaboratorRepository.FindAllProjectCollaboratorsBetween(
                project,
                initDate,
                endDate
            );

            {
                if (initDate > endDate)
                {
                    return Enumerable.Empty<IHolidayPeriod>();
                }
                return holidayPlanRepository.FindAllHolidayPeriodsForAlltCollaboratorsBetweenDates(validCollaborators.ToList(), initDate, endDate);

            }
        }
        //uc22
        public int GetHolidayDaysForProjectCollaboratorBetweenDates(
            IAssociationProjectCollaborator association,
            DateOnly initDate,
            DateOnly endDate
        )
        {
            if (initDate > endDate)
            {
                return 0;
            }
            if (association.AssociationIntersectDates(initDate, endDate))
            {
                var colaborador = association.GetCollaborator();
                var project = association.GetProject();
                var _holidayPlans = holidayPlanRepository?.FindAll() ?? Enumerable.Empty<IHolidayPlan>();
                var collaboratorHolidayPlan = _holidayPlans.FirstOrDefault(hp =>
                    hp.GetCollaborator().Equals(colaborador)
                );

                if (collaboratorHolidayPlan == null)
                    return 0;

                int totalHolidayDays = 0;

                foreach (var holidayColabPeriod in collaboratorHolidayPlan.GetHolidayPeriods())
                {
                    DateOnly holidayStart = holidayColabPeriod.GetInitDate();
                    DateOnly holidayEnd = holidayColabPeriod.GetFinalDate();

                    if (association.AssociationIntersectDates(holidayStart, holidayEnd))
                    {
                        totalHolidayDays += holidayColabPeriod.GetNumberOfCommonUtilDaysBetweenPeriods(
                            holidayStart,
                            holidayEnd
                        );
                    }
                }

                return totalHolidayDays;
            }
            return 0;
        }
    }
}