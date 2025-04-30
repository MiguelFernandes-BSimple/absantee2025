using Domain.Interfaces;

namespace Domain.Models
{
    public class TrainingModule : ITrainingModule
    {
        public Guid Id { get; }
        public Guid TrainingSubjectId { get; }
        public List<PeriodDateTime> Periods { get; }

        public TrainingModule(Guid trainingSubjectId, List<PeriodDateTime> periods)
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

            Id = Guid.NewGuid();
            TrainingSubjectId = trainingSubjectId;
            Periods = periods;
        }

        public TrainingModule(Guid id, Guid trainingSubjectId, List<PeriodDateTime> periods)
        {
            Id = id;
            TrainingSubjectId = trainingSubjectId;
            Periods = periods;
        }
    }
}
