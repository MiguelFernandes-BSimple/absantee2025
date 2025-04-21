using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class TrainingModule
    {
        private long _id;
        private long _subjectId;
        private List<PeriodDateTime> _periodsList;

        public TrainingModule(long id, long subjectId, List<PeriodDateTime> periodsList)
        {
            _id = id;
            _subjectId = subjectId;
            _periodsList = periodsList;
        }

        public TrainingModule(long subjectId, List<PeriodDateTime> periodsList)
        {
            _subjectId = subjectId;
            _periodsList = periodsList;
        }

        public long GetId()
        {
            return _id;
        }

        public long GetSubjectId()
        {
            return _subjectId;
        }

        public List<PeriodDateTime> GetPeriodsList()
        {
            return _periodsList;
        }
    }
}