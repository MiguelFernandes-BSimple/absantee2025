using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Visitor
{
    public interface ISubjectVisitor
    {
        long _id { get; }
        string _title { get; }
        string _description { get; }
    } 
}