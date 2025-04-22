using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.IRepository;
using Domain.Models;

namespace Domain.Factory
{
    public class TrainingSubjectFactory
    {
        private readonly ITrainingSubjectRepository _repository;

        public TrainingSubjectFactory(ITrainingSubjectRepository repository)
        {
            _repository = repository;
        }

        public async Task<TrainingSubject> Create(string subject, string description)
        {
            if (await _repository.IsDuplicated(subject))
                throw new ArgumentException("Subject must be unique");

            return new TrainingSubject(subject, description);
        }
    }
}
