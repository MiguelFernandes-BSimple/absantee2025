﻿using System;
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
            if (initDate > finalDate)
                throw new ArgumentException("Invalid Arguments");
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

        public bool Intersects(IPeriodDate periodDate)
        {
            return _initDate <= periodDate.GetFinalDate() && periodDate.GetInitDate() <= _finalDate;
        }

        public IPeriodDate? GetIntersection(IPeriodDate periodDate)
        {
            DateOnly effectiveStart = _initDate > periodDate.GetInitDate() ? _initDate : periodDate.GetInitDate();
            DateOnly effectiveEnd = _finalDate < periodDate.GetFinalDate() ? _finalDate : periodDate.GetFinalDate();

            if (effectiveStart > effectiveEnd)
            {
                return null; // No valid intersection
            }

            return new PeriodDate(effectiveStart, effectiveEnd);
        }

        public int Duration()
        {
            return _finalDate.DayNumber - _initDate.DayNumber + 1;
        }

        public bool Contains(IPeriodDate periodDate)
        {
            return _initDate <= periodDate.GetInitDate()
            && _finalDate >= periodDate.GetFinalDate();
        }

        public bool ContainsDate(DateOnly date)
        {
            return _initDate <= date && _finalDate >= date;
        }
    }
}
