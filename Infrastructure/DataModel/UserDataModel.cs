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
        public PeriodDateTimeDataModel PeriodDateTime { get; set; }

        public UserDataModel() { }
    }
}