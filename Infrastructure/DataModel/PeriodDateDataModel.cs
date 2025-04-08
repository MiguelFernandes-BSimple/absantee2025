using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.Models;

namespace Infrastructure.DataModel
{
    public class PeriodDateDataModel
    {

        public DateOnly _initDate { get; set; }
        public DateOnly _finalDate { get; set; }

        public PeriodDateDataModel()
        { }

        public PeriodDateDataModel(IPeriodDate periodDate)
        {
            _initDate = periodDate.GetInitDate();
            _finalDate = periodDate.GetFinalDate();
        }
    }
}