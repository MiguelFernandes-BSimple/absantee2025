using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Models;

namespace Domain.Visitor
{
    public interface IProjectManagerVisitor
    {
        public long Id { get; }
        public long UserId { get; }
        public PeriodDateTime PeriodDateTime { get; }
    }
}