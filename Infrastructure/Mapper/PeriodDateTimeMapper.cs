using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Interfaces;
using Domain.Models;
using Infrastructure.DataModel;

namespace Infrastructure.Mapper
{
    public class PeriodDateTimeMapper
    {
        public PeriodDateTime ToDomain(PeriodDateTimeDataModel periodDateTimeDM)
        {
            PeriodDateTime periodDateTime = new PeriodDateTime(periodDateTimeDM._initDate, periodDateTimeDM._finalDate);

            return periodDateTime;
        }

        public IEnumerable<PeriodDateTime> ToDomain(IEnumerable<PeriodDateTimeDataModel> periodDateTimeDM)
        {
            return periodDateTimeDM.Select(ToDomain);
        }

        public PeriodDateTimeDataModel ToDataModel(PeriodDateTime periodDate)
        {
            return new PeriodDateTimeDataModel(periodDate);
        }

        public IEnumerable<PeriodDateTimeDataModel> ToDataModel(IEnumerable<PeriodDateTime> periodDate)
        {
            return periodDate.Select(ToDataModel);
        }
    }
}