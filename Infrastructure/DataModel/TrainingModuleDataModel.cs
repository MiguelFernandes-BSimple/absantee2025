using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public Guid Id { get; set; }
        public Guid TrainingSubjectId { get; set; }
        public List<PeriodDateTime> Periods { get; set; }

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
