using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.Models;
using Domain.Visitor;

namespace Domain.Factory;

public interface ITrainingModuleFactory
{
    TrainingModule Create(long SubjectID, List<ITrainingPeriod> trainingPeriods);
    TrainingModule Create(ITrainingModuleVisitor trainingModuleVisitor);
}
