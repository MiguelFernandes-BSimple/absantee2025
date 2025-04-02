using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;

namespace Domain.Models
{
    public class PeriodDateTime : IPeriodDateTime
    {
        private DateTime _initDate;
        private DateTime _endDate;

        public PeriodDateTime(DateTime initDate, DateTime endDate)
        {
            _initDate = initDate;
            _endDate = endDate;
        }

        public DateTime GetInitDate()
        {
            return _initDate;
        }

        public DateTime GetFinalDate()
        {
            return _endDate;
        }
    }
}
