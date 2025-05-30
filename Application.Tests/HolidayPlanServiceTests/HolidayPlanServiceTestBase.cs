using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Services;
using AutoMapper;
using Domain.Factory;
using Domain.IRepository;
using Moq;

namespace Application.Tests.HolidayPlanServiceTests
{
    public class HolidayPlanServiceTestBase
    {
        protected Mock<IAssociationProjectCollaboratorRepository> _associationProjectCollaboratorRepository;
        protected Mock<IHolidayPlanRepository> _holidayPlanRepository;
        protected Mock<IHolidayPlanFactory> _holidayPlanFactory;
        protected Mock<IHolidayPeriodFactory> _holidayPeriodFactory;
        protected Mock<IMapper> _mapper;

        protected HolidayPlanService _holidayPlanService;

        public HolidayPlanServiceTestBase()
        {
            _associationProjectCollaboratorRepository = new Mock<IAssociationProjectCollaboratorRepository>();
            _holidayPlanFactory = new Mock<IHolidayPlanFactory>();
            _holidayPlanRepository = new Mock<IHolidayPlanRepository>();
            _holidayPeriodFactory = new Mock<IHolidayPeriodFactory>();
            _mapper = new Mock<IMapper>();

            _holidayPlanService = new HolidayPlanService(
                _associationProjectCollaboratorRepository.Object,
                _holidayPlanRepository.Object,
                _holidayPlanFactory.Object,
                _holidayPeriodFactory.Object,
                _mapper.Object);
        }
    }
}
