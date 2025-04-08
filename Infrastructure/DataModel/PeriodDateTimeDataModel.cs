using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.Models;

namespace Infrastructure.DataModel
{
    public class PeriodDateTimeDataModel
    {

        public long Id { get; set; }
        public DateTime _initDate { get; set; }
        public DateTime _finalDate { get; set; }


        public PeriodDateTimeDataModel( )
        {}

        public PeriodDateTimeDataModel(PeriodDateTime periodDateTime)
        {
            Id = periodDateTime.GetId();
            _initDate = periodDateTime.GetInitDate();
            _finalDate = periodDateTime.GetFinalDate();
        }
    }
}