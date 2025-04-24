using Domain.Factory;
using Domain.Models;
using Domain.Visitor;
namespace Infrastructure.DataModel
{
    public class HRManagerDataModel : IHRManagerVisitor
    {
        public Guid Id{ get; set;}

        public Guid UserId { get; set;}

        public PeriodDateTime PeriodDateTime { get; set;}

        public HRManagerDataModel(HRManager hRManager)
        {
            Id = hRManager.Id;
            UserId = hRManager.UserId;
            PeriodDateTime = hRManager.PeriodDateTime;
        }
    }
}