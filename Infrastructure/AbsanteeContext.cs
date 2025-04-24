using Domain.Models;
using Infrastructure.DataModel;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class AbsanteeContext : DbContext, IAbsanteeContext
    {
        public virtual DbSet<CollaboratorDataModel> Collaborators { get; set; }
        public virtual DbSet<AssociationProjectCollaboratorDataModel> Associations { get; set; }
        public virtual DbSet<HolidayPlanDataModel> HolidayPlans { get; set; }
        public virtual DbSet<ProjectDataModel> Projects { get; set; }
        public virtual DbSet<UserDataModel> Users { get; set; }
        public virtual DbSet<TrainingSubjectDataModel> TrainingSubjects { get; set; }
        public virtual DbSet<TrainingModuleDataModel> TrainingModules { get; set; }
        public virtual DbSet<AssociationTrainingModuleCollaboratorDataModel> TrainingModuleCollaborator { get; set; }

        public AbsanteeContext(DbContextOptions<AbsanteeContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HolidayPeriodDataModel>()
                .OwnsOne(h => h.PeriodDate);

            modelBuilder.Entity<AssociationProjectCollaboratorDataModel>()
                .OwnsOne(a => a.PeriodDate);

            //modelBuilder.Entity<TrainingPeriodDataModel>()
            //    .OwnsOne(a => a.PeriodDate);

            modelBuilder.Entity<CollaboratorDataModel>()
                .OwnsOne(a => a.PeriodDateTime);


            modelBuilder.Entity<ProjectDataModel>()
                .OwnsOne(a => a.PeriodDate);

            modelBuilder.Entity<UserDataModel>()
                .OwnsOne(a => a.PeriodDateTime);

            modelBuilder.Entity<TrainingModuleDataModel>()
                .OwnsMany(t => t.Periods);

            base.OnModelCreating(modelBuilder);
        }
    }
}
