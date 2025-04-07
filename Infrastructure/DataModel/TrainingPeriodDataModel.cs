using Domain.Models;
namespace Infrastructure.DataModel
{
    public class TrainingPeriodDataModel
    {
        public long Id { get; set; }
        public PeriodDateDataModel PeriodDate { get; set; }

        public TrainingPeriodDataModel()
        {
        }
    }
}
