using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;

namespace Infrastructure.DataModel
{
    public class PeriodDateTimeDataModel
    {
        public DateTime _initDate { get; set; }
        public DateTime _finalDate { get; set; }


        public PeriodDateTimeDataModel( )
        {}

        public PeriodDateTimeDataModel(IPeriodDateTime periodDateTime)
        {
            _initDate = periodDateTime.GetInitDate();
            _finalDate = periodDateTime.GetFinalDate();
        }
    }
}