using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Interfaces;
using Domain.Models;
using Infrastructure.DataModel;

namespace Infrastructure.Mapper
{
    public class PeriodDateMapper
    {
        public PeriodDate ToDomain(PeriodDateDataModel periodDateDM)
        {
            PeriodDate periodDate = new PeriodDate(periodDateDM._initDate, periodDateDM._finalDate);

            return periodDate;
        }

        public IEnumerable<PeriodDate> ToDomain(IEnumerable<PeriodDateDataModel> periodDateDM)
        {
            return periodDateDM.Select(ToDomain);
        }

        public PeriodDateDataModel ToDataModel(PeriodDate periodDate)
        {
            return new PeriodDateDataModel(periodDate);
        }

        public IEnumerable<PeriodDateDataModel> ToDataModel(IEnumerable<PeriodDate> periodDate)
        {
            return periodDate.Select(ToDataModel);
        }
    }
}
