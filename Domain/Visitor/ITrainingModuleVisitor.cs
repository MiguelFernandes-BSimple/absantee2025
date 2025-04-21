using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Interfaces;

namespace Domain.Visitor
{
    public interface ITrainingModuleVisitor
    {
        long Id { get; }
        long SubjectID { get; }
        List<ITrainingPeriod> GetTrainingPeriods();
    }
}