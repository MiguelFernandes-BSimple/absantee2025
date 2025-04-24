using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Interfaces;
using Domain.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class TrainingPeriodRepository : GenericRepository<ITrainingPeriod, ITrainingPeriod>, ITrainingPeriodRepository
    {
        private readonly IMapper _mapper;
        public TrainingPeriodRepository(AbsanteeContext context, IMapper mapper) : base(context, mapper)
        {
            _mapper = mapper;
        }

        public override ITrainingPeriod? GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public override Task<ITrainingPeriod?> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
