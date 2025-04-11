using System.ComponentModel.DataAnnotations.Schema;
using Domain.Interfaces;
using Domain.Models;
using Domain.Visitor;

namespace Infrastructure.DataModel
{
    [Table("User")]
    public class UserDataModel : IUserVisitor
    {
        public long Id { get; set; }
        public string Names { get; set; }
        public string Surnames { get; set; }
        public string Email { get; set; }
        public PeriodDateTime PeriodDateTime { get; set; }

        public UserDataModel(IUser user)
        {
            Id = user.GetId();
            Names = user.GetNames();
            Surnames = user.GetSurnames();
            Email = user.GetEmail();
            PeriodDateTime = (PeriodDateTime)user.GetPeriodDateTime();
        }
    }
}