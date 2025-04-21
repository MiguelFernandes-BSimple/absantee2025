using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Visitor
{
    public interface IAssociationCollabTrainingModuleVisitor
    {
        public long _id { get; }
        public long _collaboratorId { get;  }
        public long _trainingModuleId { get; }
    }
}