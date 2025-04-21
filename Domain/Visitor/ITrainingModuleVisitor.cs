using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Models;

namespace Domain.Visitor
{
    public interface ITrainingModuleVisitor
    {
        public long id { get; }
        public long subjectId { get; }
        public List<PeriodDateTime> periodsList { get; }
    }
}