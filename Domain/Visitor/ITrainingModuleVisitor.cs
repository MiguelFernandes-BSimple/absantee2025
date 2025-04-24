using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Domain.Visitor
{
    public interface ITrainingModuleVisitor
    {
        Guid Id { get; }
        Guid TrainingSubjectId { get; }
        List<PeriodDateTime> Periods { get; }
    }
}
