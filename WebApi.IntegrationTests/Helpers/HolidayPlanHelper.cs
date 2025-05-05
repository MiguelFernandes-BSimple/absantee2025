using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTO;
using Domain.Models;

namespace WebApi.IntegrationTests.Helpers
{
    public static class HolidayPlanHelper
    {
        public static CreateHolidayPlanDTO GenerateCreateHolidayPlanDto(Guid collaboratorId, IEnumerable<PeriodDate> holidayPeriods)
        {
            List<CreateHolidayPeriodDTO> holidayPeriodsDto = holidayPeriods.Select(hp => new CreateHolidayPeriodDTO
            {
                InitDate = hp.InitDate,
                FinalDate = hp.FinalDate
            }).ToList();

            return new CreateHolidayPlanDTO
            {
                CollaboratorId = collaboratorId,
                HolidayPeriods = holidayPeriodsDto
            };
        }
    }
}
