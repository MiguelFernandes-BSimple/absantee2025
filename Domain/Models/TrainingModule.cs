using System;
using System.Collections.Generic;
using Domain.Interfaces;

namespace Domain.Models
{
    public class TrainingModule : ITrainingModule
    {
        private long _id;
        private  long _subjectId;
        private List<PeriodDateTime> _periodos;

        public TrainingModule(long subjectId, List<PeriodDateTime> periodos)
        {
            _subjectId = subjectId;
            _periodos = periodos;
        }

        public TrainingModule(long id, long subjectId, List<PeriodDateTime> periodos)
            : this(subjectId, periodos){
                _id = id;

            }

        public long GetId()
        {
            return _id;
        }

        public void SetId(long id)
        {
            _id = id;
        }

        public long GetSubjectId()
        {
            return _subjectId;
        }

        public List<PeriodDateTime> GetPeriodos()
        {
            return _periodos;
        }

        public void SetPeriodos(List<PeriodDateTime> periodos)
        {
            _periodos = periodos;
        }
    }
}
