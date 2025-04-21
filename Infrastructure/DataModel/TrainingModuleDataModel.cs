using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.Models;
using Domain.Visitor;

namespace Infrastructure.DataModel
{
    public class TrainingModuleDataModel : ITrainingModuleVisitor
    {
        public long Id { get; }
        public long TrainingSubjectId { get; }
        public List<PeriodDateTime> Periods { get; }

        public TrainingModuleDataModel()
        {
        }

        public TrainingModuleDataModel(ITrainingModule trainingModule)
        {
            Id = trainingModule.Id;
            TrainingSubjectId = trainingModule.TrainingSubjectId;
            Periods = trainingModule.Periods;
        }
    }
}
