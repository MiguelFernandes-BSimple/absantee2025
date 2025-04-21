using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.IRepository;

namespace Application.Services
{
    public class TrainingModuleService
    {
        private ICollaboratorRepository _collabRepo;
        private ISubjectRepository _subjectRepo;
        private ITrainingModuleRepository _trainingModuleRepo;

        public TrainingModuleService(ICollaboratorRepository collabRepo, ISubjectRepository subjectRepo, ITrainingModuleRepository trainingModuleRepo)
        {
            _collabRepo = collabRepo;
            _subjectRepo = subjectRepo;
            _trainingModuleRepo = trainingModuleRepo;
        }

        public async Task<bool> AddCollaboratorToTrainingModule(long collabId, long trainingModuleId)
        {
            if (_trainingModuleRepo.GetById(trainingModuleId) == null)
            {
                throw new ArgumentException("TrainingModule does not exist");
            }

            try
            {
                return await _collabRepo.AddToTrainingModule(collabId, trainingModuleId);

            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IEnumerable<ICollaborator>> FindAllActiveCollaboratorWithoutFinishedTrainingInSubject()
        {

        }
    }
}