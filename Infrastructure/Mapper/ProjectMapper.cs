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
    public class ProjectMapper : IMapper<Project, ProjectDataModel>
    {
        public Project ToDomain(ProjectDataModel projectDM)
        {
            Project proj = new Project(projectDM.Id, projectDM.Title, projectDM.Acronym, projectDM.PeriodDate);

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
