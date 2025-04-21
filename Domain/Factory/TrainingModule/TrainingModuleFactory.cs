using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.IRepository;
using Domain.Models;
using Domain.Visitor;

namespace Domain.Factory
{
    public class TrainingModuleFactory : ITrainingModuleFactory
    {
        private readonly ITrainingSubjectRepository _subjectRepository;

        public TrainingModuleFactory(ITrainingSubjectRepository subjectRepository)
        {
            _subjectRepository = subjectRepository;
        }

        public async Task<TrainingModule> Create(long traingSubjectId, List<PeriodDateTime> periods)
        {
            var trainingSubject = await _subjectRepository.GetByIdAsync(traingSubjectId);

            if (trainingSubject == null)
                throw new ArgumentException("Training Subject must exists");

            return new TrainingModule(traingSubjectId, periods);
        }

        public TrainingModule Create(ITrainingModuleVisitor trainingModuleVisitor)
        {
            return new TrainingModule(trainingModuleVisitor.Id, trainingModuleVisitor.TrainingSubjectId, trainingModuleVisitor.Periods);
        }
    }
}
