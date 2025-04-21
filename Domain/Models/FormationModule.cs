using Domain.Interfaces;

namespace Domain.Models
{
    public class FormationModule : IFormationModule
    {
        private long _id;
        private long _formationSubjectId;
        private List<IFormationPeriod> _formationPeriods;

        public FormationModule(long formationSubjectId, List<IFormationPeriod> formationPeriods)
        {
            _formationSubjectId = formationSubjectId;
            _formationPeriods = formationPeriods;
        }

        public FormationModule(long id, long formationSubjectId, List<IFormationPeriod> formationPeriods)
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

        public List<IFormationPeriod> GetFormationPeriods()
        {
            return new List<IFormationPeriod>(_formationPeriods);
        }

    }
}