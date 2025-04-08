using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Infrastructure.DataModel
{
    public class ProjectDataModel
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Acronym { get; set; }
        public PeriodDateDataModel PeriodDate { get; set; }

        public ProjectDataModel()
        {
        }

        public ProjectDataModel(Project project)
        {
            Id = project.GetId();
            Title = project.GetTitle();
            Acronym = project.GetAcronym();
            PeriodDate = new PeriodDateDataModel(project.GetPeriodDate());
        }
    }
}
