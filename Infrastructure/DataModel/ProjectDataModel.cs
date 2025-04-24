using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.Models;
using Domain.Visitor;

namespace Infrastructure.DataModel
{
    public class ProjectDataModel : IProjectVisitor
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Acronym { get; set; }
        public PeriodDate PeriodDate { get; set; }

        public ProjectDataModel(Project project)
        {
            Id = project.Id;
            Title = project.GetTitle();
            Acronym = project.GetAcronym();
            PeriodDate = project.GetPeriodDate();
        }

        public ProjectDataModel()
        {
        }
    }
}
