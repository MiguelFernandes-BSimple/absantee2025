using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.Models;
using Domain.Visitor;
namespace Domain.Factory;

public interface IProjectFactory
{
    Project Create(long id, string title, string acronym, IPeriodDate periodDate);
    Project Create(IProjectVisitor visitor);
}
