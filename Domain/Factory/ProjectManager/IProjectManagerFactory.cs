using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.Models;
using Domain.Visitor;

namespace Domain.Factory
{
    public interface IProjectManagerFactory
    {
        Task<ProjectManager> Create(long userId, PeriodDateTime periodDateTime);

        Task<ProjectManager> Create(long userId, DateTime initDate);
        public ProjectManager Create(IProjectManagerVisitor projectManagerVisitor);
    }
}