using System.ComponentModel.DataAnnotations.Schema;
using Domain.Interfaces;
using Domain.Models;
using Domain.Visitor;

namespace Infrastructure.DataModel
{
    [Table("User")]
    public class UserDataModel : IUserVisitor
    {
        public Guid Id { get; set; }
        public string Names { get; set; }
        public string Surnames { get; set; }
        public string Email { get; set; }
        public PeriodDateTime PeriodDateTime { get; set; }

        public UserDataModel(IUser user)
        {
            if (user.Id != Guid.Empty)
                Id = user.Id;

            Names = user.Names;
            Surnames = user.Surnames;
            Email = user.Email;
            PeriodDateTime = user.PeriodDateTime;
        }

        public UserDataModel()
        {
        }
    }
}