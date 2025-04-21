using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.IRepository;
using Domain.Visitor;
using Infrastructure.DataModel;
using Infrastructure.Mapper;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class AssociationCollabTrainingModuleRepository : GenericRepository<IAssociationCollabTrainingModule, IAssociationCollabTrainingModuleVisitor>, IAssociationCollabTrainingModuleRepository
    {
        private readonly IMapper<IAssociationCollabTrainingModule,IAssociationCollabTrainingModuleVisitor> _mapper;

        public AssociationCollabTrainingModuleRepository(AbsanteeContext context, IMapper<IAssociationCollabTrainingModule,IAssociationCollabTrainingModuleVisitor> mapper) : base(context, mapper){
            _mapper = mapper;
        }

        public override IAssociationCollabTrainingModule? GetById(long id)
        {
            var associationDM = _context.Set<AssociationCollabTrainingModuleDataModel>().FirstOrDefault(actmdm => actmdm._id == id);

            if(associationDM == null) return null;

            var association = _mapper.ToDomain(associationDM);
            return association; 
        }

        public override async Task<IAssociationCollabTrainingModule?> GetByIdAsync(long id)
        {
            var associationDM = await _context.Set<AssociationCollabTrainingModuleDataModel>().FirstOrDefaultAsync(actmdm => actmdm._id == id);

            if(associationDM == null) return null;

            var association = _mapper.ToDomain(associationDM);
            return association; 
        }

        public bool CheckIfCanAdd(long collabId, long trainingModuleId){
            var checkCollabId = _context.Set<AssociationCollabTrainingModuleDataModel>().FirstOrDefault(a => a._collaboratorId == collabId);

            var checkTrainingModuleId = _context.Set<AssociationCollabTrainingModuleDataModel>().FirstOrDefault(t => t._trainingModuleId == trainingModuleId);

            if(checkCollabId == null || checkTrainingModuleId == null)
                return true;
            
            return false;
        }
    }
}