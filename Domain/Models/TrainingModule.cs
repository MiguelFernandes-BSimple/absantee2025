using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Domain.Interfaces;

namespace Domain.Models
{
    public class TrainingModule : ITrainingModule
    {
        public long Id { get; }
        public long TrainingSubjectId { get; }
        public List<PeriodDateTime> Periods { get; }

        public TrainingModule(long trainingSubjectId, List<PeriodDateTime> periods)
        {
            // Check for overlapping periods
            for (int i = 0; i < periods.Count; i++)
            {
                var periodA = periods[i];
                //check if period is in the future
                if (DateTime.Now > periodA._initDate)
                    throw new ArgumentException("Periods must start in the future");

                for (int j = i + 1; j < periods.Count; j++)
                {
                    var periodB = periods[j];

                    if (periodA._initDate <= periodB._finalDate && periodB._initDate <= periodA._finalDate)
                    {
                        throw new ArgumentException("Training periods cannot overlap.");
                    }
                }
            }

            TrainingSubjectId = trainingSubjectId;
            Periods = periods;
        }

        public TrainingModule(long id, long trainingSubjectId, List<PeriodDateTime> periods)
        {
            Id = id;
            TrainingSubjectId = trainingSubjectId;
            Periods = periods;
        }
    }
}
