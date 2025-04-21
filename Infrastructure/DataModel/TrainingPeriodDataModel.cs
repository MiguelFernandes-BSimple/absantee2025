using Domain.Interfaces;
using Domain.Models;
using Domain.Visitor;
namespace Infrastructure.DataModel
{
    public class TrainingPeriodDataModel : ITrainingPeriodVisitor
    {
        public long Id { get; set; }
        public PeriodDate PeriodDate { get; set; }

        public TrainingPeriodDataModel()
        {
        }

        public TrainingPeriodDataModel(ITrainingPeriod trainingPeriod)
        {
            Id = trainingPeriod.GetId();
            PeriodDate = (PeriodDate)trainingPeriod.GetPeriodDate();
        }
    }
}
