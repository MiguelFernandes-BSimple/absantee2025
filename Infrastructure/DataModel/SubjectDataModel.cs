using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Models;
using Domain.Visitor;

namespace Infrastructure.DataModel
{
    public class SubjectDataModel : ISubjectVisitor
    {
        public long _id { get; set; }
        public string _title { get; set; }
        public string _description { get; set; }

        public SubjectDataModel(Subject subject)
        {
            _id = subject.GetId();
            _title = subject.GetTitle();
            _description = subject.GetDescription();
        }

        public SubjectDataModel() { }
    }
}