namespace Domain
{
    public class CollaboratorService
    {
        private IAssociationProjectCollaboratorRepository _associationProjectCollaboratorRepository;
        private IHolidayPlanRepository _holidayPlanRepository;

        public CollaboratorService(IAssociationProjectCollaboratorRepository associationProjectCollaboratorRepository, IHolidayPlanRepository holidayPlanRepository)
        {
            this._associationProjectCollaboratorRepository = associationProjectCollaboratorRepository;
            this._holidayPlanRepository = holidayPlanRepository;
        }

        //UC15: Como gestor de RH, quero listar os colaboradores que já registaram períodos de férias superiores a x dias 
         public IEnumerable<ICollaborator> FindAllWithHolidayPeriodsLongerThan(int days)
        {
            return _holidayPlanRepository
                .FindAllWithHolidayPeriodsLongerThan(days)
                .Select(p => p.GetCollaborator())
                .Distinct();
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

                return _holidayPlanRepository.GetHolidayPlansWithHolidayPeriodValid(initDate, endDate)
                                                .Select(h => h.GetCollaborator())
                                                .Distinct();


            }
        }

        public IEnumerable<ICollaborator> FindAllByProject(IProject project)
        {
            return this._associationProjectCollaboratorRepository.FindAllByProject(project).Select(a => a.GetCollaborator());
        }

        public IEnumerable<ICollaborator> FindAllByProjectAndBetweenPeriod(IProject project, DateOnly initDate, DateOnly finalDate)
        {
            return this._associationProjectCollaboratorRepository.FindAllByProjectAndBetweenPeriod(project, initDate, finalDate).Select(a => a.GetCollaborator());
        }

    }
}