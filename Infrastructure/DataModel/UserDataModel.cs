using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Infrastructure.DataModel
{
    [Table("User")]
    public class UserDataModel
    {
        public long Id { get; set; }
        public string Names { get; set; }
        public string Surnames { get; set; }
        public string Email { get; set; }
        public PeriodDateTimeDataModel PeriodDateTime { get; set; }

        public UserDataModel() { }

        public UserDataModel(User user)
        {
            Id = user.GetId();
            Names = user.GetNames();
            Surnames = user.GetSurnames();
            Email = user.GetEmail();
            PeriodDateTime = new PeriodDateTimeDataModel(user.GetPeriodDateTime());
        }
    }
}