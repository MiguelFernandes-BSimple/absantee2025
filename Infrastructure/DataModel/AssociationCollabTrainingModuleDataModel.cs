using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.Models;
using Domain.Visitor;

namespace Infrastructure.DataModel
{
    public class AssociationCollabTrainingModuleDataModel : IAssociationCollabTrainingModuleVisitor
    {
         public long _id { get; set; }
        public long _collaboratorId { get; set; }
        public long _trainingModuleId { get; set; }

        public AssociationCollabTrainingModuleDataModel(AssociationCollabTrainingModule actm){
            _id = actm.GetId();
            _collaboratorId = actm.GetCollaboratorId();
            _trainingModuleId = actm.GetTrainingModuleId();
        }

        public AssociationCollabTrainingModuleDataModel(){}
    }
}