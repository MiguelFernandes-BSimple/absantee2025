using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.Models;
using Infrastructure.DataModel;

namespace Infrastructure.Mapper
{
    public class ProjectMapper
    {
        public Project ToDomain(ProjectDataModel projectDM)
        {
            IPeriodDate periodDate = new PeriodDate(projectDM.PeriodDate._initDate, projectDM.PeriodDate._finalDate);
            Project proj = new Project(projectDM.Title, projectDM.Acronym, periodDate);

            proj.SetId(projectDM.Id);

            return proj;
        }

        public IEnumerable<Project> ToDomain(IEnumerable<ProjectDataModel> projectsDM)
        {
            return projectsDM.Select(ToDomain);
        }

        public ProjectDataModel ToDataModel(Project project)
        {
            return new ProjectDataModel(project);
        }

        public IEnumerable<ProjectDataModel> ToDataModel(IEnumerable<Project> projects)
        {
            return projects.Select(ToDataModel);
        }
    }
}
