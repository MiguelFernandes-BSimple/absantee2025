using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.Models;

namespace Domain.Visitor
{
    public interface ITrainingModuleVisitor
    {
        long Id { get; set; }
        long SubjectID { get; set; }
        List<PeriodDateTime> PeriodDateTime { get; set; }
    }
}