using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.Models;
using Domain.Visitor;

namespace Domain.Factory
{
    public interface ITrainingModuleFactory
    {
        Task<TrainingModule> Create(long traingSubjectId, List<PeriodDateTime> periods);
        TrainingModule Create(ITrainingModuleVisitor trainingModuleVisitor);
    }
}
