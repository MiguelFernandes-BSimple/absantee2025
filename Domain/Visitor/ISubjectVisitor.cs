using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Models;

namespace Domain.Visitor
{
    public class ISubjectVisitor
    {
        public long Id { get; }
        public string Title { get; }
        public string Description { get; }
    }
}

