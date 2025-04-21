using Domain.Interfaces;

namespace Domain.Models
{
    public class FormationModule : IFormationModule
    {
        private long _id;
        private long _formationSubjectId;
        private List<PeriodDateTime> _formationPeriods;

        public FormationModule(long formationSubjectId, List<PeriodDateTime> formationPeriods)
        {
            _formationSubjectId = formationSubjectId;
            _formationPeriods = formationPeriods;
        }

        public FormationModule(long id, long formationSubjectId, List<PeriodDateTime> formationPeriods)
        {
            _id = id;
            _formationSubjectId = formationSubjectId;
            _formationPeriods = formationPeriods;
        }

        public long GetId()
        {
            return _id;
        }

        public long GetFormationSubjectId()
        {
            return _formationSubjectId;
        }

        public List<PeriodDateTime> GetFormationPeriods()
        {
            return new List<PeriodDateTime>(_formationPeriods);
        }

    }
}