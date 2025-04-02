using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IPeriodDate
    {
        public DateOnly GetInitDate();
        public DateOnly GetFinalDate();
        public bool Intersects(IPeriodDate periodDate);
        public IPeriodDate? GetIntersection(IPeriodDate periodDate);
        public bool Contains(IPeriodDate periodDate);
        public bool ContainsDate(DateOnly date);
        public int Duration();
    }
}
