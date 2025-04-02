using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;

namespace Domain.Models
{
    public class PeriodDate : IPeriodDate
    {
        private DateOnly _initDate;
        private DateOnly _finalDate;

        public PeriodDate(DateOnly initDate, DateOnly finalDate)
        {
            _initDate = initDate;
            _finalDate = finalDate;
        }

        public DateOnly GetInitDate()
        {
            return _initDate;
        }

        public DateOnly GetFinalDate()
        {
            return _finalDate; 
        }
    }
}
