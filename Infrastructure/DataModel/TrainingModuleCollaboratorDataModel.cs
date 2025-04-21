using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.Visitor;

namespace Infrastructure.DataModel
{
    public class TrainingModuleCollaboratorDataModel : ITrainingModuleCollaboratorsVisitor
    {
        public long TrainingModuleId {  get; set; }
        public long CollaboratorId {  get; set; }

        public TrainingModuleCollaboratorDataModel()
        {
        }

        public TrainingModuleCollaboratorDataModel(ITrainingModuleCollaborators trainingModuleCollaborators)
        {
            TrainingModuleId = trainingModuleCollaborators.TrainingModuleId;
            CollaboratorId = trainingModuleCollaborators.CollaboratorId;
        }
    }
}
