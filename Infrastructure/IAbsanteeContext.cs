using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.DataModel;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public interface IAbsanteeContext
    {
        DbSet<CollaboratorDataModel> Collaborators { get; set; }
        DbSet<AssociationProjectCollaboratorDataModel> Associations { get; set; }
        DbSet<HolidayPlanDataModel> HolidayPlans { get; set; }
        DbSet<ProjectDataModel> Projects { get; set; }
        DbSet<UserDataModel> Users { get; set; }
        DbSet<TrainingManagerDataModel> TrainingManagers { get; set; }

    }
}
