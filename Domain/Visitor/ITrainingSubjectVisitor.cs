using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Visitor
{
    public interface ITrainingSubjectVisitor
    {
        long Id { get; set; }
        string Subject { get; set; }
        string Description { get; set; }
    }
}
