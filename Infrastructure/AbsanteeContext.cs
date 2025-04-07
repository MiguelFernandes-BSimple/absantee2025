﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Infrastructure.DataModel;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class AbsanteeContext : DbContext
    {
        public virtual DbSet<CollaboratorDataModel> Collaborators { get; set; }
        public virtual DbSet<AssociationProjectCollaboratorDataModel> Associations  { get; set; }
        public virtual DbSet<HolidayPlanDataModel> HolidayPlans { get; set; }
        public virtual DbSet<ProjectDataModel> Projects { get; set; }

        public AbsanteeContext(DbContextOptions<AbsanteeContext> options) : base(options) 
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HolidayPeriodDataModel>()
                .OwnsOne(h => h.PeriodDate);

            modelBuilder.Entity<AssociationProjectCollaboratorDataModel>()
                .OwnsOne(a => a.Period);

            modelBuilder.Entity<TrainingPeriodDataModel>()
                .OwnsOne(a => a.PeriodDate);

            modelBuilder.Entity<CollaboratorDataModel>()
                .OwnsOne(a => a.PeriodDateTime);


            modelBuilder.Entity<ProjectDataModel>()
                .OwnsOne(a => a.PeriodDate);

            modelBuilder.Entity<UserDataModel>()
                .OwnsOne(a => a.PeriodDateTime);

            base.OnModelCreating(modelBuilder);
        }
    }
}
