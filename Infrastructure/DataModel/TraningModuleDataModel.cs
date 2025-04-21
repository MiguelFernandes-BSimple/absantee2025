using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.Models;
using Domain.Visitor;

namespace Infrastructure.DataModel
{
    public class TraningModuleDataModel : ITrainingModuleVisitor
    {

        public long id { get; set; }

        public long subjectId { get; set; }

        public List<PeriodDateTime> periodsList { get; set; }

        public TraningModuleDataModel(ITraningModule tm)
        {
            id = tm.GetId();
            subjectId = tm.GetSubjectId();
            periodsList = tm.GetPeriodsList();
        }

        public TraningModuleDataModel()
        {
        }
    }
}