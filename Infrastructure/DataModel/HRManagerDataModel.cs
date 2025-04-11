using Domain.Factory;
using Domain.Models;
using Domain.Visitor;
namespace Infrastructure.DataModel
{
    public class HRManagerDataModel : IHRManagerVisitor
    {
        public long Id{ get; set;}

        public long UserId { get; set;}

        public PeriodDateTime PeriodDateTime { get; set;}


        public HRManagerDataModel()
        {
        }

        public HRManagerDataModel(HRManager hRManager)
        {
            Id = hRManager.GetId();
            UserId = hRManager.GetUserId();
            PeriodDateTime = (PeriodDateTime)hRManager.GetPeriodDateTime();
        }
    }
}