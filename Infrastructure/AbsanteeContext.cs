using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class AbsanteeContext : DbContext
    {
        public virtual DbSet<Collaborator> Collaborators { get; set; }
        public virtual DbSet<AssociationProjectCollaborator> Associations  { get; set; }
        public virtual DbSet<HolidayPlanRepository> HolidayPlans { get; set; }
        public virtual DbSet<Project> Projects { get; set; }

        public AbsanteeContext(DbContextOptions<AbsanteeContext> options) : base(options) 
        {
        }
    }
}
