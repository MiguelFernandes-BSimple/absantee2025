using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Interfaces;
using Domain.IRepository;
using Domain.Models;
using Infrastructure.DataModel;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class TrainingPeriodRepositoryEF : GenericRepositoryEF<ITrainingPeriod, TrainingPeriod, TrainingPeriodDataModel>, ITrainingPeriodRepository
    {
        private readonly IMapper _mapper;

        public TrainingPeriodRepositoryEF(AbsanteeContext context, IMapper mapper) : base(context, mapper)
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
