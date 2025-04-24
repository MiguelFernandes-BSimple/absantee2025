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
    Task<Project> Create(string title, string acronym, PeriodDate periodDate);
    Project Create(IProjectVisitor visitor);
}
