using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.Models;
using Domain.Visitor;

namespace Domain.Factory
{
    public interface ITrainingManagerFactory
    {
        Task<TrainingManager> Create(long userId, PeriodDateTime periodDateTime);

        Task<TrainingManager> Create(long userId, DateTime initDate);
        public TrainingManager Create(ITrainingManagerVisitor trainingManagerFactory);
    }
}