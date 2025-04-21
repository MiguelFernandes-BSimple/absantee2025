using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.Visitor;

namespace Infrastructure.DataModel
{
    public class TrainingSubjectDataModel : ITrainingSubjectVisitor
    {
        public long Id { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }

        public TrainingSubjectDataModel()
        {
        }

        public TrainingSubjectDataModel(ITrainingSubject trainingSubject)
        {
            this.Id = trainingSubject.Id;
            this.Subject = trainingSubject.Subject;
            this.Description = trainingSubject.Description;
        }

    }
}
